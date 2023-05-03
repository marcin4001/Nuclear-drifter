using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public int indexEq = 0;
    [Range(0,3)]
    public int indexBackground = 0;
    public string nameObj;
    public bool isLocked = true;
    public int keyId = -1;
    public bool canUsePicklock = false;
    private EqChestController controller;
    private PlayerClickMove player;
    private GUIScript gUI;
    private SoundsTrigger sound;
    private SoundUse soundUse;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<EqChestController>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        sound = FindObjectOfType<SoundsTrigger>();
        soundUse = FindObjectOfType<SoundUse>();
        if(keyId > -1)
        {
            isLocked = !controller.GetKeyUse(indexEq);
        }
    }

    public void Use()
    {
        if (!isLocked)
        {
            OpenBox();
        }
        else
        {
            bool playerHaveKey = controller.GetInvPlayer().FindItemB(keyId);
            if (playerHaveKey)
            {
                if (!player.ObjIsNearPlayer(transform.position, 1.1f))
                {
                    if (indexBackground == 3)
                    {
                        gUI.AddText("The " + nameObj + " is too");
                        gUI.AddText("far");
                    }
                    else
                        gUI.AddText("The " + nameObj + " is too far");
                    return;
                }
                OpenBox();
                isLocked = false;
                controller.SetKeyUse(indexEq);
                gUI.AddText("Lock has been opened");
                if (canUsePicklock)
                {
                    float randomNum = Random.Range(0f, 1f);
                    if(randomNum < 0.6f)
                    {
                        Slot lockpick = controller.GetInvPlayer().FindItem(keyId);
                        controller.GetInvPlayer().RemoveOne(lockpick);
                        gUI.AddText("Your lockpick has been");
                        gUI.AddText("broken");
                    }
                }
            }
            else
            {
                if(indexBackground == 3)
                {
                    gUI.AddText("The " + nameObj + " is");
                    gUI.AddText("locked");
                }
                else
                    gUI.AddText("The " + nameObj + " is locked");
                soundUse.PlayLock();
                if(canUsePicklock)
                {
                    gUI.AddText("You can use a lockpick");
                    gUI.AddText("to open this lock");
                }
            }
        }
    }

    public void OpenBox()
    {
        if (player.ObjIsNearPlayer(transform.position, 1.1f))
        {
            if (indexBackground == 0 || indexBackground == 1)
                sound.PlayOpenChest();
            if (indexBackground == 3)
                sound.PlayRefrigeratorOpen();
            controller.Open(indexEq, indexBackground, gameObject);
        }
        else
        {
            if (indexBackground == 3)
            {
                gUI.AddText("The " + nameObj + " is too");
                gUI.AddText("far");
            }
            else
                gUI.AddText("The " + nameObj + " is too far");
        }
    }

    public void LockOpen()
    {
        isLocked = false;
        controller.SetKeyUse(indexEq);
    }
}
