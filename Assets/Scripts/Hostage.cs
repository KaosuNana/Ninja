using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class Hostage : MonoBehaviour {
	SkeletonAnimation mAnimation;
	GameController gameController;
	public GameObject scoreObject;
	public int score;
	bool canAnimateIdle;
	public float idle2Time;
	void Start () {
		canAnimateIdle=true;
		gameController= GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		mAnimation=GetComponent<SkeletonAnimation>();
        mAnimation.AnimationState.Complete += delegate {
            if (mAnimation.AnimationName == "win")
            {
                Destroy(gameObject);
			}
        };
		InvokeRepeating ("idle2Animation",idle2Time,idle2Time);
      
	}
	

	void Update () {
		
	}
	void idleAnimation(){
		
	}
	void idle2Animation(){
		if (canAnimateIdle) {
			mAnimation.AnimationState.SetAnimation(0,"idle2",false);
			mAnimation.AnimationState.AddAnimation(0,"idle",true,0);
		}

	}

	public void winAnimation(){
		AudioManager.instance.playSound(AudioManager.instance.soundHostageRescued);
		gameObject.tag="Untagged";
		GetComponent<BoxCollider2D>().enabled=false;
		mAnimation.AnimationState.SetAnimation(0,"win",false);
		canAnimateIdle=false;
		scoreObject.SetActive(true);
		gameController.addScore(score);
		gameController.checkWin();

	}

	public void loseAnimation(){
		canAnimateIdle = false;
		mAnimation.AnimationState.SetAnimation(0,"lose",false);
	}


}
