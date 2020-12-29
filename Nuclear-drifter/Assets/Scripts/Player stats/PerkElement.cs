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

    private PerksPanel panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = FindObjectOfType<PerksPanel>();
    }

    public void ClickBtn()
    {
        panel.ShowDesc(this);
    }
}
