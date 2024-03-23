using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMisc", menuName = "Item/MiscItem")]
public class MiscItem : Item
{
    public bool isBackpack = false;
    public bool isCigarette = false;
    public bool isMoney = false;
    public int cigaretteDamage = 0;
    private Health hp;
    private void Awake()
    {
        type = ItemType.Misc;
    }

    public void SetHP(Health _hp)
    {
        hp = _hp;
    }

    public override void Use()
    {
        SoundUse sound = FindObjectOfType<SoundUse>();
        if (isBackpack)
        {
            sound.PlayOpenBackpack();
            PropertyPlayer.property.OpenBackpack();
            return;
        }
        if (gUI == null) gUI = FindObjectOfType<GUIScript>();
        if(isCigarette && hp != null)
        {
            hp.Damage(cigaretteDamage);
            if(hp.isDead())
            {
                BadEnding ending = hp.GetComponent<BadEnding>();
                ending.End();
            }
            else
            {
                sound.PlayCough();
            }
            if (gUI != null)
            {
                gUI.AddText("Don't smoke!");
                gUI.AddText("Smoking kills!");
            }
            return;
        }
        if(isMoney)
        {
            MoneyScreen moneyscreen = FindObjectOfType<MoneyScreen>();
            if (moneyscreen != null)
            {
                moneyscreen.OpenMoney();
                return;
            }
        }

        if (gUI != null) gUI.AddText("This cannot be used");
    }
}
