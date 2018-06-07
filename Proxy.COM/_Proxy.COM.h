

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0361 */
/* at Wed Jan 11 10:42:40 2006
 */
/* Compiler settings for _Proxy.COM.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef ___Proxy2ECOM_h__
#define ___Proxy2ECOM_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IProxy_FWD_DEFINED__
#define __IProxy_FWD_DEFINED__
typedef interface IProxy IProxy;
#endif 	/* __IProxy_FWD_DEFINED__ */


#ifndef __CProxy_FWD_DEFINED__
#define __CProxy_FWD_DEFINED__

#ifdef __cplusplus
typedef class CProxy CProxy;
#else
typedef struct CProxy CProxy;
#endif /* __cplusplus */

#endif 	/* __CProxy_FWD_DEFINED__ */


/* header files for imported files */
#include "prsht.h"
#include "mshtml.h"
#include "mshtmhst.h"
#include "exdisp.h"
#include "objsafe.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

#ifndef __IProxy_INTERFACE_DEFINED__
#define __IProxy_INTERFACE_DEFINED__

/* interface IProxy */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IProxy;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A47C4CCE-73C6-44DD-9383-86C626C58E02")
    IProxy : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE NativeToPunycode( 
            /* [in] */ BSTR __MIDL_0020,
            /* [in] */ BSTR __MIDL_0021,
            /* [in] */ BOOL __MIDL_0022,
            /* [in] */ BOOL __MIDL_0023,
            /* [retval][out] */ BSTR *__MIDL_0024) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE NativeToRace( 
            /* [in] */ BSTR __MIDL_0025,
            /* [in] */ BSTR __MIDL_0026,
            /* [in] */ BOOL __MIDL_0027,
            /* [in] */ BOOL __MIDL_0028,
            /* [retval][out] */ BSTR *__MIDL_0029) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE PunycodeToNative( 
            /* [in] */ BSTR __MIDL_0030,
            /* [in] */ BSTR __MIDL_0031,
            /* [in] */ BOOL __MIDL_0032,
            /* [in] */ BOOL __MIDL_0033,
            /* [retval][out] */ BSTR *__MIDL_0034) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE RaceToNative( 
            /* [in] */ BSTR __MIDL_0035,
            /* [in] */ BSTR __MIDL_0036,
            /* [in] */ BOOL __MIDL_0037,
            /* [in] */ BOOL __MIDL_0038,
            /* [retval][out] */ BSTR *__MIDL_0039) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE PunycodeToRace( 
            /* [in] */ BSTR __MIDL_0040,
            /* [in] */ BSTR __MIDL_0041,
            /* [in] */ BOOL __MIDL_0042,
            /* [in] */ BOOL __MIDL_0043,
            /* [retval][out] */ BSTR *__MIDL_0044) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE RaceToPunycode( 
            /* [in] */ BSTR __MIDL_0045,
            /* [in] */ BSTR __MIDL_0046,
            /* [in] */ BOOL __MIDL_0047,
            /* [in] */ BOOL __MIDL_0048,
            /* [retval][out] */ BSTR *__MIDL_0049) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IProxyVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IProxy * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IProxy * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IProxy * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IProxy * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IProxy * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IProxy * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IProxy * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *NativeToPunycode )( 
            IProxy * This,
            /* [in] */ BSTR __MIDL_0020,
            /* [in] */ BSTR __MIDL_0021,
            /* [in] */ BOOL __MIDL_0022,
            /* [in] */ BOOL __MIDL_0023,
            /* [retval][out] */ BSTR *__MIDL_0024);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *NativeToRace )( 
            IProxy * This,
            /* [in] */ BSTR __MIDL_0025,
            /* [in] */ BSTR __MIDL_0026,
            /* [in] */ BOOL __MIDL_0027,
            /* [in] */ BOOL __MIDL_0028,
            /* [retval][out] */ BSTR *__MIDL_0029);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *PunycodeToNative )( 
            IProxy * This,
            /* [in] */ BSTR __MIDL_0030,
            /* [in] */ BSTR __MIDL_0031,
            /* [in] */ BOOL __MIDL_0032,
            /* [in] */ BOOL __MIDL_0033,
            /* [retval][out] */ BSTR *__MIDL_0034);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *RaceToNative )( 
            IProxy * This,
            /* [in] */ BSTR __MIDL_0035,
            /* [in] */ BSTR __MIDL_0036,
            /* [in] */ BOOL __MIDL_0037,
            /* [in] */ BOOL __MIDL_0038,
            /* [retval][out] */ BSTR *__MIDL_0039);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *PunycodeToRace )( 
            IProxy * This,
            /* [in] */ BSTR __MIDL_0040,
            /* [in] */ BSTR __MIDL_0041,
            /* [in] */ BOOL __MIDL_0042,
            /* [in] */ BOOL __MIDL_0043,
            /* [retval][out] */ BSTR *__MIDL_0044);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *RaceToPunycode )( 
            IProxy * This,
            /* [in] */ BSTR __MIDL_0045,
            /* [in] */ BSTR __MIDL_0046,
            /* [in] */ BOOL __MIDL_0047,
            /* [in] */ BOOL __MIDL_0048,
            /* [retval][out] */ BSTR *__MIDL_0049);
        
        END_INTERFACE
    } IProxyVtbl;

    interface IProxy
    {
        CONST_VTBL struct IProxyVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IProxy_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IProxy_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IProxy_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IProxy_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IProxy_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IProxy_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IProxy_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IProxy_NativeToPunycode(This,__MIDL_0020,__MIDL_0021,__MIDL_0022,__MIDL_0023,__MIDL_0024)	\
    (This)->lpVtbl -> NativeToPunycode(This,__MIDL_0020,__MIDL_0021,__MIDL_0022,__MIDL_0023,__MIDL_0024)

#define IProxy_NativeToRace(This,__MIDL_0025,__MIDL_0026,__MIDL_0027,__MIDL_0028,__MIDL_0029)	\
    (This)->lpVtbl -> NativeToRace(This,__MIDL_0025,__MIDL_0026,__MIDL_0027,__MIDL_0028,__MIDL_0029)

#define IProxy_PunycodeToNative(This,__MIDL_0030,__MIDL_0031,__MIDL_0032,__MIDL_0033,__MIDL_0034)	\
    (This)->lpVtbl -> PunycodeToNative(This,__MIDL_0030,__MIDL_0031,__MIDL_0032,__MIDL_0033,__MIDL_0034)

#define IProxy_RaceToNative(This,__MIDL_0035,__MIDL_0036,__MIDL_0037,__MIDL_0038,__MIDL_0039)	\
    (This)->lpVtbl -> RaceToNative(This,__MIDL_0035,__MIDL_0036,__MIDL_0037,__MIDL_0038,__MIDL_0039)

#define IProxy_PunycodeToRace(This,__MIDL_0040,__MIDL_0041,__MIDL_0042,__MIDL_0043,__MIDL_0044)	\
    (This)->lpVtbl -> PunycodeToRace(This,__MIDL_0040,__MIDL_0041,__MIDL_0042,__MIDL_0043,__MIDL_0044)

#define IProxy_RaceToPunycode(This,__MIDL_0045,__MIDL_0046,__MIDL_0047,__MIDL_0048,__MIDL_0049)	\
    (This)->lpVtbl -> RaceToPunycode(This,__MIDL_0045,__MIDL_0046,__MIDL_0047,__MIDL_0048,__MIDL_0049)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IProxy_NativeToPunycode_Proxy( 
    IProxy * This,
    /* [in] */ BSTR __MIDL_0020,
    /* [in] */ BSTR __MIDL_0021,
    /* [in] */ BOOL __MIDL_0022,
    /* [in] */ BOOL __MIDL_0023,
    /* [retval][out] */ BSTR *__MIDL_0024);


void __RPC_STUB IProxy_NativeToPunycode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IProxy_NativeToRace_Proxy( 
    IProxy * This,
    /* [in] */ BSTR __MIDL_0025,
    /* [in] */ BSTR __MIDL_0026,
    /* [in] */ BOOL __MIDL_0027,
    /* [in] */ BOOL __MIDL_0028,
    /* [retval][out] */ BSTR *__MIDL_0029);


void __RPC_STUB IProxy_NativeToRace_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IProxy_PunycodeToNative_Proxy( 
    IProxy * This,
    /* [in] */ BSTR __MIDL_0030,
    /* [in] */ BSTR __MIDL_0031,
    /* [in] */ BOOL __MIDL_0032,
    /* [in] */ BOOL __MIDL_0033,
    /* [retval][out] */ BSTR *__MIDL_0034);


void __RPC_STUB IProxy_PunycodeToNative_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IProxy_RaceToNative_Proxy( 
    IProxy * This,
    /* [in] */ BSTR __MIDL_0035,
    /* [in] */ BSTR __MIDL_0036,
    /* [in] */ BOOL __MIDL_0037,
    /* [in] */ BOOL __MIDL_0038,
    /* [retval][out] */ BSTR *__MIDL_0039);


void __RPC_STUB IProxy_RaceToNative_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IProxy_PunycodeToRace_Proxy( 
    IProxy * This,
    /* [in] */ BSTR __MIDL_0040,
    /* [in] */ BSTR __MIDL_0041,
    /* [in] */ BOOL __MIDL_0042,
    /* [in] */ BOOL __MIDL_0043,
    /* [retval][out] */ BSTR *__MIDL_0044);


void __RPC_STUB IProxy_PunycodeToRace_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IProxy_RaceToPunycode_Proxy( 
    IProxy * This,
    /* [in] */ BSTR __MIDL_0045,
    /* [in] */ BSTR __MIDL_0046,
    /* [in] */ BOOL __MIDL_0047,
    /* [in] */ BOOL __MIDL_0048,
    /* [retval][out] */ BSTR *__MIDL_0049);


void __RPC_STUB IProxy_RaceToPunycode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IProxy_INTERFACE_DEFINED__ */



#ifndef __EncodingIdn_LIBRARY_DEFINED__
#define __EncodingIdn_LIBRARY_DEFINED__

/* library EncodingIdn */
/* [helpstring][uuid][version] */ 


EXTERN_C const IID LIBID_EncodingIdn;

EXTERN_C const CLSID CLSID_CProxy;

#ifdef __cplusplus

class DECLSPEC_UUID("F5C525CB-6A7F-4BA1-A07F-5BD524FFE9B5")
CProxy;
#endif
#endif /* __EncodingIdn_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


