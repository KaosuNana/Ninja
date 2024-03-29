using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class WoodenBox : MonoBehaviour {
	public float FORCE_TO_DESTROY_OBJECTS;
	SkeletonAnimation mAnimation;
	// Use this for initialization
	void Start () {
		mAnimation = GetComponent<SkeletonAnimation> ();
		mAnimation.AnimationState.Complete+= delegate {
			Destroy(gameObject);
		};
	}
	public void breakBox(){
		AudioManager.instance.playSound(AudioManager.instance.soundWoodenBoxBreak);
		mAnimation.AnimationState.SetAnimation (0, "broke", false);
	//	GetComponent<Rigidbody2D>().velocity=Vector3.zero;
	//	GetComponent<Rigidbody2D>().isKinematic=true;
		disableCollision ();
	}
	public void disableCollision(){
		gameObject.layer = LayerMask.NameToLayer ("DisableCollision");
	}
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		 if (coll.gameObject.tag=="hostage"){
			if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>FORCE_TO_DESTROY_OBJECTS   || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>FORCE_TO_DESTROY_OBJECTS){
				coll.gameObject.GetComponent<Hostage>().winAnimation();
			}


		}





	}


}
