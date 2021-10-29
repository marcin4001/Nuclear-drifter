using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoLockDoor : MonoBehaviour
{
    public string text;
    public string[] tips;
    public NPCbg npc;
    private Carpet door;
    private GUIScript gUI;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<Carpet>();
        if (door == null)
            Destroy(this);
        gUI = FindObjectOfType<GUIScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hero" && door.isLock && !door.closeInNight)
        {
            if (npc != null)
                npc.SetText(text);
            foreach(string tip in tips)
            {
                gUI.AddText(tip);
            }
        }
    }
}
