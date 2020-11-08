using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingManager
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// チュートリアルの有無
    /// </summary>
    /// <param name="nValue">0:チュートリアル無、1:チュートリアル有</param>
    public static void SaveTutorialMode(int nValue)
    {
        PlayerPrefs.SetInt(PrefsKey.gameTutorial, nValue);
        PlayerPrefs.Save();
    }
    public static int LoadTutorialMode()
    {
        return PlayerPrefs.GetInt(PrefsKey.gameTutorial, 0);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// 走行距離の保存
    /// </summary>
    /// <param name="nValue">走行距離</param>
    public static void SaveDistance(int nValue)
    {
        PlayerPrefs.SetInt(PrefsKey.gameDistance, nValue);
        PlayerPrefs.Save();
        SaveTotalDistance(nValue);
        SaveBestDistance(nValue);
    }
    public static int LoadDistance()
    {
        return PlayerPrefs.GetInt(PrefsKey.gameDistance, 0);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// 総走行距離の保存
    /// </summary>
    /// <param name="nValue">走行距離</param>
    public static void SaveTotalDistance(int nValue)
    {
        int nDistance = LoadTotalDistance() + nValue;
        PlayerPrefs.SetInt(PrefsKey.gameTotalDistance, nDistance);
        PlayerPrefs.Save();
    }
    public static int LoadTotalDistance()
    {
        return PlayerPrefs.GetInt(PrefsKey.gameTotalDistance, 0);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// ベスト走行距離の保存
    /// </summary>
    /// <param name="nValue">走行距離</param>
    public static void SaveBestDistance(int nValue)
    {
        int nBestDistance = LoadBestDistance();
        if(nBestDistance < nValue)
        {
            PlayerPrefs.SetInt(PrefsKey.gameBestDistance, nValue);
            PlayerPrefs.Save();
        }
    }
    public static int LoadBestDistance()
    {
        return PlayerPrefs.GetInt(PrefsKey.gameBestDistance, 0);
    }
}
