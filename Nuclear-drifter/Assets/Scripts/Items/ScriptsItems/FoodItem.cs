using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFood", menuName = "Item/FoodItem")]
public class FoodItem : Item
{
    public int addHP = 0;
    public bool healsPoison = false;
    public bool healsRad = false;
    public bool isPoison = false;
    public bool addRad = false;
    private Health hp;

    private void Awake()
    {
        type = ItemType.Food;
    }

    public void SetHP(Health _hp)
    {
        hp = _hp;
    }

    public override void Use()
    {
        Debug.Log("Use: " + name);
        if(hp != null)
        {
            hp.AddHealth(addHP);
            if (healsPoison) hp.isPoison = false;
            if (healsRad) hp.isRad = false;
            if (isPoison) hp.isPoison = true;
            if (addRad) hp.isRad = true;
        }
    }
}
