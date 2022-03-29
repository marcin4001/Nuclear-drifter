using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScaler600p : MonoBehaviour
{
    public CanvasScaler[] scalers;
    public Vector2 minRes;
    public bool runOnStart = false;

    private void Start()
    {
        if(runOnStart)
        {
            ChangeScale(new Vector2(Screen.width, Screen.height));
        }
    }

    public void ChangeScale(Vector2 res)
    {
        Debug.Log(res);
        if (res.x <= minRes.x && res.y <= minRes.y)
        {
            foreach (CanvasScaler scaler in scalers)
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }
        else
        {
            foreach (CanvasScaler scaler in scalers)
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        }
    }
}
