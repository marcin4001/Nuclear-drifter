using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum chanceTo
{
    hit, shot
}

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
    public bool goodTrader = false;
    public int meleeWeaponUses = 0;
    public int gunUses = 0;
    public int chanceToHit = 70;
    public int chanceToShot = 70;
    public int chanceToHitDefault = 70;
    public int chanceToShotDefault = 70;
    public List<string> otherSkills;

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

    public void AddPerk(int id, string namePerk)
    {
        if (id < perks.Length)
            return;
        List<Perk> tempList = perks.ToList();
        while (tempList.Count <= id)
        {
            tempList.Add(new Perk());
        }
        perks = tempList.ToArray();
        perks[id].name = namePerk;
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
        goodTrader = false;
        meleeWeaponUses = 0;
        gunUses = 0;
        chanceToHit = chanceToHitDefault;
        chanceToShot = chanceToShotDefault;
    }

    public void AddUses(WeaponItem weapon)
    {
        if (weapon.isBomb)
            return;
        if (weapon.isMeleeWeapon)
        {
            meleeWeaponUses += 1;
            CheckUses(chanceTo.hit);
        }
        else
        {
            gunUses += 1;
            CheckUses(chanceTo.shot);
        }
    }

    public void CheckUses(chanceTo chanceTo_)
    {
        if(chanceTo_ == chanceTo.hit)
        {
            if (chanceToHit < 100 && meleeWeaponUses % 10 == 0)
                chanceToHit += 1;
        }
        if(chanceTo_ == chanceTo.shot)
        {
            if (chanceToShot < 100 && gunUses % 10 == 0)
                chanceToShot += 1;
        }
    }

    public void AddOtherSkill(string skill)
    {
        if (otherSkills == null)
            otherSkills = new List<string>();
        if(!otherSkills.Contains(skill))
            otherSkills.Add(skill);
    }

    public string Skilled(string skill)
    {
        if (otherSkills == null)
            return "☐";
        if (otherSkills.Contains(skill))
            return "■";
        else
            return "☐";
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
