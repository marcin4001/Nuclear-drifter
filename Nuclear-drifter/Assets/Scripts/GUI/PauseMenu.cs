using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button mapBtn;
    private MapControl mc;
    private Canvas pouseCanvas;
    public Canvas exitMsgCanvas;
    private bool activeMenu = false;
    private GUIScript gUI;
    private PlayerClickMove player;
    public bool activeEsc = true;
    // Start is called before the first frame update
    void Start()
    {
        mc = FindObjectOfType<MapControl>();
        pouseCanvas = GetComponent<Canvas>();
        pouseCanvas.enabled = activeMenu;
        gUI = FindObjectOfType<GUIScript>();
        player = FindObjectOfType<PlayerClickMove>();
        exitMsgCanvas.enabled = false;
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && activeEsc)
        {
            ActivePouseMenu();
        }
    }

    public void ActivePouseMenu()
    {
        if (activeEsc)
        {
            activeMenu = !activeMenu;
            mapBtn.enabled = !activeMenu;
            pouseCanvas.enabled = activeMenu;
            gUI.blockGUI = activeMenu;
            mc.keyActive = !activeMenu;
            player.active = !activeMenu;
            if (activeMenu) Time.timeScale = 0.0f;
            else Time.timeScale = 1.0f;
        }
    }

    public void ExitButton()
    {
        activeEsc = false;
        exitMsgCanvas.enabled = true;
    }

    public void YesButton()
    {
        SceneManager.LoadScene(0);
    }

    public void NoButton()
    {
        activeEsc = true;
        exitMsgCanvas.enabled = false;
    }

}
