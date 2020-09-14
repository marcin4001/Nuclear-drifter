using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCloserDoor : MonoBehaviour
{
    public Carpet carpet;

    public void DoorClose(bool value)
    {
        if(carpet == null)carpet = GetComponent<Carpet>();
        carpet.isLock = value; 
    }
}
