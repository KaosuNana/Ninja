using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class InAppPurchase : MonoBehaviour
{

    public GameObject dialogWait;
    bool _isInitialized = false;
    const string REMOVE_AD = "id1408850272.stickman.item.1";
    const string BUY_GOLD = "id1408850272.stickman.ios.1";
    const string BUY_COMBO1 = "id1408850272.stickman.gold.1";
    const string BUY_COMBO2 = "id1408850272.stickman.gold.2";
    const string BUY_COMBO3 = "id1408850272.stickman.gold.3";
    const string BUY_COMBO4 = "id1408850272.stickman.gold.4";

    const string BUY_NINJA1 = "id1408850272.stickman.ninja.1";
    const string BUY_NINJA2 = "id1408850272.stickman.ninja.2";
    const string BUY_NINJA3 = "id1408850272.stickman.ninja.3";
    const string BUY_NINJA4 = "id1408850272.stickman.ninja.4";

    public delegate void OnRemoveAd();
    public static event OnRemoveAd onRemoveAd;

    public delegate void OnPurchasePack();
    public static event OnPurchasePack onPurchasePack;

    public delegate void OnPurchaseFail();
    public static event OnPurchaseFail onPurchaseFail;

    public delegate void OnRestore();
    public static event OnRestore onRestore;

    // Use this for initialization
    void Start()
    {

        print("init inapp");

        #if UNITY_IPHONE
        OpenIAB.mapSku(REMOVE_AD, OpenIAB_iOS.STORE, REMOVE_AD);
        OpenIAB.mapSku(BUY_GOLD, OpenIAB_iOS.STORE, BUY_GOLD);
        OpenIAB.mapSku(BUY_COMBO1, OpenIAB_iOS.STORE, BUY_COMBO1);
        OpenIAB.mapSku(BUY_COMBO2, OpenIAB_iOS.STORE, BUY_COMBO2);
        OpenIAB.mapSku(BUY_COMBO3, OpenIAB_iOS.STORE, BUY_COMBO3);
        OpenIAB.mapSku(BUY_COMBO4, OpenIAB_iOS.STORE, BUY_COMBO4);
        OpenIAB.mapSku(BUY_NINJA1, OpenIAB_iOS.STORE, BUY_NINJA1);
        OpenIAB.mapSku(BUY_NINJA2, OpenIAB_iOS.STORE, BUY_NINJA2);
        OpenIAB.mapSku(BUY_NINJA3, OpenIAB_iOS.STORE, BUY_NINJA3);
        OpenIAB.mapSku(BUY_NINJA4, OpenIAB_iOS.STORE, BUY_NINJA4);
        #elif UNITY_ANDROID

        //OpenIAB.mapSku(REMOVE_AD, OpenIAB_Android.STORE_GOOGLE, REMOVE_AD);
        //OpenIAB.mapSku(BUY_GOLD, OpenIAB_Android.STORE_GOOGLE, BUY_GOLD);
        //OpenIAB.mapSku(BUY_COMBO1, OpenIAB_Android.STORE_GOOGLE, BUY_COMBO1);
        //OpenIAB.mapSku(BUY_COMBO2, OpenIAB_Android.STORE_GOOGLE, BUY_COMBO2);
        //OpenIAB.mapSku(BUY_COMBO3, OpenIAB_Android.STORE_GOOGLE, BUY_COMBO3);
        //OpenIAB.mapSku(BUY_COMBO4, OpenIAB_Android.STORE_GOOGLE, BUY_COMBO4);
        //OpenIAB.mapSku(BUY_NINJA1, OpenIAB_Android.STORE_GOOGLE, BUY_NINJA1);
        //OpenIAB.mapSku(BUY_NINJA2, OpenIAB_Android.STORE_GOOGLE, BUY_NINJA2);
        //OpenIAB.mapSku(BUY_NINJA3, OpenIAB_Android.STORE_GOOGLE, BUY_NINJA3);
        //OpenIAB.mapSku(BUY_NINJA4, OpenIAB_Android.STORE_GOOGLE, BUY_NINJA4);
        #endif
        initInApp ();

	}
    public void restoreRemoveAdPurchase()
    {
    }
	public void removeAds(){
	}
	public void buyGold(){
	}
	public void buyCombo1(){
	}
	public void buyCombo2(){
	}
	public void buyCombo3(){
	}
	public void buyCombo4(){
	}
    public void buyNinja1()
    {
    }

    public void buyNinja2()
    {
    }
    public void buyNinja3()
    {
    }
    public void buyNinja4()
    {
    }

	public void querryItem(){
        if (dialogWait!=null)
        dialogWait.GetComponent<ToastMessage>().show("Please wait...");
       
       
          //  Debug.Log("mainMenu 2  " + mainMenu.GetComponent<MainMenuController>().dialogWait.name);
        
       
    //    OpenIAB.queryInventory(new string[] { REMOVE_AD, BUY_NINJA1, BUY_NINJA2, BUY_NINJA3, BUY_NINJA4 });
	}
	void initInApp(){
        var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAiWekxx8WZBiRgKUz+TjHvnEnVpXC7kqg9Y3Fxvj+CVv7ULf1MFVDcdt/6z7dkHIUQO1alKYjkxoFIZ+YdF8Xkpcfry5d2odDwkzb+rtbxZpcK2Kx0B6OdIGHc191GqAhOdSPa/In8IoM8RIydBhdlMh42Qx3NJ++a9ebMBBiNUxRKtIkORJJHIUERgvNTpLDvkS6S0Wpw4w0dx4GnabP7VTDDSzX34OcWqPJaLZ9MMfrCzG/e0m6YCQPBH4rBvlsDtg+fuewqBNmoI+ujo2jmlXGyQRqFk1tK/fi2NpZosWQzeyhe1e/gXMzDak12MuccVlHaWUpYoRR0M4617YIywIDAQAB";

	}
	private void OnEnable()
	{
		// Listen to all events for illustration purposes
		//OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
		//OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
		//OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		//OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		//OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
		//OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
		//OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		//OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
  //      OpenIABEventManager.transactionRestoredEvent += OnTransactionRestored;
  //      OpenIABEventManager.restoreSucceededEvent += OnRestoreSucceeded;
  //      OpenIABEventManager.restoreFailedEvent += OnRestoreFailed;
     

    }
	private void OnDisable()
	{
		// Remove all event handlers
		//OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
		//OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		//OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		//OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		//OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		//OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
		//OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		//OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
  //      OpenIABEventManager.transactionRestoredEvent -= OnTransactionRestored;
  //      OpenIABEventManager.restoreSucceededEvent -= OnRestoreSucceeded;
  //      OpenIABEventManager.restoreFailedEvent -= OnRestoreFailed;
	}

    private void OnTransactionRestored(string sku)
    {
        Debug.Log("Transaction restored: " + sku);
       
        if ( sku.Equals(REMOVE_AD))
        {
            if (GameConstant.isRemoveAd() != 1)
            {
                // Handle the purchase succeed
                GameConstant.setRemoveAd(1);
            }

            onRestore();
            Debug.Log("Restore Remove Ads");

        }

        if (sku.Equals(BUY_NINJA1))
        {
            // Handle the purchase succeed
            if (GameConstant.getStatusGhost() == -1)
            {
                GameConstant.setStatusGhost(0);
            }
            //  OpenIAB.consumeProduct(inventory.GetPurchase(BUY_SECTION_4));
        }
        if (sku.Equals(BUY_NINJA2))
        {
            // Handle the purchase succeed
            if (GameConstant.getStatusPowerful() == -1)
            {
                GameConstant.setStatusPowerful(0);
            }
            //  OpenIAB.consumeProduct(inventory.GetPurchase(BUY_SECTION_4));
        }
        if (sku.Equals(BUY_NINJA3))
        {
            // Handle the purchase succeed

            if (GameConstant.getStatusBomb() == -1)
            {
                GameConstant.setStatusBomb(0);

            }
            //  OpenIAB.consumeProduct(inventory.GetPurchase(BUY_SECTION_4));
        }
        if (sku.Equals(BUY_NINJA4))
        {
            // Handle the purchase succeed
            if (GameConstant.getStatusGolden() == -1)
            {
                GameConstant.setStatusGolden(0);

            }

            //  OpenIAB.consumeProduct(inventory.GetPurchase(BUY_SECTION_4));
        }

        onRestore();
    }

    private void OnRestoreSucceeded()
    {
        if (dialogWait != null)
      dialogWait.GetComponent<ToastMessage>().show("Restore successfully!");
      
    }

    private void OnRestoreFailed(string error)
    {
        if (dialogWait != null)
      dialogWait.GetComponent<ToastMessage>().show("Restore failed.");

       
    }
    
        private void billingSupportedEvent()
	{
		_isInitialized = true;
		Debug.Log("billingSupportedEvent");
	//	querryItem ();
	}
	private void billingNotSupportedEvent(string error)
	{
		Debug.Log("billingNotSupportedEvent: " + error);
	}
	//private void queryInventorySucceededEvent(Inventory inventory)
	//{
	


	//	if (inventory != null)
	//	{

	//	//	_label = inventory.ToString();
	//		_inventory = inventory;
 //           Debug.Log("queryInventorySucceededEvent: " + _inventory.ToString());
			
          
	//	}
	//}
	private void queryInventoryFailedEvent(string error)
	{
		Debug.Log("queryInventoryFailedEvent: " + error);

//		_label = error;
	}
	private void purchaseFailedEvent(int errorCode, string errorMessage)
	{
		Debug.Log("purchaseFailedEvent: " + errorMessage);

		if (errorCode == 7){
			onPurchaseFail();
		}
//		_label = "Purchase Failed: " + errorMessage;
	}

	private void consumePurchaseFailedEvent(string error)
	{
		Debug.Log("consumePurchaseFailedEvent: " + error);
	//	_label = "Consume Failed: " + error;
	}
}
