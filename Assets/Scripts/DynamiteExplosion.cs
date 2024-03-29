using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteExplosion : MonoBehaviour {
	// Use this for initialization
	public float power =20;
	void Start () {
		Destroy(this.gameObject, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) { 

        Debug.Log("dynamite "+other.gameObject.tag);
		if (other.gameObject.GetComponent<StandEnemy>()!=null){
			other.gameObject.GetComponent<StandEnemy>().killedByDynamite(this.gameObject,power);

		} else 	if (other.gameObject.tag=="woodenBox"){
			other.gameObject.GetComponent<WoodenBox>().breakBox();

		}	else	if (other.gameObject.tag=="hostage"){
			other.gameObject.GetComponent<Hostage>().winAnimation();
		}else 	if  (other.gameObject.GetComponent<FlyEnemy>()!=null){
			
			other.gameObject.GetComponent<FlyEnemy>().killedByDynamite(this.gameObject,power);

        } else if (other.gameObject.tag == "bombBox")
        {
            other.gameObject.GetComponent<BombBox>().blow();
        }else 	if (other.gameObject.transform.parent.gameObject.tag == "iceCube"){
			other.gameObject.transform.parent.gameObject.GetComponent<IceCube> ().breakTheIce ();
		}


	
	}
}
