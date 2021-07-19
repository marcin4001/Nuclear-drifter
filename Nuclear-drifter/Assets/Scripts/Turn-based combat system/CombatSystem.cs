using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public Camera camMain;
    public Camera camBattle;
    public Canvas battleCanvas;
    private TypeScene typeSc;
    private PlayerClickMove player;
    private Vector3 currentCamPos;
    private EnemyTrigger enemyTr;
    private GUIScript gUI;
    public Transform[] enemyPoints;
    public List<GameObject> enemysObjs;
    public List<Enemy> enemies;
    public GameObject rootBloodSc;
    public Animator animBlood;
    private bool playerRound = true;
    public bool isAttack = false;
    public int currentIndex = 0;
    public WeaponItem currentWeapon;
    public int handDamage = 2;
    public int soundHand = 0;
    public Slot weaponSlot;
    public int actionPoint = 2;
    public int maxAP = 2;
    public int sumExp;
    private Experience experience;
    public GameObject lightNight;
    public Animator animBomb;
    private FightSound fightSound;
    private Inventory inv;
    private Health hpPlayer;
    private BadEnding ending;
    private MapControl map;
    private SoundsTrigger soundsMain;
    private DayCycle cycle;
    private GridNode grid;
    // Start is called before the first frame update
    void Start()
    {
        typeSc = FindObjectOfType<TypeScene>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        hpPlayer = FindObjectOfType<Health>();
        ending = player.GetComponent<BadEnding>();
        map = FindObjectOfType<MapControl>();
        inv = FindObjectOfType<Inventory>();
        fightSound = GetComponent<FightSound>();
        experience = FindObjectOfType<Experience>();
        soundsMain = FindObjectOfType<SoundsTrigger>();
        grid = FindObjectOfType<GridNode>();
        cycle = FindObjectOfType<DayCycle>();
        battleCanvas.enabled = false;
        enemysObjs = new List<GameObject>();
        enemies = new List<Enemy>();
        BlockPlayer(false);
        camBattle.enabled = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (typeSc.combatState)
        {
            if (!playerRound)
            {
                if (!isAttack)
                {
                    if (currentIndex < enemies.Count)
                    {
                        enemies[currentIndex].Attack();
                        currentIndex += 1;
                        isAttack = true;
                    }
                    else
                    {
                        currentIndex = 0;
                        playerRound = true;
                        ResetAP();
                        BlockPlayer(false);
                        ShowWeaponCurrent();
                    }
                }
            }
            lightNight.SetActive(typeSc.lightNight);
        }
    }

    public bool WeaponIsBomb()
    {
        if (currentWeapon != null)
        {
            if (weaponSlot.isBomb())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void ResetAP()
    {
        actionPoint = maxAP;
    }

    public void UseAP()
    {
        actionPoint -= 1;
        if(actionPoint <= 0)
        {
            gUI.ClearText();
            gUI.AddText("End of your round");
            BlockPlayer(true);
            Invoke("AfterEat", 1.5f);
        }
        else
        {
            gUI.AddText("You have " + actionPoint + " more action");
            gUI.AddText("point!");
        }
    }

    private void AfterEat()
    {
        EnemyRound();
    }

    public void ClearWeapon()
    {
        weaponSlot = null;
        currentWeapon = null;
    }

    public void SelectHand()
    {
        ClearWeapon();
        ShowWeaponStat();
    }

    public void UseWeapon(Enemy enemy)
    {
        gUI.ClearText();
        if(currentWeapon == null)
        {
            fightSound.PlayWeapon(soundHand);
            enemy.Shot(handDamage);
            gUI.AddText(enemy.nameEnemy + " was hit!");
            gUI.AddText(enemy.nameEnemy + " lost " + handDamage + "hp");
        }
        else
        {
            if(weaponSlot.isGun())
            {
                fightSound.PlayWeapon(currentWeapon.soundId);
                int finalDamage = currentWeapon.damage + SkillsAndPerks.playerSkill.additionalGunDamage;
                if (Random.Range(0f, 1f) <= currentWeapon.criticChance)
                {
                    finalDamage = finalDamage * 2;
                    gUI.AddText("Critical Shot!!!");
                }
                enemy.Shot(finalDamage);
                inv.RemoveOne(weaponSlot);
                gUI.AddText(enemy.nameEnemy + " was hit!");
                gUI.AddText(enemy.nameEnemy + " lost " + finalDamage + "hp");
                if(weaponSlot.isOutOfAmmo())
                {
                    gUI.AddText("Out of ammo!");
                    ClearWeapon();
                }
            }
            else if(weaponSlot.isBomb())
            {
                fightSound.PlayWeapon(currentWeapon.soundId);
                animBomb.SetTrigger("Boom");
                Invoke("BombDamage", 1.1f);
                Invoke("RemoveBomb", 0.1f);
            }
            else
            {
                int hitDamage = currentWeapon.damage;
                if (Random.Range(0f, 1f) <= currentWeapon.criticChance)
                {
                    hitDamage = hitDamage * 2;
                    gUI.AddText("Critical hit!!!");
                }
                fightSound.PlayWeapon(currentWeapon.soundId);
                enemy.Shot(hitDamage);
                gUI.AddText(enemy.nameEnemy + " was hit!");
                gUI.AddText(enemy.nameEnemy + " lost " + hitDamage + "hp");
            }
        }
    }

    public void AddExp(int point)
    {
        sumExp += point;
    }

    private void RemoveBomb()
    {
        inv.RemoveOne(weaponSlot);
    }
    private void BombDamage()
    {
        foreach(Enemy e in enemies)
        {
            if (!e.isDead())
            {
                e.Shot(currentWeapon.damage);
                gUI.AddText(e.nameEnemy + " was hit!");
                gUI.AddText(e.nameEnemy + " lost " + currentWeapon.damage + "hp");
                e.Bomb();
            }
        }
        ClearWeapon();
        Invoke("AfterBomb", 2f);
    }

    private void AfterBomb()
    {
        if (!isWin()) EnemyRound();
    }

    public void SelectWeapon(WeaponItem weapon)
    {
        bool largeGun = new[] { 0, 4 }.Contains(weapon.idItem);
        Slot temp = inv.FindItem(weapon.idItem);
        if (temp != null)
        {
            if(temp.isGun() && temp.ammo <= 0)
            {
                gUI.AddText("Out of ammo!");
                return;
            }
            if (largeGun && !SkillsAndPerks.playerSkill.largeGun)
            {
                gUI.AddText("It's a large gun!");
                gUI.AddText("You can't use it now!");
                return;
            }
            weaponSlot = temp;
            currentWeapon = weapon;
            ShowWeaponStat();
        }
    }

    public void ShowWeaponCurrent()
    {
        gUI.ClearText();
        gUI.AddText("Current weapon:");
        if (currentWeapon != null)
        {
            gUI.AddText("Name: " + currentWeapon.nameItem);
            gUI.AddText(currentWeapon.description);
            gUI.AddText("Damage: " + currentWeapon.damage);
            if (weaponSlot.isGun())
            {
                gUI.AddText("Ammo: " + weaponSlot.ammo);
            }
            else
            {
                gUI.AddText("Amount: " + weaponSlot.amountItem);
            }
        }
        else
        {
            gUI.AddText("Name: Hand");
            gUI.AddText("Default");
            gUI.AddText("Damage: " + handDamage);
        }
    }

    public void ShowWeaponStat()
    {
        gUI.ClearText();
        gUI.AddText("Your choice:");
        if(currentWeapon != null)
        {
            gUI.AddText("Name: " + currentWeapon.nameItem);
            gUI.AddText(currentWeapon.description);
            gUI.AddText("Damage: " + currentWeapon.damage);
            if (weaponSlot.isGun())
            {
                gUI.AddText("Ammo: " + weaponSlot.ammo);
            }
            else
            {
                gUI.AddText("Amount: " + weaponSlot.amountItem);
            }
        }
        else
        {
            gUI.AddText("Name: Hand");
            gUI.AddText("Default");
            gUI.AddText("Damage: " + handDamage);
        }
    }
    

    public bool isWin()
    {
        bool allDead = true;
        foreach(Enemy n in enemies)
        {
            allDead = allDead & n.isDead();
        }
        if (allDead)
        {
            gUI.ClearText();
            gUI.AddText("You win!");
            BlockPlayer(false);
            enemyTr.GiveReward();
            enemyTr.Deactive();
            grid.UpdateGrid();
            SkipFight();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void EndAttack()
    {
        if(!hpPlayer.isDead()) isAttack = false;
        else
        {
            SkipFight();
            ending.End();
        }
        
    }

    public void EnemyRound()
    {
        //ClearWeapon();
        playerRound = false;
    }

    public bool Damage(Enemy _enemy)
    {
        bool isRad = SkillsAndPerks.playerSkill.radResistance ? false : _enemy.isRad;
        bool isPoisons = SkillsAndPerks.playerSkill.poisonResistance ? false : _enemy.isPoisons;
        float rngChance = Random.Range(0.0f, 1.0f);
        Debug.Log(rngChance);
        if(rngChance <= _enemy.dmgChance)
        {
            int resistance = Mathf.RoundToInt((float)_enemy.damageMax * (float)SkillsAndPerks.playerSkill.damageResistance / 100f);
            gUI.AddText("You were hit!");
            gUI.AddText("You lost " + (_enemy.damageMax - resistance) + "HP!");
            hpPlayer.Damage(_enemy.damageMax - resistance);
            if (isPoisons && rngChance <= _enemy.poisonChance)
            {
                hpPlayer.SetPoison(true);
            }
            if(isRad && rngChance <= _enemy.radChance)
            {
                hpPlayer.SetRad(true);
            }
            return true;
        }
        else
        {
            gUI.AddText(_enemy.nameEnemy + " missed!");
            return false;
        }
    }


    public void StartFight(EnemyTrigger trigger)
    {
        player.SetStop(true);
        if (map.GetActive()) map.OpenMap();
        camMain.enabled = false;
        camBattle.enabled = true;
        typeSc.combatState = true;
        enemyTr = trigger;
        battleCanvas.enabled = true;
        gUI.ActiveBtnPanel(false);
        map.keyActive = false;
        SetEnemys();
        handDamage = SkillsAndPerks.playerSkill.handDamage;
        ShowWeaponCurrent();
        BlockPlayer(false);
        playerRound = true;
        isAttack = false;
        soundsMain.Mute(true);
        ResetAP();
        cycle.SetSlowTime();
        player.RoundPosPlayer();
    }

    private void SetEnemys()
    {
        if(enemyTr.enemys.Length == 1)
        {
            enemysObjs.Add(Instantiate(enemyTr.enemys[0], enemyPoints[0].position, Quaternion.identity));
        }
        else if (enemyTr.enemys.Length == 2)
        {
            enemysObjs.Add(Instantiate(enemyTr.enemys[0], enemyPoints[1].position, Quaternion.identity));
            enemysObjs.Add(Instantiate(enemyTr.enemys[1], enemyPoints[2].position, Quaternion.identity));
        }
        else if (enemyTr.enemys.Length == 3)
        {
            for(int i = 0; i < 3; i++)
            {
                enemysObjs.Add(Instantiate(enemyTr.enemys[i], enemyPoints[i].position, Quaternion.identity));
            }
        }

        foreach(GameObject e in enemysObjs)
        {
            Enemy enemy = e.GetComponent<Enemy>();
            enemy.Init(this, fightSound);
            enemies.Add(enemy);
        }
    }

    private void ClearArea()
    {
        enemies.Clear();
        foreach(GameObject e in enemysObjs)
        {
            Destroy(e);
        }
        enemysObjs.Clear();
    }
    public void SkipFight()
    {
        ClearWeapon();
        experience.AddExp(sumExp);
        sumExp = 0;
        camMain.enabled = true;
        camBattle.enabled = false;
        player.SetLocalPosCamera();
        typeSc.combatState = false;
        battleCanvas.enabled = false;
        gUI.ActiveBtnPanel(true);
        map.keyActive = true;
        enemyTr = null;
        soundsMain.Mute(false);
        ClearArea();
        cycle.SetNormalTime();
    }

    public void SkipBtn()
    {
        gUI.ClearText();
        gUI.AddText("You escaped the fight!");
        SkipFight();
    }

    public void BlockPlayer(bool value)
    {
        typeSc.inMenu = value;
        rootBloodSc.SetActive(value);
    }

    public void ShowBlood()
    {
        animBlood.SetTrigger("Attack");
    }
 
}
