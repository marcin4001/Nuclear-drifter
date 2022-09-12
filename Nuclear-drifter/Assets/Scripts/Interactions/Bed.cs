using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private PlayerClickMove move;
    private DayCycle cycle;
    private GUIScript gUI;
    private FadePanel fade;
    private Health hpPlayer;
    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<PlayerClickMove>();
        cycle = FindObjectOfType<DayCycle>();
        gUI = FindObjectOfType<GUIScript>();
        fade = FindObjectOfType<FadePanel>();
        hpPlayer = move.GetComponent<Health>();
    }

    public void Sleep()
    {
        int hour = cycle.GetHour();
        int day = cycle.GetDay();
        int minutes = cycle.GetMinutes();
        int calculateHour = 0;
        if (hour >= 20)
        {
            day = day + 1;
            calculateHour = (24 - hour) + 7;
        }
        if(hour < 7)
        {
            calculateHour = 7 - hour;
        }

        int dehydration = (calculateHour * 60) - minutes;
        PropertyPlayer.property.AddDehydration(dehydration);
        cycle.SetTime(day, 7, 0);
        hpPlayer.AddHalfHealth();
    }
    // Update is called once per frame
    public void Use()
    {
        if (PropertyPlayer.property.isDehydrated)
        {
            gUI.AddText("You are dehydrated!");
            gUI.AddText("You can't use it now!");
            return;
        }
        int hour = cycle.GetHour();
        if(hour >= 20 || hour <= 6)
        {
            if(move.ObjIsNear("Bed", 1.0f))
            {
                fade.Fade("Sleep", gameObject);
            }
            else
            {
                gUI.AddText("The bed is too far");
            }
        }
        else
        {
            gUI.AddText("I can't go to sleep");
        }
    }
}
