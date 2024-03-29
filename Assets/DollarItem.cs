using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollarItem : MonoBehaviour {
	public enum DOLLAR_ITEM_TYPE{
        GOLD,REMOVE_ADS,COMBO1,COMBO2,COMBO3,COMBO4,NINJA1, NINJA2, NINJA3, NINJA4
	}
	public DOLLAR_ITEM_TYPE itemType;
	ShopController shopController;
	// Use this for initialization
	void Start () {
		shopController=GameObject.FindGameObjectWithTag("DialogShop").GetComponent<ShopController>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void purchase(){
		Debug.Log("Item Name: "+itemType);
		switch (itemType){
		case DOLLAR_ITEM_TYPE.GOLD:

			break;
		case DOLLAR_ITEM_TYPE.REMOVE_ADS:

			break;
		case DOLLAR_ITEM_TYPE.COMBO1:

			break;
		case DOLLAR_ITEM_TYPE.COMBO2:

			break;
		case DOLLAR_ITEM_TYPE.COMBO3:

			break;
		case DOLLAR_ITEM_TYPE.COMBO4:

			break;
		}
	}
}
