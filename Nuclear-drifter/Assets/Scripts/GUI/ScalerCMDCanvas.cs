using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalerCMDCanvas : MonoBehaviour
{
    public Vector2 resChange;
    private CanvasScaler scaler;

    // Start is called before the first frame update
    void Start()
    {
        scaler = GetComponent<CanvasScaler>();
    }

    // Update is called once per frame
    void Update()
    {
        Resolution res = Screen.currentResolution;
        Debug.Log(res);
        if(res.width == resChange.x && res.height == resChange.y)
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        }
        else
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }
    }
}
