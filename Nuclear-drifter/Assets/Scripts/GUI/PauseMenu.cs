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
    public Canvas saveMsgCanvas;
    public Canvas loadMsgCanvas;
    private bool activeMenu = false;
    private GUIScript gUI;
    public bool activeEsc = true;
    private TypeScene typeSc;
    private OptionsMenu options;
    // Start is called before the first frame update
    void Start()
    {
        mc = FindObjectOfType<MapControl>();
        pouseCanvas = GetComponent<Canvas>();
        pouseCanvas.enabled = activeMenu;
        gUI = FindObjectOfType<GUIScript>();
        exitMsgCanvas.enabled = false;
        saveMsgCanvas.enabled = false;
        loadMsgCanvas.enabled = false;
        Time.timeScale = 1.0f;
        typeSc = FindObjectOfType<TypeScene>();
        options = FindObjectOfType<OptionsMenu>();
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
        activeEsc = false;
        saveMsgCanvas.enabled = true;
    }

    public void LoadBtn()
    {
        bool isLoad = SaveAndLoad.CanLoad(1);
        if (isLoad)
        {
            activeEsc = false;
            loadMsgCanvas.enabled = true;
        }
        else
            gUI.AddText("Save doesn't exist!");
    }

    public void OptionBtn()
    {
        activeEsc = false;
        options.OpenOptions();
    }

    public void CloseOptionMenu()
    {
        activeEsc = true;
        options.CloseOptions();
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
            gUI.perksBtn.enabled = !activeMenu;
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

    public void YesButtonSave()
    {
        InventoryBox inv = FindObjectOfType<InventoryBox>();
        NPCList npc = FindObjectOfType<NPCList>();
        SaveAndLoad.SaveTemp(inv);
        SaveAndLoad.SaveTemp(npc);
        SaveAndLoad.HardSave(1);
        gUI.AddText("The game has been saved!");
        activeEsc = true;
        saveMsgCanvas.enabled = false;
    }

    public void NoButtonSave()
    {
        activeEsc = true;
        saveMsgCanvas.enabled = false;
    }

    public void YesButtonLoad()
    {
        SaveAndLoad.Load(1);
        SceneManager.LoadScene(PropertyPlayer.property.currentScene);
    }

    public void NoButtonLoad()
    {
        activeEsc = true;
        loadMsgCanvas.enabled = false;
    }
    

    public bool GetActive()
    {
        return activeMenu;
    }
}
