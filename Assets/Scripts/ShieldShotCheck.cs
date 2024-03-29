using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldShotCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) { 
		if (other.gameObject.tag == "playerShuriken"){
			if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null){
				transform.parent.gameObject.GetComponent<StandEnemy>().removeProtection(other.gameObject.GetComponent<PlayerShuriken>().damage);
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (transform.parent.gameObject.GetComponent<StandEnemy>()!=null){
			transform.parent.gameObject.GetComponent<StandEnemy>().enableBodyAndHeadDetect();
		}
	}

}
