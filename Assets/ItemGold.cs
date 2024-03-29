using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemGold : MonoBehaviour {
	public enum GOLD_ITEM_TYPE{
		DART,TRIPPE_DART,SUPER_DART,BOMB
	}
	public GOLD_ITEM_TYPE itemType;
	public int price;
	public UILabel priceText,amountText;
	ShopController shopController;
	// Use this for initialization
	void Start () {

		shopController=GameObject.FindGameObjectWithTag("DialogShop").GetComponent<ShopController>();	
		checkAvailable();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void confirmPurchase(){

		switch (itemType){
		case GOLD_ITEM_TYPE.DART:
			if (GameConstant.getDartCount()==0){
				if (GameConstant.getGold()>=price){
					GameConstant.addGold(-price);
					GameConstant.addDartCount(1);
                    shopController.SetItemStatus();
					}
					else {
					shopController.showNotify();
				}
			} 
			break;
		case GOLD_ITEM_TYPE.TRIPPE_DART:
			if (GameConstant.getTrippleDartCount()==0){
				if (GameConstant.getGold()>=price){
					GameConstant.addGold(-price);
					GameConstant.addTrippleDartCount(1);
                    shopController.SetItemStatus();
					} else {
					shopController.showNotify();
				}
			}
			break;
		case GOLD_ITEM_TYPE.SUPER_DART:
			if (GameConstant.getSuperDartCount()==0){
				if (GameConstant.getGold()>=price){
					GameConstant.addGold(-price);
					GameConstant.addSuperDartCount(1);
                    shopController.SetItemStatus();
					} else {
					shopController.showNotify();
				}
			} 
			break;
		case GOLD_ITEM_TYPE.BOMB:
			if (GameConstant.getBombCount()==0){
				if (GameConstant.getGold()>=price){
					GameConstant.addGold(-price);
					GameConstant.addBombCount(1);
                    shopController.SetItemStatus();
					} else {
					shopController.showNotify();
				}
			}
			break;

		} 
		checkAvailable();
	}
	public void checkAvailable(){
		switch (itemType){
		case GOLD_ITEM_TYPE.DART:
			if (GameConstant.getDartCount()==0){
				priceText.text=""+price;
				amountText.text = "拥有: 0";
			} else {
				priceText.text="使用";
				amountText.text = "拥有: " + GameConstant.getDartCount ();
			}
			break;
		case GOLD_ITEM_TYPE.TRIPPE_DART:
			if (GameConstant.getTrippleDartCount()==0){
				priceText.text=""+price;
				amountText.text = "拥有: 0";
			} else {
				priceText.text= "使用";
				amountText.text = "拥有: " + GameConstant.getTrippleDartCount ();
			}
			break;
		case GOLD_ITEM_TYPE.SUPER_DART:
			if (GameConstant.getSuperDartCount()==0){
				priceText.text=""+price;
				amountText.text = "拥有: 0";
			} else {
				priceText.text= "使用";
				amountText.text = "拥有: " + GameConstant.getSuperDartCount ();
			}
			break;
		case GOLD_ITEM_TYPE.BOMB:
			if (GameConstant.getBombCount()==0){
				priceText.text=""+price;
				amountText.text = "拥有: 0";
			} else {
				priceText.text= "使用";
				amountText.text = "拥有: " + GameConstant.getBombCount ();
			}
			break;

		} 
		shopController.updateGoldAmount();
	}
	public void purchase(){
		switch (itemType){
		case GOLD_ITEM_TYPE.DART:
			if (GameConstant.getDartCount()==0){
				shopController.dialogConfirm.GetComponent<DialogConfirm>().show("购买飞镖?",0);
				ShopController.selectedItem=ShopController.SELECTED_SHOP_ITEM_TYPE.DART;
				ShopController.selectedObject=this.gameObject;
			} else {
				if (shopController.gameController!=null){
					
					shopController.gameController.addDart();
					checkAvailable();
				}

			}
	
			break;
		case GOLD_ITEM_TYPE.TRIPPE_DART:
			if (GameConstant.getTrippleDartCount()==0){
				shopController.dialogConfirm.GetComponent<DialogConfirm>().show("购买三连镖?",1);
				ShopController.selectedItem=ShopController.SELECTED_SHOP_ITEM_TYPE.TRIPLEDART;
				ShopController.selectedObject=this.gameObject;
			} else {
				if (shopController.gameController!=null){
					shopController.gameController.addTrippleDart();
					checkAvailable();
				}
			}
		
			break;
		case GOLD_ITEM_TYPE.SUPER_DART:
			if (GameConstant.getSuperDartCount()==0){
				shopController.dialogConfirm.GetComponent<DialogConfirm>().show("购买超级镖?",2);
				ShopController.selectedItem=ShopController.SELECTED_SHOP_ITEM_TYPE.SUPERDART;
				ShopController.selectedObject=this.gameObject;
			} else {
				if (shopController.gameController!=null){
				shopController.gameController.addSupperDart();
				checkAvailable();
				}
			}
		
			break;
		case GOLD_ITEM_TYPE.BOMB:
			if (GameConstant.getBombCount()==0){
				shopController.dialogConfirm.GetComponent<DialogConfirm>().show("购买炸药?",3);
				ShopController.selectedItem=ShopController.SELECTED_SHOP_ITEM_TYPE.BOMB;
				ShopController.selectedObject=this.gameObject;
			} else {
				if (shopController.gameController!=null){
				shopController.gameController.addBomb();
				checkAvailable();
				}
			}
		
			break;

		}

	}
}
