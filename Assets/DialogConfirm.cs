using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogConfirm : MonoBehaviour
{
    public UILabel textContent;
    public delegate void OnClickOK();
    public static event OnClickOK onClickOk;
    public delegate void OnClickNo();
    public static event OnClickNo onClickNo;

    public GameObject btnYes;
    public GameObject btnNo;

    public GameObject[] itemList;
    // Use this for initialization
    void Start()
    {

    }

    void OnEnable()
    {
        UICamera.selectedObject = btnYes;
    }
    // Update is called once per frame
    void Update()
    {
        btnYes.transform.GetChild(1).gameObject.SetActive(UICamera.selectedObject == btnYes);
        btnNo.transform.GetChild(1).gameObject.SetActive(UICamera.selectedObject == btnNo);
        if (UICamera.selectedObject == itemList[0] || UICamera.selectedObject == itemList[1] ||
           UICamera.selectedObject == itemList[2] || UICamera.selectedObject == itemList[3])
        {
            UICamera.selectedObject = btnYes;
        }
    }
    public void close()
    {
        gameObject.SetActive(false);
    }
    public void show(string content, int itemId)
    {
        PlayerPrefs.SetInt("CurrentItemID", itemId);
        gameObject.SetActive(true);
        //textContent.text = "" + content;
        ShowText(itemId);
    }

    void ShowText(int index)
    {
        if (index < 10)
        {
            for (int i = 0; i < 4; i++)
            {
                textContent.transform.GetChild(i).gameObject.SetActive(i == index);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                textContent.transform.GetChild(i + 4).gameObject.SetActive(i == (index - 10));
            }
        }
    }

}
