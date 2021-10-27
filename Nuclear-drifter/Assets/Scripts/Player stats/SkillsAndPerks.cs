using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int meleeWeaponUses = 0;
    public int gunUses = 0;
    public int chanceToHit = 70;
    public int chanceToShot = 70;
    public int chanceToHitDefault = 70;
    public int chanceToShotDefault = 70;

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
                chanceToHit += 2;
        }
        if(chanceTo_ == chanceTo.shot)
        {
            if (chanceToShot < 100 && gunUses % 10 == 0)
                chanceToShot += 2;
        }
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
