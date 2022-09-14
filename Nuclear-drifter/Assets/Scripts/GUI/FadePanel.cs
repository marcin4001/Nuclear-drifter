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
    private string soundMethod = "";
    public Image imgOpacity;
    
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = false;
        if (imgOpacity != null) imgOpacity.enabled = false;
        anim = GetComponent<Animator>();
        menu = FindObjectOfType<PauseMenu>();
        map = FindObjectOfType<MapControl>();
        
    }

    public void EnableImg(bool value)
    {
        if (imgOpacity != null) imgOpacity.enabled = value;
    }

    public void EnableEndImg()
    {
        if (imgOpacity != null)
        {
            imgOpacity.enabled = true;
            menu.activeEsc = false;
            map.keyActive = false;
            if (map.GetActive()) map.OpenMap();
        }
    }

    public void Fade(string method, GameObject obj, float time = 0.0f, string _soundMethod = "")
    {
        objMethod = obj;
        nameMethod = method;
        soundMethod = _soundMethod;
        
        anim.SetTrigger("FadeIn");
        if(soundMethod != "")
            Invoke("SoundMethod", 1.5f);
        Invoke("FadeOut", 1.5f + time);
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

    private void SoundMethod()
    {
        objMethod.SendMessage(soundMethod);
    }

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

    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }

    //private void Test()
    //{
    //    Debug.Log("Change");
    //}
}
