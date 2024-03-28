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
    public bool closeInDay = false;
    public string locationInside;
    public bool noSound = false;
    private SoundUse sound;
    // Start is called before the first frame update
    void Start()
    {
        box = FindObjectOfType<MessageBox>();
        gUI = FindObjectOfType<GUIScript>();
        time = FindObjectOfType<TimeGame>();
        sound = FindObjectOfType<SoundUse>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            if (closeInNight) IsNight();
            if (closeInDay) IsDay();
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
                if (closeInDay) gUI.AddText("Opening at 9 pm");
                sound.PlayLock();
            }
        }
    }

    public void IsNight()
    {
        if (time.hour >= 6 && time.hour < 21) isLock = false;
        else isLock = true;
    }

    public void IsDay()
    {
        if (time.hour >= 6 && time.hour < 21) isLock = true;
        else isLock = false;
    }
}
