using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSound : MonoBehaviour
{
    public AudioClip[] soundsEnemy;
    public AudioClip[] soundsWeapon;
    private AudioSource source;
    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void SetVolume(float value)
    {
        source.volume = value;
    }

    public void PlayEnemy(int index)
    {
        if(soundsEnemy.Length > 0)
        {
            if(index >= 0 && index < soundsEnemy.Length)
            {
                source.PlayOneShot(soundsEnemy[index]);
            }
        }
    }

    public void PlayWeapon(int index)
    {
        if (soundsWeapon.Length > 0)
        {
            if (index >= 0 && index < soundsWeapon.Length)
            {
                source.PlayOneShot(soundsWeapon[index]);
            }
        }
    }
}
