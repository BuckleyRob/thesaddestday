using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMuter : MonoBehaviour{
    public AudioMixer group;
    public Image muteImage;

    private void Start(){
        group.GetFloat("MasterVolume", out var oldVal);
        muteImage.enabled = oldVal < -50;
    }

    private void Update(){
        if (Input.GetKeyUp(KeyCode.M)){
            group.GetFloat("MasterVolume", out var oldVal);
            group.SetFloat("MasterVolume", oldVal < -50 ? 0 : -80);
            muteImage.enabled = oldVal > -50;
        }
    }
}
