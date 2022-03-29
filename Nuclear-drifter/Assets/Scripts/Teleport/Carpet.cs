using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{
    private MessageBox box;
    public Animator door;
    public string sceneName;
    public Vector2 startPos;
    private GUIScript gUI;
    private TimeGame time;
    public bool isLock = false;
    public bool closeInNight = false;
    public string locationInside;
    public bool noSound = false;
    // Start is called before the first frame update
    void Start()
    {
        box = FindObjectOfType<MessageBox>();
        gUI = FindObjectOfType<GUIScript>();
        time = FindObjectOfType<TimeGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            if (closeInNight) IsNight();
            if (!isLock)
            {
                if (door != null) box.door = door;
                box.noSound = noSound;
                box.sceneName = sceneName;
                box.playerPos = startPos;
                box.location = locationInside;
                box.ShowBox();
            }
            else
            {
                gUI.AddText("The door is locked!");
                if(closeInNight) gUI.AddText("Opening at 6 am");
            }
        }
    }

    public void IsNight()
    {
        if (time.hour >= 6 && time.hour < 21) isLock = false;
        else isLock = true;
    }
}
