using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardObject : MonoBehaviour {
    public GameObject imgTick, itemIcon, amount, imgToday,btClaim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void claimReward(){
        imgToday.SetActive(false);
        btClaim.SetActive(false);

    }

    void closeDialog(){
        
    }
}
