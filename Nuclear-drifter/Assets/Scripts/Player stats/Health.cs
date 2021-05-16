using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    public bool isRad = false;
    public bool isPoison = false;
    public int maxAfterRad = 0;
    public int levelRad = 0;
    private RadMeterUI radMeter;

    private void Awake()
    {
        radMeter = FindObjectOfType<RadMeterUI>();
    }

    public bool isFull()
    {
        return (currentHealth >= maxHealth);
    }

    public bool isFullHealth()
    {
        return (isFull() && !isRad && !isPoison);
    }

    public void SetFullHP()
    {
        SetRad(false);
        SetPoison(false);
        SetFull();
    }

    public void SetRadStart(bool value, int level)
    {
        isRad = value;
        levelRad = level;
        if (levelRad == 1)
        {
            maxAfterRad = maxHealth;
            maxHealth = (int)(0.8f * maxHealth);
        }
        else if (levelRad == 2)
        {
            maxHealth = (int)(0.7f * maxAfterRad);
        }
        else if (levelRad == 3)
        {
            maxHealth = (int)(0.6f * maxAfterRad);
        }
        radMeter.SetRadLevel(levelRad);
    }

    public void SetRad(bool value)
    {
        if(value)
        {
            int temp = levelRad + 1;
            if (temp <= 3)
                levelRad = temp;
            if (levelRad == 1)
            {
                maxAfterRad = maxHealth;
                maxHealth = (int)(0.8f * maxHealth);
            }
            else if(levelRad == 2)
            {
                maxHealth = (int)(0.7f * maxAfterRad);
            }
            else if(levelRad == 3)
            {
                maxHealth = (int)(0.6f * maxAfterRad);
            }
            if (currentHealth > maxHealth) currentHealth = maxHealth;
        }
        else
        {
            maxHealth = maxAfterRad;
            levelRad = 0;
        }
        isRad = value;
        radMeter.SetRadLevel(levelRad);
    }

    public void SetPoison(bool value)
    {
        if (value)
        {
            if (value != isPoison) StartCoroutine("Poisoned");
        }
        else
        {
            if (value != isPoison) StopCoroutine("Poisoned");
        }
        isPoison = value;
    }

    private IEnumerator Poisoned()
    {
        while (true)
        {
            if (currentHealth > 20) currentHealth -= 1;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public bool isDead()
    {
        return (currentHealth <= 0);
    }

    public void AddHealth(int points)
    {
        int temp = currentHealth + points;
        if (temp > maxHealth) currentHealth = maxHealth;
        else if (temp < 0) currentHealth = 0;
        else currentHealth = temp;
    }

    public void SetFull()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int points)
    {
        int temp = currentHealth - points;
        if (temp < 0) currentHealth = 0;
        else currentHealth = temp; 
    }

    public void SetMaxHealth(int points)
    {
        if (points > 0)
        {
            maxHealth = points;
            currentHealth = maxHealth;
            maxAfterRad = maxHealth;
            SetRad(false);
            SetPoison(false);
        }

    }

    public void AddToMaxHealth(int points)
    {
        if (isRad)
        {
            SetMaxHealth(maxAfterRad + points);
        }
        else
        {
            SetMaxHealth(maxHealth + points);
        }
    }
}
