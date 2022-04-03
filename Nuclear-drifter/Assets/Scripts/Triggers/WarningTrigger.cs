using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTrigger : MonoBehaviour
{
    public string[] texts;
    private GUIScript gUI;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hero")
        {
            gUI.ClearText();
            foreach (string text in texts)
                gUI.AddText(text);
            gUI.ShowWarning();
            Destroy(gameObject);
        }
    }
}
