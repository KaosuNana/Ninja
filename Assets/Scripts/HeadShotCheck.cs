using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShotCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) { 
		if (other.gameObject.tag == "playerShuriken"){
			
			Debug.Log("headshot check");
			if (transform.parent.gameObject.GetComponent<FlyEnemy>()!=null){
				transform.parent.gameObject.GetComponent<FlyEnemy>().setCurrentCollider(other);
				transform.parent.gameObject.GetComponent<FlyEnemy>().headShot(other.gameObject.GetComponent<PlayerShuriken>().damage*other.gameObject.GetComponent<PlayerShuriken>().headShotMultiplier,other.attachedRigidbody.velocity);

			}
			if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null){
				transform.parent.gameObject.GetComponent<StandEnemy>().setCurrentCollider(other);
				transform.parent.gameObject.GetComponent<StandEnemy>().bodyShotDetect.SetActive(false);
                transform.parent.gameObject.GetComponent<StandEnemy>().headShot(other.gameObject.GetComponent<PlayerShuriken>().damage*other.gameObject.GetComponent<PlayerShuriken>().headShotMultiplier, other.attachedRigidbody.velocity);
			}

		}

		if (other.gameObject.tag == "playerSuperShuriken"){
			Debug.Log("headshot check");
			if (transform.parent.gameObject.GetComponent<FlyEnemy>()!=null){
				transform.parent.gameObject.GetComponent<FlyEnemy>().setCurrentCollider(other);
				transform.parent.gameObject.GetComponent<FlyEnemy>().headShot(other.gameObject.GetComponent<PlayerSuperShuriken>().damage*other.gameObject.GetComponent<PlayerSuperShuriken>().headShotMultiplier,other.attachedRigidbody.velocity);

			}
			if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null){
				transform.parent.gameObject.GetComponent<StandEnemy>().setCurrentCollider(other);
				transform.parent.gameObject.GetComponent<StandEnemy>().bodyShotDetect.SetActive(false);
                transform.parent.gameObject.GetComponent<StandEnemy>().headShot(other.gameObject.GetComponent<PlayerSuperShuriken>().damage*other.gameObject.GetComponent<PlayerSuperShuriken>().headShotMultiplier, other.attachedRigidbody.velocity);
			}

		}


	}


	void OnTriggerExit2D(Collider2D other)
	{
		if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null){

		
			Invoke("enableBody",0.1f);
		}


	}

	void enableBody(){
		if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null)
			transform.parent.gameObject.GetComponent<StandEnemy>().bodyShotDetect.SetActive(true);
	}

}
