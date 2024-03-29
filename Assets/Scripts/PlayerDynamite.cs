using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class PlayerDynamite : MonoBehaviour {
	public GameObject explosion;
	public SkeletonAnimation mAnimation;
	public float throwForce;
	public float damage;
	public float headShotMultiplier;
	public int MAX_BOUNCE= 4;
	bool exploded =false;
	int currentBounceCount;
	// Use this for initialization
	void Start () {
		currentBounceCount=MAX_BOUNCE;
		mAnimation.AnimationState.Complete += delegate {
			destroyDynamite();
		};
	}
	public void fly(Vector2 direction){
		Debug.Log("direction  "+direction);
		GetComponent<Rigidbody2D>().velocity =  throwForce *direction.normalized;
	}
	public void explode(){
		Debug.Log("explode");
        GameObject.FindWithTag("GameController").GetComponent<GameController>().shakeCam();
		exploded=true;
		mAnimation.AnimationState.SetAnimation(0,"blowup",false);
		Instantiate(explosion,transform.position,Quaternion.identity);
		GetComponent<Rigidbody2D>().velocity=Vector2.zero;
		GetComponent<Rigidbody2D>().isKinematic=true;
		AudioManager.instance.playSound(AudioManager.instance.soundExplosion);
	}
	void destroyDynamite(){
		if (exploded){
			Destroy(gameObject);

		}
	}
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag=="wall" || coll.gameObject.tag=="ground" ||coll.gameObject.tag=="metalBox" ||coll.gameObject.tag=="woodenBar" ||coll.gameObject.tag=="spinWoodenBar"){
			if (currentBounceCount>0){
				currentBounceCount--;
				AudioManager.instance.playSound(AudioManager.instance.soundDynamiteBounce);
				if (currentBounceCount==0) explode();
			}
		}

		else  if (coll.gameObject.tag=="enemy"|| coll.gameObject.tag=="woodenBox" ||coll.gameObject.tag=="iceCube" || coll.gameObject.tag=="hostage" ||coll.gameObject.tag=="bombBox"){
			explode();
		
	
		} else if (coll.gameObject.tag == "canon"){
			coll.transform.parent.gameObject.GetComponent<Canon>().shoot();
			explode ();
		}


	}


}
