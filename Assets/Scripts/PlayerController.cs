using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class PlayerController : MonoBehaviour {
	public enum PlayerAbility {
		NORMAL,GHOST, POWERFUL, BOMB, GOLDEN
	}
	public PlayerAbility playerAbility= PlayerAbility.NORMAL;
	SkeletonAnimation mAnimation;
	public float maxIdleTime;
	float idleTime;
	public bool canAnimateIdle;
	// Use this for initialization
	void Start () {
		idleTime = 0;
		canAnimateIdle = true;
		mAnimation=GetComponent<SkeletonAnimation>();

	

	}
	public void updateSkin(){
		mAnimation=GetComponent<SkeletonAnimation>();
		if (GameConstant.getStatusGolden()==1){
			playerAbility= PlayerAbility.GOLDEN;
			mAnimation.skeleton.SetSkin("golden");
			mAnimation.skeleton.SetSlotsToSetupPose();
			mAnimation.AnimationState.Apply(mAnimation.skeleton);

		} else if (GameConstant.getStatusPowerful()==1){
			playerAbility= PlayerAbility.POWERFUL;

			mAnimation.skeleton.SetSkin("purple");
			mAnimation.skeleton.SetSlotsToSetupPose();
			mAnimation.AnimationState.Apply(mAnimation.skeleton);
		}else if (GameConstant.getStatusGhost()==1){
			playerAbility= PlayerAbility.GHOST;

			mAnimation.skeleton.SetSkin("green");
			mAnimation.skeleton.SetSlotsToSetupPose();
			mAnimation.AnimationState.Apply(mAnimation.skeleton);
		}else if (GameConstant.getStatusBomb()==1){
			playerAbility= PlayerAbility.BOMB;

			mAnimation.skeleton.SetSkin("red");
			mAnimation.skeleton.SetSlotsToSetupPose();
			mAnimation.AnimationState.Apply(mAnimation.skeleton);
		} else {
			playerAbility= PlayerAbility.NORMAL;

			mAnimation.skeleton.SetSkin("basic");
			mAnimation.skeleton.SetSlotsToSetupPose();
			mAnimation.AnimationState.Apply(mAnimation.skeleton);
		
		}



	}
	// Update is called once per frame
	void Update () {
		idleTime += Time.deltaTime;
		if (idleTime > maxIdleTime) {
			idleTime = 0;
			if (canAnimateIdle)
				idle2Animation ();
		}
	}

	public void winAnimation(){
		mAnimation.AnimationState.SetAnimation(0,"win",false);
	}

	public void loseAnimation(){
		mAnimation.AnimationState.SetAnimation(0,"lose",true);
	}


	public void throwAnimation(){
		mAnimation.AnimationState.SetAnimation(0,"attack",false);
		mAnimation.AnimationState.AddAnimation(0,"idle_normal",true,0);
	}

	void idleAnimation(){
		
	}
		
	void idle2Animation(){
		mAnimation.AnimationState.SetAnimation(0,"idle_normal2",false);
		mAnimation.AnimationState.AddAnimation(0,"idle_normal",true,0);
	}


	public void faceLeft(){
		idleTime = 0;
		gameObject.transform.rotation = Quaternion.Euler (new Vector3 (0,180,0));
	}
	public void faceRight(){
		idleTime = 0;
		gameObject.transform.rotation = Quaternion.Euler (new Vector3 (0,0,0));
	}
}

