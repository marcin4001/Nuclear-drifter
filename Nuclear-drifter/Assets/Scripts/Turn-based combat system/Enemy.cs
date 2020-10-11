﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string nameEnemy;
    public int damageMax = 0;
    public float dmgChance = 0.5f;
    public int hp;
    public Animator anim;
    public bool isPoisons = false;
    public float poisonChance = 0.2f;
    public int expEnemy;
    private CombatSystem system;
    private Collider2D col;
    private GUIScript gUI;
    
    public bool isDead()
    {
        return hp <= 0;
    }

    public void Shot(int point)
    {
        hp = hp - point;
    }
    public void Init(CombatSystem sys)
    {
        system = sys;
        col = GetComponent<Collider2D>();
        gUI = FindObjectOfType<GUIScript>();
    }

    public void Attack()
    {
        if (!isDead())
        {
            bool isHurt = system.Damage(this);
            if (isHurt) system.ShowBlood();
            Invoke("AfterAttack", 1.1f);
        }
        else
        {
            Invoke("AfterAttack", 0.1f);
        }
    }

    private void AfterAttack()
    {
        system.EndAttack();
    }

    public void Damage()
    {
        system.BlockPlayer(true);
        system.UseWeapon(this);
        if (!system.WeaponIsBomb())
        {
            if (isDead())
            {
                gUI.AddText(nameEnemy + " was killed!");
                col.enabled = false;
                system.AddExp(expEnemy);
                anim.SetTrigger("Dead");
            }
            Invoke("AfterDamage", 2f);
        }
    }

    public void Bomb()
    {
        if (isDead())
        {
            gUI.AddText(nameEnemy + " was killed!");
            col.enabled = false;
            system.AddExp(expEnemy);
            anim.SetTrigger("Dead");
        }
    }

    private void AfterDamage()
    {
        if(!system.isWin()) system.EnemyRound();
    }
}