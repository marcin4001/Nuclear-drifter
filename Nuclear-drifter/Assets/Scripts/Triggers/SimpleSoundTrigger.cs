using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSoundTrigger : MonoBehaviour
{
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (source != null)
            source.volume = PlayerPrefs.GetFloat("sfxSound", 1.0f);
        else
            Destroy(this);
    }

}
