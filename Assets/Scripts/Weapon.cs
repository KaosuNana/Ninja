using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public enum WEAPON_TYPE{
		SHURIKEN,TRIPLE_SHURIKEN,SUPER_SHURIKEN,DYNAMITE,ADD_MORE
	}
	public WEAPON_TYPE weaponType;
	GameController gameController;
	void Start(){
		

	}
	void OnClick(){
		gameController=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		print ("On click");
		if (weaponType == WEAPON_TYPE.ADD_MORE){
			gameController.showItemShop();
		

		}


	}
}
