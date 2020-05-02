using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArea : MonoBehaviour
{
    private SignController controller;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<SignController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Hero")
        {
            controller.FoundArea(index);
        }

    }
}
