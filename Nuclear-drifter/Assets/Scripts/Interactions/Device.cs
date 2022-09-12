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
    public float minDistance = 1.1f;
    private Inventory inv;
    private PlayerClickMove player;
    private GUIScript gUI;
    private FadePanel fade;
    private Slot _item;
    private SoundsTrigger sound;
    private ChangeSpriteDevice changeSprite;
    private JobDevice job;

    // Start is called before the first frame update
    void Start()
    {
        changeSprite = GetComponent<ChangeSpriteDevice>();
        inv = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        if (indexDevice < 0) isFixed = stateInit;
        else isFixed = DeviceList.global.devices[indexDevice].repair;
        if (changeSprite != null)
            changeSprite.Change(isFixed);
        fade = FindObjectOfType<FadePanel>();
        sound = FindObjectOfType<SoundsTrigger>();
        job = GetComponent<JobDevice>();
        if (isFixed)
            tag = "Well";
    }

    public void Use()
    {
        if(isFixed)
        {
            if (job == null)
                gUI.AddText(nameDevice + " is working");
            else
                job.Work();
        }
        else
        {
            if (!SkillsAndPerks.playerSkill.repair)
            {
                gUI.AddText("You can't repair devices!");
                return;
            }
            if (PropertyPlayer.property.isDehydrated)
            {
                gUI.AddText("You are dehydrated!");
                gUI.AddText("You can't use it now!");
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
                if(player.ObjIsNearPlayer(transform.position, minDistance))
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
        if (changeSprite != null)
            changeSprite.Change(isFixed);
        DeviceList.global.devices[indexDevice].repair = isFixed;
        gUI.AddText("Repair was successful!");
        tag = "Well";
    }
}
