using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsTrigger : MonoBehaviour
{
    public AudioClip geigerCounter;
    public AudioClip openDoor;
    public AudioClip closeDoor;
    private AudioSource sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<AudioSource>();
        sr.loop = true;
    }

    public void OpenDoor()
    {
        sr.PlayOneShot(openDoor);
    }

    public void CloseDoor()
    {
        sr.PlayOneShot(closeDoor);
    }

    public void StartGeiger()
    {
        sr.clip = geigerCounter;
        sr.Play();
    }

    public void StopGeiger()
    {
        sr.Stop();
        sr.clip = null;
    }

    public bool isPlayed()
    {
        return sr.isPlaying;
    }
}
