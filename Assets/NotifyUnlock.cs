using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyUnlock : MonoBehaviour {
    public enum STAGE { STAGE3, STAGE4 };
    public STAGE stage;
	// Use this for initialization
	void Start () {
        switch (stage){
            case STAGE.STAGE3:
                if (GameConstant.getStageStatus(2)==0){
                    gameObject.SetActive(true);
                } else{
                    gameObject.SetActive(false);
                }
                break;
            case STAGE.STAGE4:
                if (GameConstant.getStageStatus(3) == 0)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
