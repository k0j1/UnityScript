using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class GameSystem : MonoBehaviour
{
    // タイトル用
    //インスペクターウィンドウからゲームオブジェクトを設定する
    [SerializeField] GameObject MainPanel = null;
    [SerializeField] GameObject OptionPanel = null;
    [SerializeField] GameObject DisplayPanel = null;
    [SerializeField] GameObject BGMPanel = null;
    
    // キャラクター選択画面用
    [SerializeField] GameObject CharSelPanel = null;
    [SerializeField] GameObject PointGetPanel = null;
    [SerializeField] GameObject ScorePanel = null;
    [SerializeField] ChangeCharactor ChangeChar = null;
    public GameObject MyPoints = null;
    public GameObject NeedPoints = null;
    public GameObject GameStartBtn = null;
    public GameObject PurchaseBtn = null;
    public GameObject GameObject_Ad = null;
    public Text m_strMyPoint = null;
    public Text m_strBestTime = null;
    public Text m_strTotalPoint = null;
    public Text m_strFabCharactor = null;
    public Text m_strTotalPlayTime = null;
    public Text m_strPrePlayTime = null;

    // ゲーム結果画面用
    public GameObject ResultPanel = null;
    public GameObject m_imageResultTitle = null;
    public Text m_strResultTimeText = null;
    public Text m_strResultPointText = null;

    void Start()
    {
        //OptionWindow = GetComponent<OptionWindow> ();
        StartMenu();
        ChangeCharactor();
        DispPoint();
        //Invoke("ChangeCharactor", 1.0f);
        //Observable.NextFrame().Subscribe(_ => Debug.Log("Next Frame"));
    }

    // Start is called before the first frame update
    public void StartCharacterSelect()
    {
        SceneManager.LoadScene ("CharacterSelect");
        //Application.LoadLevel("CharacterSelect");
    }    
    public void StartGame()
    {
        SceneManager.LoadScene ("Game");
        //Application.LoadLevel("Game");
    }
    
    public void FinishGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        Application.Quit();
    #elif UNITY_ANDROID        
        UnityEngine.Application.Quit();
        Application.Quit();
    #endif
    }
    
    public void OpenOption()
    {
        MainPanel.SetActive(true);
        OptionPanel.SetActive(true);
        DisplayPanel.SetActive(false);
        BGMPanel.SetActive(false);
    }
    
    public void OpenDisplayMenu()
    {
        DisplayPanel.SetActive(true);
        BGMPanel.SetActive(false);        
    }
    
    public void OpenBGMMenu()
    {
        DisplayPanel.SetActive(false);
        BGMPanel.SetActive(true);        
    }

    public void CloseOption()
    {
        MainPanel.SetActive(true);
        OptionPanel.SetActive(false);
        DisplayPanel.SetActive(false);
        BGMPanel.SetActive(false);
    }
    
    public void StartMenu()
    {
        if(MainPanel) MainPanel.SetActive(true);
        if(OptionPanel) OptionPanel.SetActive(false);
        if(DisplayPanel) DisplayPanel.SetActive(false);
        if(BGMPanel) BGMPanel.SetActive(false);
    }
    
    void ChangeCharactor()
    {
        if(ChangeChar)
        {
            // 選択中のキャラクター番号
            int nSelCharactor = SettingManager.LoadSelectCharactor();
            Debug.Log(nSelCharactor);
            ChangeChar.Change(nSelCharactor);
        }
    }
    public void ChangeCharactorDisp()
    {
        int nSelCharactor = SettingManager.LoadSelectCharactor();
        int nGetCharactor = SettingManager.LoadGetCharactor(nSelCharactor);
        Debug.Log("nGetCharactor = " + nGetCharactor);
        switch (nGetCharactor)
        {
            // 未購入
            case 0:
                NeedPoints.SetActive(true);
                GameStartBtn.SetActive(false);
                PurchaseBtn.SetActive(true);
                break;
            // 購入済
            case 1:
                NeedPoints.SetActive(false);
                GameStartBtn.SetActive(true);
                PurchaseBtn.SetActive(false);
                break;
        }
    }

    public void ChangeTab(int nTabIndex)
    {
        Admod ad = GameObject_Ad.GetComponent<Admod>();
        switch (nTabIndex)
        {
            case 0:
                ad.DestroyAds();
                CharSelPanel.SetActive(true);
                PointGetPanel.SetActive(false);
                ScorePanel.SetActive(false);
                DispPoint();
                break;
            case 1:
                ad.InitializeAds();
                CharSelPanel.SetActive(false);
                PointGetPanel.SetActive(true);
                ScorePanel.SetActive(false);
                break;
            case 2:
                ad.DestroyAds();
                CharSelPanel.SetActive(false);
                PointGetPanel.SetActive(false);
                ScorePanel.SetActive(true);
                DispScore();
                break;
        }
    }

    // ポイント表示
    public void DispPoint()
    {
        if(MyPoints)
        {
            m_strMyPoint.text = SettingManager.LoadGameTotalPoint() + "pt";
        }
    }

    // 成績表示
    public void DispScore()
    {
        // お気に入り
        if (m_strFabCharactor)
        {
            m_strFabCharactor.text = SettingManager.LoadFabCharactorStr();
        }
        // 総合ポイント
        if (m_strTotalPoint)
        {
            int nTotalPoints = SettingManager.LoadGameTotalPoint();
            m_strTotalPoint.text = nTotalPoints + "pt";
        }
        // 最高タイム
        if (m_strBestTime)
        {
            int nSeconds = SettingManager.LoadGameBestTime();
            m_strBestTime.text = string.Format("{0:00}:{1:00}:{2:00}", nSeconds / 60 / 60, nSeconds / 60, nSeconds % 60);
        }
        // 総合タイム
        if (m_strTotalPlayTime)
        {
            int nSeconds = SettingManager.LoadGameTotalPlayTime();
            m_strTotalPlayTime.text = string.Format("{0:00}:{1:00}:{2:00}", nSeconds / 60 / 60, nSeconds / 60, nSeconds % 60);
        }
        // 前回タイム
        if (m_strPrePlayTime)
        {
            int nSeconds = SettingManager.LoadGamePrePlayTime();
            m_strPrePlayTime.text = string.Format("{0:00}:{1:00}:{2:00}", nSeconds / 60 / 60, nSeconds / 60, nSeconds % 60);
        }

    }

    // ゲーム結果画面表示
    bool bDispEnd = false;
    public void DispResult()
    {
        // 例外処理
        if (!ResultPanel) return;

        ResultPanel.SetActive(true);

        StartCoroutine("DispResultText");
    }
    IEnumerator DispResultText()
    {
        if (!bDispEnd)
        {
            bDispEnd = true;
            // タイトル表示
            yield return new WaitForSeconds(1); //1秒待つ
            m_imageResultTitle.SetActive(true);

            int nSeconds = SettingManager.LoadGamePrePlayTime();

            // 生存時間表示
            yield return new WaitForSeconds(1); //1秒待つ
            m_strResultTimeText.text = "<B>生存時間：" + string.Format("{0:00}:{1:00}", nSeconds / 60, nSeconds % 60) + "</B>";

            // 獲得ポイント表示
            yield return new WaitForSeconds(1); //1秒待つ
            int nPoint = (int)(nSeconds / 60) * 100 + nSeconds % 60;
            m_strResultPointText.text = "<B>獲得ポイント：" + nPoint + "pt</B>";
            SettingManager.SaveGameTotalPoint(nPoint);

            // キャラクター使用回数カウント
            int nSelCharactor = SettingManager.LoadSelectCharactor();
            SettingManager.SaveCntCharactor(nSelCharactor);
        }
    }

    public void LoginGooglePlayGames()
    {
        // ログインずみなら何もしない
        if (Social.localUser.authenticated) return;

        // 初期化
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();


        // サインイン実行
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                // サインイン成功！
                // GooglePlayGamesにスコア送信
                int nTotalPoint = SettingManager.LoadGameTotalPoint();
                Social.ReportScore(nTotalPoint, "CgkIxKeO8tQBEAIQAg", (bool success1) =>
                {
                    // handle success or failure
                });
                // GooglePlayGamesにタイム送信
                int nBestTime = SettingManager.LoadGameBestTime();
                Social.ReportScore((long)(nBestTime * 1000), "CgkIxKeO8tQBEAIQAQ", (bool success2) =>
                {
                    // handle success or failure
                });
                // GooglePlayGamesにタイム送信
                int nTotalPlayTime = SettingManager.LoadGameTotalPlayTime();
                Social.ReportScore((long)(nTotalPlayTime * 1000), "CgkIxKeO8tQBEAIQAw", (bool success3) =>
                {
                    // handle success or failure
                });
            }
        });
    }

    public void DispRanking()
    {
        Social.ShowLeaderboardUI();
    }
}
