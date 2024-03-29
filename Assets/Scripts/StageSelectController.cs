using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageSelectController : MonoBehaviour {
	public GameObject page1,page2,btNext,btPrevious;
	public GameObject[] stageList,countStarObject;
	public Transform leftPos,rightPos,middlePos;
	int currentPage=1;
	public static int selectedWorld=1;
	public GameObject dialogShop;
	// Use this for initialization




	public void openInAppShop(){
		dialogShop.SetActive(true);
		dialogShop.GetComponent<ShopController> ().clickInAppTab ();
	}


	void Start () {
		Time.timeScale=1f;
     
		if (currentPage==1){
			//btPrevious.SetActive(false);
            MyUtil.disableButton(btPrevious);
         
		} else {
            //	btNext.SetActive(true);
            MyUtil.enableButton(btNext);
		}
      //  GameConstant.setStageStatus(2, 0);
     //  GameConstant.setStageStatus(3, 0);
    //   GameConstant.setStageStatus(4, 0);

		for (int i=0;i<stageList.Length;i++){
		
			if (GameConstant.getStageStatus(i+1) == 0){
				stageList[i].transform.Find("lock").gameObject.SetActive(false);
                countStarObject[i].SetActive(true);
                switch (i){
                    case 0:
                        countStarObject[i].GetComponent<UILabel>().text = "" + GameConstant.countStarStage1()+"/180";
                        break;
                    case 1:
                        countStarObject[i].GetComponent<UILabel>().text = "" + GameConstant.countStarStage2()+ "/180";
                        break;
                    case 2:
                        countStarObject[i].GetComponent<UILabel>().text = "" + GameConstant.countStarStage3()+ "/180";
                        break;
                    case 3:
                        countStarObject[i].GetComponent<UILabel>().text = "" + GameConstant.countStarStage4()+ "/180";
                        break;

                }

				stageList[i].GetComponent<BoxCollider>().enabled=true;
			} else {
				stageList[i].transform.Find("lock").gameObject.SetActive(true);		
				stageList[i].GetComponent<BoxCollider>().enabled=false;
			}
		}

       
    //    for (int i = 0 ;i< 240;i++){
      //      GameConstant.setLevelStatus(i + 1, 3);
    //////   }

    //   GameConstant.addGold(9999999);

       // GameConstant.addGold(9999999);

        if (GameConstant.isRemoveAd() == 0)
        {
       

        }

    }
   
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 4; i++)
        {
			stageList[i].transform.GetChild(3).gameObject.SetActive(UICamera.selectedObject == stageList[i]);

        }
        if (Input.GetButtonDown("Cancel"))
			back();
	}

	public void nextPage(){
		
		currentPage++;
//		btPrevious.SetActive(true);
        MyUtil.enableButton(btPrevious);
	//	btNext.SetActive(false);
        MyUtil.disableButton(btNext);
		page1.GetComponent<PageLevelObject>().movePage(leftPos.localPosition);
		page2.GetComponent<PageLevelObject>().movePage(middlePos.localPosition);
		AudioManager.instance.playClickAtMainMenuSound();
	}

	public void previousPage(){
		currentPage--;
	//	btPrevious.SetActive(false);
        MyUtil.disableButton(btPrevious);
	//	btNext.SetActive(true);
        MyUtil. enableButton(btNext);
		page1.GetComponent<PageLevelObject>().movePage(middlePos.localPosition);
		page2.GetComponent<PageLevelObject>().movePage(rightPos.localPosition);
		AudioManager.instance.playClickAtMainMenuSound();
	
	}

	public void back(){
		AudioManager.instance.playClickAtMainMenuSound();
		SceneManager.LoadScene("MainMenu");
	}
	public void selectStage1(){
		AudioManager.instance.playClickAtMainMenuSound();
		selectedWorld=1;
		SceneManager.LoadScene("LevelSelect");
	}
	public void selectStage2(){
		AudioManager.instance.playClickAtMainMenuSound();
		selectedWorld=2;
		SceneManager.LoadScene("LevelSelect");
	}
	public void selectStage3(){
		AudioManager.instance.playClickAtMainMenuSound();
		selectedWorld=3;
		SceneManager.LoadScene("LevelSelect");
	}
	public void selectStage4(){
		AudioManager.instance.playClickAtMainMenuSound();
		selectedWorld=4;
		SceneManager.LoadScene("LevelSelect");
	}
    void OnEnable()
    {



    }

    void OnDestroy()
    {



    }
    void onRewardGift()
    {

    }
    void onRewardGiftLoadFailed()
    {

    }

	void OnApplicationPause (bool isGamePause)
	{


		//	print ("pause "+isGamePause);
		if (isGamePause) {
			
			AudioListener.volume=0;
		} else {

			if (GameConstant.isMusicOn ()==1)
				AudioListener.volume = 1;
		}
	}

}
