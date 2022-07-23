using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheckOpenTrigger : MonoBehaviour
{
    private DoorUnlockNPCs doorUnlock;
    // Start is called before the first frame update
    void Start()
    {
        doorUnlock = FindObjectOfType<DoorUnlockNPCs>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hero")
        {
            bool result = doorUnlock.CheckDoor();
            if (result)
                Destroy(gameObject);
        }
    }
}
