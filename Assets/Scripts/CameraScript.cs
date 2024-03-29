using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class CameraScript : MonoBehaviour {
    Vector3 originalPos;
    Camera camera;
    float shakeDuration;
    public float shakeAmount;
	// Use this for initialization
	void Start () {
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 1280.0f / 720.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		 camera = GetComponent<Camera>();

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
        originalPos = transform.position;
	

	}

	void resizeCam(object sender, EventArgs args){
		GetComponent<Camera>().pixelRect= new Rect(GetComponent<Camera>().pixelRect.x,GetComponent<Camera>().pixelRect.y-CalculateBannerHeight(),GetComponent<Camera>().pixelRect.width,GetComponent<Camera>().pixelRect.height);
		Debug.Log(""+CalculateBannerHeight());
	}
	int CalculateBannerHeight() {
		if (Screen.height <= 400*Mathf.RoundToInt(Screen.dpi/160)) {
			return 32*Mathf.RoundToInt(Screen.dpi/160);
		} else if (Screen.height <= 720*Mathf.RoundToInt(Screen.dpi/160)) {
			return 50*Mathf.RoundToInt(Screen.dpi/160);
		} else {
			return 90*Mathf.RoundToInt(Screen.dpi/160);
		}
	}

    public void shakeCam(){
        shakeDuration = 0.2f;
    }
	// Update is called once per frame
	void Update () {
        if (shakeDuration > 0)
        {
            transform.position = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.unscaledDeltaTime ;
        }
        if (shakeDuration<0)
        {

            shakeDuration = 0f;
            transform.position = originalPos;
        }
	}
}
