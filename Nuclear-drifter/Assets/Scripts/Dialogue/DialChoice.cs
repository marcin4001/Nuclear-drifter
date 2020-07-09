using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialChoice : MonoBehaviour
{
    public Dialogue dial;
    public Text textCh;
    private Button buttonCh;
    private DialogueController controller;
    // Start is called before the first frame update
    void Start()
    {
        buttonCh = GetComponent<Button>();
        controller = FindObjectOfType<DialogueController>();
    }


    public void SetChoice(Dialogue d)
    {
        dial = d;
        textCh.text = dial.choice;
        buttonCh.interactable = true;
    }

    public void Clear()
    {
        dial = null;
        textCh.text = "";
        buttonCh.interactable = false;
    }

    public void Click()
    {
        if(controller == null)controller = FindObjectOfType<DialogueController>();
        controller.SetDialogue(dial);
    }
}
