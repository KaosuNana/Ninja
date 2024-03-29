using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class IceCube : MonoBehaviour {
	public GameObject shurikenDetect;
	public GameObject spine;
	SkeletonAnimation mAnimation;
	void Start () {
		mAnimation = spine.GetComponent<SkeletonAnimation> ();
	}

	// Update is called once per frame
	void Update () {

	}
	public void breakTheIce(){
		gameObject.layer = LayerMask.NameToLayer ("DisableCollision");
		foreach (Transform child in transform) {
			child.gameObject.layer = LayerMask.NameToLayer ("DisableCollision");
		}
		AudioManager.instance.playSound(AudioManager.instance.soundIceBreak);
		mAnimation.AnimationState.SetAnimation (0, "break", false);
		Destroy(gameObject,1f);

	}

	void OnCollisionEnter2D(Collision2D coll) {


	}

}
