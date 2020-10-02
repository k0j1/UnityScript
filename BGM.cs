using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM : MonoBehaviour
{
    //Dropdownを格納する変数
    [SerializeField] private Dropdown titleDropdown = null;
    [SerializeField] private Dropdown gameDropdown = null;
    //Title Audio
    [SerializeField] private AudioClip title01 = null;
    [SerializeField] private AudioClip title02 = null;
    [SerializeField] private AudioClip title03 = null;
    [SerializeField] private AudioClip title04 = null;
    [SerializeField] private AudioClip title05 = null;
    [SerializeField] private AudioClip title06 = null;
    [SerializeField] private AudioClip title07 = null;
    [SerializeField] private AudioClip title08 = null;
    [SerializeField] private AudioClip title09 = null;
    
    [SerializeField] private Image image = null;
    [SerializeField] private Sprite spriteOn = null;
    [SerializeField] private Sprite spriteOff = null;
    
    private AudioSource audioSource;
    private bool bPlaying = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad (this);
        bPlaying = true;
        
        //image = GameObject.Find("BGMButton").GetComponent<Image>();
        //spriteOn = Resources.Load<Sprite>("BGM-ON");
        //spriteOff = Resources.Load<Sprite>("BGM-OFF");
        audioSource = this.GetComponent<AudioSource>();
        
        audioSource.volume = SettingManager.LoadBGMVolume();
        titleDropdown.value = SettingManager.LoadTitleBGM();
        gameDropdown.value = SettingManager.LoadGameBGM();
        SetTitleBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SwitchPlayAndStop()
    {
        if(bPlaying)
        {
            Stop();
            bPlaying = false; 
        }
        else
        {
            Play();
            bPlaying = true;
        }
    }
    
    public void Play()
    {
        audioSource.Play();
        if(null != spriteOn)
            image.sprite = spriteOn;
    }
    
    public void Stop()
    {
        audioSource.Stop();
        if(null != spriteOff)
            image.sprite = spriteOff;
    }
    
    public void SetVolume(float fVolume)
    {
        audioSource.volume = fVolume;
    }
    
    public void SetTitleBGM()
    {
        switch(titleDropdown.value)
        {
            default:
            case 0: audioSource.clip = title01; break;
            case 1: audioSource.clip = title02; break;
            case 2: audioSource.clip = title03; break;
            case 3: audioSource.clip = title04; break;
            case 4: audioSource.clip = title05; break;
            case 5: audioSource.clip = title06; break;
            case 6: audioSource.clip = title07; break;
            case 7: audioSource.clip = title08; break;
            case 8: audioSource.clip = title09; break;
        }
        SettingManager.SaveTitleBGM(titleDropdown.value);
        //Stop();
        Play();
    }
    public void SetGameBGM()
    {
        SettingManager.SaveGameBGM(gameDropdown.value);
    }
}
