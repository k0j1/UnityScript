using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSkillSound : MonoBehaviour
{
    public bool bSoundStart = false;
    
    public AudioClip sound1;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bSoundStart) 
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
            bSoundStart = false;
        }
    }
    
    public void StartSound()
    {
        bSoundStart = true;
    }
}
