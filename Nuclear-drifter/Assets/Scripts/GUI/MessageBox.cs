using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private Canvas mbCanvas;
    private PlayerClickMove move;
    public Animator door;
    // Start is called before the first frame update
    void Start()
    {
        mbCanvas = GetComponent<Canvas>();
        move = FindObjectOfType<PlayerClickMove>();
        mbCanvas.enabled = false;
    }

    public void ButtonNo()
    {
        mbCanvas.enabled = false;
        move.active = true;
        move.SetStop(false);
    }

    public void ButtonYes()
    {
        mbCanvas.enabled = false;
        door.SetTrigger("Open");
    }

    public void ShowBox()
    {
        mbCanvas.enabled = true;
        move.active = false;
        move.SetStop(true);
    }
}
