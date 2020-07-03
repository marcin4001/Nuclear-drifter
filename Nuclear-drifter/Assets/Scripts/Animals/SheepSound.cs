using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSound : MonoBehaviour
{
    private SoundsTrigger st;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (st == null) st = FindObjectOfType<SoundsTrigger>();
        if (st != null) st.StartBleat();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (st == null) st = FindObjectOfType<SoundsTrigger>();
        if (st != null) if (!st.isPlayed()) st.StartBleat();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (st == null) st = FindObjectOfType<SoundsTrigger>();
        if (st != null) st.Stop();
    }
}
