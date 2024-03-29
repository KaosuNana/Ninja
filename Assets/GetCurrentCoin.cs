using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCurrentCoin : MonoBehaviour {
    public UILabel lbCoin;
	// Use this for initialization
	void Start () {
		lbCoin.text =  string.Format("{0:#,###0}", GameConstant.getGold());
	}
	private void OnEnable()
	{
		// Listen to all events for illustration purposes
		ShopController.onUpdateGold += updateGold ;


	}
	private void OnDisable()
	{
		// Remove all event handlers
		ShopController.onUpdateGold -= updateGold ;
	}
	public void updateGold(){
		lbCoin.text =  string.Format("{0:#,###0}", GameConstant.getGold());
	}
	// Update is called once per frame
	void Update () {
		
	}
}
