using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {


	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	 float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.01f;
	public float decreaseFactor = 1f;

	Camera camera;
	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera> ();
		
	}


    public void ShakeCamera(){
		if (camera != null) {
            //			camera.DOShakePosition (1f,3f,3);
            shakeDuration = 2f;
		}
	}

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.position;
	}

	void Update()
	{
        if (shakeDuration > 0 )
		{
            camTransform.position = originalPos + Random.insideUnitSphere * shakeAmount * 0.4f;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
         
			shakeDuration = 0f;
            camTransform.position = originalPos;
		}
	}
}
