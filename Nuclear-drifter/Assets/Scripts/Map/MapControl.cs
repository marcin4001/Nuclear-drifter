using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public Canvas mapCanvas;
    public Camera mapCam;
    private bool actveMap = false;
    private GUIScript gUI;
    private PlayerClickMove player;
    public bool keyActive = true;
    // Start is called before the first frame update
    void Start()
    {
        mapCanvas.enabled = false;
        mapCam.enabled = false;
        gUI = FindObjectOfType<GUIScript>();
        player = FindObjectOfType<PlayerClickMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && keyActive)
        {
            OpenMap();
        }
    }

    public void OpenMap()
    {
        actveMap = !actveMap;
        mapCam.enabled = actveMap;
        mapCanvas.enabled = actveMap;
        gUI.blockGUI = actveMap;
        player.active = !actveMap;
    }

    public bool GetActive()
    {
        return actveMap;
    }
}
