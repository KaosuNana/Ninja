using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageLevelObject : MonoBehaviour {
	bool isMoving;
	Vector3 targetPos;

	// Use this for initialization
	void Start () {
		isMoving=false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving){
			transform.localPosition= Vector3.Lerp(transform.localPosition,targetPos,Time.deltaTime*10);
			if (transform.localPosition==targetPos){
				isMoving=false;

			}
		}
	}
	public void movePage(Vector3 toPos){
		isMoving=true;
		targetPos=toPos;

	}
}
