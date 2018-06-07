//-----------------------------------------------------------------------------
// Ugly code sproxy.exe generated file do not modify this file
//
// Created: 03/22/2004@16:43:30
//-----------------------------------------------------------------------------

#pragma once

#ifndef _WIN32_WINDOWS
#pragma message("warning: defining _WIN32_WINDOWS = 0x0410")
#define _WIN32_WINDOWS 0x0410
#endif

#include <atlsoap.h>

namespace Converter
{
	//-------------------------------------------------------------------------
	// Defintions
	//-------------------------------------------------------------------------
	#define NAMESPACE		"http://name-services.com/webservice/encoding/"
	#define LNAMESPACE		L"http://name-services.com/webservice/encoding/"

	template <typename TClient = CSoapSocketClientT<> > class CConverterStub :  public TClient, public CSoapRootHandler
	{
		protected:
			const _soapmap**	GetFunctionMap			();
			const _soapmap**	GetHeaderMap			();
			void*				GetHeaderValue			();
			const wchar_t*		GetNamespaceUri			();
			const char*			GetServiceName			();
			const char*			GetNamespaceUriA		();
			HRESULT				CallFunction			(void*, const wchar_t*, int, size_t);
			HRESULT				GetClientReader			(ISAXXMLReader** ppReader);

		public:
			CConverterStub (ISAXXMLReader *pReader = NULL) : TClient ("http://localhost/")
			{
				SetClient (true);
				SetReader (pReader);
			}
	
			~CConverterStub()
			{
				Uninitialize();
			}

			HRESULT __stdcall QueryInterface(REFIID riid, void **ppv)
			{
				if (ppv == NULL)
				{
					return E_POINTER;
				}

				*ppv = NULL;

				if (InlineIsEqualGUID(riid, IID_IUnknown) || InlineIsEqualGUID(riid, IID_ISAXContentHandler))
				{
					*ppv = static_cast<ISAXContentHandler *>(this);
					return S_OK;
				}

				return E_NOINTERFACE;
			}

			ULONG __stdcall AddRef				()		{ return 1; }
			ULONG __stdcall Release				()		{ return 1; }
			void			Uninitialize		()		{ UninitializeSOAP(); }
			HRESULT			NativeToRace		(BSTR, BSTR*);
			HRESULT			RaceToPunycode		(BSTR, BSTR*);
			HRESULT			RaceToNative		(BSTR, BSTR*);
			HRESULT			PunycodeToNative	(BSTR, BSTR*);
			HRESULT			PunycodeToRace		(BSTR, BSTR*);
			HRESULT			NativeToPunycode	(BSTR, BSTR*);
	};

	typedef CConverterStub<> CConverter;

	struct __CConverter_NativeToRace_struct
	{
		BSTR domain;
		BSTR NativeToRaceResult;
	};

	extern __declspec(selectany) const _soapmapentry __CConverter_NativeToRace_entries[] =
	{
		{
			0xF159EEB8, 
			"domain", 
			L"domain", 
			sizeof("domain")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_IN | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_NativeToRace_struct, domain),
			NULL,
			NULL,
			-1,
		},
		{
			0x243918E4, 
			"NativeToRaceResult", 
			L"NativeToRaceResult", 
			sizeof("NativeToRaceResult")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_OUT | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_NativeToRace_struct, NativeToRaceResult),
			NULL,
			NULL,
			-1,
		},
		{ 0x00000000 }
	};

	extern __declspec(selectany) const _soapmap MAP_NATIVE_TO_RACE =
	{
		0x168A0234,
		"NativeToRace",
		L"NativeToRaceResponse",
		sizeof("NativeToRace")-1,
		sizeof("NativeToRaceResponse")-1,
		SOAPMAP_FUNC,
		__CConverter_NativeToRace_entries,
		sizeof(__CConverter_NativeToRace_struct),
		1,
		-1,
		SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
		0x3B4CD25A,
		NAMESPACE,
		LNAMESPACE,
		sizeof(NAMESPACE)-1
	};

	struct __CConverter_RaceToPunycode_struct
	{
		BSTR domain;
		BSTR RaceToPunycodeResult;
	};

	extern __declspec(selectany) const _soapmapentry __CConverter_RaceToPunycode_entries[] =
	{

		{
			0xF159EEB8, 
			"domain", 
			L"domain", 
			sizeof("domain")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_IN | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_RaceToPunycode_struct, domain),
			NULL,
			NULL,
			-1,
		},
		{
			0x94491E64, 
			"RaceToPunycodeResult", 
			L"RaceToPunycodeResult", 
			sizeof("RaceToPunycodeResult")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_OUT | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_RaceToPunycode_struct, RaceToPunycodeResult),
			NULL,
			NULL,
			-1,
		},
		{ 0x00000000 }
	};

	extern __declspec(selectany) const _soapmap MAP_RACE_TO_PUNY =
	{
		0xCAB167B4,
		"RaceToPunycode",
		L"RaceToPunycodeResponse",
		sizeof("RaceToPunycode")-1,
		sizeof("RaceToPunycodeResponse")-1,
		SOAPMAP_FUNC,
		__CConverter_RaceToPunycode_entries,
		sizeof(__CConverter_RaceToPunycode_struct),
		1,
		-1,
		SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
		0x3B4CD25A,
		NAMESPACE,
		LNAMESPACE,
		sizeof(NAMESPACE) - 1
	};


	struct __CConverter_RaceToNative_struct
	{
		BSTR domain;
		BSTR RaceToNativeResult;
	};

	extern __declspec(selectany) const _soapmapentry __CConverter_RaceToNative_entries[] =
	{

		{
			0xF159EEB8, 
			"domain", 
			L"domain", 
			sizeof("domain")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_IN | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_RaceToNative_struct, domain),
			NULL,
			NULL,
			-1,
		},
		{
			0x00829F64, 
			"RaceToNativeResult", 
			L"RaceToNativeResult", 
			sizeof("RaceToNativeResult")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_OUT | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_RaceToNative_struct, RaceToNativeResult),
			NULL,
			NULL,
			-1,
		},
		{ 0x00000000 }
	};

	extern __declspec(selectany) const _soapmap MAP_RACE_TO_NATIVE =
	{
		0x2B4F28B4,
		"RaceToNative",
		L"RaceToNativeResponse",
		sizeof("RaceToNative")-1,
		sizeof("RaceToNativeResponse")-1,
		SOAPMAP_FUNC,
		__CConverter_RaceToNative_entries,
		sizeof(__CConverter_RaceToNative_struct),
		1,
		-1,
		SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
		0x3B4CD25A,
		NAMESPACE,
		LNAMESPACE,
		sizeof(NAMESPACE)-1
	};


	struct __CConverter_PunycodeToNative_struct
	{
		BSTR domain;
		BSTR PunycodeToNativeResult;
	};

	extern __declspec(selectany) const _soapmapentry __CConverter_PunycodeToNative_entries[] =
	{

		{
			0xF159EEB8, 
			"domain", 
			L"domain", 
			sizeof("domain")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_IN | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_PunycodeToNative_struct, domain),
			NULL,
			NULL,
			-1,
		},
		{
			0x14F07730, 
			"PunycodeToNativeResult", 
			L"PunycodeToNativeResult", 
			sizeof("PunycodeToNativeResult")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_OUT | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_PunycodeToNative_struct, PunycodeToNativeResult),
			NULL,
			NULL,
			-1,
		},
		{ 0x00000000 }
	};

	extern __declspec(selectany) const _soapmap MAP_PUNY_TO_NATIVE =
	{
		0x12922380,
		"PunycodeToNative",
		L"PunycodeToNativeResponse",
		sizeof("PunycodeToNative")-1,
		sizeof("PunycodeToNativeResponse")-1,
		SOAPMAP_FUNC,
		__CConverter_PunycodeToNative_entries,
		sizeof(__CConverter_PunycodeToNative_struct),
		1,
		-1,
		SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
		0x3B4CD25A,
		NAMESPACE,
		LNAMESPACE,
		sizeof(NAMESPACE)-1
	};


	struct __CConverter_PunycodeToRace_struct
	{
		BSTR domain;
		BSTR PunycodeToRaceResult;
	};

	extern __declspec(selectany) const _soapmapentry __CConverter_PunycodeToRace_entries[] =
	{

		{
			0xF159EEB8, 
			"domain", 
			L"domain", 
			sizeof("domain")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_IN | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_PunycodeToRace_struct, domain),
			NULL,
			NULL,
			-1,
		},
		{
			0x251AC864, 
			"PunycodeToRaceResult", 
			L"PunycodeToRaceResult", 
			sizeof("PunycodeToRaceResult")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_OUT | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_PunycodeToRace_struct, PunycodeToRaceResult),
			NULL,
			NULL,
			-1,
		},
		{ 0x00000000 }
	};

	extern __declspec(selectany) const _soapmap MAP_PUNY_TO_RACE =
	{
		0xD69591B4,
		"PunycodeToRace",
		L"PunycodeToRaceResponse",
		sizeof("PunycodeToRace")-1,
		sizeof("PunycodeToRaceResponse")-1,
		SOAPMAP_FUNC,
		__CConverter_PunycodeToRace_entries,
		sizeof(__CConverter_PunycodeToRace_struct),
		1,
		-1,
		SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
		0x3B4CD25A,
		NAMESPACE,
		LNAMESPACE,
		sizeof(NAMESPACE)-1
	};


	struct __CConverter_NativeToPunycode_struct
	{
		BSTR domain;
		BSTR NativeToPunycodeResult;
	};

	extern __declspec(selectany) const _soapmapentry __CConverter_NativeToPunycode_entries[] =
	{

		{
			0xF159EEB8, 
			"domain", 
			L"domain", 
			sizeof("domain")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_IN | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_NativeToPunycode_struct, domain),
			NULL,
			NULL,
			-1,
		},
		{
			0x8A5EA9B0, 
			"NativeToPunycodeResult", 
			L"NativeToPunycodeResult", 
			sizeof("NativeToPunycodeResult")-1, 
			SOAPTYPE_STRING, 
			SOAPFLAG_NONE | SOAPFLAG_OUT | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL | SOAPFLAG_NULLABLE,
			offsetof(__CConverter_NativeToPunycode_struct, NativeToPunycodeResult),
			NULL,
			NULL,
			-1,
		},
		{ 0x00000000 }
	};

	extern __declspec(selectany) const _soapmap MAP_NATIVE_TO_PUNY =
	{
		0x9C56F600,
		"NativeToPunycode",
		L"NativeToPunycodeResponse",
		sizeof("NativeToPunycode")-1,
		sizeof("NativeToPunycodeResponse")-1,
		SOAPMAP_FUNC,
		__CConverter_NativeToPunycode_entries,
		sizeof(__CConverter_NativeToPunycode_struct),
		1,
		-1,
		SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
		0x3B4CD25A,
		NAMESPACE,
		LNAMESPACE,
		sizeof(NAMESPACE)-1
	};

	extern __declspec(selectany) const _soapmap * __CConverter_funcs[] =
	{
		&MAP_NATIVE_TO_RACE,
		&MAP_RACE_TO_PUNY,
		&MAP_RACE_TO_NATIVE,
		&MAP_PUNY_TO_NATIVE,
		&MAP_PUNY_TO_RACE,
		&MAP_NATIVE_TO_PUNY,
		NULL
	};

	template <typename TClient>
	inline HRESULT CConverterStub<TClient>::NativeToRace(BSTR domain, BSTR* NativeToRaceResult)
	{
		if ( NativeToRaceResult == NULL )
			return E_POINTER;

		HRESULT __atlsoap_hr = InitializeSOAP(NULL);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_INITIALIZE_ERROR);
			return __atlsoap_hr;
		}
		
		CleanupClient();

		CComPtr<IStream> __atlsoap_spReadStream;
		__CConverter_NativeToRace_struct __params;
		memset(&__params, 0x00, sizeof(__params));
		__params.domain = domain;

		__atlsoap_hr = SetClientStruct(&__params, 0);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_OUTOFMEMORY);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = GenerateResponse(GetWriteStream());
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_GENERATE_ERROR);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = SendRequest(_T("SOAPAction: \"http://name-services.com/webservice/encoding/NativeToRace\"\r\n"));
		if (FAILED(__atlsoap_hr))
		{
			goto __skip_cleanup;
		}
		__atlsoap_hr = GetReadStream(&__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_READ_ERROR);
			goto __skip_cleanup;
		}
		
		// cleanup any in/out-params and out-headers from previous calls
		Cleanup();
		__atlsoap_hr = BeginParse(__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_PARSE_ERROR);
			goto __cleanup;
		}

		*NativeToRaceResult = __params.NativeToRaceResult;
		goto __skip_cleanup;
		
	__cleanup:
		Cleanup();
	__skip_cleanup:
		ResetClientState(true);
		memset(&__params, 0x00, sizeof(__params));
		return __atlsoap_hr;
	}

	template <typename TClient>
	inline HRESULT CConverterStub<TClient>::RaceToPunycode(BSTR domain, BSTR* RaceToPunycodeResult)
	{
		if ( RaceToPunycodeResult == NULL )
			return E_POINTER;

		HRESULT __atlsoap_hr = InitializeSOAP(NULL);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_INITIALIZE_ERROR);
			return __atlsoap_hr;
		}
		
		CleanupClient();

		CComPtr<IStream> __atlsoap_spReadStream;
		__CConverter_RaceToPunycode_struct __params;
		memset(&__params, 0x00, sizeof(__params));
		__params.domain = domain;

		__atlsoap_hr = SetClientStruct(&__params, 1);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_OUTOFMEMORY);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = GenerateResponse(GetWriteStream());
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_GENERATE_ERROR);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = SendRequest(_T("SOAPAction: \"http://name-services.com/webservice/encoding/RaceToPunycode\"\r\n"));
		if (FAILED(__atlsoap_hr))
		{
			goto __skip_cleanup;
		}
		__atlsoap_hr = GetReadStream(&__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_READ_ERROR);
			goto __skip_cleanup;
		}
		
		// cleanup any in/out-params and out-headers from previous calls
		Cleanup();
		__atlsoap_hr = BeginParse(__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_PARSE_ERROR);
			goto __cleanup;
		}

		*RaceToPunycodeResult = __params.RaceToPunycodeResult;
		goto __skip_cleanup;
		
	__cleanup:
		Cleanup();
	__skip_cleanup:
		ResetClientState(true);
		memset(&__params, 0x00, sizeof(__params));
		return __atlsoap_hr;
	}

	template <typename TClient>
	inline HRESULT CConverterStub<TClient>::RaceToNative(BSTR domain, BSTR* RaceToNativeResult)
	{
		if ( RaceToNativeResult == NULL )
			return E_POINTER;

		HRESULT __atlsoap_hr = InitializeSOAP(NULL);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_INITIALIZE_ERROR);
			return __atlsoap_hr;
		}
		
		CleanupClient();

		CComPtr<IStream> __atlsoap_spReadStream;
		__CConverter_RaceToNative_struct __params;
		memset(&__params, 0x00, sizeof(__params));
		__params.domain = domain;

		__atlsoap_hr = SetClientStruct(&__params, 2);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_OUTOFMEMORY);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = GenerateResponse(GetWriteStream());
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_GENERATE_ERROR);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = SendRequest(_T("SOAPAction: \"http://name-services.com/webservice/encoding/RaceToNative\"\r\n"));
		if (FAILED(__atlsoap_hr))
		{
			goto __skip_cleanup;
		}
		__atlsoap_hr = GetReadStream(&__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_READ_ERROR);
			goto __skip_cleanup;
		}
		
		// cleanup any in/out-params and out-headers from previous calls
		Cleanup();
		__atlsoap_hr = BeginParse(__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_PARSE_ERROR);
			goto __cleanup;
		}

		*RaceToNativeResult = __params.RaceToNativeResult;
		goto __skip_cleanup;
		
	__cleanup:
		Cleanup();
	__skip_cleanup:
		ResetClientState(true);
		memset(&__params, 0x00, sizeof(__params));
		return __atlsoap_hr;
	}

	template <typename TClient>
	inline HRESULT CConverterStub<TClient>::PunycodeToNative(BSTR domain, BSTR* PunycodeToNativeResult)
	{
		if ( PunycodeToNativeResult == NULL )
			return E_POINTER;

		HRESULT __atlsoap_hr = InitializeSOAP(NULL);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_INITIALIZE_ERROR);
			return __atlsoap_hr;
		}
		
		CleanupClient();

		CComPtr<IStream> __atlsoap_spReadStream;
		__CConverter_PunycodeToNative_struct __params;
		memset(&__params, 0x00, sizeof(__params));
		__params.domain = domain;

		__atlsoap_hr = SetClientStruct(&__params, 3);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_OUTOFMEMORY);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = GenerateResponse(GetWriteStream());
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_GENERATE_ERROR);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = SendRequest(_T("SOAPAction: \"http://name-services.com/webservice/encoding/PunycodeToNative\"\r\n"));
		if (FAILED(__atlsoap_hr))
		{
			goto __skip_cleanup;
		}
		__atlsoap_hr = GetReadStream(&__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_READ_ERROR);
			goto __skip_cleanup;
		}
		
		// cleanup any in/out-params and out-headers from previous calls
		Cleanup();
		__atlsoap_hr = BeginParse(__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_PARSE_ERROR);
			goto __cleanup;
		}

		*PunycodeToNativeResult = __params.PunycodeToNativeResult;
		goto __skip_cleanup;
		
	__cleanup:
		Cleanup();
	__skip_cleanup:
		ResetClientState(true);
		memset(&__params, 0x00, sizeof(__params));
		return __atlsoap_hr;
	}

	template <typename TClient>
	inline HRESULT CConverterStub<TClient>::PunycodeToRace(BSTR domain, BSTR* PunycodeToRaceResult)
	{
		if ( PunycodeToRaceResult == NULL )
			return E_POINTER;

		HRESULT __atlsoap_hr = InitializeSOAP(NULL);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_INITIALIZE_ERROR);
			return __atlsoap_hr;
		}
		
		CleanupClient();

		CComPtr<IStream> __atlsoap_spReadStream;
		__CConverter_PunycodeToRace_struct __params;
		memset(&__params, 0x00, sizeof(__params));
		__params.domain = domain;

		__atlsoap_hr = SetClientStruct(&__params, 4);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_OUTOFMEMORY);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = GenerateResponse(GetWriteStream());
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_GENERATE_ERROR);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = SendRequest(_T("SOAPAction: \"http://name-services.com/webservice/encoding/PunycodeToRace\"\r\n"));
		if (FAILED(__atlsoap_hr))
		{
			goto __skip_cleanup;
		}
		__atlsoap_hr = GetReadStream(&__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_READ_ERROR);
			goto __skip_cleanup;
		}
		
		// cleanup any in/out-params and out-headers from previous calls
		Cleanup();
		__atlsoap_hr = BeginParse(__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_PARSE_ERROR);
			goto __cleanup;
		}

		*PunycodeToRaceResult = __params.PunycodeToRaceResult;
		goto __skip_cleanup;
		
	__cleanup:
		Cleanup();
	__skip_cleanup:
		ResetClientState(true);
		memset(&__params, 0x00, sizeof(__params));
		return __atlsoap_hr;
	}

	template <typename TClient>
	inline HRESULT CConverterStub<TClient>::NativeToPunycode(BSTR domain, BSTR* NativeToPunycodeResult)
	{
		if ( NativeToPunycodeResult == NULL )
			return E_POINTER;

		HRESULT __atlsoap_hr = InitializeSOAP(NULL);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_INITIALIZE_ERROR);
			return __atlsoap_hr;
		}
		
		CleanupClient();

		CComPtr<IStream> __atlsoap_spReadStream;
		__CConverter_NativeToPunycode_struct __params;
		memset(&__params, 0x00, sizeof(__params));
		__params.domain = domain;

		__atlsoap_hr = SetClientStruct(&__params, 5);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_OUTOFMEMORY);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = GenerateResponse(GetWriteStream());
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_GENERATE_ERROR);
			goto __skip_cleanup;
		}
		
		__atlsoap_hr = SendRequest(_T("SOAPAction: \"http://name-services.com/webservice/encoding/NativeToPunycode\"\r\n"));
		if (FAILED(__atlsoap_hr))
		{
			goto __skip_cleanup;
		}
		__atlsoap_hr = GetReadStream(&__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_READ_ERROR);
			goto __skip_cleanup;
		}
		
		// cleanup any in/out-params and out-headers from previous calls
		Cleanup();
		__atlsoap_hr = BeginParse(__atlsoap_spReadStream);
		if (FAILED(__atlsoap_hr))
		{
			SetClientError(SOAPCLIENT_PARSE_ERROR);
			goto __cleanup;
		}

		*NativeToPunycodeResult = __params.NativeToPunycodeResult;
		goto __skip_cleanup;
		
	__cleanup:
		Cleanup();
	__skip_cleanup:
		ResetClientState(true);
		memset(&__params, 0x00, sizeof(__params));
		return __atlsoap_hr;
	}

	template <typename TClient>
	ATL_NOINLINE inline const _soapmap ** CConverterStub<TClient>::GetFunctionMap()
	{
		return __CConverter_funcs;
	}

	template <typename TClient>
	ATL_NOINLINE inline const _soapmap ** CConverterStub<TClient>::GetHeaderMap()
	{
		static const _soapmapentry __CConverter_NativeToRace_atlsoapheader_entries[] =
		{
			{ 0x00000000 }
		};

		static const _soapmap __CConverter_NativeToRace_atlsoapheader_map = 
		{
			0x168A0234,
			"NativeToRace",
			L"NativeToRaceResponse",
			sizeof("NativeToRace")-1,
			sizeof("NativeToRaceResponse")-1,
			SOAPMAP_HEADER,
			__CConverter_NativeToRace_atlsoapheader_entries,
			0,
			0,
			-1,
			SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
			0x3B4CD25A,
			NAMESPACE,
			LNAMESPACE,
			sizeof(NAMESPACE)-1
		};

		static const _soapmapentry __CConverter_RaceToPunycode_atlsoapheader_entries[] =
		{
			{ 0x00000000 }
		};

		static const _soapmap __CConverter_RaceToPunycode_atlsoapheader_map = 
		{
			0xCAB167B4,
			"RaceToPunycode",
			L"RaceToPunycodeResponse",
			sizeof("RaceToPunycode")-1,
			sizeof("RaceToPunycodeResponse")-1,
			SOAPMAP_HEADER,
			__CConverter_RaceToPunycode_atlsoapheader_entries,
			0,
			0,
			-1,
			SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
			0x3B4CD25A,
			NAMESPACE,
			LNAMESPACE,
			sizeof(NAMESPACE)-1
		};

		static const _soapmapentry __CConverter_RaceToNative_atlsoapheader_entries[] =
		{
			{ 0x00000000 }
		};

		static const _soapmap __CConverter_RaceToNative_atlsoapheader_map = 
		{
			0x2B4F28B4,
			"RaceToNative",
			L"RaceToNativeResponse",
			sizeof("RaceToNative")-1,
			sizeof("RaceToNativeResponse")-1,
			SOAPMAP_HEADER,
			__CConverter_RaceToNative_atlsoapheader_entries,
			0,
			0,
			-1,
			SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
			0x3B4CD25A,
			NAMESPACE,
			LNAMESPACE,
			sizeof(NAMESPACE)-1
		};

		static const _soapmapentry __CConverter_PunycodeToNative_atlsoapheader_entries[] =
		{
			{ 0x00000000 }
		};

		static const _soapmap __CConverter_PunycodeToNative_atlsoapheader_map = 
		{
			0x12922380,
			"PunycodeToNative",
			L"PunycodeToNativeResponse",
			sizeof("PunycodeToNative")-1,
			sizeof("PunycodeToNativeResponse")-1,
			SOAPMAP_HEADER,
			__CConverter_PunycodeToNative_atlsoapheader_entries,
			0,
			0,
			-1,
			SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
			0x3B4CD25A,
			NAMESPACE,
			LNAMESPACE,
			sizeof(NAMESPACE)-1
		};

		static const _soapmapentry __CConverter_PunycodeToRace_atlsoapheader_entries[] =
		{
			{ 0x00000000 }
		};

		static const _soapmap __CConverter_PunycodeToRace_atlsoapheader_map = 
		{
			0xD69591B4,
			"PunycodeToRace",
			L"PunycodeToRaceResponse",
			sizeof("PunycodeToRace")-1,
			sizeof("PunycodeToRaceResponse")-1,
			SOAPMAP_HEADER,
			__CConverter_PunycodeToRace_atlsoapheader_entries,
			0,
			0,
			-1,
			SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
			0x3B4CD25A,
			NAMESPACE,
			LNAMESPACE,
			sizeof(NAMESPACE)-1
		};

		static const _soapmapentry __CConverter_NativeToPunycode_atlsoapheader_entries[] =
		{
			{ 0x00000000 }
		};

		static const _soapmap __CConverter_NativeToPunycode_atlsoapheader_map = 
		{
			0x9C56F600,
			"NativeToPunycode",
			L"NativeToPunycodeResponse",
			sizeof("NativeToPunycode")-1,
			sizeof("NativeToPunycodeResponse")-1,
			SOAPMAP_HEADER,
			__CConverter_NativeToPunycode_atlsoapheader_entries,
			0,
			0,
			-1,
			SOAPFLAG_NONE | SOAPFLAG_PID | SOAPFLAG_DOCUMENT | SOAPFLAG_LITERAL,
			0x3B4CD25A,
			NAMESPACE,
			LNAMESPACE,
			sizeof(NAMESPACE)-1
		};


		static const _soapmap * __CConverter_headers[] =
		{
			&__CConverter_NativeToRace_atlsoapheader_map,
			&__CConverter_RaceToPunycode_atlsoapheader_map,
			&__CConverter_RaceToNative_atlsoapheader_map,
			&__CConverter_PunycodeToNative_atlsoapheader_map,
			&__CConverter_PunycodeToRace_atlsoapheader_map,
			&__CConverter_NativeToPunycode_atlsoapheader_map,
			NULL
		};
		
		return __CConverter_headers;
	}

	template <typename TClient>
	ATL_NOINLINE inline void * CConverterStub<TClient>::GetHeaderValue()
	{
		return this;
	}

	template <typename TClient>
	ATL_NOINLINE inline const wchar_t * CConverterStub<TClient>::GetNamespaceUri()
	{
		return LNAMESPACE;
	}

	template <typename TClient>
	ATL_NOINLINE inline const char * CConverterStub<TClient>::GetServiceName()
	{
		return NULL;
	}

	template <typename TClient>
	ATL_NOINLINE inline const char * CConverterStub<TClient>::GetNamespaceUriA()
	{
		return NAMESPACE;
	}

	template <typename TClient>
	ATL_NOINLINE inline HRESULT CConverterStub<TClient>::CallFunction(void*, const wchar_t*, int, size_t)
	{
		return E_NOTIMPL;
	}

	template <typename TClient>
	ATL_NOINLINE inline HRESULT CConverterStub<TClient>::GetClientReader(ISAXXMLReader** ppReader)
	{
		if (ppReader == NULL)
		{
			return E_INVALIDARG;
		}
		
		CComPtr<ISAXXMLReader> spReader = GetReader();
		if (spReader.p != NULL)
		{
			*ppReader = spReader.Detach();
			return S_OK;
		}
		return TClient::GetClientReader(ppReader);
	}
} // namespace Converter
