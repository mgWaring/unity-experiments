using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    private AudioSource output;

    void Start () {
        output = GetComponent<AudioSource> ();
    }

    public void PlayOnce (List<AudioClip> sounds) {
        output.clip = sounds[Random.Range (0, sounds.Count)];
        output.Play();
    }
}