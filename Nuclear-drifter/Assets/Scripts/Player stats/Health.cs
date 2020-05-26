using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    public bool isRad = false;
    public bool isPoison = false;
    public bool isFull()
    {
        return (currentHealth >= maxHealth);
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
        }

    }

}
