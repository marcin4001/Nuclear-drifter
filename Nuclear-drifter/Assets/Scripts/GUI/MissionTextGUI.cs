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

    public void Show()
    {
        StartCoroutine(ShowE());
    }

    private IEnumerator ShowE()
    {
        textMission.enabled = true;
        yield return new WaitForSeconds(time);
        textMission.enabled = false;
    }
}
