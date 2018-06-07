//-----------------------------------------------------------------------------
// Soap Headers
//-----------------------------------------------------------------------------

#ifndef __SOAP_H__
#define __SOAP_H__

#include <windows.h>

// Definitiones
struct _soap_request_map
{
	char		Action[512];
	char		ResultNode[512];
	char		SOAPAction[512];
	char		SOAPXml[512];
};

typedef struct _soap_request_map SOAP_REQUEST_MAP;

class CSoap
{
	//-------------------------------------------------------------------------
	// Data members
	//-------------------------------------------------------------------------
	private:
		char				m_soapUrl[256];
	
	//-------------------------------------------------------------------------
	// Constructors
	//-------------------------------------------------------------------------
	public:
							CSoap				();
							CSoap				(const char*);

	//-------------------------------------------------------------------------
	// Public methods
	//-------------------------------------------------------------------------
	public:
		void				SetUrl				(const char*);
		const char*			GetUrl				();
		HRESULT				NativeToRace		(BSTR, BSTR*);
		HRESULT				NativeToPunycode	(BSTR, BSTR*);
		HRESULT				PunycodeToNative	(BSTR, BSTR*);
		HRESULT				RaceToNative		(BSTR, BSTR*);
		HRESULT				PunycodeToRace		(BSTR, BSTR*);
		HRESULT				RaceToPunycode		(BSTR, BSTR*);

	//-------------------------------------------------------------------------
	// Private methods
	//-------------------------------------------------------------------------
	public:
		char*					ToUTF8				(BSTR);
		BSTR					FromUTF8			(char*);
		BSTR					CallMethod			(const char*, BSTR);
		const SOAP_REQUEST_MAP*	GetSoapRequestMap	(const char*);
		char*					ParseResponse		(const SOAP_REQUEST_MAP*, const char*);
};

#endif // __SOAP_H__