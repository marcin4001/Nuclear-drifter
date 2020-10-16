using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSound : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        if(sounds.Length > 0)
        {
            if(index >= 0 && index < sounds.Length)
            {
                Debug.Log("sound");
                source.PlayOneShot(sounds[index]);
            }
        }
    }
}
