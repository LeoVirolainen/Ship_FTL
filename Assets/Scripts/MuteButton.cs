using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour {
    public AudioSource[] audioSources;
    private bool muteOn = false;

    private void Start() {
        GameObject sfxParent = GameObject.Find("SFX");
        audioSources = sfxParent.GetComponentsInChildren<AudioSource>();
    }

    public void ToggleMute() {
        foreach (AudioSource soundCtr in audioSources) {
            if (soundCtr.mute == false) {
                soundCtr.mute = true;
                muteOn = true;
            } else {
                soundCtr.mute = false;
                muteOn = false;
            }
        }
    }
}
