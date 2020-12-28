using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerksPanel : MonoBehaviour
{
    private Canvas perksCanvas;
    private GUIScript gUI;
    private TypeScene typeSc;
    private MapControl map;
    private FadePanel fade;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        typeSc = FindObjectOfType<TypeScene>();
        map = FindObjectOfType<MapControl>();
        fade = FindObjectOfType<FadePanel>();
        perksCanvas = GetComponent<Canvas>();
        perksCanvas.enabled = false;
    }

    public void Open()
    {
        active = !active;
        perksCanvas.enabled = active;
        gUI.blockGUI = active;
        map.keyActive = !active;
        gUI.DeactiveBtn(!active);
        fade.EnableImg(active);
        typeSc.inMenu = active;
        if (active)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
