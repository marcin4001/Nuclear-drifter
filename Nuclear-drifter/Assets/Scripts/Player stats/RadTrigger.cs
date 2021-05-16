using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadTrigger : MonoBehaviour
{
    public Health health;
    private SoundsTrigger st;
    public bool playerIsNear = false;
    public float counter = 0.0f;
    private float counterMaxCurrent= 2.5f;
    private float counterMaxNoRes = 2.5f;
    private float counterMaxRes = 3.5f;
    private bool death = false;
    private TypeScene typeSc;
    private bool radRes = false;

    void Start()
    {
        typeSc = FindObjectOfType<TypeScene>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        radRes = SkillsAndPerks.playerSkill.radResistance;
        if (radRes)
            counterMaxCurrent = counterMaxRes;
        else
            counterMaxCurrent = counterMaxNoRes;
        health = collision.GetComponentInParent<Health>();
        if (health != null && !typeSc.combatState)
        {
            playerIsNear = true;
            typeSc.radZone = true;
            if (st == null) st = FindObjectOfType<SoundsTrigger>();
            if (st != null) st.StartGeiger();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        health = collision.GetComponentInParent<Health>();
        if (health != null && !typeSc.combatState)
        {
            playerIsNear = true;
            //if(!health.isRad)Invoke("SetRad", radRes ? 3.5f : 2.5f);
            typeSc.radZone = true;
            if (st == null) st = FindObjectOfType<SoundsTrigger>();
            if (st != null) if(!st.isPlayed()) st.StartGeiger();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        health = collision.GetComponentInParent<Health>();
        if (health != null && !typeSc.combatState)
        {
            typeSc.radZone = false;
            playerIsNear = false;
            if (st == null) st = FindObjectOfType<SoundsTrigger>();
            if (st != null) st.Stop();
        }
    }

    private void SetRad()
    {
        if(playerIsNear)health.SetRad(true);
    }

    void Update()
    {
        if(health != null && !typeSc.combatState)
        {
            if(playerIsNear && !health.isDead())
            {
                if(counter >= counterMaxCurrent)
                {
                    if(health.levelRad < 3)
                        health.SetRad(true);
                    else
                    {
                        if (!death) health.Damage(health.currentHealth);
                    }
                        
                    counter = 0.0f;
                }
                else
                {
                    counter += Time.deltaTime;
                }
            }
            else
            {
                counter = 0.0f;
            }
            if(health.isDead() && !death)
            {
                BadEnding ending = health.GetComponent<BadEnding>();
                if (ending != null) ending.End();
                death = true;
            }
        }
    }
}
