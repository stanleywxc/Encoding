// Proxy.cpp : Implementation of CProxy

#include "stdafx.h"
#include "Proxy.h"
#include "Soap.h"

// CProxy
STDMETHODIMP CProxy::NativeToPunycode(BSTR pServiceURL, BSTR pSource, BOOL pbUTF8, BOOL pbRaw, BSTR* pRetval)
{
	return this->Convert(ACTION_NATIVETOPUNYCODE, pServiceURL, pSource, pbUTF8, pbRaw, pRetval);
}

STDMETHODIMP CProxy::NativeToRace(BSTR pServiceURL, BSTR pSource, BOOL pbUTF8, BOOL pbRaw, BSTR* pRetval)
{
	return this->Convert(ACTION_NATIVETORACE, pServiceURL, pSource, pbUTF8, pbRaw, pRetval);
}

STDMETHODIMP CProxy::PunycodeToNative(BSTR pServiceURL, BSTR pSource, BOOL pbUTF8, BOOL pbRaw, BSTR* pRetval)
{
	return this->Convert(ACTION_PUNYCODETONATIVE, pServiceURL, pSource, pbUTF8, pbRaw, pRetval);
}

STDMETHODIMP CProxy::RaceToNative(BSTR pServiceURL, BSTR pSource, BOOL pbUTF8, BOOL pbRaw, BSTR* pRetval)
{
	return this->Convert(ACTION_RACETONATIVE, pServiceURL, pSource, pbUTF8, pbRaw, pRetval);
}

STDMETHODIMP CProxy::PunycodeToRace(BSTR pServiceURL, BSTR pSource, BOOL pbUTF8, BOOL pbRaw, BSTR* pRetval)
{
	return this->Convert(ACTION_PUNYCODETORACE, pServiceURL, pSource, pbUTF8, pbRaw, pRetval);
}

STDMETHODIMP CProxy::RaceToPunycode(BSTR pServiceURL, BSTR pSource, BOOL pbUTF8, BOOL pbRaw, BSTR* pRetval)
{
	return this->Convert(ACTION_RACETOPUNYCODE, pServiceURL, pSource, pbUTF8, pbRaw, pRetval);
}

HRESULT CProxy::Convert(enum Action pAction, BSTR pServiceURL, BSTR pSource, BOOL pbUTF8, BOOL pbRaw, BSTR* pRetval)
{
	HRESULT				hResult			= S_OK;
	char*				serviceURL		= this->ToChars (pServiceURL);
	BSTR				bstrConverted	= NULL;
	BSTR				bstrAllocated	= NULL;
	BSTR				bstrReturn		= NULL;
	CSoap				stub;

	// Initialize the return value
	*pRetval = NULL;

	if (NULL == serviceURL)
		goto cleanup;

	if ((NULL == pSource) || (0 == ::SysStringLen (pSource)))
		goto cleanup;

	if (NULL == pRetval)
		goto cleanup;

	// Set the server we're connecting to
	stub.SetUrl (serviceURL);

	// Convert from UTF8 to UTF16?
	if (pbUTF8)
	{
		bstrAllocated = this->FromUTF8 (pSource);
		bstrConverted = bstrAllocated;
	}
	else bstrConverted = pSource;

	// Valid?
	if (NULL == bstrConverted)
		goto cleanup;

	// Convert the string
	switch(pAction)
	{
		case ACTION_NATIVETOPUNYCODE:
			hResult = stub.NativeToPunycode(bstrConverted, pRetval);
			break;
		case ACTION_NATIVETORACE:
			hResult = stub.NativeToRace(bstrConverted, pRetval);
			break;
		case ACTION_PUNYCODETONATIVE:
			hResult = stub.PunycodeToNative(bstrConverted, pRetval);
			break;
		case ACTION_RACETONATIVE:
			hResult = stub.RaceToNative(bstrConverted, pRetval);
			break;
		case ACTION_PUNYCODETORACE:
			hResult = stub.PunycodeToRace(bstrConverted, pRetval);
			break;
		case ACTION_RACETOPUNYCODE:
			hResult = stub.RaceToPunycode(bstrConverted, pRetval);
			break;
		default:
			break;
	}

	// Did the command succeed?
	if (!SUCCEEDED (hResult))
	{
		*pRetval = NULL;
		goto cleanup;
	}

	// Do we have a result?
	if (NULL == (*pRetval))
		goto cleanup;

	// need to convert it to raw data format
	if (pbRaw)
	{
		// Convert the BSTR to RAW format
		bstrReturn = this->ToRawFormat (*pRetval);

		// Free the original BSTR that we got from the WebService
		::SysFreeString (*pRetval);

		// Return the RAW BSTR
		*pRetval = bstrReturn;
		goto cleanup;
	}

	// need to convert it back to UTF8
	if (pbUTF8)
	{
		// Convert the BSTR to UTF8
		bstrReturn = this->ToUTF8 (*pRetval);

		// Free the original BSTR that we got from the WebService
		::SysFreeString (*pRetval);

		// Return the UTF8 BSTR
		*pRetval = bstrReturn;
	}

cleanup:
	if (NULL != serviceURL)
		this->Free (serviceURL);

	if (NULL != bstrAllocated)
		::SysFreeString (bstrAllocated);

	// Check if the retured result is NULL, return NULL 
	// will cause possible failure in asp page
	// For safe purpose, always return a empty BSTR
	if (NULL == *pRetval)
		*pRetval = ::SysAllocString(L"");

	// Done
	return S_OK;
}

BSTR CProxy::ToRawFormat(BSTR pSource)
{
	WCHAR		wFormat[32];
	LPWSTR		pBuffer		= NULL;
	LPWSTR		pHold		= NULL;
	LPCWSTR		pvSource	= NULL;
	BSTR		bstrReturn	= NULL;
	int			size		= 0;
	int			totl		= 0;
	int			idx			= 0;

	// Initializes
	size		= ::SysStringLen (pSource);
	pvSource	= (LPCWSTR) pSource;

	// for each char in the BSTR
	for (idx = 0; idx < size; idx++)
	{
		::memset (wFormat, 0, 32 * sizeof(WCHAR));
		wsprintfW (wFormat, L"&#%d;", (int)((WCHAR) pvSource [idx]));

		// Get the size of the buffer
		totl += (int) ::wcslen(wFormat);

		// reallocate the buffer
		pHold = (LPWSTR) this->Reallocate (pBuffer, (totl + 1) * sizeof (WCHAR));
		if (NULL == pHold)
		{
			// FATAL ERROR: Reallocate does not always return a buffer
			Free (pBuffer);
			return NULL;
		}
		else pBuffer = pHold;

		// Concatenate the strings
		StrNCatW (pBuffer, wFormat, totl + 1);
	}

	// Create the BSTR
	if (NULL != pBuffer)
		bstrReturn = ::SysAllocString (pBuffer);

	Free(pBuffer);
	return bstrReturn;
}

BSTR CProxy::FromUTF8(BSTR pSource)
{
	int			size		= 0;
	LPSTR		pBuffer		= NULL;
	LPWSTR		pwBuffer	= NULL;
	BSTR		bstrReturn	= NULL;

	//1. Convert it from Wide-char based on ASNI page to multi-bytes fitst(to true UTF8)
	// Get the buffer size
	size = WideCharToMultiByte(CP_ACP, 0, (LPCWSTR)pSource, -1, NULL, 0, NULL, NULL);
	if (0 == size)
		goto cleanup;

	// allocate the buffer.
	pBuffer = (char *)this->Allocate((size + 1) * sizeof(char));
	if (NULL == pBuffer)
		goto cleanup;

	// convert it to UTF8 from ASNI Page
	size = WideCharToMultiByte(CP_ACP, 0, (LPCWSTR)pSource, -1, pBuffer, size, NULL, NULL);
	if (0 == size) 
		goto cleanup;

	//2. Convert it to Unicode based on UTF8
	// Get the buffer size
	size = MultiByteToWideChar(CP_UTF8, 0, pBuffer, -1, NULL, 0);
	if (0 == size)
		goto cleanup;

	// allocate the buffer.
	pwBuffer = (LPWSTR)this->Allocate((size + 1) * sizeof(WCHAR));
	if (NULL == pwBuffer)
		goto cleanup;

	// Convert it to Unicode from UTF8
	size = MultiByteToWideChar(CP_UTF8, 0, pBuffer, -1, pwBuffer, size);
	if (0 == size)
		goto cleanup;

	// allocate bstr
	bstrReturn = ::SysAllocString(pwBuffer);

cleanup:
	if (NULL != pBuffer)
		this->Free(pBuffer);

	if (NULL != pwBuffer)
		this->Free(pwBuffer);

	// Done.
	return bstrReturn;
}

BSTR CProxy::ToUTF8(BSTR pSource)
{
	int			size		= 0;
	LPSTR		pBuffer		= NULL;
	LPWSTR		pwBuffer	= NULL;
	BSTR		bstrReturn	= NULL;

	//1. Convert it from UTF16 to UTF8 multi-bytes first.
	// Get the buffer size
	size = WideCharToMultiByte(CP_UTF8, 0, (LPCWSTR)pSource, -1, NULL, 0, NULL, NULL);
	if (0 == size)
		return NULL;

	// allocate the buffer.
	pBuffer = (char *)this->Allocate((size + 1) * sizeof(char));
	if (NULL == pBuffer)
		goto cleanup;

	// convert it.
	size = WideCharToMultiByte(CP_UTF8, 0, (LPCWSTR)pSource, -1, pBuffer, size, NULL, NULL);
	if (0 == size) 
		goto cleanup;

	//2. Convert to from UTF8 multi-bytes to wide-char based on ANSI page(Not unicode)
	// Get the buffer size
	size = MultiByteToWideChar(CP_ACP, 0, pBuffer, -1, NULL, 0);
	if (0 == size)
		goto cleanup;

	// allocate the buffer.
	pwBuffer = (LPWSTR)this->Allocate((size + 1) * sizeof(WCHAR));
	if (NULL == pwBuffer)
		goto cleanup;

	// Convert it to Unicode from UTF8
	size = MultiByteToWideChar(CP_ACP, 0, pBuffer, -1, pwBuffer, size);
	if (0 == size)
		goto cleanup;

	// allocate bstr
	bstrReturn = ::SysAllocString(pwBuffer);

cleanup:
	if (NULL != pBuffer)
		this->Free(pBuffer);

	if (NULL != pwBuffer)
		this->Free(pwBuffer);

	// Done.
	return bstrReturn;
}

char* CProxy::ToChars (BSTR pSource)
{
	int		size	= 0;
	char*	pBuffer	= NULL;

	// Do we have a valid BSTR?
	if (NULL == pSource || 0 == ::SysStringLen (pSource))
		return NULL;

	// Convert it from wide char to UTF8 multi-bytes first.
	// Get the buffer size
	size = WideCharToMultiByte(CP_UTF8, 0, (LPCWSTR)pSource, -1, NULL, 0, NULL, NULL);
	if (0 == size)
		return NULL;

	// allocate the buffer.
	pBuffer = (char *)this->Allocate((size + 1) * sizeof(char));
	if (NULL == pBuffer)
		return NULL;

	// convert it.
	size = WideCharToMultiByte(CP_UTF8, 0, (LPCWSTR)pSource, -1, pBuffer, size, NULL, NULL);
	if (0 == size)
	{
		if (NULL != pBuffer)
			this->Free(pBuffer);

		pBuffer = NULL;
	}

	// Return the result
	return pBuffer;
}

void* CProxy::Allocate(int pSize)
{
	return ::HeapAlloc (GetProcessHeap (), HEAP_ZERO_MEMORY, pSize);
}

void* CProxy::Reallocate(void* pBuffer, int pSize)
{
	// Do we have a buffer?
	if (NULL == pBuffer)
		return Allocate (pSize);

	// Reallocate the memory
	return ::HeapReAlloc (GetProcessHeap (), HEAP_ZERO_MEMORY, pBuffer, pSize);
}

void CProxy::Free (void* pvBuffer)
{
	// Do we have a buffer?
	if (NULL == pvBuffer)
		return;

	// Free the valid buffer
	::HeapFree (GetProcessHeap (), 0, pvBuffer);
}