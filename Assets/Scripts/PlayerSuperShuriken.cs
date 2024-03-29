using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperShuriken : MonoBehaviour {

	Rigidbody2D rig;
	public float throwForce;
	public float damage;
	public float headShotMultiplier;
	public int MAX_SHURIKEN_BOUNCE= 6;
	int currentBounceCount;
	// Use this for initialization
	void Start () {
		currentBounceCount=MAX_SHURIKEN_BOUNCE;



	}
	public void fly(Vector2 direction){
		//Debug.Log("direction  "+direction);
		GetComponent<Rigidbody2D>().velocity =  throwForce *direction.normalized;
	}
	// Update is called once per frame
	void Update () {
		

	}
	public void destroyShuriken(){
		Destroy(gameObject);
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag=="wall" || coll.gameObject.tag=="ground" ||coll.gameObject.tag=="bounceItem"){
			if (currentBounceCount>0){

				currentBounceCount--;
				if (currentBounceCount==0) destroyShuriken();
				AudioManager.instance.playShurikenBounceSound();
			}
		}
		else 	if (coll.gameObject.tag==""){
			destroyShuriken();

		}	else if (coll.gameObject.tag == "bombBox"){
				coll.gameObject.GetComponent<BombBox>().blow();
				destroyShuriken();
		}	else	if (coll.gameObject.tag=="woodenBox" ){
				coll.gameObject.GetComponent<WoodenBox> ().breakBox();

		} else if (coll.gameObject.tag == "hostage"){
			coll.gameObject.GetComponent<Hostage>().winAnimation();
			destroyShuriken();
		}else if (coll.gameObject.tag == "canon"){
			coll.transform.parent.gameObject.GetComponent<Canon>().shoot();
			destroyShuriken();
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{


	}
	void OnTriggerEnter2D(Collider2D other) { 

		Debug.Log("super shuriken trigger  "+other.name);

		if (other.gameObject.transform.parent.gameObject.tag == "iceCube"){
			other.gameObject.transform.parent.gameObject.GetComponent<IceCube> ().breakTheIce ();
		}
	}
}
