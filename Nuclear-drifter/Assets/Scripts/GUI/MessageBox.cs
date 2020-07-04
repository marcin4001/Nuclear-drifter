using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageBox : MonoBehaviour
{
    private Canvas mbCanvas;
    private PlayerClickMove move;
    public Animator door;
    public string sceneName;
    private Health playerHP;
    private TimeGame time;
    public Vector2 playerPos;
    private GUIScript gUI;
    private PauseMenu menu;
    private SoundsTrigger st;

    //public LoadingScreen loadingSc;
    // Start is called before the first frame update
    void Start()
    {
        mbCanvas = GetComponent<Canvas>();
        move = FindObjectOfType<PlayerClickMove>();
        mbCanvas.enabled = false;
        playerHP = move.GetComponent<Health>();
        time = FindObjectOfType<TimeGame>();
        gUI = FindObjectOfType<GUIScript>();
        menu = FindObjectOfType<PauseMenu>();
        st = FindObjectOfType<SoundsTrigger>();
        
    }

    public void ButtonNo()
    {
        mbCanvas.enabled = false;
        move.active = true;
        move.SetStop(false);
        gUI.blockGUI = false;
        Time.timeScale = 1.0f;
        if (menu != null) menu.activeEsc = true;
    }

    public void ButtonYes()
    {
        mbCanvas.enabled = false;
        if(door != null) door.SetTrigger("Open");
        Invoke("Load", 1.5f);
        Time.timeScale = 1.0f;
        st.OpenDoor();
    }

    public void ShowBox()
    {
        mbCanvas.enabled = true;
        move.active = false;
        move.SetStop(true);
        gUI.blockGUI = true;
        Time.timeScale = 0.0f;
        if (menu != null) menu.activeEsc = false;
    }

    private void Load()
    {
        SetProperty();
        //if (loadingSc != null) loadingSc.ShowLoading();
        SceneManager.LoadScene(sceneName);
    }

    private void SetProperty()
    {
        PropertyPlayer.property.currentHealth = playerHP.currentHealth;
        PropertyPlayer.property.maxHealth = playerHP.maxAfterRad;
        PropertyPlayer.property.isPoison = playerHP.isPoison;
        PropertyPlayer.property.isRad = playerHP.isRad;

        PropertyPlayer.property.day = time.day;
        PropertyPlayer.property.hour = time.hour;
        PropertyPlayer.property.minutes = time.minutes;

        PropertyPlayer.property.startPos = playerPos;
        PropertyPlayer.property.posOutside = move.transform.position;
        PropertyPlayer.property.SaveInvBox();
    }
}
