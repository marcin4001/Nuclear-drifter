using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private Canvas dialCanvas;
    private bool testB = false;
    private MapControl map;
    private PauseMenu menu;
    // Start is called before the first frame update
    void Start()
    {
        dialCanvas = GetComponent<Canvas>();
        map = FindObjectOfType<MapControl>();
        menu = FindObjectOfType<PauseMenu>();
        dialCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            testB = !testB;
            if (testB) OpenDialogue();
            else Close();
        }
    }

    public void Close()
    {
        dialCanvas.enabled = false;
        map.keyActive = true;
        menu.activeEsc = true;
    }

    public void OpenDialogue()
    {
        dialCanvas.enabled = true;
        map.keyActive = false;
        menu.activeEsc = false;
    }
}
