using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour {
	TweenPosition moveUp;
	// Use this for initialization
	void Awake(){

	
	}
	void Start () {

		Destroy(this.gameObject,1.5f);
	}
	public void setPosition(){
		moveUp=GetComponent<TweenPosition>();
		moveUp.from= transform.position;
		moveUp.to = new Vector3(transform.position.x,transform.position.y+0.5f,0);
		moveUp.ResetToBeginning();
		moveUp.PlayForward();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
