using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour {
	Rigidbody2D rig;
	public float throwForce;
	public float damage;
	public float headShotMultiplier;
	public GameObject explosionPrefab;
	public void fly(Vector2 direction){

		GetComponent<Rigidbody2D>().velocity =  throwForce *direction.normalized;
	}

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * throwForce;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * throwForce;
	}
	public void explode(){
		AudioManager.instance.playSound(AudioManager.instance.soundExplosion);
		Instantiate (explosionPrefab,gameObject.transform.position,Quaternion.identity);
		Destroy(this.gameObject);

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag=="wall" || coll.gameObject.tag=="ground" ||coll.gameObject.tag=="bounceItem" ||coll.gameObject.tag=="metalBox" ){
			explode();
		}
		 else if (coll.gameObject.tag == "bombBox"){
			coll.gameObject.GetComponent<BombBox>().blow();
			explode();
		}else	if (coll.gameObject.tag=="woodenBox" ){
			coll.gameObject.GetComponent<WoodenBox> ().breakBox();
			explode ();

		}else if (coll.gameObject.tag == "hostage"){
			coll.gameObject.GetComponent<Hostage>().winAnimation();
			explode();
		}else if (coll.gameObject.tag == "canon"){
			coll.transform.parent.gameObject.GetComponent<Canon>().shoot();
			explode();
		}else if (coll.gameObject.tag == "iceCube"){
			coll.gameObject.GetComponent<IceCube> ().breakTheIce ();
			explode();
		}else if (coll.gameObject.tag == "enemy"){
			
			explode();
		}

	}
}
