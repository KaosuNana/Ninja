using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabPlayerPanel : MonoBehaviour
{

    public GameObject btnTabItem;
    public GameObject btnTabPlayer;
    public GameObject[] unlockedBtn;
    public GameObject[] lockedBtn;
    public GameObject[] selectedBtn;

    public void UpdateNavigation()
    {
        btnTabPlayer.GetComponent<UIKeyNavigation>().constraint = UIKeyNavigation.Constraint.Explicit;
        btnTabPlayer.GetComponent<UIKeyNavigation>().onLeft = btnTabItem;
        if (lockedBtn[0].activeSelf)
            btnTabPlayer.GetComponent<UIKeyNavigation>().onDown = lockedBtn[0];
        else if(unlockedBtn[0].activeSelf)
            btnTabPlayer.GetComponent<UIKeyNavigation>().onDown = unlockedBtn[0];
        else if (selectedBtn[0].activeSelf)
            btnTabPlayer.GetComponent<UIKeyNavigation>().onDown = selectedBtn[0];
        //if (GameConstant.getStatusGhost() == -1) //0Î´¹ºÂò
        //{
        //    lockedBtn[1].GetComponent<UIKeyNavigation>().onLeft = lockedBtn[0];
        //}        
        //if (GameConstant.getStatusPowerful() == -1) //1Î´¹ºÂò
        //{

        //}        
        //if (GameConstant.getStatusBomb() == -1) //2Î´¹ºÂò
        //{

        //}        
        //if (GameConstant.getStatusGolden() == -1) //3Î´¹ºÂò
        //{

        //}

        for (int i = 0; i < unlockedBtn.Length; i++)
        {
            if (i == 0)
            {
                unlockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = null;
                lockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = null;
                selectedBtn[i].GetComponent<UIKeyNavigation>().onLeft = null;

                if (lockedBtn[i + 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onRight = lockedBtn[i + 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onRight = lockedBtn[i + 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onRight = lockedBtn[i + 1];
                }else if (unlockedBtn[i+1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onRight = unlockedBtn[i + 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onRight = unlockedBtn[i + 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onRight = unlockedBtn[i + 1];
                }else if (selectedBtn[i + 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onRight = selectedBtn[i + 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onRight = selectedBtn[i + 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onRight = selectedBtn[i + 1];
                }

                unlockedBtn[i].GetComponent<UIKeyNavigation>().onUp = btnTabPlayer;
                lockedBtn[i].GetComponent<UIKeyNavigation>().onUp = btnTabPlayer;
                selectedBtn[i].GetComponent<UIKeyNavigation>().onUp = btnTabPlayer;
            }
            else if (i == 3)
            {
                unlockedBtn[i].GetComponent<UIKeyNavigation>().onRight = null;
                lockedBtn[i].GetComponent<UIKeyNavigation>().onRight = null;
                selectedBtn[i].GetComponent<UIKeyNavigation>().onRight = null;

                if (lockedBtn[i - 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = lockedBtn[i - 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = lockedBtn[i - 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onLeft = lockedBtn[i - 1];
                }
                else if (unlockedBtn[i - 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = unlockedBtn[i - 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = unlockedBtn[i - 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onLeft = unlockedBtn[i - 1];
                }
                else if (selectedBtn[i - 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = selectedBtn[i - 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = selectedBtn[i - 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onLeft = selectedBtn[i - 1];
                }

                unlockedBtn[i].GetComponent<UIKeyNavigation>().onUp = btnTabPlayer;
                lockedBtn[i].GetComponent<UIKeyNavigation>().onUp = btnTabPlayer;
                selectedBtn[i].GetComponent<UIKeyNavigation>().onUp = btnTabPlayer;
            }
            else
            {
                if (lockedBtn[i - 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = lockedBtn[i - 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = lockedBtn[i - 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onLeft = lockedBtn[i - 1];
                }
                else if (unlockedBtn[i - 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = unlockedBtn[i - 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = unlockedBtn[i - 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onLeft = unlockedBtn[i - 1];
                }
                else if (selectedBtn[i - 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = selectedBtn[i - 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onLeft = selectedBtn[i - 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onLeft = selectedBtn[i - 1];
                }

                if (lockedBtn[i + 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onRight = lockedBtn[i + 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onRight = lockedBtn[i + 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onRight = lockedBtn[i + 1];
                }
                else if (unlockedBtn[i + 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onRight = unlockedBtn[i + 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onRight = unlockedBtn[i + 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onRight = unlockedBtn[i + 1];
                }
                else if (selectedBtn[i + 1].activeSelf)
                {
                    unlockedBtn[i].GetComponent<UIKeyNavigation>().onRight = selectedBtn[i + 1];
                    lockedBtn[i].GetComponent<UIKeyNavigation>().onRight = selectedBtn[i + 1];
                    selectedBtn[i].GetComponent<UIKeyNavigation>().onRight = selectedBtn[i + 1];
                }

                unlockedBtn[i].GetComponent<UIKeyNavigation>().onUp = btnTabPlayer;
                lockedBtn[i].GetComponent<UIKeyNavigation>().onUp = btnTabPlayer;
                selectedBtn[i].GetComponent<UIKeyNavigation>().onUp = btnTabPlayer;
            }
        }
    }
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            unlockedBtn[i].transform.GetChild(1).gameObject.SetActive(UICamera.selectedObject == unlockedBtn[i]);
            lockedBtn[i].transform.GetChild(2).gameObject.SetActive(UICamera.selectedObject == lockedBtn[i]);
            selectedBtn[i].transform.GetChild(1).gameObject.SetActive(UICamera.selectedObject == selectedBtn[i]);
        }
    }
}
