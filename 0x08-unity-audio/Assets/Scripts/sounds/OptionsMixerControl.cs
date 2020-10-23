using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMixerControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    public void SetBGMVol(float BGMVol){
        audioMixer.SetFloat("BGM", BGMVol);
    }
    public void SetSFXVol(float SFXVol){
        audioMixer.SetFloat("SFX", SFXVol);
    }
}
