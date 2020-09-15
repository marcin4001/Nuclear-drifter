using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightFire : MonoBehaviour
{
    private Animator anim;
    private TimeGame time;
    public Light light;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        time = FindObjectOfType<TimeGame>();
        if (light != null) light.enabled = false;
    }

    private void OnWillRenderObject()
    {
        //Debug.Log("Burn");
        if(time.hour >= 5 && time.hour < 20)
        {
            anim.SetBool("Burn", false);
            if (light != null) light.enabled = false;
        }
        else
        {
            anim.SetBool("Burn", true);
            if (light != null) light.enabled = true;
        }
    }
}
