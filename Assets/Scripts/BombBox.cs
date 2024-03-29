using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class BombBox : MonoBehaviour {
	public GameObject explosionEffect;
	SkeletonAnimation mAnimation;
    bool allowBlow;
    public float delayedTime =0.1f;
	// Use this for initialization
	void Start () {
		mAnimation=GetComponent<SkeletonAnimation>();
        allowBlow = false;
	}
	public void blow(){
        // Invoke("delayedBlow", 0.1f);
        allowBlow = true;
	}

    void delayedBlow(){
      //  CancelInvoke("delayedBlow");
        AudioManager.instance.playSound(AudioManager.instance.soundBombBox);
        mAnimation.AnimationState.SetAnimation(0, "blowup2", false);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        mAnimation.AnimationState.Complete += delegate {
            Destroy(gameObject);
        };
        GameObject.FindWithTag("GameController").GetComponent<GameController>().shakeCam();
        //gameObject.SetActive(false);
        disableCollision();
    }
	public void disableCollision(){
		gameObject.layer = LayerMask.NameToLayer ("DisableCollision");
	}
	// Update is called once per frame
	void Update () {
        if (allowBlow){
            delayedTime -= Time.deltaTime;
            if (delayedTime<=0){
                allowBlow = false;
                delayedTime = 0.1f;
                delayedBlow();
              
            }
        }

	}

	void OnCollisionEnter2D(Collision2D coll) {
		
	}


}
