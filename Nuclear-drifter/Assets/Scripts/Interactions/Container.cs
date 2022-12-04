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
                OpenBox();
                isLocked = false;
                controller.SetKeyUse(indexEq);
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
