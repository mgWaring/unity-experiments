using UnityEngine;

public abstract class CharSounds
{
    public abstract AudioSource source { get; set; }
    public abstract AudioClip[] stepSounds { get; }
    public abstract AudioClip[] jumpSounds { get; }
    public abstract AudioClip[] swingSounds { get; }
    public abstract AudioClip[] bumpSounds { get; }
    public abstract AudioClip[] deathSounds { get; }
    public abstract AudioClip[] spawnSounds { get; }
    public abstract AudioClip[] celebrateSounds { get; }

    public void AssignSource(AudioSource parent_source)
    {
        source = parent_source;
    }

    void PlayStepSound()
    {
        AudioClip _clip = stepSounds[Random.Range(0, stepSounds.Length)];
        source.PlayOneShot(_clip);
    }

    void PlayJumpSound()
    {
        AudioClip _clip = stepSounds[Random.Range(0, stepSounds.Length)];
        source.PlayOneShot(_clip);
    }

    void PlaySwingSound()
    {
        AudioClip _clip = stepSounds[Random.Range(0, stepSounds.Length)];
        source.PlayOneShot(_clip);
    }

    void PlayBumpSound()
    {
        AudioClip _clip = stepSounds[Random.Range(0, stepSounds.Length)];
        source.PlayOneShot(_clip);
    }

    void PlayDeathSound()
    {
        AudioClip _clip = stepSounds[Random.Range(0, stepSounds.Length)];
        source.PlayOneShot(_clip);
    }

    void PlaySpawnSound()
    {
        AudioClip _clip = stepSounds[Random.Range(0, stepSounds.Length)];
        source.PlayOneShot(_clip);
    }

    void PlayCelebrateSound()
    {
        AudioClip _clip = stepSounds[Random.Range(0, stepSounds.Length)];
        source.PlayOneShot(_clip);
    }
}