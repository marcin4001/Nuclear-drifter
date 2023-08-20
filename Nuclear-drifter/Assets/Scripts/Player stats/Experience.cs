using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public int currentExp = 0;
    public int level = 1;
    public int prevTh = 0;
    public int lvlPoint = 0;
    private GUIScript gUI;
    // Start is called before the first frame update
    void Start()
    {
        currentExp = PropertyPlayer.property.currentExp;
        level = PropertyPlayer.property.level;
        prevTh = PropertyPlayer.property.prevTh;
        lvlPoint = PropertyPlayer.property.lvlPoint;
        gUI = FindObjectOfType<GUIScript>();
        int expLexel = currentExp - (25 * (5 * level + 4) * (level - 1));
        int maxExpLevel = (25 * (5 * (level + 1) + 4) * level) - (25 * (5 * level + 4) * (level - 1));
        gUI.ShowExp(level, expLexel, maxExpLevel);
    }

    public void AddExp(int exp)
    {
        if (exp > 0) gUI.AddText("You got " + exp + " exp");
        currentExp = currentExp + exp;
        while(currentExp >= 25 * (5 * (level + 1) + 4) * level)
        {
            prevTh = 25 * (5 * (level + 1) + 4) * level;
            level = level + 1;
            lvlPoint = lvlPoint + 1;
            gUI.ShowNextLvl();
        }
        UpdateProperty();
        int expLexel = currentExp - (25 * (5 * level + 4) * (level - 1));
        int maxExpLevel = (25 * (5 * (level + 1) + 4) * level) - (25 * (5 * level + 4) * (level - 1));
        gUI.ShowExp(level, expLexel, maxExpLevel);
    }

    public bool RemoveLevelPoints(int point)
    {
        int temp = lvlPoint - point;
        if(temp >= 0)
        {
            lvlPoint = temp;
            UpdateProperty();
            return true;
        }
        else
            return false;
    }

    private void UpdateProperty()
    {
        PropertyPlayer.property.currentExp = currentExp;
        PropertyPlayer.property.level = level;
        PropertyPlayer.property.prevTh = prevTh;
        PropertyPlayer.property.lvlPoint = lvlPoint;
    }

}
