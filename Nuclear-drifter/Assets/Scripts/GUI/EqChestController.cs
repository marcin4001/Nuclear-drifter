using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqChestController : MonoBehaviour
{
    private GUIScript gUI;
    public Canvas canvasEq;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        canvasEq.enabled = active;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (active) Close();
            else Open(); //test
        }
    }

    public void Close()
    {
        active = false;
        canvasEq.enabled = active;
        gUI.move.active = !active;
        gUI.blockGUI = active;
    }

    public void Open()
    {
        active = true;
        canvasEq.enabled = active;
        gUI.move.active = !active;
        gUI.blockGUI = active;
    }
}
