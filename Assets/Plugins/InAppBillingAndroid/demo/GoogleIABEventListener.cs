using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Prime31
{
	public class GoogleIABEventListener : MonoBehaviour
	{
		public IAPBuy iap;
#if UNITY_ANDROID

		public Text debugger;
		void OnEnable()
		{
			// Listen to all events for illustration purposes
			GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
			GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
			GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
			GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
			GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
			GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
			GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
			GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
			GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		}
	
	
		void OnDisable()
		{
			// Remove all event handlers
			GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
			GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
			GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
			GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
			GoogleIABManager.purchaseCompleteAwaitingVerificationEvent -= purchaseCompleteAwaitingVerificationEvent;
			GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
			GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
			GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
			GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
		}
	
	
	
		void billingSupportedEvent()
		{
			Debug.Log( "billingSupportedEvent" );
		}
	
	
		void billingNotSupportedEvent( string error )
		{
			Debug.Log( "billingNotSupportedEvent: " + error );
		}
	
	
		void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
		{
			Debug.Log( string.Format( "queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count ) );
			Prime31.Utils.logObject( purchases );
			Prime31.Utils.logObject( skus );
		}
	
	
		void queryInventoryFailedEvent( string error )
		{
			Debug.Log( "queryInventoryFailedEvent: " + error );
		}
	
	
		void purchaseCompleteAwaitingVerificationEvent( string purchaseData, string signature )
		{
			Debug.Log( "purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature );
		}
	
	
		void purchaseSucceededEvent( GooglePurchase purchase )
		{
			debugger.text =( "purchaseSucceededEvent: " + purchase );
			iap.AddCoin ();
		}
	
	
		void purchaseFailedEvent( string error, int response )
		{
			debugger.text = ( "purchaseFailedEvent: " + error + ", response: " + response );
		}
	
	
		void consumePurchaseSucceededEvent( GooglePurchase purchase )
		{
			debugger.text =( "consumePurchaseSucceededEvent: " + purchase );
		}
	
	
		void consumePurchaseFailedEvent( string error )
		{
			debugger.text =( "consumePurchaseFailedEvent: " + error );
		}
	
	
#endif
	}

}
	
	
