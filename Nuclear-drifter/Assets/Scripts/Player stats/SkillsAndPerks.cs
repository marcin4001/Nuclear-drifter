using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsAndPerks : MonoBehaviour
{
    public static SkillsAndPerks playerSkill;

    public Perk[] perks;

    public int handDamage = 5;
    public int additionalGunDamage = 0;
    public bool largeGun = false;
    public bool radResistance = false;
    public bool poisonResistance = false;
    public int damageResistance = 0;
    public bool repair = false;

    void Awake()
    {
        if (!playerSkill)
        {
            DontDestroyOnLoad(this.gameObject);
            playerSkill = this;
        }
        else Destroy(gameObject);
    }

    public Perk GetPerk(int id)
    {
        if (id >= 0 && id < perks.Length)
            return perks[id];
        else
            return null;
    }

    public void SetDefault()
    {
        foreach(Perk perk in perks)
        {
            perk.Reset();
        }
        handDamage = 5;
        additionalGunDamage = 0;
        largeGun = false;
        radResistance = false;
        poisonResistance = false;
        damageResistance = 0;
        repair = false;
    }

    public static string GetJson()
    {
        string json = JsonUtility.ToJson(playerSkill);
        return json;
    }

    public static void JsonToObj(string json)
    {
        JsonUtility.FromJsonOverwrite(json, playerSkill);
    }
}
