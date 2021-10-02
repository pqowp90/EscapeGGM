using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public string MixerName;

    void Start()
    {
        mixer = Resources.Load<AudioMixer>("base 1");
        
    }
    public void SoundVolume(float val){
        if(val==0)
            return;
        mixer.SetFloat(MixerName,Mathf.Log10(val)*20);
    }


}