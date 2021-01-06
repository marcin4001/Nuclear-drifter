using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour
{
    public string nameDevice;
    public Item tool;
    public Item part;
    public List<string> result;
    public bool stateInit = true;
    public int indexDevice = -1;
    public bool isFixed;
    private Inventory inv;
    private PlayerClickMove player;
    private GUIScript gUI;
    private FadePanel fade;
    private Slot _item;
    private SoundsTrigger sound;

    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        if (indexDevice < 0) isFixed = stateInit;
        else isFixed = DeviceList.global.devices[indexDevice].repair;
        fade = FindObjectOfType<FadePanel>();
        sound = FindObjectOfType<SoundsTrigger>();
    }

    public void Use()
    {
        if(isFixed)
        {
            gUI.AddText(nameDevice + " is working");
        }
        else
        {
            if (!SkillsAndPerks.playerSkill.repair)
            {
                gUI.AddText("You can't repair devices!");
                return;
            }
            bool canFix = true;
            result = new List<string>();
            result.Add(nameDevice + " is broken");
            result.Add("Items needed for repair:");
            if(!inv.FindItemB(tool.idItem))
            {
                result.Add("- " + tool.nameItem);
                canFix = false;
            }
            _item = inv.FindItem(part.idItem);
            if(_item == null)
            {
                result.Add("- " + part.nameItem);
                canFix = false;
            }
            if(canFix)
            {
                if(player.ObjIsNearPlayer(transform.position, 1.1f))
                {
                    inv.RemoveOne(_item);
                    sound.UseTool();
                    fade.EnableImg(true);
                    Invoke("Repair", 1.0f);
                }
                else
                {
                    gUI.AddText(nameDevice + " is too far");
                }
            }
            else
            {
                foreach(string res in result)
                {
                    gUI.AddText(res);
                }
            }
        }
    }

    private void Repair()
    {
        fade.EnableImg(false);
        isFixed = true;
        DeviceList.global.devices[indexDevice].repair = isFixed;
        gUI.AddText("Repair was successful!");
    }
}
