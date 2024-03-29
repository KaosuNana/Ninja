using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using UnityEngine.Advertisements;
public class GameController : MonoBehaviour
{
    public float timePlayed;
    string NAME_DYNAMITE = "LE_Dynamite";
    string NAME_SHURIKEN = "LE_SingleShuriken";
    string NAME_TRIPLESHURIKEN = "LE_TripleShuriken";
    string NAME_SUPERSHURIKEN = "LE_SuperShuriken";
    public string rateGameLink;
    public GameObject dialogPause, dialogWinLose, dialogLevelInfo, dialogShop;
    public GameObject[] dialogPauseButton;
    public GameObject[] dialogResultButton;
    public GameObject dialogShopBtn;
    public UILabel lblEndGameScore;
    public UISprite endGameStar, endGameTitle;
    public Weapon.WEAPON_TYPE currentWeaponType;
    public int MAX_SHURIKEN = 3;
    private AssetLevelData data;
    public GameObject playerPrefab;
    int totalWeapon;
    public float shootDelay;
    float countShootDelay;
    public GameObject aimPoint, aimSign, playerShuriken, playerDynamite, playerSuperShuriken, score1000Prefab, score2000Prefab, score3000Prefab, score3600Prefab, score2400Prefab, score1200Prefab, goldUIPrefab;
    public GameObject goMouse;
    public Transform objectHidePos;
    Transform playerPosition;
    Transform playerGround;
    public GameObject playerObject;
    GameObject parentGameObject;
    public float _3ShurikenAngle;
    List<GameObject> weaponThrowed;
    Vector3 aimDir;
    public static int currentWorld, currentLevel;
    public UIGrid grid_AvailableWeapons;
    public GameObject aw_SingleShuriken, aw_TripleShuriken, aw_SuperShuriken, aw_Dynamite, aw_AddMore;
    int totalScore;
    RaycastHit touchHit;
    public bool isWin;
    bool isStarted, isGameOver;
    public bool isPaused, isOpenShop, isTouchedDown, isBtnClick;
    public UILabel lblTotalScore, lblCurrentLevel;
    public GameObject dialogLevelInfoLevel, dialogLevelInfoScore, dialogLevelInfoStars, dialogWinLoseNextButton, dialogWinGoldAmount, dialogWinVideoAd;
    public GameObject dialogWinStar1, dialogWinStar2, dialogWinStar3, dialogRateGame, eSpark1, eSpark2, eSpark3;
    public GameObject tutorialLv1, tutorialLv2a, tutorialLv2b, tutorial2c, tutorial2d, btSkip;
    Spine.Bone playerHeadBone;
    Vector2 originalHeadPosition;


    public static PlayerController.PlayerAbility currentAbility;
    int totalEnemyCount = 0;
    int winGameEnemyCount = 0;
    public static int totalGold;
    public GameObject playerDialogAnim;
    public static int STAR_TO_UNLOCK = 140;

    private Array _values;
    void Start()
    {
        _values = Enum.GetValues(typeof(KeyCode));
        //Time.timeScale=0.2f;
#if UNITY_IOS
        rateGameLink = "https://itunes.apple.com/us/app/stickman-dismount-ragdoll/id1408850272?mt=8";
#elif UNITY_ANDROID
        rateGameLink = "https://play.google.com/store/apps/details?id=stickman.ninja.warriors";
#endif

        MainMenuController.countPlayTime++;
        Time.timeScale = 1f;
        isWin = false;
        isGameOver = false;
        isStarted = false;
        isPaused = false;
        isOpenShop = false;
        isTouchedDown = false;
        timePlayed = 0;
        totalGold = 0;
        currentWorld = StageSelectController.selectedWorld;
        currentLevel = LevelSelectController.selectedLevel;
        totalScore = 0;
        lblTotalScore.text = " " + totalScore;//分数
        int levelToDisplay = currentLevel % 60;
        if (levelToDisplay == 0) levelToDisplay = 60;
        lblCurrentLevel.text = " " + currentWorld + "-" + levelToDisplay;
        parentGameObject = GameObject.Find("MapObjects");
        //#if !UNITY_EDITOR
        data = (AssetLevelData)Resources.Load<AssetLevelData>("Maps/" + currentLevel);
        Debug.Log("Maps/" + currentLevel);
        foreach (LevelObject obj in data.GetAllObjects())
        {
            GameObject o = Instantiate(obj.prefab) as GameObject;
            o.transform.position = obj.position;
            o.transform.eulerAngles = obj.rotation;
            if (o.gameObject.tag != "enemy")
                o.transform.localScale = obj.scale;
            o.transform.parent = parentGameObject.transform;

        }
        //#endif


        foreach (Transform child in parentGameObject.transform)
        {
            Debug.Log("Map Items: " + child.name);

            if (child.name.Contains(NAME_DYNAMITE))
            {
                Instantiate(aw_Dynamite.transform, grid_AvailableWeapons.transform);
            }
            if (child.name.Contains(NAME_SHURIKEN))
            {
                Instantiate(aw_SingleShuriken.transform, grid_AvailableWeapons.transform);
            }
            if (child.name.Contains(NAME_SUPERSHURIKEN))
            {
                Instantiate(aw_SuperShuriken.transform, grid_AvailableWeapons.transform);
            }
            if (child.name.Contains(NAME_TRIPLESHURIKEN))
            {
                Instantiate(aw_TripleShuriken.transform, grid_AvailableWeapons.transform);
            }

            if (child.tag == "enemy")
            {
                totalEnemyCount++;
            }

        }

        if (countCurrentWeapon() < 5)
        {
            showAddMoreButton();

        }



        countShootDelay = shootDelay;
        totalWeapon = countCurrentWeapon();
        weaponThrowed = new List<GameObject>();



        playerPosition = GameObject.FindGameObjectWithTag("playerPosition").transform;

        playerGround = playerPosition.Find("ground");

        playerObject = Instantiate(playerPrefab, playerGround.position, Quaternion.identity) as GameObject;
        if (playerPosition.gameObject.name == "playerPositionFaceLeft")
        {
            playerObject.GetComponent<PlayerController>().faceLeft();
        }

        playerObject.GetComponent<PlayerController>().updateSkin();

        updateItemForPlayerAbility();

        showLevelInfo();
        //AudioManager.instance.stopMusic();
        AudioManager.instance.playLevelStartSound();


        SkeletonAnimation playerSkeletonAnimation = playerObject.GetComponent<SkeletonAnimation>();
        playerHeadBone = playerSkeletonAnimation.Skeleton.FindBone("Control_me");
        originalHeadPosition = playerHeadBone.GetLocalPosition();
        playerSkeletonAnimation.UpdateWorld += SkeletonAnimation_UpdateLocal;
        //	for (int i = 0; i <	skeletonAnimation.Skeleton.bones.Count; i++) {
        //		Debug.Log (""+ 	skeletonAnimation.Skeleton.bones.Items[i]);
        //	}
        //GoogleAdmob.bannerView.Hide();




        originalDialogPos = new Vector3(50, 0, 0);
        Debug.Log("originalDialogPos " + originalDialogPos);
        aw_AddMore.SetActive(false);
    }

    void SkeletonAnimation_UpdateLocal(ISkeletonAnimation animated)
    {
        if (aimPoint.activeSelf)
        {
            var localPositon = playerObject.transform.InverseTransformPoint(aimPoint.transform.position);
            //	Debug.Log ("BONE: " + localPositon);
            playerHeadBone.SetPositionSkeletonSpace(localPositon);

        }
        else
        {

            Invoke("setOriginalHead", 0.4f);
        }


    }
    void setOriginalHead()
    {

        playerHeadBone.SetPosition(originalHeadPosition);
        CancelInvoke("setOriginalHead");
    }
    public void updatePlayerWinLoseDialogSkin()
    {
        SkeletonAnimation playerAnim = playerDialogAnim.GetComponent<SkeletonAnimation>();
        if (GameConstant.getStatusGolden() == 1)
        {
            //skin5
            playerAnim.skeleton.SetSkin("Skin5");
            playerAnim.skeleton.SetSlotsToSetupPose();
            playerAnim.AnimationState.Apply(playerAnim.skeleton);
        }
        else if (GameConstant.getStatusPowerful() == 1)
        {
            //skin3
            playerAnim.skeleton.SetSkin("Skin3");
            playerAnim.skeleton.SetSlotsToSetupPose();
            playerAnim.AnimationState.Apply(playerAnim.skeleton);
        }
        else if (GameConstant.getStatusGhost() == 1)
        {
            //skin2
            playerAnim.skeleton.SetSkin("Skin2");
            playerAnim.skeleton.SetSlotsToSetupPose();
            playerAnim.AnimationState.Apply(playerAnim.skeleton);
        }
        else if (GameConstant.getStatusBomb() == 1)
        {
            //skin4
            playerAnim.skeleton.SetSkin("Skin4");
            playerAnim.skeleton.SetSlotsToSetupPose();
            playerAnim.AnimationState.Apply(playerAnim.skeleton);
        }
        else
        {
            //skin1
            playerAnim.skeleton.SetSkin("Skin1");
            playerAnim.skeleton.SetSlotsToSetupPose();
            playerAnim.AnimationState.Apply(playerAnim.skeleton);

        }
    }


    public void updateItemForPlayerAbility()
    {
        foreach (Transform child in parentGameObject.transform)
        {
            //Debug.Log(""+child.tag);
            if (child.tag == "metalBox" || child.tag == "spinWoodenBar" || child.tag == "woodenBar" || child.tag == "woodenBox")
            {
                child.gameObject.layer = LayerMask.NameToLayer("LevelItems");
            }
        }
        switch (playerObject.GetComponent<PlayerController>().playerAbility)
        {

            case PlayerController.PlayerAbility.GHOST:
                currentAbility = PlayerController.PlayerAbility.GHOST;
                foreach (Transform child in parentGameObject.transform)
                {
                    //	Debug.Log(""+child.tag);
                    if (child.tag == "metalBox" || child.tag == "spinWoodenBar" || child.tag == "woodenBar" || child.tag == "woodenBox")
                    {
                        child.gameObject.layer = LayerMask.NameToLayer("IgnoreShuriken");
                    }
                }
                break;
            case PlayerController.PlayerAbility.GOLDEN:
                currentAbility = PlayerController.PlayerAbility.GOLDEN;

                break;

            case PlayerController.PlayerAbility.POWERFUL:
                currentAbility = PlayerController.PlayerAbility.POWERFUL;

                break;

            case PlayerController.PlayerAbility.BOMB:
                currentAbility = PlayerController.PlayerAbility.BOMB;

                break;

            case PlayerController.PlayerAbility.NORMAL:
                currentAbility = PlayerController.PlayerAbility.NORMAL;

                break;


        }
    }

    void initPlayer()
    {

    }
    void showLevelInfo()
    {
        dialogLevelInfo.SetActive(true);
        dialogLevelInfoLevel.GetComponent<UILabel>().text = " " + currentWorld + "-" + currentLevel;//关卡
        dialogLevelInfoScore.GetComponent<UILabel>().text = "" + GameConstant.getLevelHighScore(currentLevel);
        int levelStatus;
        levelStatus = GameConstant.getLevelStatus(LevelSelectController.selectedLevel);

        if (levelStatus == 3)
        {
            dialogLevelInfoStars.GetComponent<UISprite>().spriteName = "3star";
        }
        else if (levelStatus == 2)
        {
            dialogLevelInfoStars.GetComponent<UISprite>().spriteName = "2star";
        }
        else if (levelStatus == 1)
        {
            dialogLevelInfoStars.GetComponent<UISprite>().spriteName = "1star";
        }
        else if (levelStatus == 0)
        {
            dialogLevelInfoStars.GetComponent<UISprite>().spriteName = "0star";
        }
        //dialogLevelInfoStars

    }
    public void startGame()
    {
        dialogLevelInfo.SetActive(false);
        isStarted = true;
        if (currentLevel == 1 && GameConstant.canShowTut1())
        {
            tutorialLv1.SetActive(true);
            //btSkip.SetActive(true);
            isSwitch = true;

        }

        //if (currentLevel == 2 && GameConstant.canShowTut2())
        //{
        //    tutorialLv2a.SetActive(true);
        //    Time.timeScale = 0f;
        //    btSkip.SetActive(true);
        //}
        //&& LevelSelectController.selectedLevel/10==0
        //if (GameConstant.canShowRate() && LevelSelectController.selectedLevel % 10 == 0 && GameConstant.canShowRateServer())
        //{
        //    showDialogRateGame();
        //}
    }
    public void skipTutorial()
    {
        if (LevelSelectController.selectedLevel == 1)
        {
            tutorialLv1.SetActive(false);
            GameConstant.hideTut1();
        }
        else if (LevelSelectController.selectedLevel == 2)
        {
            tutorialLv2a.SetActive(false);
            tutorialLv2b.SetActive(false);
            Time.timeScale = 1f;
            GameConstant.hideTut2();
        }

        btSkip.SetActive(false);

    }
    public void hideTutorialShop1()
    {
        tutorialLv2a.SetActive(false);
        if (GameConstant.getDartCount() == 0)
        {
            tutorialLv2b.SetActive(true);
        }
        else
        {
            tutorial2d.SetActive(true);
        }

        btSkip.SetActive(false);

    }
    public void hideTutorialShop2()
    {
        tutorialLv2b.SetActive(false);
        btSkip.SetActive(false);

        tutorial2c.SetActive(true);
        //      GameConstant.hideTut2();
    }
    public void hideTutorialShop2c()
    {
        tutorial2c.SetActive(false);
        if (GameConstant.getDartCount() > 0)
            tutorial2d.SetActive(true);
        else
            GameConstant.hideTut2();
    }
    public void hideTutorialShop2d()
    {
        tutorial2d.SetActive(false);
        GameConstant.hideTut2();
    }

    bool hasWeapon()
    {

        if (grid_AvailableWeapons.GetChildList().Count > 1) return true;
        else return false;
    }
    int countCurrentWeapon()
    {
        int count = 0;
        for (int i = 0; i < grid_AvailableWeapons.GetChildList().Count; i++)
        {
            if (grid_AvailableWeapons.GetChildList()[i].tag == "uiweapon")
                count++;
        }
        return count;
    }
    bool hasAddMoreButton()
    {
        for (int i = 0; i < grid_AvailableWeapons.GetChildList().Count; i++)
        {
            if (grid_AvailableWeapons.GetChildList()[i].tag == "buttons")
            {
                return true;
            }
        }
        return false;
    }
    void removeAddMoreButton()
    {
        for (int i = 0; i < grid_AvailableWeapons.GetChildList().Count; i++)
        {
            if (grid_AvailableWeapons.GetChildList()[i].tag == "buttons")
            {
                NGUITools.DestroyImmediate(grid_AvailableWeapons.GetChildList()[i].gameObject);
            }
        }

    }
    void showAddMoreButton()
    {
        if (!hasAddMoreButton())
        {
            Instantiate(aw_AddMore.transform, grid_AvailableWeapons.transform);
            Invoke("gridReposition", 0.1f);
        }
    }
    bool canAddWeapon()
    {
        if (countCurrentWeapon() < 5) return true;
        else return false;
    }
    void removeOneWeapon()
    {
        Debug.Log("countCurrentWeapon1 " + countCurrentWeapon());
        if (countCurrentWeapon() >= 1)
        {
            grid_AvailableWeapons.RemoveChild(grid_AvailableWeapons.GetChild(0));
            grid_AvailableWeapons.GetChild(0).gameObject.tag = "Untagged";
            NGUITools.DestroyImmediate(grid_AvailableWeapons.GetChild(0).gameObject);
            grid_AvailableWeapons.repositionNow = true;
            Debug.Log("countCurrentWeapon2 " + countCurrentWeapon());
            if (countCurrentWeapon() <= 0)
            {
                InvokeRepeating("checkGameResult", 3f, 3f);
            }
            showAddMoreButton();

        }
        else
        {


        }
    }
    void setCurrentWeaponType()
    {
        if (grid_AvailableWeapons.GetChildList().Count > 1)
            currentWeaponType = grid_AvailableWeapons.GetChild(0).GetComponent<Weapon>().weaponType;
    }
    bool isTouchButtons()
    {

        return UICamera.hoveredObject.tag == "buttons";
    }
    // Update is called once per frame
    Vector3 originalDialogPos;
    public float shakeDialogAmount;

    public GameObject btNextLv;
    public bool isSwitch { get; set; }
    IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(0.3f);
        isSwitch = false;
    }
    void Update()
    {
        if (tutorialLv1.activeSelf)
        {
            foreach (KeyCode key in _values)
            {
                if (Input.GetKeyDown(key))
                {
                    tutorialLv1.SetActive(false);
                    StartCoroutine(SetDelay());
                }
            }
        }
        //Debug.Log(UICamera.selectedObject);
        if (dialogNextLevel.activeSelf)
            UICamera.selectedObject = btNextLv;
        btNextLv.transform.GetChild(1).gameObject.SetActive(UICamera.selectedObject == btNextLv);
        grid_AvailableWeapons.transform.GetChild(grid_AvailableWeapons.transform.childCount - 1).gameObject.SetActive(false);
        foreach (var t in dialogPauseButton)
        {
            t.transform.GetChild(0).gameObject.SetActive(UICamera.selectedObject == t);
        }

        foreach (var variable in dialogResultButton)
        {
            variable.transform.GetChild(0).gameObject.SetActive(UICamera.selectedObject == variable);
        }

        if (!dialogResultButton[0].transform.GetChild(0).gameObject.activeSelf && !dialogResultButton[1].transform
                                                                                   .GetChild(0).gameObject.activeSelf
                                                                               && !dialogResultButton[2].transform
                                                                                   .GetChild(0).gameObject.activeSelf
            && dialogResultButton[2].activeSelf && !dialogPause.activeSelf && !dialogShop.activeSelf && dialogWinLose.activeSelf)
        {
            UICamera.selectedObject = dialogResultButton[2];
        }

        if (!dialogResultButton[0].transform.GetChild(0).gameObject.activeSelf && !dialogResultButton[1].transform
                                                                                   .GetChild(0).gameObject.activeSelf
                                                                               && !dialogResultButton[2].transform
                                                                                   .GetChild(0).gameObject.activeSelf
            && !dialogResultButton[2].activeSelf && !dialogPause.activeSelf && !dialogShop.activeSelf && dialogWinLose.activeSelf)
        {
            UICamera.selectedObject = dialogResultButton[1];
        }

        if (!dialogPauseButton[0].transform.GetChild(0).gameObject.activeSelf && !dialogPauseButton[1].transform
                                                                                  .GetChild(0).gameObject.activeSelf
                                                                              && !dialogPauseButton[2].transform
                                                                                  .GetChild(0).gameObject.activeSelf &&
                                                                              !dialogPauseButton[3].transform
                                                                                  .GetChild(0).gameObject.activeSelf &&
                                                                              dialogPause.activeSelf &&
                                                                              !dialogShop.activeSelf)
        {
            UICamera.selectedObject = dialogPauseButton[3];
        }
        if (Input.GetButtonDown("Cancel") && !dialogShop.activeSelf && !dialogWinLose.activeSelf && !dialogLevelInfo.activeSelf)
            pause();
        if (Input.GetButtonDown("Cancel") && dialogShop.activeSelf)
        {
            dialogShop.GetComponent<ShopController>().closeShop();
            resume();
            //UICamera.selectedObject = dialogShopBtn;
        }

        if (goMouse.transform.position.x > 5)
        {
            goMouse.transform.position = new Vector3(5, goMouse.transform.position.y, goMouse.transform.position.z);
        }
        if (goMouse.transform.position.x < -5)
        {
            goMouse.transform.position = new Vector3(-5, goMouse.transform.position.y, goMouse.transform.position.z);
        }
        if (goMouse.transform.position.y > 3)
        {
            goMouse.transform.position = new Vector3(goMouse.transform.position.x, 3, goMouse.transform.position.z);
        }
        if (goMouse.transform.position.y < -2.5)
        {
            goMouse.transform.position = new Vector3(goMouse.transform.position.x, -2.5f, goMouse.transform.position.z);
        }
        aimPoint.transform.position = goMouse.transform.position;
        rotateAimSign();
        if (aimPoint.transform.position.x > playerPosition.position.x)
        {

            playerObject.GetComponent<PlayerController>().faceRight();

        }
        else
        {
            playerObject.GetComponent<PlayerController>().faceLeft();
        }

        if (goMouse.transform.position.x >= 4f && goMouse.transform.position.x <= 4.3f && goMouse.transform.position.y >= -3.2f && goMouse.transform.position.y <= -3.1f)
        {
            isBtnClick = true;
        }
        else
        {
            isBtnClick = false;
        }
        countShootDelay += Time.deltaTime;
        timePlayed += Time.deltaTime;
        if (shakeDialogTime > 0)
        {
            if (shakeDialogTime > 0)
            {
                dialogWinLose.transform.localPosition = originalDialogPos + UnityEngine.Random.insideUnitSphere * shakeDialogAmount;

                shakeDialogTime -= Time.unscaledDeltaTime;
            }
            else
            {

                shakeDialogTime = 0f;
                dialogWinLose.transform.localPosition = originalDialogPos;
            }
        }
        //if ((Input.GetMouseButtonDown(0)) && !isTouchButtons() && canThrowWeapon())
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)) && !isTouchButtons() && canThrowWeapon() && !isBtnClick && isSwitch == false)
        {

            isTouchedDown = true;
            aimSign.SetActive(true);
            aimPoint.SetActive(true);
            //goMouse.SetActive(false);
            rotateAimSign();
            checkPlayerFacingDirection();
            //Debug.Log("hasWeapon "+ grid_AvailableWeapons.GetChildList().Count);

        }
        //else if ((Input.GetMouseButtonUp(0)) && !isTouchButtons() && canThrowWeapon() && isTouchedDown)
        else if ((Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.JoystickButton0)) && !isTouchButtons() && canThrowWeapon() && isTouchedDown && !isBtnClick && isSwitch == false)
        {
            goMouse.transform.position = aimPoint.transform.position;
            isTouchedDown = false;
            setCurrentWeaponType();
            aimSign.SetActive(false);
            aimPoint.SetActive(false);
            //goMouse.SetActive(true);
            //aimSign.transform.position = objectHidePos.position;
            //aimPoint.transform.position = objectHidePos.position;

            if (countCurrentWeapon() > 0 && countShootDelay >= shootDelay && hasWeapon())
            {
                countShootDelay = 0;
                switch (currentWeaponType)
                {
                    case Weapon.WEAPON_TYPE.SHURIKEN:
                        throwShuriken();
                        break;
                    case Weapon.WEAPON_TYPE.DYNAMITE:
                        throwDynamite();
                        break;
                    case Weapon.WEAPON_TYPE.SUPER_SHURIKEN:
                        throwSuperShuriken();
                        break;
                    case Weapon.WEAPON_TYPE.TRIPLE_SHURIKEN:
                        throw3Shuriken();
                        break;
                }
                if (currentLevel == 1 && tutorialLv1.activeSelf)
                {
                    tutorialLv1.SetActive(false);
                    btSkip.SetActive(false);
                    GameConstant.hideTut1();
                }
                removeOneWeapon();
                AudioManager.instance.playSound(AudioManager.instance.soundRoar);
                AudioManager.instance.playThrowSound();


            }


        }
        else if (Input.GetKey(KeyCode.Space) && isBtnClick)
        {
            showShop();
        }
        else if (Input.GetMouseButton(0) && !isTouchButtons() && canThrowWeapon())
        {


            rotateAimSign();
            checkPlayerFacingDirection();

        }
        if (isWin && durationScoreTime > 0)
        {
            startAddScore();
        }

    }
    float lerpScoreTime = 0f, durationScoreTime = 0f;
    int scoreToShow = 0, goldToShow = 0;
    public void startAddScore()
    {
        //   print("" + lerpScoreTime);

        lerpScoreTime += Time.unscaledDeltaTime / durationScoreTime;
        scoreToShow = (int)Mathf.Lerp(scoreToShow, totalScore, lerpScoreTime);
        goldToShow = (int)Mathf.Lerp(goldToShow, totalGold, lerpScoreTime);
        if (scoreToShow >= totalScore - 1)
        {
            durationScoreTime = 0;
            scoreToShow = totalScore;
            goldToShow = totalGold;
        }

        lblEndGameScore.text = " " + scoreToShow.ToString();//分数
        dialogWinGoldAmount.GetComponent<UILabel>().text = "+ " + goldToShow.ToString();

    }
    void checkPlayerFacingDirection()
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 touchPos = new Vector3(wp.x, wp.y, 0);
        if (touchPos.x > playerPosition.position.x)
        {

            playerObject.GetComponent<PlayerController>().faceRight();

        }
        else
        {
            playerObject.GetComponent<PlayerController>().faceLeft();
        }

    }
    public void nextLevel()
    {
        if (LevelSelectController.selectedLevel < 240)
        {

            if (LevelSelectController.selectedLevel == 60)
            {
                if (GameConstant.countStarStage1() > STAR_TO_UNLOCK)
                {
                    GameConstant.setStageStatus(2, 0);
                    LevelSelectController.selectedLevel++;
                    if (GameConstant.getLastPlayedLevel() < LevelSelectController.selectedLevel)
                        GameConstant.setLastPlayedLevel(GameConstant.getLastPlayedLevel() + 1);
                    StageSelectController.selectedWorld++;
                    reloadScene();
                }
                else
                {
                    notEnoughStars();
                }
            }
            else if (LevelSelectController.selectedLevel == 120)
            {
                if (GameConstant.countStarStage2() > STAR_TO_UNLOCK)
                {
                    GameConstant.setStageStatus(3, 0);
                    LevelSelectController.selectedLevel++;
                    if (GameConstant.getLastPlayedLevel() < LevelSelectController.selectedLevel)
                        GameConstant.setLastPlayedLevel(GameConstant.getLastPlayedLevel() + 1);
                    StageSelectController.selectedWorld++;
                    reloadScene();
                }
                else
                {
                    notEnoughStars();
                }
            }
            else if (LevelSelectController.selectedLevel == 180)
            {
                if (GameConstant.countStarStage3() > STAR_TO_UNLOCK)
                {
                    GameConstant.setStageStatus(4, 0);
                    LevelSelectController.selectedLevel++;
                    if (GameConstant.getLastPlayedLevel() < LevelSelectController.selectedLevel)
                        GameConstant.setLastPlayedLevel(GameConstant.getLastPlayedLevel() + 1);
                    StageSelectController.selectedWorld++;
                    reloadScene();
                }
                else
                {
                    notEnoughStars();
                }
            }
            else
            {

                reloadScene();
                LevelSelectController.selectedLevel++;
                if (GameConstant.getLastPlayedLevel() < LevelSelectController.selectedLevel)
                    GameConstant.setLastPlayedLevel(GameConstant.getLastPlayedLevel() + 1);
            }

            //

        }
        else
        {
            toLevelSelect();
        }


    }

    /// <summary>
    /// 通关不点击下一关直接返回关卡选择
    /// </summary>
    public void nextLevel2()
    {
        if (LevelSelectController.selectedLevel < 240)
        {

            if (LevelSelectController.selectedLevel == 60)
            {
                if (GameConstant.countStarStage1() > STAR_TO_UNLOCK)
                {
                    GameConstant.setStageStatus(2, 0);
                    LevelSelectController.selectedLevel++;
                    if (GameConstant.getLastPlayedLevel() < LevelSelectController.selectedLevel)
                        GameConstant.setLastPlayedLevel(GameConstant.getLastPlayedLevel() + 1);
                    StageSelectController.selectedWorld++;
                    toLevelSelect();
                }
                else
                {
                    notEnoughStars();
                }
            }
            else if (LevelSelectController.selectedLevel == 120)
            {
                if (GameConstant.countStarStage2() > STAR_TO_UNLOCK)
                {
                    GameConstant.setStageStatus(3, 0);
                    LevelSelectController.selectedLevel++;
                    if (GameConstant.getLastPlayedLevel() < LevelSelectController.selectedLevel)
                        GameConstant.setLastPlayedLevel(GameConstant.getLastPlayedLevel() + 1);
                    StageSelectController.selectedWorld++;
                    toLevelSelect();
                }
                else
                {
                    notEnoughStars();
                }
            }
            else if (LevelSelectController.selectedLevel == 180)
            {
                if (GameConstant.countStarStage3() > STAR_TO_UNLOCK)
                {
                    GameConstant.setStageStatus(4, 0);
                    LevelSelectController.selectedLevel++;
                    if (GameConstant.getLastPlayedLevel() < LevelSelectController.selectedLevel)
                        GameConstant.setLastPlayedLevel(GameConstant.getLastPlayedLevel() + 1);
                    StageSelectController.selectedWorld++;
                    toLevelSelect();
                }
                else
                {
                    notEnoughStars();
                }
            }
            else
            {
                LevelSelectController.selectedLevel++;
                if (GameConstant.getLastPlayedLevel() < LevelSelectController.selectedLevel)
                    GameConstant.setLastPlayedLevel(GameConstant.getLastPlayedLevel() + 1);
                toLevelSelect();

            }

            //

        }
        else
        {
            toLevelSelect();
        }


    }


    public GameObject dialogNextLevel;
    void notEnoughStars()
    {
        dialogWinLose.SetActive(false);
        dialogNextLevel.SetActive(true);
    }
    void calculateStars()
    {
        foreach (Transform child in parentGameObject.transform)
        {
            if (child.tag == "enemy")
            {
                if (child.gameObject.GetComponent<StandEnemy>() != null)
                {
                    if (child.gameObject.GetComponent<StandEnemy>().enemyState == StandEnemy.State.DEAD)
                    {
                        continue;
                    }

                }
                else if (child.gameObject.GetComponent<FlyEnemy>() != null)
                {
                    if (child.gameObject.GetComponent<FlyEnemy>().currentState == FlyEnemy.State.DEAD)
                    {
                        continue;
                    }
                }
                winGameEnemyCount++;
            }
        }
        if (totalScore > GameConstant.getLevelHighScore(currentLevel))
            GameConstant.setLevelHighScore(currentLevel, totalScore);
        if (totalEnemyCount != 0)
        {
            if (winGameEnemyCount == 0)
            {
                starCount = 3;
                dialogWinStar1.SetActive(true);

            }
            else if (winGameEnemyCount == 1)
            {
                starCount = 2;
                dialogWinStar1.SetActive(true);

            }
            else if (winGameEnemyCount >= 2)
            {
                starCount = 1;
                dialogWinStar1.SetActive(true);

            }
        }
        else if (totalEnemyCount == 0)
        {
            if (countCurrentWeapon() >= totalWeapon - 1)
            {
                starCount = 3;
                dialogWinStar1.SetActive(true);

            }
            else if (countCurrentWeapon() >= totalWeapon - 2)
            {
                starCount = 2;
                dialogWinStar1.SetActive(true);

            }
            else if (countCurrentWeapon() >= totalWeapon - 3)
            {
                starCount = 1;
                dialogWinStar1.SetActive(true);

            }

        }


        if (starCount > GameConstant.getLevelStatus(LevelSelectController.selectedLevel))
            GameConstant.setLevelStatus(LevelSelectController.selectedLevel, starCount);

        // Unlock stage moi va level moi
        if (LevelSelectController.selectedLevel < 240)
        {
            if (GameConstant.getLevelStatus(LevelSelectController.selectedLevel + 1) == -1)
                GameConstant.setLevelStatus(LevelSelectController.selectedLevel + 1, 0);

            if (LevelSelectController.selectedLevel == 60)
            {
                if (GameConstant.countStarStage1() > STAR_TO_UNLOCK)
                {
                    GameConstant.setStageStatus(2, 0);
                }
            }
            else if (LevelSelectController.selectedLevel == 120)
            {
                if (GameConstant.countStarStage2() > STAR_TO_UNLOCK)
                {
                    GameConstant.setStageStatus(3, 0);
                }
            }
            else if (LevelSelectController.selectedLevel == 180)
            {
                if (GameConstant.countStarStage3() > STAR_TO_UNLOCK)
                {
                    GameConstant.setStageStatus(4, 0);
                }
            }

        }

        //     Firebase.Analytics.FirebaseAnalytics.LogEvent("StarCount", "level"+LevelSelectController.selectedLevel, starCount);

    }

    public void winStar1Done()
    {
        //   Debug.Log("starCount  "+starCount);
        eSpark1.SetActive(true);
        if (starCount >= 2)
        {
            dialogWinStar2.SetActive(true);
        }
    }
    public void winStar2Done()
    {
        eSpark2.SetActive(true);
        if (starCount >= 3)
        {
            dialogWinStar3.SetActive(true);
        }
    }
    public void winStar3Done()
    {
        eSpark3.SetActive(true);

    }
    int starCount = 0;
    public void win()
    {
        isWin = true;
        CancelInvoke("checkGameResult");
        //playerObject.GetComponent<SkeletonAnimation> ().ignoreTimeScale = true;
        playerObject.GetComponent<PlayerController>().winAnimation();

        for (int i = 0; i < grid_AvailableWeapons.GetChildList().Count; i++)
        {
            if (grid_AvailableWeapons.GetChildList()[i].tag != "buttons")
            {
                addScore(1000);
                GameObject scoreObj = grid_AvailableWeapons.GetChild(i).transform.Find("ngui1000").gameObject;
                scoreObj.GetComponent<TweenAlpha>().delay = 0.3f * i;
                scoreObj.GetComponent<TweenPosition>().delay = 0.3f * i;
                scoreObj.SetActive(true);
            }

        }



        Invoke("showDialogWin", 2f);
        // Luu so sao


    }
    void showDialogWin()
    {

        CancelInvoke("showDialogWin");
        Time.timeScale = 0f;
        dialogWinLose.SetActive(true);
        calculateStars();
        if (totalGold == 0)
            totalGold = 30;
        else
            totalGold *= 6;
        //totalGold = 300000;
        dialogWinGoldAmount.GetComponent<UILabel>().text = "+ " + totalGold;
        GameConstant.addGold(totalGold);
        durationScoreTime = 2f;
        lblEndGameScore.text = " " + totalScore;//分数
        endGameTitle.spriteName = "text_win";
        dialogWinLoseNextButton.SetActive(true);
        dialogWinLose.GetComponent<TweenScale>().ResetToBeginning();
        dialogWinLose.GetComponent<TweenScale>().PlayForward();
        AudioManager.instance.playSoundWin();
        updatePlayerWinLoseDialogSkin();
        playerDialogAnim.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "Win-anim", true);
        //   showAdmob();
        //   Invoke("showAdmob",0.5f);
        UICamera.selectedObject = dialogResultButton[2];
    }
    /* 
      public  void showAdmob(){
          if (GameConstant.isRemoveAd() == 0 && currentLevel>2)
          {
              Debug.Log("Show Admob Out");

               GoogleAdmob.ShowInterstitial();


          }
      }*/
    public void addGold(int amount)
    {
        totalGold += amount;

    }
    bool canThrowWeapon()
    {

        return !isWin && isStarted && !isOpenShop && !isPaused && !isGameOver;
    }
    public void showDialogGameOver()
    {
        Time.timeScale = 0f;

        dialogWinLose.SetActive(true);
        dialogWinVideoAd.SetActive(false);
        dialogWinLoseNextButton.SetActive(false);
        lblEndGameScore.text = " " + totalScore;//分数
        endGameTitle.spriteName = "text_lose";
        dialogWinLose.GetComponent<TweenScale>().ResetToBeginning();
        dialogWinLose.GetComponent<TweenScale>().PlayForward();
        AudioManager.instance.playsoundLose();
        Debug.Log("Game Over");
        updatePlayerWinLoseDialogSkin();
        playerDialogAnim.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "lose-anim", true);
        UICamera.selectedObject = dialogResultButton[1];
    }
    public void gameOver()
    {
        isGameOver = true;
        CancelInvoke("checkGameResult");
        playerObject.GetComponent<PlayerController>().canAnimateIdle = false;
        playerObject.GetComponent<PlayerController>().loseAnimation();
        GameObject[] hostageList = GameObject.FindGameObjectsWithTag("hostage");
        for (int i = 0; i < hostageList.Length; i++)
        {
            hostageList[i].GetComponent<Hostage>().loseAnimation();
        }

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i].GetComponent<StandEnemy>() != null)
            {
                enemyList[i].GetComponent<StandEnemy>().stopMoving();
                enemyList[i].GetComponent<StandEnemy>().winAnimation();

            }

            if (enemyList[i].GetComponent<FlyEnemy>() != null)
            {
                enemyList[i].GetComponent<FlyEnemy>().stopMoving();
                enemyList[i].GetComponent<FlyEnemy>().winAnimation();
            }

        }


        Invoke("showDialogGameOver", 2f);
        //  showAdmob();
        // Invoke("showAdmob", 0.5f);


    }
    public void gameoverShowAdMob()
    {
        originalDialogPos = dialogWinLose.transform.localPosition;
        //  if (isGameOver) showAdmob();
    }
    void throwShuriken()
    {

        GameObject shuriken = Instantiate(playerShuriken, playerPosition.position, aimSign.transform.rotation) as GameObject;
        weaponThrowed.Add(shuriken);
        shuriken.GetComponent<PlayerShuriken>().fly(aimDir.normalized);
        playerObject.GetComponent<PlayerController>().throwAnimation();

    }
    void throwDynamite()
    {

        GameObject dynamite = Instantiate(playerDynamite, playerPosition.position, aimSign.transform.rotation) as GameObject;
        weaponThrowed.Add(dynamite);
        dynamite.GetComponent<PlayerDynamite>().fly(aimDir.normalized);
        playerObject.GetComponent<PlayerController>().throwAnimation();

    }
    void throw3Shuriken()
    {


        GameObject shuriken1 = Instantiate(playerShuriken, playerPosition.position, aimSign.transform.rotation) as GameObject;
        GameObject shuriken2 = Instantiate(playerShuriken, playerPosition.position, aimSign.transform.rotation) as GameObject;
        GameObject shuriken3 = Instantiate(playerShuriken, playerPosition.position, aimSign.transform.rotation) as GameObject;
        weaponThrowed.Add(shuriken1);
        weaponThrowed.Add(shuriken2);
        weaponThrowed.Add(shuriken3);

        Vector3 newDirection = Quaternion.AngleAxis(_3ShurikenAngle, Vector3.forward) * aimDir;
        Vector3 newDirection2 = Quaternion.AngleAxis(-_3ShurikenAngle, Vector3.forward) * aimDir;

        shuriken1.GetComponent<PlayerShuriken>().fly(aimDir.normalized);
        shuriken2.GetComponent<PlayerShuriken>().fly(newDirection);
        shuriken3.GetComponent<PlayerShuriken>().fly(newDirection2);

        playerObject.GetComponent<PlayerController>().throwAnimation();

    }

    void throwSuperShuriken()
    {

        GameObject superShuriken = Instantiate(playerSuperShuriken, playerPosition.position, aimSign.transform.rotation) as GameObject;
        weaponThrowed.Add(superShuriken);
        superShuriken.GetComponent<PlayerSuperShuriken>().fly(aimDir.normalized);
        playerObject.GetComponent<PlayerController>().throwAnimation();



    }
    bool isAllHostagesRescued()
    {
        GameObject[] hostageList = GameObject.FindGameObjectsWithTag("hostage");
        if (hostageList.Length <= 0)
        {
            return true;
        }
        else return false;
    }
    public void checkWin()
    {

        if (isAllHostagesRescued())
        {
            win();
        }
    }

    public void checkGameResult()
    {
        //GameObject[] shurikenList=  GameObject.FindGameObjectsWithTag("playerShuriken");
        //	GameObject[] dynamiteList=  GameObject.FindGameObjectsWithTag("playerDynamite");
        //	GameObject[] superShurikenList=  GameObject.FindGameObjectsWithTag("playerSuperShuriken");
        Debug.Log("checkGameResult  <<<<<<<    ");
        for (int i = 0; i < weaponThrowed.Count; i++)
        {

            if (weaponThrowed[i] != null) return;
        }
        if (!isAllHostagesRescued())
        {
            gameOver();
        }
        else
        {
            win();
        }


    }
    void rotateAimSign()
    {
        //Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 touchPos = new Vector3(wp.x, wp.y, 0);
        Vector3 touchPos = new Vector3(goMouse.transform.position.x, goMouse.transform.position.y, 0);
        aimSign.transform.position = playerPosition.position;
        aimPoint.transform.position = touchPos;

        aimDir = aimPoint.transform.position - aimSign.transform.position;
        float rotationZ = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        aimSign.transform.rotation = Quaternion.AngleAxis(rotationZ, Vector3.forward);





    }

    public void showDialogRateGame()
    {
        dialogRateGame.SetActive(true);

    }

    public void rateGameOk()
    {
        GameConstant.disableShowRate();
        dialogRateGame.SetActive(false);
        Application.OpenURL(rateGameLink);

    }

    public void rateGameCancel()
    {
        dialogRateGame.SetActive(false);
    }


    public static void reloadScene()
    {


        SceneManager.LoadScene("GamePlay");
    }
    public void addScore(int score)
    {
        totalScore += score;
        lblTotalScore.text = " " + totalScore;
        Debug.Log("score to add  " + score);

    }
    public void showGoldOnScene(int gold, Vector3 position)
    {
        GameObject scoreObj = Instantiate(goldUIPrefab, new Vector3(position.x, position.y - 0.5f, position.z), Quaternion.identity) as GameObject;
        scoreObj.GetComponent<ScoreText>().setPosition();
    }
    public void showScoreOnScene(int score, Vector3 position)
    {
        //		Debug.Log("showScoreOnScene  "+ position);
        switch (score)
        {
            case 1000:
                GameObject scoreObj = Instantiate(score1000Prefab, position, Quaternion.identity) as GameObject;
                scoreObj.GetComponent<ScoreText>().setPosition();
                break;
            case 2000:
                GameObject scoreObj2 = Instantiate(score2000Prefab, position, Quaternion.identity) as GameObject;
                scoreObj2.GetComponent<ScoreText>().setPosition();
                break;
            case 3000:
                GameObject scoreObj3 = Instantiate(score3000Prefab, position, Quaternion.identity) as GameObject;
                scoreObj3.GetComponent<ScoreText>().setPosition();
                break;
            case 1200:
                GameObject scoreObj4 = Instantiate(score1200Prefab, position, Quaternion.identity) as GameObject;
                scoreObj4.GetComponent<ScoreText>().setPosition();
                break;
            case 2400:
                GameObject scoreObj5 = Instantiate(score2400Prefab, position, Quaternion.identity) as GameObject;
                scoreObj5.GetComponent<ScoreText>().setPosition();
                break;
            case 3600:
                GameObject scoreObj6 = Instantiate(score3600Prefab, position, Quaternion.identity) as GameObject;
                scoreObj6.GetComponent<ScoreText>().setPosition();
                break;
        }

    }


    public void replay()
    {
        AudioManager.instance.playClickAtMainMenuSound();
        GameController.reloadScene();


    }



    public void pause()
    {
        AudioManager.instance.playClickAtMainMenuSound();
        isPaused = true;
        dialogPause.SetActive(true);
        UICamera.selectedObject = dialogPauseButton[3];
        dialogPause.GetComponent<TweenPosition>().PlayForward();
        Time.timeScale = 0f;


    }

    public void resume()
    {
        AudioManager.instance.playClickAtMainMenuSound();
        isPaused = false;
        Time.timeScale = 1f;
        UICamera.selectedObject = null;
        dialogPause.SetActive(false);
        dialogPause.GetComponent<TweenPosition>().ResetToBeginning();
    }


    public void toLevelSelect()
    {

        AudioManager.instance.playClickAtMainMenuSound();
        SceneManager.LoadScene("LevelSelect");




    }
    public void showItemShop()
    {
        AudioManager.instance.playClickAtMainMenuSound();
        Time.timeScale = 0f;
        isOpenShop = true;
        dialogShop.SetActive(true);
        dialogShop.GetComponent<ShopController>().clickItemTab();
    }
    public void showShop()
    {
        AudioManager.instance.playClickAtMainMenuSound();
        Time.timeScale = 0f;
        isOpenShop = true;
        dialogShop.SetActive(true);
    }
    void gridReposition()
    {
        grid_AvailableWeapons.Reposition();
    }
    public void addDart()
    {

        if (hasAddMoreButton())
        {
            Debug.Log("addDart");
            removeAddMoreButton();
            GameObject newAmmo = Instantiate(aw_SingleShuriken.transform, grid_AvailableWeapons.transform).gameObject;
            newAmmo.transform.SetAsFirstSibling();
            if (canAddWeapon()) showAddMoreButton();
            Invoke("gridReposition", 0.1f);
            GameConstant.addDartCount(-1);
            dialogShop.GetComponent<ShopController>().closeShop();
        }

        /*

		if (child.name.Contains(NAME_DYNAMITE)){
			Instantiate(aw_Dynamite.transform,grid_AvailableWeapons.transform);
		}
		if (child.name.Contains(NAME_SHURIKEN)){
			
		}
		if (child.name.Contains(NAME_SUPERSHURIKEN)){
			Instantiate(aw_SuperShuriken.transform,grid_AvailableWeapons.transform);
		}
		if (child.name.Contains(NAME_TRIPLESHURIKEN)){
			Instantiate(aw_TripleShuriken.transform,grid_AvailableWeapons.transform);
		}
*/
    }
    public void addTrippleDart()
    {

        if (hasAddMoreButton())
        {
            Debug.Log("addTrippleDart");
            removeAddMoreButton();
            GameObject newAmmo = Instantiate(aw_TripleShuriken.transform, grid_AvailableWeapons.transform).gameObject;
            newAmmo.transform.SetAsFirstSibling();
            if (canAddWeapon()) showAddMoreButton();
            Invoke("gridReposition", 0.1f);
            GameConstant.addTrippleDartCount(-1);
            dialogShop.GetComponent<ShopController>().closeShop();
        }


    }
    public void addSupperDart()
    {

        if (hasAddMoreButton())
        {
            Debug.Log("addSupperDart");
            removeAddMoreButton();
            GameObject newAmmo = Instantiate(aw_SuperShuriken.transform, grid_AvailableWeapons.transform).gameObject;
            newAmmo.transform.SetAsFirstSibling();
            if (canAddWeapon()) showAddMoreButton();
            Invoke("gridReposition", 0.1f);
            GameConstant.addSuperDartCount(-1);
            dialogShop.GetComponent<ShopController>().closeShop();
        }


    }
    public void addBomb()
    {

        if (hasAddMoreButton())
        {
            Debug.Log("addBomb");
            removeAddMoreButton();
            GameObject newAmmo = Instantiate(aw_Dynamite.transform, grid_AvailableWeapons.transform).gameObject;
            newAmmo.transform.SetAsFirstSibling();
            if (canAddWeapon()) showAddMoreButton();
            Invoke("gridReposition", 0.1f);
            GameConstant.addBombCount(-1);
            dialogShop.GetComponent<ShopController>().closeShop();
        }


    }
    float shakeDialogTime = 0;
    public void shakeDialog()
    {
        AudioManager.instance.playSound(AudioManager.instance.soundStar);
        shakeDialogTime = 0.2f;

    }

    public void shakeCam()
    {
        // AudioManager.instance.playSound(AudioManager.instance.soundStar);
        GameObject.FindWithTag("MainCamera").GetComponent<CameraScript>().shakeCam();
        if (GameConstant.canVibrate())
            Handheld.Vibrate();
    }

    public static void vibrate()
    {

    }


    void OnEnable()
    {
        InAppPurchase.onRemoveAd += removeAd;
        InAppPurchase.onPurchasePack += purchasePack;

    }

    void OnDestroy()
    {
        InAppPurchase.onRemoveAd -= removeAd;
        InAppPurchase.onPurchasePack -= purchasePack;


    }

    void OnApplicationPause(bool pauseStatus)
    {
        Debug.Log("Paused = " + pauseStatus);
        if (pauseStatus)
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


    void rewardAdFailed()
    {

    }
    void onRewardVideo()
    {
        Debug.Log("onRewardGift Event Catched");
        durationScoreTime = 2f;
        goldToShow = totalGold;
        GameConstant.addGold(GameController.totalGold);
        totalGold = totalGold * 2;
        // dialogWinGoldAmount.GetComponent<UILabel>().text = "+ " + totalGold*2;
        dialogWinVideoAd.SetActive(false);


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

    }

    public void ShowRewardedVideo()
    {
        Debug.Log("Unity reward");
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
            dialogWinGoldAmount.GetComponent<UILabel>().text = "+ " + totalGold * 2;
            dialogWinVideoAd.SetActive(false);
            GameConstant.addGold(GameController.totalGold);
            GameConstant.countTimeShowAd = Time.realtimeSinceStartup;
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");
            GameConstant.countTimeShowAd = Time.realtimeSinceStartup;
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }

}
