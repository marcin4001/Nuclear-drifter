using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FadePanel : MonoBehaviour
{
    private Image img;
    private Animator anim;
    private string nameMethod = "";
    private GameObject objMethod;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = false;
        anim = GetComponent<Animator>();
    }

    public void Fade(string method, GameObject obj)
    {
        objMethod = obj;
        nameMethod = method;

        anim.SetTrigger("FadeIn");
        Invoke("FadeOut", 1.4f);
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.F))
    //    {
    //        Fade("Test", gameObject);
    //    }
    //}

    private void FadeOut()
    {
        objMethod.SendMessage(nameMethod);
        anim.SetTrigger("FadeOut");
    }

    //private void Test()
    //{
    //    Debug.Log("Change");
    //}
}
