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

    public void SetRad(bool value)
    {
        
        if(value)
        {
            if (!isRad)
            {
                maxAfterRad = maxHealth;
                maxHealth = (int)(0.8f * maxHealth);
                if (currentHealth > maxHealth) currentHealth = maxHealth;
            }
        }
        else
        {
            maxHealth = maxAfterRad;
        }
        isRad = value;
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
            isRad = false;
            isPoison = false;
        }

    }

}
