using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class Canon : MonoBehaviour {
	public GameObject canonBody,canonBarrel,canonBall,canonShootEffect;
	public Transform canonSpawnPos;

	bool isSpinRight;
	Vector3 currentDirection;
	 Vector3 rightDirection = new Vector3(0, 0, -1f);
	 Vector3 leftDirection = new Vector3(0, 0, 1f);
	public float rotateSpeed ;
	// Use this for initialization
	void Start () {
		isSpinRight=false;
		currentDirection= leftDirection;
	}
	
	// Update is called once per frame
	void Update () {
				if (isSpinRight ){
					if (canonBarrel.transform.eulerAngles.z >=350){
						canonBarrel.transform.eulerAngles=new Vector3(0,0,0);
						currentDirection=leftDirection;
						isSpinRight=false;
					}
				} else {
					if (canonBarrel.transform.eulerAngles.z >=180){
						canonBarrel.transform.eulerAngles=new Vector3(0,0,180);
						currentDirection=rightDirection;
						isSpinRight=true;
					}
				}
		canonBarrel.transform.Rotate(currentDirection * (rotateSpeed * Time.deltaTime * 100f));
	}
	GameObject shootEffect;
	public void shoot(){
        
		AudioManager.instance.playSound(AudioManager.instance.soundCannonShoot);
		GameObject canonBallObj = Instantiate(canonBall,canonSpawnPos.position,Quaternion.identity)as GameObject;
		canonBallObj.GetComponent<CanonBall>().fly((canonSpawnPos.position - canonBarrel.transform.position).normalized );
		canonShootEffect.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0,"fire",false);
        GameObject.FindWithTag("GameController").GetComponent<GameController>().shakeCam();
	}



}
