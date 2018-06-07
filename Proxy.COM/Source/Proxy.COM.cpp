// Proxy.COM.cpp : Implementation of DLL Exports.

#include "stdafx.h"
#include "resource.h"

// The module attribute causes DllMain, DllRegisterServer and DllUnregisterServer to be automatically implemented for you
[ module(dll, uuid = "{B3EDDA36-04E6-4D2F-A39D-46C07038265E}", 
		 name = "EncodingIdn", 
		 helpstring = "Encoding.Idn 1.0 Type Library",
		 resource_name = "IDR_PROXYCOM") ]
class CProxyCOMModule
{
public:
// Override CAtlDllModuleT members
};
		 
