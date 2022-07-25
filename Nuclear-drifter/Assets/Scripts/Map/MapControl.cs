using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapControl : MonoBehaviour
{
    public Canvas mapCanvas;
    public Camera mapCam;
    private bool actveMap = false;
    private GUIScript gUI;
    private PlayerClickMove player;
    public bool keyActive = true;
    private TypeScene typeSc;
    private PauseMenu pause;
    private SoundsTrigger sound;

    // Start is called before the first frame update
    void Start()
    {
        mapCanvas.enabled = false;
        mapCam.enabled = false;
        gUI = FindObjectOfType<GUIScript>();
        typeSc = FindObjectOfType<TypeScene>();
        player = FindObjectOfType<PlayerClickMove>();
        pause = FindObjectOfType<PauseMenu>();
        sound = FindObjectOfType<SoundsTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && keyActive)
        {
            OpenMapEsc();
        }
    }

    public void OpenMap()
    {
        actveMap = !actveMap;
        mapCam.enabled = actveMap;
        mapCanvas.enabled = actveMap;
        gUI.blockGUI = actveMap;
        player.active = !actveMap;
        typeSc.inMenu = actveMap;
        gUI.DeactiveButtons(!actveMap);
        pause.activeEsc = !actveMap;
        sound.PlayClickButton();
    }

    public void OpenMapEsc()
    {
        actveMap = !actveMap;
        mapCam.enabled = actveMap;
        mapCanvas.enabled = actveMap;
        gUI.blockGUI = actveMap;
        player.active = !actveMap;
        typeSc.inMenu = actveMap;
        gUI.DeactiveButtons(!actveMap);
        pause.activeEsc = !actveMap;
    }

    public bool GetActive()
    {
        return actveMap;
    }
}
