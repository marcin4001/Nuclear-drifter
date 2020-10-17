using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightFire : MonoBehaviour
{
    private Animator anim;
    private TimeGame time;
    public Light lightNight;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        time = FindObjectOfType<TimeGame>();
        if (lightNight != null) lightNight.enabled = false;
    }

    private void OnWillRenderObject()
    {
        //Debug.Log("Burn");
        if(time.hour >= 5 && time.hour < 20)
        {
            anim.SetBool("Burn", false);
            if (lightNight != null) lightNight.enabled = false;
        }
        else
        {
            anim.SetBool("Burn", true);
            if (lightNight != null) lightNight.enabled = true;
        }
    }
}
