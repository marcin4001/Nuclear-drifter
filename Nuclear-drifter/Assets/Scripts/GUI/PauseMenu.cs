using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button mapBtn;
    public Button missionBtn;
    private MapControl mc;
    private Canvas pouseCanvas;
    public Canvas exitMsgCanvas;
    private bool activeMenu = false;
    private GUIScript gUI;
    public bool activeEsc = true;
    private TypeScene typeSc;
    // Start is called before the first frame update
    void Start()
    {
        mc = FindObjectOfType<MapControl>();
        pouseCanvas = GetComponent<Canvas>();
        pouseCanvas.enabled = activeMenu;
        gUI = FindObjectOfType<GUIScript>();
        exitMsgCanvas.enabled = false;
        Time.timeScale = 1.0f;
        typeSc = FindObjectOfType<TypeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && activeEsc)
        {
            ActivePouseMenu();
        }
    }

    public void SaveBtn()
    {
        InventoryBox inv = FindObjectOfType<InventoryBox>();
        NPCList npc = FindObjectOfType<NPCList>();
        SaveAndLoad.SaveTemp(inv);
        SaveAndLoad.SaveTemp(npc);
        SaveAndLoad.HardSave();
    }

    public void ActivePouseMenu()
    {
        if (activeEsc)
        {
            activeMenu = !activeMenu;
            mapBtn.enabled = !activeMenu;
            missionBtn.enabled = !activeMenu;
            pouseCanvas.enabled = activeMenu;
            gUI.blockGUI = activeMenu;
            mc.keyActive = !activeMenu;
            if (activeMenu)
            {
                Time.timeScale = 0.0f;
                //typeSc.inMenu = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                //typeSc.inMenu = false;
            }
            typeSc.inMenu = activeMenu;
        }
    }

    public void ExitButton()
    {
        activeEsc = false;
        exitMsgCanvas.enabled = true;
    }

    public void YesButton()
    {
        SaveAndLoad.CloseGame();
        SceneManager.LoadScene(0);
    }

    public void NoButton()
    {
        activeEsc = true;
        exitMsgCanvas.enabled = false;
    }

    public bool GetActive()
    {
        return activeMenu;
    }
}
