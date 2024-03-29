using UnityEngine;
using System.Collections;

public class GameConstant
{
    public static float countTimeShowAd = 0f;
    public static bool DEBUG_MODE = true;
    public static string GOLD = "gold";
    public static string SELECTED_LEVEL = "selectedLevel";
   // public static string SELECTED_WORLD = "selectedWorld";
    public static string LEVEL_STATUS = "levelStatus";
    public static string LEVEL_HIGHSCORE = "levelHighScore";
    public static string LAST_PAGE_STAGE_1 = "lastPageStage1";
    public static string LAST_PAGE_STAGE_2 = "lastPageStage2";
    public static string LAST_PAGE_STAGE_3 = "lastPageStage3";
    public static string LAST_PAGE_STAGE_4 = "lastPageStage4";


    public static string TOTAL_STARS = "totalStars";

    public static string NINJA_GOLDEN = "NINJA_GOLDEN";
    public static string NINJA_GHOST = "NINJA_GHOST";
    public static string NINJA_BOMB = "NINJA_BOMB";
    public static string NINJA_POWERFULL = "NINJA_POWERFULL";

    public static string DART_COUNT = "DART_COUNT";
    public static string TRIPPLE_DART_COUNT = "TRIPPLE_DART_COUNT";
    public static string SUPER_DART_COUNT = "SUPER_DART_COUNT";
    public static string BOMB_COUNT = "BOMB_COUNT";

    public static string REMOVE_AD = "remove_ad";

    public static string SOUND = "sound";
    public static string MUSIC = "music";

    public static string STAGE_STATUS = "stageStatus";

    public static string DAILY_COUNT = "daily_count";

    public static string LAST_DAY_GET_DAILY = "last_day_daily";

    public static string KEY_BANNER = "key_banner";
    public static string KEY_FULL = "key_full";
    public static string KEY_REWARD = "key_reward";
    public static string KEY_SHOWRATE = "key_showrate";
	public static string KEY_SHOWRATE_SERVER = "key_showrate_server";

    public static string KEY_SHOW_BANNER = "key_showBanner";

    public static string kEY_HIDE_TUTORIAL_1 = "key_hide_tutorial1";
    public static string kEY_HIDE_TUTORIAL_2 = "key_hide_tutorial2";
    public static string KEY_CAN_VIBRATE = "key_can_vibrate";
    public static string KEY_PLAY_TIME = "key_play_time";
    public static string KEY_AD_DELAY_TIME = "key_ad_delay_time";
	public static string KEY_LEVEL_TO_SHOW_ADS = "key_level_to_show_ads";
	public static string KEY_SHOW_DAILY = "key_show_daily";


    public static int getDailyCount()
    {
        return PlayerPrefs.GetInt(DAILY_COUNT, 1);
    }

    public static void addDailyCount(int amount)
    {
        PlayerPrefs.SetInt(DAILY_COUNT, getDailyCount() + amount);
        PlayerPrefs.Save();
    }

    public static string getLastDayDaily()
    {
        return PlayerPrefs.GetString(LAST_DAY_GET_DAILY, "" + System.DateTime.Now);
    }

    public static void setLastDayDaily()
    {
        PlayerPrefs.SetString(LAST_DAY_GET_DAILY, "" + System.DateTime.Now);
        PlayerPrefs.Save();
    }

    public static int isRemoveAd()
    {
        return PlayerPrefs.GetInt(REMOVE_AD, 0);
    }
    public static void setRemoveAd(int status)
    {
        PlayerPrefs.SetInt(REMOVE_AD, status);
        PlayerPrefs.Save();
    }

    public static int getDartCount()
    {
        return PlayerPrefs.GetInt(DART_COUNT, 0);
    }
    public static void addDartCount(int amount)
    {
        PlayerPrefs.SetInt(DART_COUNT, getDartCount() + amount);
        PlayerPrefs.Save();
    }

    public static int getTrippleDartCount()
    {
        return PlayerPrefs.GetInt(TRIPPLE_DART_COUNT, 0);
    }
    public static void addTrippleDartCount(int amount)
    {
        PlayerPrefs.SetInt(TRIPPLE_DART_COUNT, getTrippleDartCount() + amount);
        PlayerPrefs.Save();
    }

    public static int getSuperDartCount()
    {
        return PlayerPrefs.GetInt(SUPER_DART_COUNT, 0);
    }
    public static void addSuperDartCount(int amount)
    {
        PlayerPrefs.SetInt(SUPER_DART_COUNT, getSuperDartCount() + amount);
        PlayerPrefs.Save();
    }

    public static int getBombCount()
    {
        return PlayerPrefs.GetInt(BOMB_COUNT, 0);
    }
    public static void addBombCount(int amount)
    {
        PlayerPrefs.SetInt(BOMB_COUNT, getBombCount() + amount);
        PlayerPrefs.Save();
    }


    public static int getGold()
    {
        return PlayerPrefs.GetInt(GOLD, 500);
    }
    public static void addGold(int amount)
    {
        PlayerPrefs.SetInt(GOLD, getGold() + amount);
        PlayerPrefs.Save();
    }


    public static int getStatusGolden()
    {
        return PlayerPrefs.GetInt(NINJA_GOLDEN, -1);
    }
    public static void setStatusGolden(int status)
    {
        PlayerPrefs.SetInt(NINJA_GOLDEN, status);
        PlayerPrefs.Save();
    }

    public static int getStatusGhost()
    {
        return PlayerPrefs.GetInt(NINJA_GHOST, -1);
    }
    public static void setStatusGhost(int status)
    {
        PlayerPrefs.SetInt(NINJA_GHOST, status);
        PlayerPrefs.Save();
    }

    public static int getStatusBomb()
    {
        return PlayerPrefs.GetInt(NINJA_BOMB, -1);
    }
    public static void setStatusBomb(int status)
    {
        PlayerPrefs.SetInt(NINJA_BOMB, status);
        PlayerPrefs.Save();
    }

    public static int getStatusPowerful()
    {
        return PlayerPrefs.GetInt(NINJA_POWERFULL, -1);
    }

    public static void setStatusPowerful(int status)
    {
        PlayerPrefs.SetInt(NINJA_POWERFULL, status);
        PlayerPrefs.Save();
    }

    public static int countStarStage1()
    {
        int count = 0;
        for (int i = 1; i <= 60; i++)
        {
            if (getLevelStatus(i) > 0)
            {
                count += getLevelStatus(i);
            }
        }
        return count;
    }

    public static int countStarStage2()
    {
        int count = 0;
        for (int i = 61; i <= 120; i++)
        {
            if (getLevelStatus(i) > 0)
            {
                count += getLevelStatus(i);
            }
        }
        return count;
    }
    public static int countStarStage3()
    {
        int count = 0;
        for (int i = 121; i <= 180; i++)
        {
            if (getLevelStatus(i) > 0)
            {
                count += getLevelStatus(i);
            }
        }
        return count;
    }
    public static int countStarStage4()
    {
        int count = 0;
        for (int i = 181; i <= 240; i++)
        {
            if (getLevelStatus(i) > 0)
            {
                count += getLevelStatus(i);
            }
        }
        return count;
    }
    public static int getTotalStars()
    {
        return PlayerPrefs.GetInt(TOTAL_STARS, 0);
    }
    public static void addStars(int number)
    {
        PlayerPrefs.SetInt(TOTAL_STARS, getTotalStars() + number);
        PlayerPrefs.Save();
    }

    public static int isMusicOn()
    {
        return PlayerPrefs.GetInt(GameConstant.MUSIC, 1);
    }
    public static int isSoundOn()
    {
        return PlayerPrefs.GetInt(GameConstant.SOUND, 1);
    }

    // 0: off ; 1: on
    public static void setMusic(int isOn)
    {
        PlayerPrefs.SetInt(GameConstant.MUSIC, isOn);
        PlayerPrefs.Save();
    }
    public static void setSound(int isOn)
    {
        PlayerPrefs.SetInt(GameConstant.SOUND, isOn);
        PlayerPrefs.Save();
    }

    public static int getLevelStatus(int level)
    {
        return PlayerPrefs.GetInt(GameConstant.LEVEL_STATUS + level, -1);
    }

    public static void setLevelStatus(int level, int status)
    {
        PlayerPrefs.SetInt(GameConstant.LEVEL_STATUS + level, status);
        PlayerPrefs.Save();
    }


    public static int getStageStatus(int stage)
    {
        GameConstant.setStageStatus(2, -1);
        Debug.Log("Level:" + PlayerPrefs.GetInt(GameConstant.STAGE_STATUS+"2", -1));
        if (stage == 1) return 0;
        else return PlayerPrefs.GetInt(GameConstant.STAGE_STATUS + stage, -1);
    }
    public static void unlockStage(int stage)
    {
        PlayerPrefs.SetInt(GameConstant.STAGE_STATUS + stage, 0);
        PlayerPrefs.Save();
    }

    public static void setStageStatus(int stage, int status)
    {
        PlayerPrefs.SetInt(GameConstant.STAGE_STATUS + stage, status);
        PlayerPrefs.Save();
    }


    public static int getLevelHighScore(int level)
    {

        return PlayerPrefs.GetInt(GameConstant.LEVEL_HIGHSCORE + level, 0);
    }

    public static void setLevelHighScore(int level, int score)
    {
        PlayerPrefs.SetInt(GameConstant.LEVEL_HIGHSCORE + level, score);
        PlayerPrefs.Save();
    }

	public static int getLastPlayedLevel()
	{
		return PlayerPrefs.GetInt(GameConstant.SELECTED_LEVEL, 0);
	}

	public static void setLastPlayedLevel(int lv)
	{
		PlayerPrefs.SetInt(GameConstant.SELECTED_LEVEL, lv);
		PlayerPrefs.Save();
	}

    public static int getLastPageStage1()
    {
        return PlayerPrefs.GetInt(GameConstant.LAST_PAGE_STAGE_1, 1);
    }

    public static int getLastPageStage2()
    {
        return PlayerPrefs.GetInt(GameConstant.LAST_PAGE_STAGE_2, 1);
    }

    public static int getLastPageStage3()
    {
        return PlayerPrefs.GetInt(GameConstant.LAST_PAGE_STAGE_3, 1);
    }

    public static int getLastPageStage4()
    {
        return PlayerPrefs.GetInt(GameConstant.LAST_PAGE_STAGE_4, 1);
    }

    public static void setLastPageStage1(int page)
    {
        PlayerPrefs.SetInt(GameConstant.LAST_PAGE_STAGE_1, page);
        PlayerPrefs.Save();
    }

    public static void setLastPageStage2(int page)
    {
        PlayerPrefs.SetInt(GameConstant.LAST_PAGE_STAGE_2, page);
        PlayerPrefs.Save();
    }
    public static void setLastPageStage3(int page)
    {
        PlayerPrefs.SetInt(GameConstant.LAST_PAGE_STAGE_3, page);
        PlayerPrefs.Save();
    }
    public static void setLastPageStage4(int page)
    {
        PlayerPrefs.SetInt(GameConstant.LAST_PAGE_STAGE_4, page);
        PlayerPrefs.Save();
    }


    public static void setBannerID(string id)
    {
        PlayerPrefs.SetString(GameConstant.KEY_BANNER, id);
        PlayerPrefs.Save();
    }


    public static string getBannerID()
    {
        #if  UNITY_ANDROID
		return PlayerPrefs.GetString(GameConstant.KEY_BANNER , "ca-app-pub-3940256099942544/6300978111");
        #elif UNITY_IPHONE
		return PlayerPrefs.GetString(GameConstant.KEY_BANNER, "ca-app-pub-3940256099942544/6300978111");
        #endif
            return PlayerPrefs.GetString(GameConstant.KEY_BANNER , "");
    }

    public static void setFullID(string id)
    {
        PlayerPrefs.SetString(GameConstant.KEY_FULL, id);
        PlayerPrefs.Save();
    }


    public static string getFullID()
    {
        
        #if UNITY_ANDROID
		return PlayerPrefs.GetString(GameConstant.KEY_FULL , "ca-app-pub-3940256099942544/1033173712");
        #elif UNITY_IPHONE
		return PlayerPrefs.GetString(GameConstant.KEY_FULL, "ca-app-pub-3940256099942544/1033173712");
        #endif
                return PlayerPrefs.GetString(GameConstant.KEY_FULL, "");

      
    }

    public static void setRewardID(string id)
    {
        PlayerPrefs.SetString(GameConstant.KEY_REWARD, id);
        PlayerPrefs.Save();
    }


    public static string getRewardID()
    {
    
        #if UNITY_ANDROID
		return PlayerPrefs.GetString(GameConstant.KEY_REWARD , "ca-app-pub-3940256099942544/5224354917");
        #elif UNITY_IPHONE
		return PlayerPrefs.GetString(GameConstant.KEY_REWARD, "ca-app-pub-3940256099942544/5224354917");
        #endif
            return PlayerPrefs.GetString(GameConstant.KEY_REWARD, "");

  
    }

    public static int hasClickRestore()
    {

        return PlayerPrefs.GetInt("clickRestore" , 0);
    }

    public static void clickedRestore()
    {
        PlayerPrefs.SetInt("clickRestore", 1);
        PlayerPrefs.Save();
    }

    public static bool canShowRate()
    {
        return PlayerPrefs.GetInt(GameConstant.KEY_SHOWRATE, 0)!=1?true:false;
    }

    // 0: off ; 1: on
    public static void disableShowRate()
    {
        PlayerPrefs.SetInt(GameConstant.KEY_SHOWRATE, 1);
        PlayerPrefs.Save();
    }
	public static bool canShowRateServer()
	{
		return PlayerPrefs.GetInt(GameConstant.KEY_SHOWRATE_SERVER, 0)==1?true:false;
	}

	// 0: off ; 1: on
	public static void setShowRateServer(int num)
	{
		PlayerPrefs.SetInt(GameConstant.KEY_SHOWRATE_SERVER, num);
		PlayerPrefs.Save();
	}


    public static bool canShowBanner()
    {
        return PlayerPrefs.GetInt(GameConstant.KEY_SHOW_BANNER, 1) == 1 ? true : false;
    }

    // 0: off ; 1: on
    public static void setShowBanner(int show)
    {
        PlayerPrefs.SetInt(GameConstant.KEY_SHOW_BANNER, show);
        PlayerPrefs.Save();
    }


    public static bool canShowTut1()
    {
        return PlayerPrefs.GetInt(GameConstant.kEY_HIDE_TUTORIAL_1, 0) != 1 ? true : false;
    }

    // 0: off ; 1: on
    public static void hideTut1()
    {
        PlayerPrefs.SetInt(GameConstant.kEY_HIDE_TUTORIAL_1, 1);
        PlayerPrefs.Save();
    }
    public static bool canShowTut2()
    {
        return PlayerPrefs.GetInt(GameConstant.kEY_HIDE_TUTORIAL_2, 0) != 1 ? true : false;
    }

    // 0: off ; 1: on
    public static void hideTut2()
    {
        PlayerPrefs.SetInt(GameConstant.kEY_HIDE_TUTORIAL_2, 1);
        PlayerPrefs.Save();
    }


    public static bool canVibrate()
    {
        return PlayerPrefs.GetInt(GameConstant.KEY_CAN_VIBRATE, 0) == 1 ? true : false;
    }

    // 0: off ; 1: on
    public static void setVibrate(int onoff)
    {
        PlayerPrefs.SetInt(GameConstant.KEY_CAN_VIBRATE, onoff);
        PlayerPrefs.Save();
    }


    public static int getRetentionCount()
    {
        return PlayerPrefs.GetInt(GameConstant.KEY_PLAY_TIME, 0);
    }

    public static void increaseRetentionCount()
    {
        PlayerPrefs.SetInt(GameConstant.KEY_PLAY_TIME, getRetentionCount()+1);
        PlayerPrefs.Save();
    }

    public static int getCountTimeShowAd()
    {

        return PlayerPrefs.GetInt(KEY_AD_DELAY_TIME, 20);
    }

    public static void setCountTimeShowAd(int time)
    {
        PlayerPrefs.SetInt(KEY_AD_DELAY_TIME ,time );
        PlayerPrefs.Save();
    }

	public static int getLevelToShowAds()
	{
		return PlayerPrefs.GetInt(KEY_LEVEL_TO_SHOW_ADS, 5);
	}
	public static void setLevelToShowAds(int level)
	{
		PlayerPrefs.SetInt(KEY_LEVEL_TO_SHOW_ADS, level);
		PlayerPrefs.Save();
	}

	public static bool canShowDaily()
	{
		return PlayerPrefs.GetInt(GameConstant.KEY_SHOW_DAILY, 0) == 1 ? true : false;
	}

	// 0: off ; 1: on
	public static void setShowDaily(int onoff)
	{
		PlayerPrefs.SetInt(GameConstant.KEY_SHOW_DAILY, onoff);
		PlayerPrefs.Save();
	}


}

