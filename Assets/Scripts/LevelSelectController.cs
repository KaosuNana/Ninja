using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelectController : MonoBehaviour
{
    public static int selectedLevel = 1;  // Tu 1-240
    public GameObject levelObjectPrefab;
    public GameObject middlePos, leftPos, rightPos, btNext, btPrevious;
    public GameObject[] gridPages;
    public List<GameObject> levelList;
    public UISprite stageTitle;
    public static int currentPage;
    public GameObject dialogShop;

    //public List<GameObject> aryLevelList;
    //  public static int lastPageSelected;
    // Use this for initialization

    public void openInAppShop()
    {

        dialogShop.SetActive(true);
        dialogShop.GetComponent<ShopController>().clickInAppTab();
    }


    void Start()
    {
        Time.timeScale = 1f;
        levelList = new List<GameObject>();

        switch (StageSelectController.selectedWorld)
        {
            case 1:
                currentPage = GameConstant.getLastPageStage1();
                stageTitle.spriteName = "text_stage1";
                break;
            case 2:
                currentPage = GameConstant.getLastPageStage2();
                stageTitle.spriteName = "text_stage2";
                break;
            case 3:
                currentPage = GameConstant.getLastPageStage3();
                stageTitle.spriteName = "text_stage3";
                break;
            case 4:
                currentPage = GameConstant.getLastPageStage4();
                stageTitle.spriteName = "text_stage4";
                break;

        }
        for (int i = 0; i < gridPages.Length; i++)
        {
            if (i < currentPage - 1)
            {
                gridPages[i].transform.localPosition = leftPos.transform.localPosition;
            }
            if (i == currentPage - 1)
            {
                gridPages[i].transform.localPosition = middlePos.transform.localPosition;
            }
            if (i > currentPage - 1)
            {
                gridPages[i].transform.localPosition = rightPos.transform.localPosition;
            }
        }
        //   print("currentPage "+currentPage);
        //     print("gridPages.Length+1 " + gridPages.Length + 1);
        if (currentPage == 1) MyUtil.disableButton(btPrevious);
        if (currentPage == gridPages.Length) MyUtil.disableButton(btNext);


        for (int i = 0; i < 60; i++)
        {
            GameObject level = Instantiate(levelObjectPrefab, gridPages[i / 15].transform) as GameObject;
            level.GetComponent<LevelObjectScript>().setID((StageSelectController.selectedWorld - 1) * 60 + i + 1);
            levelList.Add(level);

        }

        gridPages[currentPage - 1].GetComponent<UIGrid>().Reposition();

        SetLevelStatus();


        if (GameConstant.getLastPlayedLevel() >= 0 && GameConstant.getLastPlayedLevel() <= 60)
        {
            UICamera.selectedObject = levelList[GameConstant.getLastPlayedLevel() == 0 ? 0 : GameConstant.getLastPlayedLevel() - 1];
        }        
        if (GameConstant.getLastPlayedLevel() >= 61 && GameConstant.getLastPlayedLevel() <= 120)
        {
            UICamera.selectedObject = levelList[GameConstant.getLastPlayedLevel() - 61];
        }        
        if (GameConstant.getLastPlayedLevel() >= 121 && GameConstant.getLastPlayedLevel() <= 180)
        {
            UICamera.selectedObject = levelList[GameConstant.getLastPlayedLevel() - 121];
        }        
        if (GameConstant.getLastPlayedLevel() >= 181 && GameConstant.getLastPlayedLevel() <= 240)
        {
            UICamera.selectedObject = levelList[GameConstant.getLastPlayedLevel() - 181];
        }
        
        if (GameConstant.getLastPlayedLevel() >= 16 && GameConstant.getLastPlayedLevel() <= 30)
        {
            nextPage();
        }        
        if (GameConstant.getLastPlayedLevel() >= 31 && GameConstant.getLastPlayedLevel() <= 45)
        {
            nextPage();
            nextPage();
        }        
        if (GameConstant.getLastPlayedLevel() >= 46 && GameConstant.getLastPlayedLevel() <= 60)
        {
            nextPage();
            nextPage();
            nextPage();
        }


    }


    void Update()
    {
        foreach (var t in levelList)
        {
            t.transform.GetChild(6).gameObject.SetActive(UICamera.selectedObject == t);
        }

        PageChange();
        if (Input.GetButtonDown("Cancel"))
            toStageSelect();
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
    public void nextPage()
    {
        Debug.Log("currentPage  " + currentPage);

        if (currentPage < gridPages.Length + 1)
        {
            gridPages[currentPage - 1].GetComponent<PageLevelObject>().movePage(leftPos.transform.localPosition);
            currentPage++;
            if (currentPage == gridPages.Length)
            {
                MyUtil.disableButton(btNext);
            }
            if (currentPage > 1)
            {
                //btPrevious.SetActive(true);
                MyUtil.enableButton(btPrevious);
            }

            if (currentPage > gridPages.Length)
                currentPage = gridPages.Length;
            gridPages[currentPage - 1].GetComponent<PageLevelObject>().movePage(middlePos.transform.localPosition);
            gridPages[currentPage - 1].GetComponent<UIGrid>().Reposition();

        }
        AudioManager.instance.playClickAtMainMenuSound();
    }
    public void previousPage()
    {
        Debug.Log("currentPage  " + currentPage);
        if (currentPage > 1)
        {
            gridPages[currentPage - 1].GetComponent<PageLevelObject>().movePage(rightPos.transform.localPosition);
            currentPage--;
            if (currentPage < gridPages.Length + 1)
            {
                //		btNext.SetActive(true);
                MyUtil.enableButton(btNext);
            }
            if (currentPage == 1)
            {
                //		btPrevious.SetActive(false);
                MyUtil.disableButton(btPrevious);
            }
            gridPages[currentPage - 1].GetComponent<PageLevelObject>().movePage(middlePos.transform.localPosition);
            gridPages[currentPage - 1].GetComponent<UIGrid>().Reposition();

        }
        AudioManager.instance.playClickAtMainMenuSound();
    }


    public void toStageSelect()
    {
        AudioManager.instance.playClickAtMainMenuSound();
        SceneManager.LoadScene("StageSelect");
    }


    void OnApplicationPause(bool isGamePause)
    {


        //	print ("pause "+isGamePause);
        if (isGamePause)
        {

            AudioListener.volume = 0;
        }
        else
        {
            //	focusCounter++;

            if (GameConstant.isMusicOn() == 1)
                AudioListener.volume = 1;
        }
    }

    void SetLevelStatus()
    {
        for (int i = 0; i < levelList.Count; i++)
        {
            if (i >= 0 && i <= 14)
            {
                levelList[i].GetComponent<UIKeyNavigation>().onUp = i - 5 >= 0 ? levelList[i - 5] : null;
                levelList[i].GetComponent<UIKeyNavigation>().onDown = i + 5 <= 14 ? levelList[i + 5] : null;
                levelList[i].GetComponent<UIKeyNavigation>().onLeft = levelList[i - 1 >= 0 ? i - 1 : 0];
                levelList[i].GetComponent<UIKeyNavigation>().onRight = i + 1 < 15 ? levelList[i + 1] : btNext;
            }

            if (i >= 15 && i <= 29)
            {
                levelList[i].GetComponent<UIKeyNavigation>().onUp = i - 5 >= 15 ? levelList[i - 5] : null;
                levelList[i].GetComponent<UIKeyNavigation>().onDown = i + 5 <= 29 ? levelList[i + 5] : null;
                levelList[i].GetComponent<UIKeyNavigation>().onLeft = i - 1 >= 15 ? levelList[i - 1] : btPrevious;
                levelList[i].GetComponent<UIKeyNavigation>().onRight = i + 1 < 30 ? levelList[i + 1] : btNext;
            }

            if (i >= 30 && i <= 44)
            {
                levelList[i].GetComponent<UIKeyNavigation>().onUp = i - 5 >= 30 ? levelList[i - 5] : null;
                levelList[i].GetComponent<UIKeyNavigation>().onDown = i + 5 <= 44 ? levelList[i + 5] : null;
                levelList[i].GetComponent<UIKeyNavigation>().onLeft = i - 1 >= 30 ? levelList[i - 1] : btPrevious;
                levelList[i].GetComponent<UIKeyNavigation>().onRight = i + 1 < 45 ? levelList[i + 1] : btNext;
            }

            if (i >= 45 && i <= 59)
            {
                levelList[i].GetComponent<UIKeyNavigation>().onUp = i - 5 >= 45 ? levelList[i - 5] : null;
                levelList[i].GetComponent<UIKeyNavigation>().onDown = i + 5 <= 59 ? levelList[i + 5] : null;
                levelList[i].GetComponent<UIKeyNavigation>().onLeft = i - 1 >= 45 ? levelList[i - 1] : btPrevious;
                levelList[i].GetComponent<UIKeyNavigation>().onRight = i + 1 < 60 ? levelList[i + 1] : levelList[59];
            }
        }
    }

    void PageChange()
    {
        if (UICamera.selectedObject == btNext)
        {
            nextPage();
            if (currentPage == 2)
                UICamera.selectedObject = levelList[15];
            if (currentPage == 3)
                UICamera.selectedObject = levelList[30];
            if (currentPage == 4)
                UICamera.selectedObject = levelList[45];
        }
        if (UICamera.selectedObject == btPrevious)
        {
            previousPage();
            if (currentPage == 2)
                UICamera.selectedObject = levelList[15];
            if (currentPage == 3)
                UICamera.selectedObject = levelList[30];
            if (currentPage == 1)
                UICamera.selectedObject = levelList[0];
        }
    }
}
