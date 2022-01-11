using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTextInfo : MonoBehaviour
{
    public Text text;
    public int saveNo;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    public void SetText()
    {
        text.text = SaveAndLoad.GetSaveInfo(saveNo);
    }
}
