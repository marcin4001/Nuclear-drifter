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
    private EqChestController controller;
    private PlayerClickMove player;
    private GUIScript gUI;
    // Start is called before the first frame update
    private void Start()
    {
        controller = FindObjectOfType<EqChestController>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
    }

    public void Use()
    {
        if (!isLocked)
        {
            if (player.ObjIsNearPlayer(transform.position, 1.1f))
                controller.Open(indexEq, indexBackground);
            else gUI.AddText("The " + nameObj + " is too far");
        }
        else gUI.AddText("The " + nameObj + " is locked");
    }


}
