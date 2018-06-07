//-----------------------------------------------------------------------------
// Soap Implementation
//-----------------------------------------------------------------------------

#include "stdafx.h"
#include <atlhttp.h>

#include "Soap.h"

SOAP_REQUEST_MAP g_soapMap[] = 
{
	{
		"NativeToPunycode",
		"NativeToPunycodeResult",
		"SOAPAction: \"http://name-services.com/webservice/encoding/NativeToPunycode\"\r\n",
		"<?xml version=\"1.0\" encoding=\"utf-8\" ?>"											\
		"<soap:Envelope	xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"	"				\
						"xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" "						\
						"xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"				\
			"<soap:Body>"																		\
				"<NativeToPunycode xmlns=\"http://name-services.com/webservice/encoding/\">"	\
				"<domain>%s</domain>"															\
				"</NativeToPunycode>"															\
			"</soap:Body>"																		\
		"</soap:Envelope>"
	},
	{	
		"NativeToRace",
		"NativeToRaceResult",
		"SOAPAction: \"http://name-services.com/webservice/encoding/NativeToRace\"\r\n",
		"<?xml version=\"1.0\" encoding=\"utf-8\" ?>"											\
		"<soap:Envelope	xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"	"				\
						"xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" "						\
						"xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"				\
			"<soap:Body>"																		\
				"<NativeToRace xmlns=\"http://name-services.com/webservice/encoding/\">"		\
				"<domain>%s</domain>"															\
				"</NativeToRace>"																\
			"</soap:Body>"																		\
		"</soap:Envelope>"
	},
	{
		"PunycodeToNative",
		"PunycodeToNativeResult",
		"SOAPAction: \"http://name-services.com/webservice/encoding/PunycodeToNative\"\r\n",
		"<?xml version=\"1.0\" encoding=\"utf-8\" ?>"											\
		"<soap:Envelope	xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"	"				\
						"xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" "						\
						"xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"				\
			"<soap:Body>"																		\
				"<PunycodeToNative xmlns=\"http://name-services.com/webservice/encoding/\">"	\
				"<domain>%s</domain>"															\
				"</PunycodeToNative>"															\
			"</soap:Body>"																		\
		"</soap:Envelope>"
	},
	{	
		"RaceToNative",
		"RaceToNativeResult",
		"SOAPAction: \"http://name-services.com/webservice/encoding/RaceToNative\"\r\n",
		"<?xml version=\"1.0\" encoding=\"utf-8\" ?>"											\
		"<soap:Envelope	xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"	"				\
						"xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" "						\
						"xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"				\
			"<soap:Body>"																		\
				"<RaceToNative xmlns=\"http://name-services.com/webservice/encoding/\">"		\
				"<domain>%s</domain>"															\
				"</RaceToNative>"																\
			"</soap:Body>"																		\
		"</soap:Envelope>"
	},
	{
		"PunycodeToRace",
		"PunycodeToRaceResult",
		"SOAPAction: \"http://name-services.com/webservice/encoding/PunycodeToRace\"\r\n",
		"<?xml version=\"1.0\" encoding=\"utf-8\" ?>"											\
		"<soap:Envelope	xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"	"				\
						"xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" "						\
						"xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"				\
			"<soap:Body>"																		\
				"<PunycodeToRace xmlns=\"http://name-services.com/webservice/encoding/\">"		\
				"<domain>%s</domain>"															\
				"</PunycodeToRace>"																\
			"</soap:Body>"																		\
		"</soap:Envelope>"
	},
	{
		"RaceToPunycode",
		"RaceToPunycodeResult",
		"SOAPAction: \"http://name-services.com/webservice/encoding/RaceToPunycode\"\r\n",
		"<?xml version=\"1.0\" encoding=\"utf-8\" ?>"											\
		"<soap:Envelope	xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"	"				\
						"xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" "						\
						"xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"				\
			"<soap:Body>"																		\
				"<RaceToPunycode xmlns=\"http://name-services.com/webservice/encoding/\">"		\
				"<domain>%s</domain>"															\
				"</RaceToPunycode>"																\
			"</soap:Body>"																		\
		"</soap:Envelope>"
	}
};

//-----------------------------------------------------------------------------
// Constructors
//----------------------------------------------------------------------------- 
CSoap::CSoap()
{
	::memset(m_soapUrl, 0, 256);	
}

CSoap::CSoap(const char* pUrl)
{
	SetUrl(pUrl);
}

//-----------------------------------------------------------------------------
// Public Methods Implementations
//----------------------------------------------------------------------------- 
void CSoap::SetUrl(const char* pUrl)
{
	size_t	size = 0;

	::memset(m_soapUrl, 0, 256);
	if (NULL != pUrl)
	{
		size = ::strlen(pUrl);
		::memcpy(m_soapUrl, pUrl, size < 255 ? size : 255);
	}
}

const char* CSoap::GetUrl()
{
	return (const char *)m_soapUrl;
}

HRESULT CSoap::NativeToRace	(BSTR pSource, BSTR* pResult)
{
	try
	{
		*pResult = this->CallMethod("NativeToRace", pSource);
	}
	catch(...)
	{
		*pResult = NULL;
	}

	return S_OK;
}

HRESULT CSoap::NativeToPunycode(BSTR pSource, BSTR* pResult)
{
	try
	{
		*pResult = this->CallMethod("NativeToPunycode", pSource);
	}
	catch(...)
	{
		*pResult = NULL;
	}

	// Done
	return S_OK;
}

HRESULT CSoap::PunycodeToNative(BSTR pSource, BSTR* pResult)
{
	try
	{
		*pResult = this->CallMethod("PunycodeToNative", pSource);
	}
	catch(...)
	{
		*pResult = NULL;
	}

	return S_OK;
}
HRESULT CSoap::RaceToNative(BSTR pSource, BSTR* pResult)
{
	try
	{
		*pResult = this->CallMethod("RaceToNative", pSource);
	}
	catch(...)
	{
		*pResult = NULL;
	}

	return S_OK;
}

HRESULT CSoap::PunycodeToRace(BSTR pSource, BSTR* pResult)
{
	try
	{
		*pResult = this->CallMethod("PunycodeToRace", pSource);
	}
	catch(...)
	{
		*pResult = NULL;
	}

	return S_OK;
}

HRESULT CSoap::RaceToPunycode(BSTR pSource, BSTR* pResult)
{
	try
	{
		*pResult = this->CallMethod("RaceToPunycode", pSource);
	}
	catch(...)
	{
		*pResult = NULL;
	}

	return S_OK;
}

//-----------------------------------------------------------------------------
// Priavte Methods Implementations
//----------------------------------------------------------------------------- 
const SOAP_REQUEST_MAP* CSoap::GetSoapRequestMap(const char* pAction)
{
	int						size	 = 0;
	const SOAP_REQUEST_MAP*	pSoapMap = NULL;

	// validate.
	if ((NULL == pAction) || (0 == pAction))
		return NULL;

	// Get the length of the action.
	size = (int)::strlen(pAction);

	// Try to get the soap action
	for (int index = 0; index < sizeof(g_soapMap) / sizeof(SOAP_REQUEST_MAP); index++)
	{
		// valid?
		if (NULL == g_soapMap[index].Action)
			continue;

		// length match?
		if (size != ::strlen(g_soapMap[index].Action))
			continue;

		// action match?
		if (0 != ::strncmp(pAction, g_soapMap[index].Action, size))
			continue;

		// Ok, get the request.
		pSoapMap = &g_soapMap[index];
		break;
	}

	// Done
	return pSoapMap;
}

BSTR CSoap::CallMethod(const char* pAction, BSTR pSource)
{
	CAtlHttpClient				client;
	CAtlNavigateData			navData;
	int							iSize			= 0;
	char*						pUTF8			= NULL;
	char*						pSoapResponse	= NULL;
	char*						pResult			= NULL;
	unsigned char*				pSoapRequest	= NULL;
	const SOAP_REQUEST_MAP*		pSoapMap		= NULL;
	BSTR						bstrReturn		= NULL;


	// Check the actions
	if (NULL == pAction)
		goto cleanup;

	// check the url has been set or not.
	if (NULL == this->GetUrl())
		goto cleanup;

	// having anything to convert?
	if ((NULL == pSource) || (0 == ::SysStringLen(pSource)))
		goto cleanup;

	//Get the soap request based on action
	pSoapMap = this->GetSoapRequestMap(pAction);
	if (NULL == pSoapMap)
		goto cleanup;

	// calculate the buffer size
	iSize = (int)::strlen(pSoapMap->SOAPXml);
	if (0 == iSize)
		goto cleanup;

	// convert the unicode to utf8
	pUTF8 = this->ToUTF8(pSource);
	if (NULL == pUTF8)
		goto cleanup;

	// get the additional size.
	iSize += (int)::strlen(pUTF8);

	// Allocate the request buffer
	pSoapRequest = (unsigned char*) ::HeapAlloc (GetProcessHeap (), HEAP_ZERO_MEMORY, iSize + 1);
	if (NULL == pSoapRequest)
		goto cleanup;

	// generate the soap request
	iSize = _snprintf((char *)pSoapRequest, iSize, (const char*)pSoapMap->SOAPXml, pUTF8);

	// Setup the navigation object
	navData.SetMethod		("POST");
	navData.SetExtraHeaders	(pSoapMap->SOAPAction);
	navData.SetPostData		(pSoapRequest, iSize, "text/xml; charset=utf-8");

	// Send the request to the Hub
	if (!client.Navigate (this->GetUrl(), &navData))
		goto cleanup;

	// Check the response to see if we succeeded
	pSoapResponse = (char*) client.GetResponse ();
	if (NULL == pSoapResponse)
		goto cleanup;

	// Parse the response, 
	// Return NULL, if parse failed
	pResult = this->ParseResponse(pSoapMap, pSoapResponse);
	if (NULL == pResult)
		goto cleanup;

	// convert it from UTF8
	bstrReturn = this->FromUTF8(pResult);

cleanup:

	// Release allocated memories.
	if (NULL != pUTF8)
		::HeapFree(::GetProcessHeap(), 0, pUTF8);

	if (NULL != pSoapRequest)
		::HeapFree(::GetProcessHeap(), 0, pSoapRequest);

	if (NULL != pResult)
		::HeapFree(::GetProcessHeap(), 0, pResult);

	// Done
	return bstrReturn;
}

char* CSoap::ToUTF8(BSTR pSource)
{
	int			size		= 0;
	LPSTR		pBuffer		= NULL;

	// Convert it from UTF16 to UTF8 multi-bytes first.
	// Get the buffer size
	size = WideCharToMultiByte(CP_UTF8, 0, (LPCWSTR)pSource, -1, NULL, 0, NULL, NULL);
	if (0 == size)
		return NULL;

	// allocate the buffer.
	pBuffer = (char *)::HeapAlloc(::GetProcessHeap(), HEAP_ZERO_MEMORY, (size + 1) * sizeof(char));
	if (NULL == pBuffer)
		return NULL;

	// convert it.
	size = WideCharToMultiByte(CP_UTF8, 0, (LPCWSTR)pSource, -1, pBuffer, size, NULL, NULL);
	if (0 == size)
	{
		if (NULL != pBuffer)
		{
			::HeapFree(::GetProcessHeap(), 0, pBuffer);
			pBuffer = NULL;
		}
	}

	// Done.
	return pBuffer;
}

BSTR CSoap::FromUTF8(char* pSource)
{
	int			size		= 0;
	LPWSTR		pwBuffer	= NULL;
	BSTR		bstrReturn	= NULL;

	if (NULL == pSource)
		return NULL;

	// Convert it to Unicode based on UTF8
	// Get the buffer size
	size = ::MultiByteToWideChar(CP_UTF8, 0, pSource, -1, NULL, 0);
	if (0 == size)
		goto cleanup;

	// allocate the buffer.
	pwBuffer = (LPWSTR)::HeapAlloc(::GetProcessHeap(), HEAP_ZERO_MEMORY, (size + 1) * sizeof(WCHAR));
	if (NULL == pwBuffer)
		goto cleanup;

	// Convert it to Unicode from UTF8
	size = ::MultiByteToWideChar(CP_UTF8, 0, pSource, -1, pwBuffer, size);
	if (0 == size)
	{
		if (NULL != pwBuffer)
			::HeapFree(::GetProcessHeap(), 0, pwBuffer);

		return NULL;
	}

	// allocate bstr
	bstrReturn = ::SysAllocString(pwBuffer);

cleanup:
	if (NULL != pwBuffer)
		::HeapFree(::GetProcessHeap(), 0, pwBuffer);

	// Done.
	return bstrReturn;
}

char* CSoap::ParseResponse(const SOAP_REQUEST_MAP* pSoapMap, const char* pSoapResponse)
{
	int		length	= 0;
	int		nodeLen	= 0;
	char*	pResult = NULL;
	char*	p1		= NULL;
	char*	p2		= NULL;

	// valid?
	if (NULL == pSoapResponse)
		return NULL;

	// Initializes
	length	= (int)::strlen(pSoapResponse);
	nodeLen	= (int)::strlen(pSoapMap->ResultNode);

	// find the starting tag
	p1 = ::StrStrA(pSoapResponse, pSoapMap->ResultNode);
	if ((NULL == p1) || (0 == *p1))
		return NULL;

	// move the pointer to the start of result
	p1 += (nodeLen + 1);

	// get the closing tag
	p2 = ::StrStrA(p1, pSoapMap->ResultNode);
	if ((NULL == p2) || (0 == *p2))
		return NULL;
	
	// get the length of the result
	length = (int)(p2 - p1 - 2);
	if (0 >= length)
		return NULL;

	// allocate memory for the result
	pResult = (char *)::HeapAlloc(::GetProcessHeap(), HEAP_ZERO_MEMORY, (length + 1) * sizeof(char));
	if (NULL == pResult)
		return NULL;

	// copy the data
	if (NULL == ::StrNCatA(pResult, p1, length + 1))
	{
		// free memory
		if (NULL != pResult)
			::HeapFree(::GetProcessHeap(), 0, pResult);

		return NULL;
	}

	// Done
	return pResult;
}