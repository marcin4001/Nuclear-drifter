using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsCanvas : MonoBehaviour
{
    public ScrollRect scroll;
    private Canvas canvasCredits;
    // Start is called before the first frame update
    void Start()
    {
        canvasCredits = GetComponent<Canvas>();
        canvasCredits.enabled = false;
    }

    public void Open()
    {
        scroll.verticalNormalizedPosition = 1.0f;
        canvasCredits.enabled = true;
    }

    public void CloseCredits()
    {
        canvasCredits.enabled = false;
    }
}
