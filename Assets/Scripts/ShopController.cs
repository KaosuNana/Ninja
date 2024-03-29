using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public enum SELECTED_SHOP_ITEM_TYPE
    {
        CHAR1, CHAR2, CHAR3, CHAR4, DART, TRIPLEDART, SUPERDART, BOMB, GOLD, REMOVEAD, COMBO1, COMBO2, COMBO3, COMBO4
    }
    public static SELECTED_SHOP_ITEM_TYPE selectedItem;
    public static GameObject selectedObject;
    public GameObject countStar;
    public GameController gameController;
    public GameObject btPlayer, btInApp, btItem;
    public GameObject btnBack;
    public GameObject btnLock1;
    public GameObject tabPlayer, tabInApp, tabItem;
    public GameObject dialogConfirm, dialogNotify, gridInApp;
    public GameObject dialogNotifyOK;
    public int PRICE_DART = 490;
    public int PRICE_TRIPLE_DART = 790;
    public int PRICE_SUPER_DART = 890;
    public int PRICE_BOMB = 990;
    int currentTabIndex = 1;
    public GameObject[] tabItemList;
    public GameObject[] tabItemBtnList;
    public GameObject[] ninjaItemList;
    public GameObject[] ninjaItemBtnList;
    public GameObject[] ninjaUnlockedItemBtnList;
    public GameObject[] ninjaSelItemBtnList;
    public GameObject shopBtn;
    public delegate void OnUpdateGold();
    public static event OnUpdateGold onUpdateGold;
    public UITabPlayerPanel uiTabPlayerPanel;

    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        updateGoldAmount();
        //  updateNinjaStatus();


    }

    void OnEnable()
    {
        //UICamera.selectedObject = null;
        //UICamera.selectedObjects(btnBack);
        UICamera.selectedObject = btnLock1;

    }
    public void updateNinjaStatus()
    {

        for (int i = 0; i < ninjaItemList.Length; i++)
        {
            ninjaItemList[i].GetComponent<NinjaItem>().updateStatus();
        }
    }
    public void updateGoldAmount()
    {
        if (this.gameObject.activeSelf)
            onUpdateGold();
        countStar.GetComponent<UILabel>().text = string.Format("{0:#,###0}", GameConstant.getGold());


    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log($"CucBtn: {UICamera.selectedObject}");
        if (UICamera.selectedObject.name == "btShop" && this.gameObject.activeSelf)
        {
            UICamera.selectedObject = btnLock1;
        }

        if (UICamera.selectedObject == btItem)
        {
            clickItemTab();
        }
        if (UICamera.selectedObject == btPlayer)
        {
            clickPlayerTab();
        }
        btnBack.transform.GetChild(1).gameObject.SetActive(UICamera.selectedObject == btnBack);
        if (tabItem.activeSelf)
        {
            foreach (var t in tabItemList)
            {
                t.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(UICamera.selectedObject == t.transform.GetChild(3).gameObject);
            }
        }
        if (tabPlayer.activeSelf)
        {
            foreach (var t in ninjaItemList)
            {
                t.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(UICamera.selectedObject == t.transform.GetChild(4).gameObject);
            }
        }
        btItem.transform.GetChild(2).gameObject.SetActive(UICamera.selectedObject == btItem);
        btPlayer.transform.GetChild(2).gameObject.SetActive(UICamera.selectedObject == btPlayer);
    }
    public void showNotify()
    {
        dialogNotify.SetActive(true);
        UICamera.selectedObject = dialogNotifyOK;
    }
    public void closeNotify()
    {
        dialogNotify.SetActive(false);
        SetItemStatus();
    }
    public void clickYes()
    {
        switch (selectedItem)
        {
            case SELECTED_SHOP_ITEM_TYPE.CHAR1://3
                selectedObject.GetComponent<NinjaItem>().confirmPurchase();
                break;
            case SELECTED_SHOP_ITEM_TYPE.CHAR2://2
                selectedObject.GetComponent<NinjaItem>().confirmPurchase();
                break;
            case SELECTED_SHOP_ITEM_TYPE.CHAR3://4
                selectedObject.GetComponent<NinjaItem>().confirmPurchase();
                break;
            case SELECTED_SHOP_ITEM_TYPE.CHAR4://1
                selectedObject.GetComponent<NinjaItem>().confirmPurchase();
                break;
            case SELECTED_SHOP_ITEM_TYPE.DART:
                selectedObject.GetComponent<ItemGold>().confirmPurchase();
                break;
            case SELECTED_SHOP_ITEM_TYPE.TRIPLEDART:
                selectedObject.GetComponent<ItemGold>().confirmPurchase();
                break;
            case SELECTED_SHOP_ITEM_TYPE.SUPERDART:
                selectedObject.GetComponent<ItemGold>().confirmPurchase();
                break;
            case SELECTED_SHOP_ITEM_TYPE.BOMB:
                selectedObject.GetComponent<ItemGold>().confirmPurchase();
                break;
            case SELECTED_SHOP_ITEM_TYPE.GOLD:

                break;
            case SELECTED_SHOP_ITEM_TYPE.REMOVEAD:

                break;
            case SELECTED_SHOP_ITEM_TYPE.COMBO1:

                break;
            case SELECTED_SHOP_ITEM_TYPE.COMBO2:

                break;
            case SELECTED_SHOP_ITEM_TYPE.COMBO3:

                break;
            case SELECTED_SHOP_ITEM_TYPE.COMBO4:

                break;

        }
        dialogConfirm.SetActive(false);
        
    }
    public void clickNo()
    {
        dialogConfirm.SetActive(false);
        SetItemStatus();
    }

    public void SetItemStatus()
    {
        if (PlayerPrefs.GetInt("CurrentItemID") == 0)
            UICamera.selectedObject = tabItemBtnList[0];
        if (PlayerPrefs.GetInt("CurrentItemID") == 1)
            UICamera.selectedObject = tabItemBtnList[1];
        if (PlayerPrefs.GetInt("CurrentItemID") == 2)
            UICamera.selectedObject = tabItemBtnList[2];
        if (PlayerPrefs.GetInt("CurrentItemID") == 3)
            UICamera.selectedObject = tabItemBtnList[3];        
        if (PlayerPrefs.GetInt("CurrentItemID") == 10 &&
            GameConstant.getStatusGhost() == -1)
            UICamera.selectedObject = ninjaItemBtnList[0];
        if (PlayerPrefs.GetInt("CurrentItemID") == 11 &&
            GameConstant.getStatusPowerful() == -1)
            UICamera.selectedObject = ninjaItemBtnList[1];
        if (PlayerPrefs.GetInt("CurrentItemID") == 12 &&
            GameConstant.getStatusBomb() == -1)
            UICamera.selectedObject = ninjaItemBtnList[2];
        if (PlayerPrefs.GetInt("CurrentItemID") == 13 &&
            GameConstant.getStatusGolden() == -1)
            UICamera.selectedObject = ninjaItemBtnList[3];        
        
        if (PlayerPrefs.GetInt("CurrentItemID") == 10 &&
            GameConstant.getStatusGhost() == 0)
            UICamera.selectedObject = ninjaUnlockedItemBtnList[0];
        if (PlayerPrefs.GetInt("CurrentItemID") == 11 &&
            GameConstant.getStatusPowerful() == 0)
            UICamera.selectedObject = ninjaUnlockedItemBtnList[1];
        if (PlayerPrefs.GetInt("CurrentItemID") == 12 &&
            GameConstant.getStatusBomb() == 0)
            UICamera.selectedObject = ninjaUnlockedItemBtnList[2];
        if (PlayerPrefs.GetInt("CurrentItemID") == 13 &&
            GameConstant.getStatusGolden() == 0)
            UICamera.selectedObject = ninjaUnlockedItemBtnList[3];        
        
        if (PlayerPrefs.GetInt("CurrentItemID") == 10 &&
            GameConstant.getStatusGhost() == 1)
            UICamera.selectedObject = ninjaSelItemBtnList[0];
        if (PlayerPrefs.GetInt("CurrentItemID") == 11 &&
            GameConstant.getStatusPowerful() == 1)
            UICamera.selectedObject = ninjaSelItemBtnList[1];
        if (PlayerPrefs.GetInt("CurrentItemID") == 12 &&
            GameConstant.getStatusBomb() == 1)
            UICamera.selectedObject = ninjaSelItemBtnList[2];
        if (PlayerPrefs.GetInt("CurrentItemID") == 13 &&
            GameConstant.getStatusGolden() == 1)
            UICamera.selectedObject = ninjaSelItemBtnList[3];
    }
    public void openShop()
    {
        gameObject.SetActive(true);
        updateGoldAmount();
    }
    public void closeShop()
    {
        AudioManager.instance.playClickAtMainMenuSound();
        if (gameController != null)
        {
            gameController.isOpenShop = false;
            if (gameController.isPaused)
            { Time.timeScale = 0f;
                UICamera.selectedObject = shopBtn;
            }
            else
                Time.timeScale = 1f;
            gameController.playerObject.GetComponent<PlayerController>().updateSkin();
            gameController.updateItemForPlayerAbility();

        }

        AudioManager.instance.playClickAtMainMenuSound();
        gameObject.SetActive(false);

    }

    public void addMoreWeapon()
    {
        if (gameController != null)
        {
            Time.timeScale = 0f;
            gameController.isPaused = true;
            gameController.isOpenShop = true;
        }
        this.gameObject.SetActive(true);
        clickItemTab();
    }


    public void clickItemTab()
    {
        hideAllButtonText();
        btItem.transform.Find("txt_Active").gameObject.SetActive(true);
        btPlayer.transform.Find("txt_InActive").gameObject.SetActive(true);
        btInApp.transform.Find("txt_InActive").gameObject.SetActive(true);


        tabPlayer.SetActive(false);
        tabItem.SetActive(true);
        tabInApp.SetActive(false);
    }

    public void clickPlayerTab()
    {
        hideAllButtonText();
        btPlayer.transform.Find("txt_Active").gameObject.SetActive(true);
        btInApp.transform.Find("txt_InActive").gameObject.SetActive(true);
        btItem.transform.Find("txt_InActive").gameObject.SetActive(true);

        tabPlayer.SetActive(true);
        tabItem.SetActive(false);
        tabInApp.SetActive(false);
    }

    public void clickInAppTab()
    {
        hideAllButtonText();
        btInApp.transform.Find("txt_Active").gameObject.SetActive(true);
        btPlayer.transform.Find("txt_InActive").gameObject.SetActive(true);
        btItem.transform.Find("txt_InActive").gameObject.SetActive(true);


        tabPlayer.SetActive(false);
        tabItem.SetActive(false);
        tabInApp.SetActive(true);

        /*
        if (GameConstant.isRemoveAd()==1) {
            gridInApp.GetComponent<UIGrid>().GetChildList()[0].gameObject.SetActive(false);

        } 
        if (GameConstant.getStatusGhost()!= -1)
        {
            gridInApp.GetComponent<UIGrid>().GetChildList()[6].gameObject.SetActive(false);

        } 
        if (GameConstant.getStatusPowerful() != -1)
        {
            gridInApp.GetComponent<UIGrid>().GetChildList()[7].gameObject.SetActive(false);

        }

        if (GameConstant.getStatusBomb() != -1)
        {
            gridInApp.GetComponent<UIGrid>().GetChildList()[8].gameObject.SetActive(false);

        }

        if (GameConstant.getStatusGolden() != -1)
        {
            gridInApp.GetComponent<UIGrid>().GetChildList()[9].gameObject.SetActive(false);

        }
        gridInApp.GetComponent<UIGrid>().Reposition();*/
    }


    void hideAllButtonText()
    {
        btPlayer.transform.Find("txt_InActive").gameObject.SetActive(false);
        btInApp.transform.Find("txt_InActive").gameObject.SetActive(false);
        btItem.transform.Find("txt_InActive").gameObject.SetActive(false);

        btPlayer.transform.Find("txt_Active").gameObject.SetActive(false);
        btInApp.transform.Find("txt_Active").gameObject.SetActive(false);
        btItem.transform.Find("txt_Active").gameObject.SetActive(false);
    }



    public void setSelectedNinja(GameObject ninjaItem)
    {
        unSelectNinja();
        ninjaItem.GetComponent<NinjaItem>().setSelected();
        switch (ninjaItem.GetComponent<NinjaItem>().ninjaType)
        {
            case NinjaItem.NINJA_TYPE.GOLDEN:
                GameConstant.setStatusGolden(1);
                PlayerPrefs.SetInt("CurrentItemID", 13);
                break;
            case NinjaItem.NINJA_TYPE.BOMB:
                GameConstant.setStatusBomb(1);
                PlayerPrefs.SetInt("CurrentItemID", 12);
                break;
            case NinjaItem.NINJA_TYPE.POWERFUL:
                GameConstant.setStatusPowerful(1);
                PlayerPrefs.SetInt("CurrentItemID", 11);
                break;
            case NinjaItem.NINJA_TYPE.GHOST:
                GameConstant.setStatusGhost(1);
                PlayerPrefs.SetInt("CurrentItemID", 10);
                break;



        }
        SetItemStatus();
        uiTabPlayerPanel.UpdateNavigation();
    }


    public void unSelectNinja()
    {
        if (GameConstant.getStatusBomb() != -1)
        {
            ninjaItemList[0].GetComponent<NinjaItem>().setUnselected();
            PlayerPrefs.SetInt("CurrentItemID", 12);
            GameConstant.setStatusBomb(0);
        }
        if (GameConstant.getStatusPowerful() != -1)
        {
            ninjaItemList[1].GetComponent<NinjaItem>().setUnselected();
            PlayerPrefs.SetInt("CurrentItemID", 11);
            GameConstant.setStatusPowerful(0);
        }
        if (GameConstant.getStatusGolden() != -1)
        {
            ninjaItemList[2].GetComponent<NinjaItem>().setUnselected();
            PlayerPrefs.SetInt("CurrentItemID", 13);
            GameConstant.setStatusGolden(0);
        }
        if (GameConstant.getStatusGhost() != -1)
        {
            ninjaItemList[3].GetComponent<NinjaItem>().setUnselected();
            PlayerPrefs.SetInt("CurrentItemID", 10);
            GameConstant.setStatusGhost(0);
        }

        SetItemStatus();
        uiTabPlayerPanel.UpdateNavigation();





    }
}