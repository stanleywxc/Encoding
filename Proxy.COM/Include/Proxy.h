// Proxy.h : Declaration of the CProxy

#pragma once
#include "resource.h"       // main symbols


// IProxy
[
	object,
	uuid			("A47C4CCE-73C6-44DD-9383-86C626C58E02"),
	dual,	
	helpstring		("IProxy Interface"),
	pointer_default	(unique)
]
__interface IProxy : IDispatch
{
	[id(1), helpstring("method NativeToPunycode")] 
	HRESULT NativeToPunycode	([in] BSTR, [in] BSTR, [in] BOOL, [in] BOOL, [out,retval] BSTR*);

	[id(2), helpstring("method NativeToRace")] 
	HRESULT NativeToRace		([in] BSTR, [in] BSTR, [in] BOOL, [in] BOOL, [out,retval] BSTR*);

	[id(3), helpstring("method PunycodeToNative")]
	HRESULT PunycodeToNative	([in] BSTR, [in] BSTR, [in] BOOL, [in] BOOL, [out,retval] BSTR*);

	[id(4), helpstring("method RaceToNative")]
	HRESULT RaceToNative		([in] BSTR, [in] BSTR, [in] BOOL, [in] BOOL, [out,retval] BSTR*);

	[id(5), helpstring("method PunycodeToRace")]
	HRESULT PunycodeToRace		([in] BSTR, [in] BSTR, [in] BOOL, [in] BOOL, [out,retval] BSTR*);

	[id(6), helpstring("method RaceToPunycode")]
	HRESULT RaceToPunycode		([in] BSTR, [in] BSTR, [in] BOOL, [in] BOOL, [out,retval] BSTR*);
};

// CProxy
[
	coclass,
	threading	("apartment"),
	vi_progid	("Encoding.Idn"),
	progid		("Encoding.Idn.1"),
	version		(1.0),
	uuid		("F5C525CB-6A7F-4BA1-A07F-5BD524FFE9B5"),
	helpstring	("EncodingIdn Class"),
	default		(IProxy),
	source		(IProxy)
]
class ATL_NO_VTABLE CProxy : public IProxy
{
	typedef enum
	{
		ACTION_NATIVETOPUNYCODE		= 1,
		ACTION_NATIVETORACE			= 2,
		ACTION_PUNYCODETONATIVE		= 3,
		ACTION_RACETONATIVE			= 4,
		ACTION_PUNYCODETORACE		= 5,
		ACTION_RACETOPUNYCODE		= 6
	} Action;

	public:
					CProxy				() {}

		DECLARE_PROTECT_FINAL_CONSTRUCT	()

		HRESULT		FinalConstruct		() {return S_OK;}
		void		FinalRelease		() {}

	public:

		STDMETHOD(NativeToPunycode)		(BSTR, BSTR, BOOL, BOOL, BSTR*);
		STDMETHOD(NativeToRace)			(BSTR, BSTR, BOOL, BOOL, BSTR*);
		STDMETHOD(PunycodeToNative)		(BSTR, BSTR, BOOL, BOOL, BSTR*);
		STDMETHOD(RaceToNative)			(BSTR, BSTR, BOOL, BOOL, BSTR*);
		STDMETHOD(PunycodeToRace)		(BSTR, BSTR, BOOL, BOOL, BSTR*);
		STDMETHOD(RaceToPunycode)		(BSTR, BSTR, BOOL, BOOL, BSTR*);

		private:
			HRESULT	Convert			(Action, BSTR, BSTR, BOOL, BOOL, BSTR*);
			BSTR	ToRawFormat		(BSTR);
			BSTR	FromUTF8		(BSTR);
			BSTR	ToUTF8			(BSTR);
			char*	ToChars			(BSTR);
			void*	Allocate		(int);
			void*	Reallocate		(void*, int);
			void	Free			(void*);
};