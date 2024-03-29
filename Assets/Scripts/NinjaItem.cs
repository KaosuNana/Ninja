using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaItem : MonoBehaviour
{
    public int price;
    public UITabPlayerPanel uiTabPlayerPanel;
    public enum NINJA_TYPE
    {
        GOLDEN, GHOST, POWERFUL, BOMB, NORMAL
    }
    public NINJA_TYPE ninjaType;
    public GameObject imgLock, imgUnlock, imgSelected;

    ShopController shopController;
    // Use this for initialization
    void Start()
    {
        shopController = GameObject.FindGameObjectWithTag("DialogShop").GetComponent<ShopController>();


        updateStatus();




    }
    public void updateStatus()
    {
        switch (ninjaType)
        {
            case NINJA_TYPE.GOLDEN:
                if (GameConstant.getStatusGolden() == 1)
                {
                    setSelected();
                }
                else if (GameConstant.getStatusGolden() == -1)
                {
                    showLock();
                }
                else setUnselected();
                break;
            case NINJA_TYPE.BOMB:
                if (GameConstant.getStatusBomb() == 1)
                    setSelected();
                else if (GameConstant.getStatusBomb() == -1)
                {
                    showLock();
                }
                else
                    setUnselected();
                break;
            case NINJA_TYPE.POWERFUL:
                if (GameConstant.getStatusPowerful() == 1)
                    setSelected();
                else if (GameConstant.getStatusPowerful() == -1)
                {
                    showLock();
                }
                else
                    setUnselected();
                break;
            case NINJA_TYPE.GHOST:
                if (GameConstant.getStatusGhost() == 1)
                    setSelected();
                else if (GameConstant.getStatusGhost() == -1)
                {
                    showLock();
                }
                else
                    setUnselected();
                break;


        }

        shopController.updateGoldAmount();
        uiTabPlayerPanel.UpdateNavigation();
    }
    void showLock()
    {
        imgLock.SetActive(true);
        imgUnlock.SetActive(false);
        imgSelected.SetActive(false);
        imgLock.transform.Find("price").GetComponent<UILabel>().text = "" + string.Format("{0:#,###0}", price);
    }
    public void setSelected()
    {
        AudioManager.instance.playSound(AudioManager.instance.soundSelectNinja);
        imgLock.SetActive(false);
        imgUnlock.SetActive(false);
        imgSelected.SetActive(true);
    }
    public void setUnselected()
    {

        imgLock.SetActive(false);
        imgUnlock.SetActive(true);
        imgSelected.SetActive(false);


    }
    // Update is called once per frame
    void Update()
    {

    }
    public void confirmPurchase()
    {
        switch (ShopController.selectedItem)
        {
            case ShopController.SELECTED_SHOP_ITEM_TYPE.CHAR1:
                Debug.Log("buyBomb");
                if (GameConstant.getGold() >= price && GameConstant.getStatusBomb() == -1)
                {
                    GameConstant.addGold(-price);
                    GameConstant.setStatusBomb(0);
                    updateStatus();
                    shopController.SetItemStatus();
                }
                else
                {
                    shopController.showNotify();
                }
                break;

            case ShopController.SELECTED_SHOP_ITEM_TYPE.CHAR2:
                Debug.Log("buyPowerful");
                if (GameConstant.getGold() >= price && GameConstant.getStatusPowerful() == -1)
                {
                    GameConstant.addGold(-price);
                    GameConstant.setStatusPowerful(0);
                    updateStatus();
                    shopController.SetItemStatus();
                }
                else
                {
                    shopController.showNotify();
                }
                break;

            case ShopController.SELECTED_SHOP_ITEM_TYPE.CHAR3:
                Debug.Log("buyGolden");
                if (GameConstant.getGold() >= price && GameConstant.getStatusGolden() == -1)
                {
                    GameConstant.addGold(-price);
                    GameConstant.setStatusGolden(0);
                    updateStatus();
                    shopController.SetItemStatus();
                }
                else
                {
                    shopController.showNotify();
                }
                break;
            case ShopController.SELECTED_SHOP_ITEM_TYPE.CHAR4:
                Debug.Log("buyGhost");
                if (GameConstant.getGold() >= price && GameConstant.getStatusGhost() == -1)
                {
                    GameConstant.addGold(-price);
                    GameConstant.setStatusGhost(0);
                    updateStatus();
                    shopController.SetItemStatus();
                }
                else
                {
                    shopController.showNotify();
                }
                break;
        }

    }
    public void buyGolden()
    {

        shopController.dialogConfirm.GetComponent<DialogConfirm>().show("购买战士火柴人?", 13);
        ShopController.selectedObject = this.gameObject;
        ShopController.selectedItem = ShopController.SELECTED_SHOP_ITEM_TYPE.CHAR3;
    }
    public void buyPowerful()
    {

        shopController.dialogConfirm.GetComponent<DialogConfirm>().show("购买刺客火柴人?", 11);
        ShopController.selectedObject = this.gameObject;
        ShopController.selectedItem = ShopController.SELECTED_SHOP_ITEM_TYPE.CHAR2;
    }
    public void buyGhost()
    {
        shopController.dialogConfirm.GetComponent<DialogConfirm>().show("购买幽灵火柴人?", 10);
        ShopController.selectedObject = this.gameObject;
        ShopController.selectedItem = ShopController.SELECTED_SHOP_ITEM_TYPE.CHAR4;
    }
    public void buyBomb()
    {
        shopController.dialogConfirm.GetComponent<DialogConfirm>().show("购买炸弹火柴人?", 12);
        ShopController.selectedObject = this.gameObject;
        ShopController.selectedItem = ShopController.SELECTED_SHOP_ITEM_TYPE.CHAR1;
    }

}
