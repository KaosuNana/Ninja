using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules;
using Spine;
public class StandEnemy : MonoBehaviour {
	public int score;
	Collider2D currentImpactShuriken;
	public enum State{
		IDLE,WALK,DEAD
	}
	public enum Direction {
		LEFT,RIGHT
	}
	public State enemyState;
	public GameObject scorePos;
	SkeletonAnimation mAnimation;
	public Direction currentDirection;
	public GameObject headShotDetect,bodyShotDetect,shieldShotDetect;
	GameController gameController;
    public GameObject shieldObject;
    Spine.Bone enemyShieldBone;
	public SkeletonRagdoll2D ragdoll;
	public GameObject bloodPrefab;

    bool canAnimateIdle;
    public float idle2Time;


	// Use this for initialization
	void Start () {
        
		gameController= GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		mAnimation=GetComponent<SkeletonAnimation>();
		ragdoll = GetComponent<SkeletonRagdoll2D> ();
	//	enemyShieldBone = mAnimation.Skeleton.FindBone("Khien_placehole");
      //  Debug.Log("check bone:  " + enemyShieldBone.ToString());
//        shieldObject.transform.parent = enemyShieldBone.;
        if (enemyState== State.IDLE){
            mAnimation.AnimationState.SetAnimation(0, "Idle", true);
			InvokeRepeating("idle2Animation", idle2Time +Random.Range(0,4), idle2Time+Random.Range(0,4));
            canAnimateIdle = true;
        }
			
		else {
			mAnimation.AnimationState.SetAnimation(0,"Run", true);
		}
		if (shieldShotDetect!=null){
			disableBodyAndHeadDetect();
		}
		/*
        mAnimation.AnimationState.Complete += delegate {
			Debug.Log("Animation End");
			if (GetComponent<EnemyAttribute>().health==2){
				mAnimation.skeleton.SetSkin("khong khien");

				if (enemyState== State.IDLE)
					mAnimation.AnimationState.SetAnimation(1,"Idle", true);
				else {
					mAnimation.AnimationState.SetAnimation(1,"Run", true);
				}
				if (shieldShotDetect!=null)
					shieldShotDetect.SetActive(false);
		//		mAnimation.AnimationState.End +=null;
			}
			if (GetComponent<EnemyAttribute>().health==1){
				mAnimation.skeleton.SetSkin("default");

				if (enemyState== State.IDLE)
					mAnimation.AnimationState.SetAnimation(2,"Idle", true);
				else {
					mAnimation.AnimationState.SetAnimation(2,"Run", true);
				}
		//		mAnimation.AnimationState.End +=null;
			}




		};

*/
		if (currentDirection==Direction.RIGHT){
			transform.eulerAngles=new Vector3(0,0,0);
		} else{
			transform.eulerAngles=new Vector3(0,180,0);
		}



	}
	public void stopMoving(){
		enemyState = State.IDLE;
	}
	public void winAnimation(){
		mAnimation.AnimationState.SetAnimation(0,"Win", false);
        canAnimateIdle = false;
	}

    void idle2Animation()
    {
        if (canAnimateIdle && enemyState != State.DEAD)
        {
            mAnimation.AnimationState.SetAnimation(0, "Win", false);
            mAnimation.AnimationState.AddAnimation(0, "Idle", true, 0);
//Debug.Log("idle2Animation ");
        }

    }
    public void blowShield(Vector2 force)
    {
        shieldObject.GetComponent<BoneFollower>().enabled = false;
        shieldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        shieldObject.GetComponent<Rigidbody2D>().velocity = force/2;
        shieldObject.GetComponent<TweenAlpha>().enabled = true;

    }


	public void disableBodyAndHeadDetect(){
		headShotDetect.SetActive(false);
		bodyShotDetect.SetActive(false);
	}
	public void enableBodyAndHeadDetect(){
		headShotDetect.SetActive(true);
		bodyShotDetect.SetActive(true);
	}


	public void setCurrentCollider(Collider2D shuriken){
		currentImpactShuriken=shuriken;
	}
    public void bodyShot(float damage,Vector2 force){
		if (GetComponent<EnemyAttribute>().health==1){
			mAnimation.AnimationState.SetAnimation(3,"Dead", false);
			GetComponent<EnemyAttribute>().health-=damage;
			checkState();

			ragdoll.Apply ();
			ragdoll.RootRigidbody.velocity = force*2 ;
			foreach (Transform child in transform)
			{
				//child is your child transform
				if (child.gameObject.name == "bodyShot") {
					GameObject blood = Instantiate (bloodPrefab, new Vector2(child.transform.position.x,child.transform.position.y+0.5F), Quaternion.identity) as GameObject;
					Debug.Log ("Body Shot  ");
				}
			}

		} else {
            removeShield(damage, force);
		}

	}
    public void headShot(float damage, Vector2 force){
		if (GetComponent<EnemyAttribute>().health==1){
			mAnimation.AnimationState.SetAnimation(3,"Dead", false);
			GetComponent<EnemyAttribute>().health-=damage;
			checkState();

			ragdoll.Apply ();
			ragdoll.RootRigidbody.velocity = force*2 ;

			foreach (Transform child in transform)
			{
				//child is your child transform
				if (child.gameObject.name == "headShot") {
					GameObject blood = Instantiate (bloodPrefab, new Vector2(child.transform.position.x,child.transform.position.y), Quaternion.identity) as GameObject;
					Debug.Log ("Body Shot  ");
				}
			}



		} else {
            removeShield(damage, force);
		}

	}
	TrackEntry removeProtectionTrack,winAnimationTrack;
	void removeProtectionEvent(TrackEntry eventEntry){
//		Debug.Log("removeProtectionEvent " +GetComponent<EnemyAttribute>().health);

		if (GetComponent<EnemyAttribute>().health==2){
			mAnimation.skeleton.SetSkin("khong khien");

			if (enemyState== State.IDLE)
				mAnimation.AnimationState.SetAnimation(0,"Idle", true);
			else {
				mAnimation.AnimationState.SetAnimation(0,"Run", true);
			}
			if (shieldShotDetect!=null)
				shieldShotDetect.SetActive(false);
			
		}
		removeProtectionTrack.Complete -= removeProtectionEvent;

	}
	void winAnimationEvent(TrackEntry eventEntry){
		
	}
	public void removeProtection(float damage){
	//	shieldShotDetect.SetActive(false);
		if (GetComponent<EnemyAttribute>().health<=2) return;
		removeProtectionTrack =  mAnimation.AnimationState.SetAnimation(1,"mat khien", false);
		removeProtectionTrack.Complete += removeProtectionEvent;
		GetComponent<EnemyAttribute>().health-=damage;
	
	
	


	}
    public void removeShield(float damage, Vector2 force){
		if (GetComponent<EnemyAttribute>().health<=1) return;
        //	mAnimation.AnimationState.SetAnimation(1,"break_shield_one", false);
        blowShield(force);

		GetComponent<EnemyAttribute>().health-=damage;
		Debug.Log("removeShield " +GetComponent<EnemyAttribute>().health);
		/*
		mAnimation.AnimationState.End += delegate {
			Debug.Log("delegate " +GetComponent<EnemyAttribute>().health);
			if (GetComponent<EnemyAttribute>().health==1){
				Debug.Log("one_shield " +GetComponent<EnemyAttribute>().health);
				mAnimation.skeleton.SetSkin("default");
				mAnimation.AnimationState.SetAnimation(2, "Idle", true);
				mAnimation.AnimationState.End +=null;
			}


		};

*/

	}
	public void killedByObject(){
		if ( GetComponent<EnemyAttribute>().health>=3){
			mAnimation.AnimationState.SetAnimation(0,"mat khien", false);
		} 
		if ( GetComponent<EnemyAttribute>().health>=2){
			//mAnimation.AnimationState.SetAnimation(1,"break_shield_one", false);
		} 
		if ( GetComponent<EnemyAttribute>().health>=1){
			mAnimation.AnimationState.SetAnimation(3,"Dead", false);
		} 
		ragdoll.Apply ();
		calculateScore();
		enemyState= State.DEAD;

		disableBodyAndHeadDetect();
		disableCollision ();
		AudioManager.instance.playDeadSound();
		Destroy(gameObject,3f);

	}
	public void killedByDynamite(GameObject sourceObject,float power){
		if ( GetComponent<EnemyAttribute>().health>=3){
			mAnimation.AnimationState.SetAnimation(0,"mat khien", false);
		} 
		if ( GetComponent<EnemyAttribute>().health>=2){
			//mAnimation.AnimationState.SetAnimation(1,"break_shield_one", false);
		} 
		if ( GetComponent<EnemyAttribute>().health>=1){
			mAnimation.AnimationState.SetAnimation(3,"Dead", false);
		} 
		ragdoll.Apply ();
		ragdoll.RootRigidbody.velocity = (this.gameObject.transform.position - sourceObject.transform.position).normalized * power;

		calculateScore();
		enemyState= State.DEAD;

		disableBodyAndHeadDetect();
		disableCollision ();
		AudioManager.instance.playDeadSound();
		Destroy(gameObject,3f);

	}
	public void checkState(){
		if (GetComponent<EnemyAttribute>().health>0){


			if (enemyState== State.IDLE)
				mAnimation.AnimationState.SetAnimation(2,"Idle", true);
			else {
				mAnimation.AnimationState.SetAnimation(2,"Run", true);
			}

		} else {
			enemyState= State.DEAD;
			disableBodyAndHeadDetect();
			GetComponent<Rigidbody2D>().AddForce(currentImpactShuriken.attachedRigidbody.velocity*50);
			calculateScore();
			disableCollision ();
			AudioManager.instance.playDeadSound();
			Destroy(gameObject,3f);

		}
	}

	void calculateScore(){
		if (GameController.currentAbility== PlayerController.PlayerAbility.GOLDEN){
			gameController.addGold(60);
            gameController.showGoldOnScene(score, scorePos.transform.position);
		} else 	gameController.addGold(30);
		gameController.showScoreOnScene(score,scorePos.transform.position);
		gameController.addScore(score);
	}
	public void removeCollider(){
		headShotDetect.GetComponents<CircleCollider2D>()[0].enabled=false;
		bodyShotDetect.GetComponents<BoxCollider2D>()[0].enabled=false;
	}
	public void disableCollision(){
		gameObject.layer = LayerMask.NameToLayer ("DisableCollision");
	//	mAnimation.Skeleton.FindBone("1");
	//	mAnimation.Skeleton.FindBone("2");
		removeRigidBody ();
	}
	void removeRigidBody(){
		Destroy (GetComponent<Rigidbody2D> ());
		GetComponent<BoxCollider2D> ().enabled = false;
	}
	// Update is called once per frame
	public float movingSpeed;
	void Update () {
		if (enemyState==State.WALK)
			transform.Translate(Vector3.right*movingSpeed*Time.deltaTime);


	}


	void OnTriggerEnter2D(Collider2D other) { 

		if (other.tag=="enemyMovingRange"){

			if (currentDirection==Direction.RIGHT){
				//	Debug.Log("left");
				currentDirection=Direction.LEFT;
				transform.eulerAngles=new Vector3(0,180,0);
			} else{
				//	Debug.Log("right");
				currentDirection=Direction.RIGHT;
				transform.eulerAngles=new Vector3(0,0,0);

			}
		} 




	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("Collide Stand Enemy  "+coll.gameObject.name);
		if ( coll.gameObject.tag=="spikeWheel" ){
			if (coll.gameObject.GetComponent<Rigidbody2D>().velocity!=Vector2.zero){
				killedByObject ();
			}

		}
		if (coll.gameObject.tag=="metalBox" ||coll.gameObject.tag=="woodenBox" || coll.gameObject.tag=="woodenBar" || coll.gameObject.tag=="woodenBarWithSteel" ||  coll.gameObject.tag=="metalBall"){
			if (Mathf.Abs(coll.gameObject.GetComponent<Rigidbody2D>().velocity.x)>1){
				killedByObject ();
			} else 	if (Mathf.Abs(coll.gameObject.GetComponent<Rigidbody2D>().velocity.y)>1){
				killedByObject ();
			} 

		}

	}
}
