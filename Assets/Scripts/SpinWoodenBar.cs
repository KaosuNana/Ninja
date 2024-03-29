using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWoodenBar : MonoBehaviour {

	public Vector3 direction = new Vector3(0, 0, 1f);
	public float speed = 1f;
	// Use this for initialization
	void Start () {
		
	}


	void Update () {
		transform.Rotate(direction * (speed * Time.deltaTime * 100f));
	}
}
