using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBGM : MonoBehaviour
{
     private AudioSource audioSource;
     private bool bPlaying = false; 
     private int gameAudioValue;
    
/*
    [SerializeField] private AudioClip game01 = null;
    [SerializeField] private AudioClip game02 = null;
    [SerializeField] private AudioClip game03 = null;
    [SerializeField] private AudioClip game04 = null;
    [SerializeField] private AudioClip game05 = null;
    [SerializeField] private AudioClip game06 = null;
*/

    [SerializeField] private Image image = null;
    [SerializeField] private Sprite spriteOn = null;
    [SerializeField] private Sprite spriteOff = null;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad (this);
        bPlaying = true;
        
        audioSource = this.GetComponent<AudioSource>();
        
        //gameAudioValue = SettingManager.LoadGameBGM();
        //SetGameBGM();
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
    
    public void SetGameBGM()
    {
        switch(gameAudioValue)
        {
            default:
            case 0: audioSource.clip = (AudioClip)Resources.Load( "stage01" ); break;
            case 1: audioSource.clip = (AudioClip)Resources.Load( "stage02" ); break;
            case 2: audioSource.clip = (AudioClip)Resources.Load( "stage03" ); break;
            case 3: audioSource.clip = (AudioClip)Resources.Load( "stage04" ); break;
            case 4: audioSource.clip = (AudioClip)Resources.Load( "stage05" ); break;
            case 5: audioSource.clip = (AudioClip)Resources.Load( "stage06" ); break;
        }
        Play();
    }
}
