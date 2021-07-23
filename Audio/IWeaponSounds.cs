using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

//different sets of audio with the same methods
public interface IWeaponSounds
{
    //play sounds..  or provide sounds to be played?
    void PlayStepSound();

    void PlayJumpSound();

    void PlaySwingSound();

    void PlayBumpSound();

    void PlayDeathSound();

    void PlaySpawnSound();

    void PlayCelebrateSound();
}