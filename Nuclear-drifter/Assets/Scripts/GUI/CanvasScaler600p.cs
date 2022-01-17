using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScaler600p : MonoBehaviour
{
    public CanvasScaler[] scalers;
    public Vector2 minRes;


    public void ChangeScale(Vector2 res)
    {
        Debug.Log(res);
        if (res.x <= minRes.x && res.y <= minRes.y)
        {
            foreach (CanvasScaler scaler in scalers)
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            Debug.Log(2);
        }
        else
        {
            foreach (CanvasScaler scaler in scalers)
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            Debug.Log(1);
        }
    }
}
