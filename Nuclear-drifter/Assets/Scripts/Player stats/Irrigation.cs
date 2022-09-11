using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Irrigation : MonoBehaviour
{
    private TimeGame time;
    private GUIScript gUI;
    private TypeScene typeScene;
    private Health hp;
    private BadEnding badEnding;
    private MapControl map;
    private TradeSystem trade;
    private DialogueController dialogue;
    private EqChestController eq;
    private bool secondWarning = false;
    private bool isKill = false;
    
    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<TimeGame>();
        gUI = FindObjectOfType<GUIScript>();
        typeScene = FindObjectOfType<TypeScene>();
        hp = GetComponent<Health>();
        badEnding = GetComponent<BadEnding>();
        map = FindObjectOfType<MapControl>();
        trade = FindObjectOfType<TradeSystem>();
        dialogue = FindObjectOfType<DialogueController>();
        eq = FindObjectOfType<EqChestController>();
        gUI.SetIrrigation(!PropertyPlayer.property.isDehydrated);
    }

    // Update is called once per frame
    void Update()
    {
        if(time.minutes == 0 && !hp.isDead())
        {
            if (PropertyPlayer.property.dehydration >= (PropertyPlayer.property.prevDehydration + 6 * 60) && PropertyPlayer.property.isDehydrated && !typeScene.combatState)
            {
                if(map.GetActive())
                    map.OpenMap();
                map.keyActive = false;
                trade.CloseCanvas();
                dialogue.CloseCanvas();
                eq.CloseCanvas();
                hp.Damage(hp.currentHealth);
                badEnding.End();
                return;
            }

            if (PropertyPlayer.property.dehydration >= (24 * 60) && !PropertyPlayer.property.isDehydrated && !typeScene.combatState)
            {
                PropertyPlayer.property.SetIsDehydrated(true);
                gUI.SetIrrigation(false);
                gUI.ClearText();
                gUI.AddText("You are dehydrated!");
                gUI.AddText("You need to drink");
                gUI.AddText("something within 6 hours!");
                gUI.ShowWarning();
                PropertyPlayer.property.SavePrevDehydration();
            }

            if (PropertyPlayer.property.dehydration >= (PropertyPlayer.property.prevDehydration + 5 * 60) && PropertyPlayer.property.isDehydrated && !typeScene.combatState && !secondWarning)
            {
                gUI.ClearText();
                gUI.AddText("You are dehydrated!");
                gUI.AddText("You need to drink");
                gUI.AddText("something within 1 hour!");
                gUI.ShowWarning();
                secondWarning = true;
            }

            
        }
    }

    public void Drink()
    {
        PropertyPlayer.property.ResetDehydration();
        gUI.SetIrrigation(true);
        secondWarning = false;
    }
}
