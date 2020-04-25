using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{
    private MessageBox box;
    public Animator door;
    // Start is called before the first frame update
    void Start()
    {
        box = FindObjectOfType<MessageBox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            box.ShowBox();
            box.door = door;
        }
    }
}
