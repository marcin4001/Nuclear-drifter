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

}
