using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemLog : MonoBehaviour
{
    public Item item;
   
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Look", 0.5f);
    }

    private void Look()
    {
        item.Look();
    }
}
