using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyShotCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) { 


		if (other.gameObject.tag == "playerShuriken" ){
			Debug.Log("Bodyshotcheck");
			if (transform.parent.gameObject.GetComponent<FlyEnemy>()!=null){
				transform.parent.gameObject.GetComponent<FlyEnemy>().setCurrentCollider(other);
				transform.parent.gameObject.GetComponent<FlyEnemy>().bodyShot(other.gameObject.GetComponent<PlayerShuriken>().damage,other.attachedRigidbody.velocity);
			} 
			if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null){
				transform.parent.gameObject.GetComponent<StandEnemy>().headShotDetect.SetActive(false);
				transform.parent.gameObject.GetComponent<StandEnemy>().setCurrentCollider(other);
                transform.parent.gameObject.GetComponent<StandEnemy>().bodyShot(other.gameObject.GetComponent<PlayerShuriken>().damage,other.attachedRigidbody.velocity);
               // transform.parent.gameObject.GetComponent<StandEnemy>().blowShield(other.attachedRigidbody.velocity);
			}
		} 
		if (other.gameObject.tag == "playerSuperShuriken" ){
			Debug.Log("Bodyshotcheck");
			if (transform.parent.gameObject.GetComponent<FlyEnemy>()!=null){
				transform.parent.gameObject.GetComponent<FlyEnemy>().setCurrentCollider(other);
				transform.parent.gameObject.GetComponent<FlyEnemy>().bodyShot(other.gameObject.GetComponent<PlayerSuperShuriken>().damage,other.attachedRigidbody.velocity);
			} 
			if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null){
				transform.parent.gameObject.GetComponent<StandEnemy>().headShotDetect.SetActive(false);
				transform.parent.gameObject.GetComponent<StandEnemy>().setCurrentCollider(other);
                transform.parent.gameObject.GetComponent<StandEnemy>().bodyShot(other.gameObject.GetComponent<PlayerSuperShuriken>().damage,other.attachedRigidbody.velocity);
               // transform.parent.gameObject.GetComponent<StandEnemy>().blowShield(other.attachedRigidbody.velocity);
			}
		}

	

	}


	void OnTriggerExit2D(Collider2D other)
	{
		if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null){

			Invoke("enableHead",0.1f);

		}

	}

	void enableHead(){
		if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null)
			transform.parent.gameObject.GetComponent<StandEnemy>().headShotDetect.SetActive(true);
	}


}
