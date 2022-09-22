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
    public Canvas saveCanvas;
    public Canvas loadCanvas;
    public SaveTextInfo[] saveInfos;
    public SaveTextInfo[] loadInfos;
    public int currentSaveNo = 0;
    private SoundsTrigger sound;
    private LoadingScreen loading;
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
        saveCanvas.enabled = false;
        loadCanvas.enabled = false;
        sound = FindObjectOfType<SoundsTrigger>();
        loading = FindObjectOfType<LoadingScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && activeEsc)
        {
            ActivePouseEsc();
        }
    }

    public void SaveBtn()
    {
        activeEsc = false;
        saveCanvas.enabled = true;
        foreach(SaveTextInfo save in saveInfos)
        {
            save.SetText();
        }
        sound.PlayClickButton();
    }

    public void LoadBtn()
    {
        activeEsc = false;
        loadCanvas.enabled = true;
        foreach (SaveTextInfo load in loadInfos)
        {
            load.SetText();
        }
        sound.PlayClickButton();
    }

    public void Save(int saveNo)
    {
        sound.PlayClickButton();
        currentSaveNo = saveNo;
        if (SaveAndLoad.CanLoad(saveNo))
        {
            saveMsgCanvas.enabled = true;
        }
        else
        {
            YesButtonSave();
        }
    }

    public void Load(int saveNo)
    {
        sound.PlayClickButton();
        bool isLoad = SaveAndLoad.CanLoad(saveNo);
        if (isLoad)
        {
            loadMsgCanvas.enabled = true;
            currentSaveNo = saveNo;
        }
        else
            gUI.AddText("Save doesn't exist!");
    }
    public void CloseSavePanel()
    {
        activeEsc = true;
        saveCanvas.enabled = false;
        sound.PlayClickButton();
    }

    public void CloseLoadPanel()
    {
        activeEsc = true;
        loadCanvas.enabled = false;
        sound.PlayClickButton();
    }

    public void OptionBtn()
    {
        activeEsc = false;
        options.OpenOptions();
        sound.PlayClickButton();
    }

    public void CloseOptionMenu()
    {
        activeEsc = true;
        options.CloseOptions();
    }

    public void ActivePouseMenu()
    {
        sound.PlayClickButton();
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

    public void ActivePouseEsc()
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
        sound.PlayClickButton();
    }

    public void YesButton()
    {
        SaveAndLoad.CloseGame();
        SceneManager.LoadScene(0);
        sound.PlayClickButton();
    }

    public void NoButton()
    {
        activeEsc = true;
        exitMsgCanvas.enabled = false;
        sound.PlayClickButton();
    }

    public void YesButtonSave()
    {
        InventoryBox inv = FindObjectOfType<InventoryBox>();
        NPCList npc = FindObjectOfType<NPCList>();
        SaveAndLoad.SaveTemp(inv);
        SaveAndLoad.SaveTemp(npc);
        SaveAndLoad.HardSave(currentSaveNo);
        gUI.AddText("The game has been saved!");
        saveMsgCanvas.enabled = false;
        currentSaveNo = 0;
        foreach (SaveTextInfo save in saveInfos)
        {
            save.SetText();
        }
        sound.PlayClickButton();
    }

    public void NoButtonSave()
    {
        saveMsgCanvas.enabled = false;
        sound.PlayClickButton();
    }

    public void YesButtonLoad()
    {
        sound.PlayClickButton();
        loadMsgCanvas.enabled = false;
        loading.ShowLoading();
        //Invoke("LoadAfterLoading", 0.1f);
        SaveAndLoad.Load(currentSaveNo);
        SceneManager.LoadScene(PropertyPlayer.property.currentScene);
    }

    private void LoadAfterLoading()
    {
        SaveAndLoad.Load(currentSaveNo);
        SceneManager.LoadScene(PropertyPlayer.property.currentScene);
    }

    public void NoButtonLoad()
    {
        sound.PlayClickButton();
        loadMsgCanvas.enabled = false;
    }
    

    public bool GetActive()
    {
        return activeMenu;
    }
}
