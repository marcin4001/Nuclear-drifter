using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTextGUI : MonoBehaviour
{
    public Text textMission;
    public float time = 2f;
    // Start is called before the first frame update
    void Start()
    {
        textMission.enabled = false;
    }

    public void Show(string text = "")
    {
        StartCoroutine(ShowE(text));
    }

    private IEnumerator ShowE(string text)
    {
        if (!string.IsNullOrEmpty(text))
            textMission.text = text;
        textMission.enabled = true;
        yield return new WaitForSeconds(time);
        textMission.enabled = false;
    }
}
