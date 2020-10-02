using System;
using UnityEngine;

public static class SettingManager
{
    public enum SEL_ANIMAL_KIND
    {
        CAT_BLACK   = 0,
        CAT_ORANGE  = 1,
        CAT_WHITE   = 2,
        DOG_CHIFUAHUA           = 3,
        DOG_CHIFUAHUA_BLACK     = 4,
        DOG_GOLDENRET           = 5,
        DOG_GREATDANE           = 6,
        DOG_GREATDANE_BLACK     = 7,
        COW         = 8,
        COW_BLACK   = 9,
        COW_BL_WH   = 10,
        BEAR        = 11,
        ELEPHANT    = 12,
        GIRAFFE     = 13,
        DEER        = 14,
        DEER_GRAY   = 15,
        GORILLA         = 16,
        GORILLA_BLACK   = 17,
        WOLF_BLACK  = 18,
        WOLF_GREY   = 19,
        HORSE       = 20,
        HORSE_BLACK = 21,
        HORSE_GREY  = 22,
        HORSE_WHITE = 23,
        PENGUIN     = 24,
        RABBIT          = 25,
        RABBIT_WHITE    = 26,
        SNAKE_YELLOW    = 27,
        SNAKE_RED       = 28,
        SPIDER_YELLOW   = 29,
        SPIDER_GREEN    = 30,
        SPIDER_RED      = 31
    }
    public static int SEL_ANIMAL_KIND_NUM = 32;

    public static string[] SEL_ANIMAL_KIND_STR = 
    {
        "ネコ(クロ)", "ネコ(オレンジ)", "ネコ(シロ)",
        "チワワ", "チワワ(クロ)", "ゴールデンレトリバー", "グレートデーン", "グレートデーン(クロ)",
        "ウシ", "ウシ(クロ)", "ウシ(シロクロ)", "クマ", "ゾウ", "キリン",
        "シカ", "シカ(グレイ)", "ゴリラ", "ゴリラ(クロ)", "オオカミ(クロ)", "オオカミ(グレイ)",
        "ウマ", "ウマ(クロ)", "ウマ(グレイ)", "ウマ(シロ)",
        "ペンギン", "ウサギ", "ウサギ(シロ)"
    };

    ////////////////////////////////////////////////////////////
    // 選択キャラクター
    public static void SaveSelectCharactor(int nSelectChar)
    {
        Debug.Log(nSelectChar);
        PlayerPrefs.SetInt(PrefsKey.selCharactor, nSelectChar);
        PlayerPrefs.Save();
    }
    public static int LoadSelectCharactor()
    {
        return PlayerPrefs.GetInt(PrefsKey.selCharactor, 0);
    }
    ////////////////////////////////////////////////////////////
    // 選択キャラクターの購入状態
    public static void SaveGetCharactor(int nSelectChar, int nGetChar)
    {
        string strGetChar = PrefsKey.getCharactor + nSelectChar;
        PlayerPrefs.SetInt(strGetChar, nGetChar);
        PlayerPrefs.Save();
    }
    public static int LoadGetCharactor(int nSelectChar)
    {
        string strGetChar = PrefsKey.getCharactor + nSelectChar;
        int nDefaultValue = 0;
        switch(nSelectChar)
        {
            case (int)SEL_ANIMAL_KIND.CAT_BLACK:
            case (int)SEL_ANIMAL_KIND.CAT_ORANGE:
            case (int)SEL_ANIMAL_KIND.CAT_WHITE:
                nDefaultValue = 1;
                break;
        }
        return PlayerPrefs.GetInt(strGetChar, nDefaultValue);
    }
    ////////////////////////////////////////////////////////////
    // 選択キャラクターを使用した数のカウント
    public static void SaveCntCharactor(int nSelectChar)
    {
        string strCntChar = PrefsKey.cntCharactor + nSelectChar;
        int nCntChar = LoadCntCharactor(nSelectChar);
        nCntChar++;
        PlayerPrefs.SetInt(strCntChar, nCntChar);
        PlayerPrefs.Save();
    }
    public static int LoadCntCharactor(int nSelectChar)
    {
        string strCntChar = PrefsKey.cntCharactor + nSelectChar;
        return PlayerPrefs.GetInt(strCntChar, 0);
    }
    public static int LoadFabCharactor()
    {
        int nMaxCnt = 0;
        int nFabChar = 0;
        for(int nIndex=0; nIndex < SEL_ANIMAL_KIND_NUM; nIndex++)
        {
            int nCntChar = LoadCntCharactor(nIndex);
            if (nMaxCnt < nCntChar)
            {
                nMaxCnt = nCntChar;
                nFabChar = nIndex;
            }
        }
        return nFabChar;
    }
    public static string LoadFabCharactorStr()
    {
        int nFabChar = LoadFabCharactor();
        return SEL_ANIMAL_KIND_STR[nFabChar];
    }
    ////////////////////////////////////////////////////////////
    // 音の大きさ
    public static void SaveBGMVolume(float bgmVolume)
    {
        PlayerPrefs.SetFloat(PrefsKey.bgmVolume, bgmVolume);
        PlayerPrefs.Save();
    }
    public static float LoadBGMVolume()
    {
        return PlayerPrefs.GetFloat(PrefsKey.bgmVolume, 0.1f);
    }
    ////////////////////////////////////////////////////////////
    // タイトルBGM
    public static void SaveTitleBGM(int nTitleBGM)
    {
        PlayerPrefs.SetInt(PrefsKey.titleBGM, nTitleBGM);
        PlayerPrefs.Save();
    }
    public static int LoadTitleBGM()
    {
        return PlayerPrefs.GetInt(PrefsKey.titleBGM, 0);
    }
    ////////////////////////////////////////////////////////////
    // ゲーム中BGM
    public static void SaveGameBGM(int nGameBGM)
    {
        PlayerPrefs.SetInt(PrefsKey.gameBGM, nGameBGM);
        PlayerPrefs.Save();
    }
    public static int LoadGameBGM()
    {
        return PlayerPrefs.GetInt(PrefsKey.gameBGM, 0);
    }
    ////////////////////////////////////////////////////////////
    // 広告取得時間
    public static bool CheckAdsEnable()
    {
        DateTime now = DateTime.Now;
        DateTime NextAdsTime = LoadAdsTime().AddDays(1);

        // ログ出力
        Debug.Log("CheckAdsTime() " + NextAdsTime + " : " + now + " = " +
            (DateTime.Compare(NextAdsTime, now) < 0 ? "1日以上経過しています" : "まだ1日経過していません"));

        if (DateTime.Compare(NextAdsTime, now) > 0) return false;

        return true;
    }
    public static void SaveAdsTime()
    {
        DateTime now = DateTime.Now;
        string strTime = now.ToBinary().ToString();
        PlayerPrefs.SetString(PrefsKey.adsTime, strTime);
        PlayerPrefs.Save();
        Debug.Log("SaveAdsTime() " + strTime);
    }
    public static DateTime LoadAdsTime()
    {
        return System.DateTime.FromBinary(LoadAdsTimeBinary());
    }
    public static long LoadAdsTimeBinary()
    {
        string defult = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Local).ToBinary().ToString();//保存していたデータが存在しない時用のデフォルト値
        string dateTimeString = PlayerPrefs.GetString(PrefsKey.adsTime, defult);
        //Debug.Log("LoadAdsTime() " + dateTimeString);

        return System.Convert.ToInt64(dateTimeString);
    }

    ////////////////////////////////////////////////////////////
    // ゲーム情報
    ////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////
    // 最高タイム
    public static void SaveGameBestTime(int nGameBestTime)
    {
        int nBestTime = LoadGameBestTime();
        if(nBestTime < nGameBestTime)
        {   // 記録更新した場合に保存
            PlayerPrefs.SetInt(PrefsKey.gameBestTime, nGameBestTime);
            PlayerPrefs.Save();
        }
    }
    public static int LoadGameBestTime()
    {
        return PlayerPrefs.GetInt(PrefsKey.gameBestTime, 0);
    }
    ////////////////////////////////////////////////////////////
    // プレイ総時間
    public static void SaveGameTotalPlayTime(int nGameTotalPlayTime)
    {
        int nTotalPlayTime = LoadGameTotalPlayTime() + nGameTotalPlayTime;
        // GooglePlayGamesにタイム送信
        Social.ReportScore((long)(nTotalPlayTime * 1000), "CgkIxKeO8tQBEAIQAw", (bool success) =>
        {
            // handle success or failure
        });
        PlayerPrefs.SetInt(PrefsKey.gameTotalPlayTime, nTotalPlayTime);
        PlayerPrefs.Save();
    }
    public static int LoadGameTotalPlayTime()
    {
        return PlayerPrefs.GetInt(PrefsKey.gameTotalPlayTime, 0);
    }
    ////////////////////////////////////////////////////////////
    // 総合ポイント
    public static void SaveGameTotalPoint(int nGameTotalPoint)
    {
        int nTotalPoint = LoadGameTotalPoint() + nGameTotalPoint;
        // GooglePlayGamesにスコア送信
        Social.ReportScore(nTotalPoint, "CgkIxKeO8tQBEAIQAg", (bool success) =>
        {
            // handle success or failure
        });
        PlayerPrefs.SetInt(PrefsKey.gameTotalPoint, nTotalPoint);
        PlayerPrefs.Save();
    }
    public static int LoadGameTotalPoint()
    {
        return PlayerPrefs.GetInt(PrefsKey.gameTotalPoint, 0);
    }
    ////////////////////////////////////////////////////////////
    // 前回タイム
    public static void SaveGamePrePlayTime(int nGamePrePlayTime)
    {
        // GooglePlayGamesにタイム送信
        Social.ReportScore((long)(nGamePrePlayTime * 1000), "CgkIxKeO8tQBEAIQAQ", (bool success) =>
        {
            // handle success or failure
            if(success)
            {

            }
            else
            {

            }
        });
        PlayerPrefs.SetInt(PrefsKey.gamePrePlayTime, nGamePrePlayTime);
        PlayerPrefs.Save();
    }
    public static int LoadGamePrePlayTime()
    {
        return PlayerPrefs.GetInt(PrefsKey.gamePrePlayTime, 0);
    }
}