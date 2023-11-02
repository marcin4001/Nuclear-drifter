using System.Collections;
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
    public bool isRad = false;
    public float radChance = 0.2f;
    public int expEnemy;
    public Sprite deathSprite;
    public int soundIndex = -1;
    public GameObject redSqr;
    public float chanceCritic = 0f;
    public bool explosionDefense = false;
    private CombatSystem system;
    private Collider2D col;
    private GUIScript gUI;
    private SpriteRenderer render;
    private FightSound sound;
    
    public bool isDead()
    {
        return hp <= 0;
    }

    public void Shot(int point)
    {
        hp = hp - point;
    }
    public void Init(CombatSystem sys, FightSound _sound)
    {
        system = sys;
        sound = _sound;
        col = GetComponent<Collider2D>();
        gUI = FindObjectOfType<GUIScript>();
        render = GetComponent<SpriteRenderer>();
        if (redSqr != null)
            redSqr.SetActive(false);
    }

    private void Death()
    {
        if(deathSprite != null)
        {
            render.sprite = deathSprite;
        }
        col.enabled = false;
        system.AddExp(expEnemy);
        anim.SetTrigger("Dead");
        AchievementCounter.global.AddEnemies(nameEnemy);
    }

    public void Attack()
    {
        if (!isDead())
        {
            sound.PlayEnemy(soundIndex);
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
                Death();
            }
            Invoke("AfterDamage", 1.5f);
        }
    }

    public void Bomb()
    {
        if (isDead())
        {
            gUI.AddText(nameEnemy + " was killed!");
            Death();
        }
    }

    private void AfterDamage()
    {
        if(!system.isWin()) system.EnemyRound();
    }

    public void ActiveRedSqr(bool value)
    {
        if(redSqr != null)
            redSqr.SetActive(value);
    }
}
