using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public bool initState = false;
    private bool isEating;
    private Animator anim;
    private string propsIE = "isEating";
    // Start is called before the first frame update
    void Start()
    {
        isEating = initState;
        anim = GetComponent<Animator>();
        anim.SetBool(propsIE, isEating);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    ChangeState();
        //}
    }

    public void ChangeState()
    {
        isEating = !isEating;
        anim.SetBool(propsIE, isEating);
    }
}
