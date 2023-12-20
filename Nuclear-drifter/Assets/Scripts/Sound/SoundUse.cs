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
    public AudioClip fallBodyClip;
    public AudioClip cashClip;
    public AudioClip pickUpClip;
    public AudioClip dropClip;
    public AudioClip lockClip;
    public AudioClip lockpickingGoodClip;
    public AudioClip levelUpClip;
    public AudioClip inhaler;
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

    public void PlayFallBody()
    {
        source.PlayOneShot(fallBodyClip);
    }

    public void PlayCash()
    {
        source.PlayOneShot(cashClip);
    }

    public void PlayPickUp()
    {
        source.PlayOneShot(pickUpClip);
    }

    public void PlayDrop()
    {
        source.PlayOneShot(dropClip);
    }

    public void PlayLock()
    {
        source.PlayOneShot(lockClip);
    }

    public void PlayLockpickingGood()
    {
        source.PlayOneShot(lockpickingGoodClip);
    }

    public void PlayLevelUp()
    {
        source.PlayOneShot(levelUpClip);
    }

    public void PlayInhaler()
    {
        source.PlayOneShot(inhaler);
    }
}
