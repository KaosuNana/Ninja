using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IphoneXCam : MonoBehaviour
{

  
    #if UNITY_IOS
     bool deviceIsIphoneX = UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneX;
       #endif
    // Use this for initialization
    void Start()
    {
        
#if UNITY_IOS
        if (deviceIsIphoneX)
        {
            GetComponent<UIRoot>().fitWidth = false;
            GetComponent<UIRoot>().fitHeight = true;
        }
#elif UNITY_ANDROID
        float windowaspect = (float)Screen.width / (float)Screen.height;
        if (windowaspect>=2){
            GetComponent<UIRoot>().fitWidth = false;
            GetComponent<UIRoot>().fitHeight=true;
        }
#endif

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
