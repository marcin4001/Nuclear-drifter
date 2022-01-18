using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public int indexEq = 0;
    [Range(0,2)]
    public int indexBackground = 0;
    public string nameObj;
    public bool isLocked = true;
    public int keyId = -1;
    private EqChestController controller;
    private PlayerClickMove player;
    private GUIScript gUI;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<EqChestController>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
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
            if(playerHaveKey)
            {
                OpenBox();
                isLocked = false;
                controller.SetKeyUse(indexEq);
            }
            else gUI.AddText("The " + nameObj + " is locked");
        }
    }

    public void OpenBox()
    {
        if (player.ObjIsNearPlayer(transform.position, 1.1f))
            controller.Open(indexEq, indexBackground, gameObject);
        else gUI.AddText("The " + nameObj + " is too far");
    }

    public void LockOpen()
    {
        isLocked = false;
        controller.SetKeyUse(indexEq);
    }
}
