using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{



    public static int countPlayTime = 0;
    int randomGift;
    public string moreGameLink;
    public GameObject btMusic, dialogShop, lblGiftTimer;
    public int GIFT_TIME = 10;
    bool isGiftReady = false;
    public GameObject dialogGift, dialogDaily;
    // Use this for initialization
    public GameObject giftButton;
    public GameObject btnPlay, btnShop;
    public UILabel lblGiftGold, lblDailyGold;
    public UISprite spriteGiftItem, spriteGiftGold, spritDailyItem;
    public GameObject lblDailyNumberOfDay;
    public GameObject btRestorePurchase, btRemoveAds;
    public static bool firstTimeOpenGame = true;
    public GameObject dialogWait, btClaimDailyReward,btExit,btCancel;
    public UILabel lblGoldAmount;
    public GameObject[] dailyObjects;
    public bool isSwitch { get; set; }
    public bool isStart { get; set; }
    //public GameObject splashObject;
    //public static bool canShowSplash = true;
    public void openInAppShop()
    {
        dialogShop.SetActive(true);
        dialogShop.GetComponent<ShopController>().clickInAppTab();
    }
    //public void hideSplash(){
    //	splashObject.SetActive (false);
    ///}

    void Awake()
    {
        Screen.fullScreen = false;
    }
    
    void Start()
    {

        isStart = true;
        isSwitch = false;
        Application.targetFrameRate = 60;
        //	if (canShowSplash) {
        //		splashObject.SetActive (true);
        //		canShowSplash = false;
        //	} else {
        //		splashObject.SetActive (false);
        //	}

#if UNITY_IOS
        moreGameLink = "https://itunes.apple.com/us/app/stickman-dismount-ragdoll/id1408850272?mt=8";
#elif UNITY_ANDROID
        moreGameLink = "https://play.google.com/store/apps/details?id=stickman.ninja.warriors";
#endif
        //UICamera.selectedObject = btnShop;
        RemoteSettings.Updated += new RemoteSettings.UpdatedEventHandler(RemoteSettingsUpdated);
        updateMusicButtonState();
        AudioManager.instance.playMainMenuMusic();
        if (GameConstant.getLevelStatus(1) == -1)
        {
            GameConstant.setLevelStatus(1, 0);
        }
        checkGift();
        //   GameConstant.addDailyCount(5);
        if (GameConstant.getDailyCount() == 1)
        {
            openDialogDaily();
        }
        else
        {

            System.DateTime lastDaily = System.DateTime.Parse(GameConstant.getLastDayDaily());
            int dayCount = (int)(System.DateTime.Now - lastDaily).TotalDays;

            //     int dayCount = GameConstant.getDailyCount();
            Debug.Log("dayCount   >>>>>>>>>>>>   " + dayCount);
            //   GameConstant.addDailyCount(5);
            //     dayCount = GameConstant.getDailyCount();

            if (dayCount >= 1 && GameConstant.getDailyCount() < 15)
            {
                openDialogDaily();
            }
        }

        if (GameConstant.isRemoveAd() == 0)
        {
            Debug.Log("Reloaded <========");


        }
        else
        {
            btRestorePurchase.SetActive(false);
            btRemoveAds.SetActive(false);
        }
        loadAdmobRewardFailed = false;
#if UNITY_ANDROID
        btRestorePurchase.SetActive(false);
#endif


        if (firstTimeOpenGame)
        {
            GameConstant.increaseRetentionCount();
            //Debug.Log("first time rawwww!");
            Debug.Log("first time rawwww!   " + GameConstant.getRetentionCount());
            if (GameConstant.getRetentionCount() % 10 == 0)
            {


                Debug.Log("first time rawwww!   " + GameConstant.getRetentionCount());
            }
        }
        firstTimeOpenGame = false;
        updateGoldText();



    }






    void updateGoldText()
    {
        lblGoldAmount.text = string.Format("{0:#,###0}", GameConstant.getGold());
    }
    void RemoteSettingsUpdated()
    {
        Debug.Log("**** GOT NEW REMOTE SETTINGS *****");
#if UNITY_IOS
        GameConstant.setBannerID(RemoteSettings.GetString("IDBanerIOS", GameConstant.getBannerID()));
        GameConstant.setFullID(RemoteSettings.GetString("IDInterIOS", GameConstant.getFullID()));
        GameConstant.setRewardID(RemoteSettings.GetString("IDRewardIOS", GameConstant.getRewardID()));
        GameConstant.setShowBanner(RemoteSettings.GetInt("ShowBannerIOS",1));
        GameConstant.setVibrate(RemoteSettings.GetInt("OnVibration", 0));
        GameConstant.setCountTimeShowAd(RemoteSettings.GetInt("CountTimeIOS", 30));
		GameConstant.setShowRateServer(RemoteSettings.GetInt("ShowRate", 0));
		GameConstant.setLevelToShowAds(RemoteSettings.GetInt("LevelStartAds", 5));
		GameConstant.setShowDaily(RemoteSettings.GetInt("ShowDaily", 0));

#endif
#if UNITY_ANDROID
        GameConstant.setBannerID(RemoteSettings.GetString("IDBanerAndroid", GameConstant.getBannerID()));
        GameConstant.setFullID(RemoteSettings.GetString("IDInterAndroid", GameConstant.getFullID()));
        GameConstant.setRewardID(RemoteSettings.GetString("IDRewardAndroid", GameConstant.getRewardID()));
        GameConstant.setShowBanner(RemoteSettings.GetInt("ShowBannerAndroid", 1));
        GameConstant.setVibrate(RemoteSettings.GetInt("OnVibration", 0));
        GameConstant.setCountTimeShowAd(RemoteSettings.GetInt("CountTimeAndroid", 15));
        GameConstant.setShowRateServer(RemoteSettings.GetInt("ShowRate", 0));
        GameConstant.setLevelToShowAds(RemoteSettings.GetInt("LevelStartAds", 5));
        GameConstant.setShowDaily(RemoteSettings.GetInt("ShowDaily", 0));

#endif


        //
        //   Debug.Log("Remote Banner ID:  "+GameConstant.getBannerID());
        //    Debug.Log("Remote Inter ID:  " + GameConstant.getFullID());
        //   Debug.Log("Remote Reward ID:  " + GameConstant.getRewardID());
        Debug.Log("canVibrate:  " + GameConstant.canVibrate());
        /*
        MobieNative.Instance.show_ads = RemoteSettings.GetInt("ShowAds", 0);
        Debug.Log("SHOW ADS " + MobieNative.Instance.show_ads);
#if UNITY_IOS
        MobieNative.Instance.type_ads = RemoteSettings.GetInt("TypeAds", 0);
        MobieNative.Instance.type_reward = RemoteSettings.GetInt("TypeReward", 0);
        MobieNative.Instance.countTime = RemoteSettings.GetInt("CountTime", 30);
#endif
#if UNITY_ANDROID
        MobieNative.Instance.type_ads = RemoteSettings.GetInt("TypeAdsAndroid",0);
        MobieNative.Instance.type_reward = RemoteSettings.GetInt("TypeRewardAndroid",0);
        MobieNative.Instance.countTime = RemoteSettings.GetInt("CountTimeAndroid",30);
#endif
        Debug.Log("SHOW TYPE " + MobieNative.Instance.type_ads);


        Debug.Log("COUNT TIME " + MobieNative.Instance.countTime);

        Debug.Log(RemoteSettings.GetInt("testInt"));
        Debug.Log(RemoteSettings.GetString("Confix"));
        Debug.Log(RemoteSettings.GetString("MoreGame"));
        Debug.Log(RemoteSettings.GetFloat("testFloat"));
        Debug.Log(RemoteSettings.GetBool("testBool"));
        Debug.Log(RemoteSettings.GetBool("testFakeKey"));
        Debug.Log(RemoteSettings.GetBool("testFakeKey", true));
        Debug.Log(RemoteSettings.HasKey("qqq"));
        Debug.Log(RemoteSettings.HasKey("testInt"));
        Debug.Log(RemoteSettings.GetBool("unity.heatmaps"));
        */
    }
    public void openDialogDaily()
    {
        if (!GameConstant.canShowDaily())
            return;
        // GameConstant.addDailyCount(2);
        dialogDaily.GetComponent<UIScaleAnimation>().open();
        //lblDailyNumberOfDay.GetComponent<UILabel>().text="Day "+GameConstant.getDailyCount();
        for (int i = 0; i < 15; i++)
        {
            if (i < GameConstant.getDailyCount() - 1)
            {
                dailyObjects[i].GetComponent<DailyRewardObject>().imgToday.SetActive(false);
                dailyObjects[i].GetComponent<DailyRewardObject>().imgTick.SetActive(true);
                dailyObjects[i].GetComponent<DailyRewardObject>().itemIcon.GetComponent<UISprite>().color = Color.gray;
            }
            else if (i == GameConstant.getDailyCount() - 1)
            {
                dailyObjects[i].GetComponent<DailyRewardObject>().imgToday.SetActive(true);
            }
        }



    }
    public void closeDialogDaily()
    {
        dialogDaily.GetComponent<UIScaleAnimation>().close();
    }
    void dailyReceived()
    {
        dailyObjects[GameConstant.getDailyCount() - 1].GetComponent<DailyRewardObject>().imgToday.SetActive(false);
        dailyObjects[GameConstant.getDailyCount() - 1].GetComponent<DailyRewardObject>().itemIcon.GetComponent<UISprite>().color = Color.gray;
        //  Invoke("closeDialogDaily",1f);
        goldToShow = GameConstant.getGold();
        switch (GameConstant.getDailyCount())
        {

            case 1:
                GameConstant.addGold(100);
                break;
            case 2:
                GameConstant.addDartCount(1);
                break;
            case 3:
                GameConstant.addGold(120);
                break;
            case 4:
                GameConstant.addTrippleDartCount(1);
                break;
            case 5:
                GameConstant.addGold(150);
                break;
            case 6:
                GameConstant.addSuperDartCount(1);
                break;
            case 7:
                GameConstant.addGold(170);
                break;
            case 8:

                GameConstant.addBombCount(1);
                break;
            case 9:
                GameConstant.addGold(200);

                break;
            case 10:
                GameConstant.addDartCount(2);

                break;
            case 11:
                GameConstant.addGold(220);

                break;
            case 12:
                GameConstant.addSuperDartCount(2);

                break;
            case 13:
                GameConstant.addGold(250);
                break;
            case 14:
                GameConstant.addGold(250);
                break;
            case 15:
                GameConstant.addDartCount(1);
                GameConstant.addBombCount(1);
                GameConstant.addSuperDartCount(1);
                GameConstant.addTrippleDartCount(1);
                break;

        }

        targetGoldLerp = GameConstant.getGold();
        goldLerpTime = 2f;
        GameConstant.setLastDayDaily();
        GameConstant.addDailyCount(1);
    }
    public void claimDailyReward()
    {
        print("click claim " + GameConstant.getDailyCount());
        dailyObjects[GameConstant.getDailyCount() - 1].GetComponent<DailyRewardObject>().imgTick.SetActive(true);
        dailyObjects[GameConstant.getDailyCount() - 1].GetComponent<DailyRewardObject>().imgTick.GetComponent<TweenScale>().enabled = true;
        dailyObjects[GameConstant.getDailyCount() - 1].GetComponent<DailyRewardObject>().imgTick.GetComponent<TweenAlpha>().enabled = true;
        dailyObjects[GameConstant.getDailyCount() - 1].GetComponent<DailyRewardObject>().imgTick.GetComponent<TweenScale>().AddOnFinished(dailyReceived);
        btClaimDailyReward.SetActive(false);
        //	dialogDaily.GetComponent<UIScaleAnimation>().close();
    }
    public void turnMusicOnOff()
    {
        if (GameConstant.isMusicOn() == 1)
        {
            GameConstant.setMusic(0);
            btMusic.GetComponent<UIButton>().normalSprite = "music_off";
            AudioListener.volume = 0;


        }
        else
        {

            GameConstant.setMusic(1);
            btMusic.GetComponent<UIButton>().normalSprite = "music_on";
            AudioListener.volume = 1;
            AudioManager.instance.playMainMenuMusic();
            AudioManager.instance.playClickAtMainMenuSound();
        }
    }
    void updateMusicButtonState()
    {
        if (GameConstant.isMusicOn() == 1)
        {
            btMusic.GetComponent<UIButton>().normalSprite = "music_on";
            AudioListener.volume = 1;
        }
        else
        {
            btMusic.GetComponent<UIButton>().normalSprite = "music_off";
            AudioListener.volume = 0;
        }


    }

    float lerpScoreTime = 0f, goldLerpTime = 0f;
    int goldToShow = 0, targetGoldLerp;
    public void startAddGold()
    {
        //   print("" + lerpScoreTime);

        lerpScoreTime += Time.unscaledDeltaTime / goldLerpTime;

        goldToShow = (int)Mathf.Lerp(goldToShow, targetGoldLerp, lerpScoreTime);
        if (goldToShow >= targetGoldLerp - 2)
        {
            goldLerpTime = 0;
            goldToShow = targetGoldLerp;
        }

        lblGoldAmount.text = string.Format("{0:#,###0}", goldToShow);


    }
    // Update is called once per frame
    void Update()
    {
        if (dialogWait.activeSelf)
        {
            btnShop.GetComponent<TweenScale>().enabled = false;
            btnPlay.GetComponent<TweenScale>().enabled = false;
        }
        if (dialogWait.activeSelf && UICamera.selectedObject != btExit && UICamera.selectedObject != btCancel)
            UICamera.selectedObject = btExit;
        btExit.transform.GetChild(1).gameObject.SetActive(UICamera.selectedObject == btExit);
        btCancel.transform.GetChild(1).gameObject.SetActive(UICamera.selectedObject == btCancel);
        if (!dialogShop.activeSelf && !isSwitch && UICamera.selectedObject != btnPlay &&
            UICamera.selectedObject != btnShop && !isStart)
            UICamera.selectedObject = btnShop;
        if (Input.GetButtonDown("Cancel"))
            BackShopPanel();
        Debug.Log($"CucBtn: {UICamera.selectedObject}");
        if (goldLerpTime > 0)
        {
            startAddGold();
        }

        if (UICamera.selectedObject == btnPlay)
        {
            btnShop.GetComponent<TweenScale>().enabled = false;
            btnPlay.GetComponent<TweenScale>().enabled = true;
        }
        else if (UICamera.selectedObject == btnShop)
        {
            btnShop.GetComponent<TweenScale>().enabled = true;
            btnPlay.GetComponent<TweenScale>().enabled = false;
        }
    }

    public void toStageSelect()
    {
        AudioManager.instance.playClickAtMainMenuSound();
        SceneManager.LoadScene("StageSelect");

    }
    public void openShop()
    {
        isSwitch = true;
        isStart = false;
        updateGoldText();
        AudioManager.instance.playClickAtMainMenuSound();
        dialogShop.GetComponent<ShopController>().openShop();
    }

    void BackShopPanel()
    {
        if (isSwitch)
        {
            isSwitch = false;
            closeShop();
            UICamera.selectedObject = btnShop;
            Debug.Log(UICamera.selectedObject);
        }
        else
        {
            dialogWait.SetActive(true);
            UICamera.selectedObject = btExit;
            btnShop.GetComponent<TweenScale>().enabled = false;
            btnPlay.GetComponent<TweenScale>().enabled = false;
        }
    }

    public void Canacel()
    {
        dialogWait.SetActive(false);
        UICamera.selectedObject = btnPlay;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
    public void closeShop()
    {
        updateGoldText();
        dialogShop.SetActive(false);
    }
    public void openMoreGame()
    {
        AudioManager.instance.playClickAtMainMenuSound();
        Application.OpenURL(moreGameLink);
        GameConstant.disableShowRate();
    }



    public void getGift()
    {

        if (isGiftReady)
        {
            // receive gift	
            dialogGift.GetComponent<UIScaleAnimation>().open();
            randomGift = Random.Range(0, 100);

            if (randomGift <= 50)
            {
                lblGiftGold.text = "+ 100";
                spriteGiftItem.gameObject.SetActive(false);
            }
            else if (randomGift > 50 && randomGift <= 65)
            {
                spriteGiftGold.gameObject.SetActive(false);
                spriteGiftItem.spriteName = "btn_phitieu";
            }
            else if (randomGift > 65 && randomGift <= 80)
            {
                spriteGiftGold.gameObject.SetActive(false);
                spriteGiftItem.spriteName = "btn_phitieu3";
            }
            else if (randomGift > 80 && randomGift <= 90)
            {
                spriteGiftGold.gameObject.SetActive(false);
                spriteGiftItem.spriteName = "btn_caugai";
            }
            else
            {
                spriteGiftGold.gameObject.SetActive(false);
                spriteGiftItem.spriteName = "btn_bom";
            }




        }
    }

    public void claimGift()
    {
        if (isGiftReady)
        {
            isGiftReady = false;
            totalSeconds = 0;
            CancelInvoke("UpdateTime");
            InvokeRepeating("UpdateTime", 0f, 1f);
            PlayerPrefs.SetString("lastTime", "" + System.DateTime.Now);
            giftButton.GetComponent<BoxCollider>().enabled = false;
            giftButton.GetComponent<TweenScale>().enabled = false;
            giftButton.GetComponent<TweenRotation>().enabled = false;
            goldToShow = GameConstant.getGold();


            if (randomGift <= 50)
            {
                GameConstant.addGold(100);
            }
            else if (randomGift > 50 && randomGift <= 65)
            {
                GameConstant.addDartCount(1);
            }
            else if (randomGift > 65 && randomGift <= 80)
            {
                GameConstant.addTrippleDartCount(1);
            }
            else if (randomGift > 80 && randomGift <= 90)
            {
                GameConstant.addSuperDartCount(1);
            }
            else
            {
                GameConstant.addBombCount(1);
            }


            targetGoldLerp = GameConstant.getGold();
            goldLerpTime = 2f;
            dialogGift.GetComponent<UIScaleAnimation>().close();

        }


    }
    static int totalSeconds;
    int minute;
    public static System.DateTime dateTimeLast, dateTimeNow, lastMinimize;
    public static bool addTime = true;

    private int focusCounter, pauseCounter;

    public static double minimizedSeconds;
    void checkGift()
    {

        string dateString = PlayerPrefs.GetString("lastTime", "full");

        if (!dateString.Equals("full"))
        {
            dateTimeLast = System.DateTime.Parse(dateString);
            totalSeconds = (int)(System.DateTime.Now - dateTimeLast).TotalSeconds;

            if (totalSeconds >= GIFT_TIME)
            {
                giftReady();
            }
            else
            {
                giftButton.GetComponent<BoxCollider>().enabled = false;
                CancelInvoke("UpdateTime");
                InvokeRepeating("UpdateTime", 0f, 1f);
            }

        }
        else
        {
            giftReady();
        }

    }
    void giftReady()
    {
        lblGiftTimer.GetComponent<UILabel>().text = "GET";
        giftButton.GetComponent<BoxCollider>().enabled = true;
        giftButton.GetComponent<TweenScale>().enabled = true;
        giftButton.GetComponent<TweenRotation>().enabled = true;
        PlayerPrefs.SetString("lastTime", "full");
        isGiftReady = true;
    }




    void OnApplicationPause(bool isGamePause)
    {


        //	print ("pause "+isGamePause);
        if (isGamePause)
        {
            GoToMinimize();
            AudioListener.volume = 0;
        }
        else
        {
            //	focusCounter++;
            GoToMaximize();
            if (GameConstant.isMusicOn() == 1)
                AudioListener.volume = 1;
        }

        Debug.Log("Paused = " + isGamePause);
        if (isGamePause)
        {
            if (GameConstant.isMusicOn() == 1)
            {
                AudioListener.volume = 0;
            }
        }
        else
        {

            if (GameConstant.isMusicOn() == 1)
            {
                AudioListener.volume = 1;
            }

        }

    }

    void OnApplicationFocus(bool isGameFocus)
    {
        //print("OnApplicationFocus "+ isGameFocus);
        if (isGameFocus)
        {

        }
    }
    public void GoToMinimize()
    {
        //print("lastMinimize");
        lastMinimize = System.DateTime.Now;
        //	PlayerPrefs.SetString("lastTime",lastMinimize.ToString());
    }

    public static void GoToMaximize()
    {

        //	print("gomaximize");
        minimizedSeconds = (System.DateTime.Now - lastMinimize).TotalSeconds;
        totalSeconds += (int)minimizedSeconds;
        //	print ("total seconds " + totalSeconds);


    }

    void UpdateTime()
    {
        //	print ("update time "+ totalSeconds);


        if (isGiftReady)
        {
            lblGiftTimer.GetComponent<UILabel>().text = "GET";
        }
        else
        {
            totalSeconds++;
            int int_second = (GIFT_TIME - totalSeconds) % 60;
            int int_minute = (GIFT_TIME - totalSeconds) / 60;
            string string_second = "" + int_second;
            string string_minute = "" + int_minute;
            if (int_second == 0) string_second = "00";
            else if (int_second < 10) string_second = "0" + int_second;
            if (int_minute == 0) string_minute = "00";
            else if (int_minute < 10) string_minute = "0" + int_minute;
            lblGiftTimer.GetComponent<UILabel>().text = string_minute + ":" + string_second;
            if (totalSeconds >= GIFT_TIME)
            {
                giftReady();
            }

        }


    }
    public void showAdmobRewardVideoForGift()
    {

    }
    void onRestore()
    {
        btRestorePurchase.SetActive(false);
        if (GameConstant.isRemoveAd() == 1) btRemoveAds.SetActive(false);
        //  dialogShop.GetComponent<ShopController>().updateNinjaStatus();
    }
    void OnEnable()
    {
        InAppPurchase.onRemoveAd += removeAd;
        InAppPurchase.onPurchasePack += purchasePack;
        InAppPurchase.onRestore += onRestore;


    }

    void OnDestroy()
    {
        InAppPurchase.onRemoveAd -= removeAd;
        InAppPurchase.onPurchasePack -= purchasePack;
        InAppPurchase.onRestore -= onRestore;


    }



    void purchasePack()
    {
        Debug.Log("purchase succeed");
        dialogShop.GetComponent<ShopController>().updateGoldAmount();
        dialogShop.GetComponent<ShopController>().updateNinjaStatus();
    }

    void removeAd()
    {
        Debug.Log("remove ad succeed");
        if (GameConstant.isRemoveAd() == 1)
        {

            btRemoveAds.SetActive(false);
        }

    }
    public bool loadAdmobRewardFailed;
    void onRewardGiftLoadFailed()
    {
        Debug.Log("Reward load failed");
        loadAdmobRewardFailed = true;
    }
    void onRewardGift()
    {
        Debug.Log("onRewardGift Event Catched");
        getGift();
    }
    public void restorePurchase()
    {

    }
    public void ShowRewardedVideo()
    {
        Debug.Log("Reward  Unity");
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show("rewardedVideo", options);


    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {

            Debug.Log("Video completed - Offer a reward to the player");
            // Reward your player here.
            getGift();

        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }



}
