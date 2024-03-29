using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public float power = 20.0F;


	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 0.3f);
		/*
		radius = GetComponent<CircleCollider2D> ().radius;
		power = GetComponent<PointEffector2D> ().for;
		Vector2 explosionPos = transform.position;
		Collider2D[] colliders = Physics2D.OverlapCircle(explosionPos, radius);
		foreach (Collider2D hit in colliders)
		{
			Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

			if (rb != null)
				rb.AddForceAtPosition  (power, explosionPos);
		}
*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) { 

		Debug.Log("bomb "+other.name);
		if (other.gameObject.GetComponent<StandEnemy>()!=null){

		
			other.gameObject.GetComponent<StandEnemy>().killedByDynamite(this.gameObject,power);

			//Debug.Log ("ragdoll " + this.transform.position);

		} else 	if (other.gameObject.tag=="woodenBox"){
			other.gameObject.GetComponent<WoodenBox>().breakBox();

		}	else 	if (other.gameObject.tag=="bombBox"){
			other.gameObject.GetComponent<BombBox>().blow();

		}else	if (other.gameObject.tag=="hostage"){
			other.gameObject.GetComponent<Hostage>().winAnimation();
		}else 	if  (other.gameObject.GetComponent<FlyEnemy>()!=null){

			other.gameObject.GetComponent<FlyEnemy>().killedByDynamite(this.gameObject,power);

		}else 	if (other.gameObject.tag == "iceCube"){
			AudioManager.instance.playSound(AudioManager.instance.soundIceBreak);
			Destroy(other.gameObject.transform.parent.gameObject);
		}



	}


}
