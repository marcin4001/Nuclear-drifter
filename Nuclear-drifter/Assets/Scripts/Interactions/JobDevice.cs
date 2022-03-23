using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JobDevice : MonoBehaviour
{
    protected GUIScript gUI;
    protected Inventory inv;
    protected PlayerClickMove player;
    protected FadePanel fade;
    protected SoundsTrigger sound;

    private void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        inv = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerClickMove>();
        fade = FindObjectOfType<FadePanel>();
        sound = FindObjectOfType<SoundsTrigger>();
    }

    public abstract void Work();
}
