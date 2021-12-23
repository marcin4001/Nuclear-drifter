using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsTrigger : MonoBehaviour
{
    public AudioClip geigerCounter;
    public AudioClip openDoor;
    public AudioClip closeDoor;
    public AudioClip stove;
    public AudioClip sheep;
    public AudioClip tool;
    public AudioClip vending_machine;
    public AudioClip error;
    public AudioClip trapdoor;
    private AudioSource sr;
   
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<AudioSource>();
        sr.loop = true;
    }

    public void SetVolume(float value)
    {
        sr.volume = value;
    }

    public void Mute(bool value)
    {
        sr.mute = value;
    }

    public void Cook()
    {
        sr.PlayOneShot(stove);
    }

    public void OpenDoor()
    {
        sr.PlayOneShot(openDoor);
    }

    public void CloseDoor()
    {
        sr.PlayOneShot(closeDoor);
    }

    public void UseTool()
    {
        sr.PlayOneShot(tool);
    }

    public void UseVendingMachine()
    {
        sr.PlayOneShot(vending_machine);
    }

    public void PlayError()
    {
        sr.PlayOneShot(error);
    }

    public void UseTrapdoor()
    {
        sr.PlayOneShot(trapdoor);
    }

    public void StartGeiger()
    {
        sr.clip = geigerCounter;
        sr.Play();
    }

    public void Stop()
    {
        sr.Stop();
        sr.clip = null;
    }

    public void StartBleat()
    {
        sr.clip = sheep;
        sr.Play();
    }

    public bool isPlayed()
    {
        return sr.isPlaying;
    }
}
