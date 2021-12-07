using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOpenDoorKey : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            anim.SetTrigger("Open");
        if (Input.GetKeyDown(KeyCode.F2))
            anim.SetTrigger("Reset");
    }
}
