using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWheel : MonoBehaviour {
	public float FORCE_TO_DESTROY_OBJECTS;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag=="woodenBox" ){
		//	AudioManager.instance.playSound(AudioManager.instance.wo);
		
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_DESTROY_OBJECTS/2   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_DESTROY_OBJECTS/2){
				coll.gameObject.GetComponent<WoodenBox> ().breakBox();
			} 


		} else if (coll.gameObject.tag == "hostage"){
			coll.gameObject.GetComponent<Hostage>().winAnimation();
	
		}



	}

	void OnTriggerEnter2D(Collider2D other) { 

		Debug.Log("spike wheel trigger  "+other.name);

		if (other.gameObject.tag == "iceCube"){
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_DESTROY_OBJECTS   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_DESTROY_OBJECTS){
				if (other.gameObject.transform.parent.gameObject.GetComponent<IceCube>()!=null)
					other.gameObject.transform.parent.gameObject.GetComponent<IceCube>().breakTheIce();
			} 

		} else if (other.gameObject.tag=="bombBox"){
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_DESTROY_OBJECTS  || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_DESTROY_OBJECTS){
				other.gameObject.GetComponent<BombBox>().blow();
			} 

		}

	}
}
