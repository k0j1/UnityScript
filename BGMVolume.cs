using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMVolume : MonoBehaviour
{
    [SerializeField] Slider  BGMVolumeSlider = null;
    [SerializeField] Text    BGMVolumeText = null;
    [SerializeField] private AudioSource audioSource = null;
    
    private float BGMVolumeValue;
    private string CheckBGMVolume;
   
    // Start is called before the first frame update
    void Start()
    {
        //BGMVolumeSlider = GetComponent<Slider>();
        //BGMVolumeText = GameObject.Find("VolumeText").GetComponent<Text>();
        //audioSource = GameObject.Find("BGMAudio").GetComponent<AudioSource>();
        
        BGMVolumeValue = SettingManager.LoadBGMVolume();
        BGMVolumeSlider.value = BGMVolumeValue;
        
        UpdateValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateValue()
    {
        CheckBGMVolume = "" + BGMVolumeSlider.value.ToString("f1");
        if(CheckBGMVolume != BGMVolumeText.text)
        {
            BGMVolumeText.text = CheckBGMVolume;
            BGMVolumeValue = float.Parse(CheckBGMVolume);
            SettingManager.SaveBGMVolume(BGMVolumeValue);
            audioSource.volume = BGMVolumeValue;
        }
    }
}
