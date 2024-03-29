using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBar : MonoBehaviour {
	public float FORCE_TO_DESTROY_OBJECTS;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag=="bombBox"){
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_DESTROY_OBJECTS   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_DESTROY_OBJECTS){
				coll.gameObject.GetComponent<BombBox>().blow();
			} 


		} else  if (coll.gameObject.tag=="iceCube"){
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_DESTROY_OBJECTS   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_DESTROY_OBJECTS){
				coll.gameObject.GetComponent<IceCube>().breakTheIce();
			}

		}else  if (coll.gameObject.tag=="hostage"){
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_DESTROY_OBJECTS   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_DESTROY_OBJECTS){
				coll.gameObject.GetComponent<Hostage>().winAnimation();
			}


		}





	}


}
