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
    private PauseMenu menu;
    private MapControl map;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = false;
        anim = GetComponent<Animator>();
        menu = FindObjectOfType<PauseMenu>();
        map = FindObjectOfType<MapControl>();
    }

    public void Fade(string method, GameObject obj)
    {
        objMethod = obj;
        nameMethod = method;
        
        anim.SetTrigger("FadeIn");
        Invoke("FadeOut", 1.4f);
        menu.activeEsc = false;
        map.keyActive = false;
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
        Invoke("Config", 2.2f);
    }

    private void Config()
    {
        menu.activeEsc = true;
        map.keyActive = true;
    }

    //private void Test()
    //{
    //    Debug.Log("Change");
    //}
}
