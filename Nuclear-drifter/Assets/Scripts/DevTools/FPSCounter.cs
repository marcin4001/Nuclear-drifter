using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text text;
    private float timer = 0f;
    private float timerMax = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DevTools.dev != null)
        {
            if(DevTools.dev.activeFPSCounter)
            {
                if (Time.timeScale > 0.1)
                {
                    if (timer > timerMax)
                    {
                        int fps = Mathf.CeilToInt(1.0f / Time.unscaledDeltaTime);
                        text.text = fps + " FPS";
                        timer = 0f;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                }
            }
            else
            {
                text.text = "";
            }
        }
        else
        {
            text.text = "";
        }
    }
}
