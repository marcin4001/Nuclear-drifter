using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadTrigger : MonoBehaviour
{
    public Health health;
    private SoundsTrigger st;
    public bool playerIsNear = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        health = collision.GetComponentInParent<Health>();
        if (health != null)
        {
            playerIsNear = true;
            if (st == null) st = FindObjectOfType<SoundsTrigger>();
            if (st != null) st.StartGeiger();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        health = collision.GetComponentInParent<Health>();
        if (health != null)
        {
            playerIsNear = true;
            Invoke("SetRad", 2.5f);
            if (st == null) st = FindObjectOfType<SoundsTrigger>();
            if (st != null) if(!st.isPlayed()) st.StartGeiger();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        health = collision.GetComponentInParent<Health>();
        if (health != null)
        {
            playerIsNear = false;
            if (st == null) st = FindObjectOfType<SoundsTrigger>();
            if (st != null) st.Stop();
        }
    }

    private void SetRad()
    {
        if(playerIsNear)health.SetRad(true);
    }
}
