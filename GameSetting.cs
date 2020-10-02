using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{    
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] GameObject SettingButton = null;
    [SerializeField] GameObject SettingPanel = null;
    
    [SerializeField] GameObject LeftJoystick = null;
    [SerializeField] GameObject RightJoystick = null;
    [SerializeField] GameObject SkillButton = null;
    
    private float m_VolumeRef = 0f;
    private bool m_Paused;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuOn ()
    {
        //m_TimeScaleRef = Time.timeScale;
        //Time.timeScale = 0f;
        
        // 設定画面表示
        SettingPanel.SetActive(true);
        // 各種ボタンを非表示
        SettingButton.SetActive(false);
        LeftJoystick.SetActive(false);
        RightJoystick.SetActive(false);
        SkillButton.SetActive(false);

        m_VolumeRef = audioSource.volume;
        audioSource.volume = 0f;

        m_Paused = true;
    }


    public void MenuOff ()
    {
        // 設定画面非表示
        SettingPanel.SetActive(false);
        // 各種ボタンを表示
        SettingButton.SetActive(true);
        LeftJoystick.SetActive(true);
        RightJoystick.SetActive(true);
        SkillButton.SetActive(true);
        
        //Time.timeScale = m_TimeScaleRef;
        audioSource.volume = m_VolumeRef;
        m_Paused = false;
    }
    
    public void GoTitle()
    {
        MenuOff();
        audioSource.Stop();
        //SceneManager.LoadScene ("Title");
        Application.LoadLevel("Title");
    }

    public void GoSelect()
    {
        MenuOff();
        audioSource.Stop();
        //SceneManager.LoadScene ("Title");
        Application.LoadLevel("CharacterSelect");
    }

    public void Finish()
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
}
