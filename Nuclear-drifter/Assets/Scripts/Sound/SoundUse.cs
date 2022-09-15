using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUse : MonoBehaviour
{
    public AudioClip eatClip;
    public AudioClip drinkClip;
    public AudioClip documentClip;
    public AudioClip backpackClip;
    public AudioClip coughClip;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void SetVolume(float value)
    {
        source.volume = value;
    }

    public void PlayEat()
    {
        source.PlayOneShot(eatClip);
    }

    public void PlayDrink()
    {
        source.PlayOneShot(drinkClip);
    }

    public void PlayRead()
    {
        source.PlayOneShot(documentClip);
    }

    public void PlayOpenBackpack()
    {
        source.PlayOneShot(backpackClip);
    }

    public void PlayCough()
    {
        source.PlayOneShot(coughClip);
    }
}
