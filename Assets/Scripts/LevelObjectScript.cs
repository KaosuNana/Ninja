using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelObjectScript : MonoBehaviour
{
    int _id = 1;
    int currentStatus;
    public GameObject imgLock, img1Star, img2Star, img3Star, imgLastPlayed;
    public UILabel idText;
    // Use this for initialization
    void Start()
    {
        currentStatus = GameConstant.getLevelStatus(_id);
        switch (currentStatus)
        {

            case -1:
                imgLock.SetActive(true);
                GetComponent<BoxCollider>().enabled = false;
                break;

            case 0:

                break;

            case 1:
                img1Star.SetActive(true);
                break;

            case 2:
                img2Star.SetActive(true);
                break;

            case 3:
                img3Star.SetActive(true);
                break;

        }

        if (_id == GameConstant.getLastPlayedLevel())
        {
            imgLastPlayed.SetActive(false);
        }
        else
        {
            imgLastPlayed.SetActive(false);
        }


    }
    public void setID(int id)
    {
        _id = id;

        int tempID = (id - 1) % 60;
        if (tempID + 1 > 9)
            idText.text = "" + (tempID + 1);
        else idText.text = "0" + (tempID + 1);

    }
    public int getID()
    {
        return _id;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void clickLevel()
    {
        Debug.Log("_id  " + _id);
        AudioManager.instance.playClickAtMainMenuSound();
        LevelSelectController.selectedLevel = _id;

        if (GameConstant.getLastPlayedLevel() < _id)
            GameConstant.setLastPlayedLevel(_id);

        SceneManager.LoadScene("GamePlay");
        switch (StageSelectController.selectedWorld)
        {
            case 1:
                GameConstant.setLastPageStage1(LevelSelectController.currentPage);
                break;
            case 2:
                GameConstant.setLastPageStage2(LevelSelectController.currentPage);
                break;
            case 3:
                GameConstant.setLastPageStage3(LevelSelectController.currentPage);
                break;
            case 4:
                GameConstant.setLastPageStage4(LevelSelectController.currentPage);
                break;

        }
    }
}
