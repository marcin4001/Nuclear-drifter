using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkElement : MonoBehaviour
{
    public Image lamp;
    public Text levelCounter;

    public string namePerk;
    [TextArea(2,3)]
    public string description;
    public int indexPerk;
    public int cost;
    public bool isDisposable = false;
    public int maxLevelPerk = 5;

    private PerksPanel panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = FindObjectOfType<PerksPanel>();
        if (levelCounter == null)
            isDisposable = true;
    }

    public void ClickBtn()
    {
        panel.ShowDesc(this);
    }

    public void ChangeTextLevel(int level)
    {
        if (levelCounter != null)
            levelCounter.text = level + ""; 
    }
}
