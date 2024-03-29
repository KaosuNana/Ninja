using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class CanonExplosion : MonoBehaviour {
	SkeletonAnimation mAnimation;
	public float power=20;
	// Use this for initialization
	void Start () {
		mAnimation= GetComponent<SkeletonAnimation>();
		mAnimation.AnimationState.SetAnimation(0,"blowup",false);
		mAnimation.AnimationState.Complete+= delegate {
			Destroy(gameObject);
		};
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter2D(Collider2D other) { 

		Debug.Log("dynamite "+other.name); 
		if (other.gameObject.GetComponent<StandEnemy>()!=null){
			other.gameObject.GetComponent<StandEnemy>().killedByDynamite(this.gameObject,power);

		} else 	if (other.gameObject.tag=="woodenBox"){
			other.gameObject.GetComponent<WoodenBox>().breakBox();

		}	else	if (other.gameObject.tag=="hostage"){
			other.gameObject.GetComponent<Hostage>().winAnimation();
		}else 	if  (other.gameObject.GetComponent<FlyEnemy>()!=null){

			other.gameObject.GetComponent<FlyEnemy>().killedByDynamite(this.gameObject,power);

		} else 	if (other.gameObject.transform.parent.gameObject.tag == "iceCube"){
			other.gameObject.transform.parent.gameObject.GetComponent<IceCube> ().breakTheIce ();
		}




	}
}
