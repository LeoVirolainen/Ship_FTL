using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour {
    public AudioSource musicSource;
    private bool muteOn = false;

    private void Start() {
        musicSource = GameObject.Find("music").GetComponent<AudioSource>();
    }

    public void ToggleMusic() {
        if (muteOn == false) {
            musicSource.mute = true;
            muteOn = true;
        } else {
            musicSource.mute = false;
            muteOn = false;
        }
    }
}
