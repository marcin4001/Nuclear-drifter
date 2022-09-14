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
    public AudioClip electricShock;
    public AudioClip donkeyWalk;
    public AudioClip purifier;
    public AudioClip boat;
    public AudioClip lockOpenDoor;
    public AudioClip clickButton;
    public AudioClip sleeping;
    public AudioClip chestOpen;
    private AudioSource sr;
    private bool blockClick = false;
   
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

    public void PlayElectricShock()
    {
        sr.PlayOneShot(electricShock);
    }

    public void PlayDonkeyWalk()
    {
        sr.PlayOneShot(donkeyWalk);
    }

    public void PlayPurifier()
    {
        sr.PlayOneShot(purifier);
    }

    public void PlayBoat()
    {
        sr.PlayOneShot(boat);
    }

    public void PlayOpenLockDoor()
    {
        sr.PlayOneShot(lockOpenDoor);
    }

    public void PlayClickButton()
    {
        if (!blockClick)
        {
            sr.PlayOneShot(clickButton);
            StartCoroutine(Unblock());
        }
    }

    public void PlaySleeping()
    {
        sr.PlayOneShot(sleeping);
    }

    public void PlayOpenChest()
    {
        sr.PlayOneShot(chestOpen);
    }

    public IEnumerator Unblock()
    {
        blockClick = true;
        yield return new WaitForSecondsRealtime(0.1f);
        blockClick = false;
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
