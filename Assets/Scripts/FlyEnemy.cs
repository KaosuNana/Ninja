using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules;
public class FlyEnemy : MonoBehaviour {
	public int score;
	Collider2D currentImpactShuriken;
	public SkeletonRagdoll2D ragdoll;
	public enum Direction {
		LEFT,RIGHT,UP,DOWN
	}
	public enum State{
		FLYING,DEAD,IDLE
	}
	public Direction currentDirection;
	public State currentState;
	public float movingSpeed;
	SkeletonAnimation mAnimation;
	GameController gameController;
    public bool isStay=false;
    public bool isVertical = false;
	public GameObject headShotDetect,bodyShotDetect;
	public GameObject scorePos;
    public GameObject bloodPrefab;
    bool allowToChangeDirection = true;
	bool canAnimateIdle;
	public float idle2Time;
	// Use this for initialization
	void Start () {
		gameController= GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		mAnimation=GetComponent<SkeletonAnimation>();
		ragdoll = GetComponent<SkeletonRagdoll2D> ();
		if (currentDirection==Direction.RIGHT){
			transform.eulerAngles=new Vector3(0,0,0);
		} else if (currentDirection == Direction.LEFT)
         {
			transform.eulerAngles=new Vector3(0,180,0);
		}

		mAnimation.AnimationState.SetAnimation(0,"Fly", true);
		if (isStay){
			mAnimation.AnimationState.SetAnimation(0,"Idle", true);
			InvokeRepeating("idle2Animation", idle2Time +Random.Range(0,4), idle2Time+Random.Range(0,4));
			canAnimateIdle = true;
		}


	}
	void idle2Animation()
	{
		if (canAnimateIdle && currentState != State.DEAD)
		{
			mAnimation.AnimationState.SetAnimation(0, "Win", false);
			mAnimation.AnimationState.AddAnimation(0, "Idle", true, 0);
		//	Debug.Log("idle2Animation ");
		}

	}

	public void setCurrentCollider(Collider2D shuriken){
		currentImpactShuriken=shuriken;
	}
	public void bodyShot(float damage,Vector2 force){
		mAnimation.AnimationState.SetAnimation(0,"Dead", false);
		ragdoll.Apply ();
		ragdoll.RootRigidbody.velocity = force*2 ;

        foreach (Transform child in transform)
        {
            //child is your child transform
            if (child.gameObject.name == "bodyShot")
            {
                GameObject blood = Instantiate(bloodPrefab, new Vector2(child.transform.position.x, child.transform.position.y + 0.5F), Quaternion.identity) as GameObject;
                Debug.Log("Body Shot  ");
            }
        }



		GetComponent<EnemyAttribute>().health-=damage;
		checkState();
	}
	public void headShot(float damage,Vector2 force){
		mAnimation.AnimationState.SetAnimation(0,"Dead", false);
		ragdoll.Apply ();
		ragdoll.RootRigidbody.velocity = force*2 ;
        foreach (Transform child in transform)
        {
            //child is your child transform
            if (child.gameObject.name == "headShot")
            {
                GameObject blood = Instantiate(bloodPrefab, new Vector2(child.transform.position.x, child.transform.position.y), Quaternion.identity) as GameObject;
                Debug.Log("Body Shot  ");
            }
        }



		GetComponent<EnemyAttribute>().health-=damage;
		checkState();
	}
	public void disableBodyAndHeadDetect(){
		headShotDetect.SetActive(false);
		bodyShotDetect.SetActive(false);
	}
	public void enableBodyAndHeadDetect(){
		headShotDetect.SetActive(true);
		bodyShotDetect.SetActive(true);
	}

	public void removeCollider(){
		headShotDetect.GetComponents<CircleCollider2D>()[0].enabled=false;
		bodyShotDetect.GetComponents<BoxCollider2D>()[0].enabled=false;
	}
	public void disableCollision(){
		gameObject.layer = LayerMask.NameToLayer ("DisableCollision");
	}
	public void checkState(){
		if (GetComponent<EnemyAttribute>().health>0){
			currentState=State.FLYING;
			mAnimation.AnimationState.AddAnimation(0, "Fly", true, 0f);

		} else {
			currentState=State.DEAD;
			headShotDetect.SetActive(false);
			AudioManager.instance.playDeadSound();
			bodyShotDetect.SetActive(false);
			calculateScore();
			disableCollision ();
			GetComponent<Rigidbody2D>().gravityScale=1f;
			GetComponent<Rigidbody2D>().AddForce(currentImpactShuriken.attachedRigidbody.velocity/1000);
			Destroy(gameObject,3f);
		}
	}
	public void killedByObject(){
		ragdoll.Apply ();
		currentState=State.DEAD;
		mAnimation.AnimationState.SetAnimation(0,"Dead", false);
		calculateScore();
		AudioManager.instance.playDeadSound();
		disableBodyAndHeadDetect();
		disableCollision ();
		GetComponent<Rigidbody2D>().gravityScale=1f;
		Destroy(gameObject,3f);

	}
	public void killedByDynamite(GameObject sourceObject,float power){


		ragdoll.Apply ();
		ragdoll.RootRigidbody.velocity = (this.gameObject.transform.position - sourceObject.transform.position).normalized * power;

		currentState=State.DEAD;
		mAnimation.AnimationState.SetAnimation(0,"Dead", false);
		calculateScore();
		AudioManager.instance.playDeadSound();
		disableBodyAndHeadDetect();
		disableCollision ();
		GetComponent<Rigidbody2D>().gravityScale=1f;
		Destroy(gameObject,3f);

	}
	void calculateScore(){
		if (GameController.currentAbility== PlayerController.PlayerAbility.GOLDEN){
			gameController.addGold(60);
            gameController.showGoldOnScene(score, scorePos.transform.position);
		}else 	gameController.addGold(30);
		gameController.showScoreOnScene(score,scorePos.transform.position);
		gameController.addScore(score);
	}
	// Update is called once per frame
	void Update () {
        if (currentState==State.FLYING && !isStay && !isVertical)
			transform.Translate(Vector3.right*movingSpeed*Time.deltaTime);
        else {
            if (currentDirection== Direction.UP){
                transform.Translate(Vector3.up * movingSpeed * Time.deltaTime);
            } 
             else if (currentDirection == Direction.DOWN){
                transform.Translate(Vector3.down * movingSpeed * Time.deltaTime);
            }
        }

	}

	void OnTriggerExit2D(Collider2D other)
	{

        //	if (other.tag=="playerShuriken"){
        //		Debug.Log("Fly Enemy  OnTriggerExit2D" +other.name);
        //	other.gameObject.layer= LayerMask.NameToLayer("Default");
        //	}
        allowToChangeDirection = true;
	}
	void OnTriggerStay2D(Collider2D other)
	{
		
	}
	void OnTriggerEnter2D(Collider2D other) { 

		if (other.tag=="enemyMovingRange"){

            if (currentDirection==Direction.RIGHT && allowToChangeDirection){
			//	Debug.Log("left");
				currentDirection=Direction.LEFT;
				transform.eulerAngles=new Vector3(0,180,0);
			} else 
                if (currentDirection == Direction.LEFT && allowToChangeDirection) {
			//	Debug.Log("right");
				currentDirection=Direction.RIGHT;
				transform.eulerAngles=new Vector3(0,0,0);
			}

            if (currentDirection == Direction.UP && allowToChangeDirection)
            {
                Debug.Log("down");
                currentDirection = Direction.DOWN;allowToChangeDirection = false;
              
            } else  if (currentDirection == Direction.DOWN && allowToChangeDirection)
            {
                Debug.Log("up");
                currentDirection = Direction.UP;allowToChangeDirection = false;

            }



		} 



	}
	public void stopMoving(){
		currentState = State.IDLE;
	}
	public void winAnimation(){
		mAnimation.AnimationState.SetAnimation(0,"Win", false);
	}
	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("Collide Fly Enemy  "+coll.gameObject.name);
		if (coll.gameObject.tag=="metalBox" ||coll.gameObject.tag=="woodenBox" || coll.gameObject.tag=="woodenBar" || coll.gameObject.tag=="woodenBarWithSteel" || coll.gameObject.tag=="spikeWheel" || coll.gameObject.tag=="metalBall"){
			if (coll.gameObject.GetComponent<Rigidbody2D>().velocity!=Vector2.zero){
				killedByObject ();
			}
		}

	}
}
