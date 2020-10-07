using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string nameEnemy;
    public int damageMax = 0;
    public float dmgChance = 0.5f;

    private CombatSystem system;

    public void Init(CombatSystem sys)
    {
        system = sys;
    }

    public void Attack()
    {
        bool isHurt = system.Damage(this);
        if(isHurt) system.ShowBlood();
        Invoke("AfterAttack", 1.1f);
    }

    private void AfterAttack()
    {
        system.EndAttack();
    }

    public void Damage()
    {
        system.BlockPlayer(true);
        //WeaponUse
        //Sound
        Invoke("AfterDamage", 1.1f);
    }

    private void AfterDamage()
    {
        system.EnemyRound();
    }
}
