using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleTextTrig : MonoBehaviour
{
    private GUIScript gUI;
    public string[] texts;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hero")
        {
            foreach (string text in texts)
            {
                gUI.AddText(text);
            }
            Destroy(gameObject);
        }
    }
}
