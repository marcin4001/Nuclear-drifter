using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScreen : MonoBehaviour
{
    private GUIScript gUI;
    private MapControl map;
    private Canvas moneyCanvas;
    private TypeScene typeSc;

    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        map = FindObjectOfType<MapControl>();
        moneyCanvas = GetComponent<Canvas>();
        typeSc = FindObjectOfType<TypeScene>();
        moneyCanvas.enabled = false;
    }

    public void OpenMoney()
    {
        moneyCanvas.enabled = true;
        map.keyActive = false;
        gUI.DeactiveBtn(false);
        typeSc.inMenu = true;
        Time.timeScale = 0.0f;
    }

    public void Close()
    {
        moneyCanvas.enabled = false;
        map.keyActive = true;
        gUI.DeactiveBtn(true);
        typeSc.inMenu = false;
        Time.timeScale = 1.0f;
    }

}
