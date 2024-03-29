using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBall : MonoBehaviour {
	public float FORCE_TO_TRIGGER_CANNON;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("Metal Ball  "+coll.gameObject.tag);
		if (coll.gameObject.tag=="canon" ){
			//Debug.Log("Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)  "+Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
			//Debug.Log("Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)  "+Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y));
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_TRIGGER_CANNON   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_TRIGGER_CANNON){
				coll.transform.parent.gameObject.GetComponent<Canon>().shoot();
			} 
		} else if (coll.gameObject.tag=="bombBox"){
	
			
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_TRIGGER_CANNON   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_TRIGGER_CANNON){
                coll.gameObject.GetComponent<BombBox>().blow();
            } 
		
		} else if (coll.gameObject.tag=="iceCube" ){

			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_TRIGGER_CANNON   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_TRIGGER_CANNON){
				coll.gameObject.GetComponent<IceCube>().breakTheIce();
			} 
		} else if (coll.gameObject.tag=="woodenBox" ){
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_TRIGGER_CANNON   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_TRIGGER_CANNON){
				coll.gameObject.GetComponent<WoodenBox> ().breakBox();
			}
		}else  if (coll.gameObject.tag=="hostage"){
			Debug.Log (" coll.GetContact(0).normalImpulse   "+ coll.GetContact(0).normalImpulse);
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_TRIGGER_CANNON   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_TRIGGER_CANNON){
				coll.gameObject.GetComponent<Hostage>().winAnimation();
			}


		}




	}


}
