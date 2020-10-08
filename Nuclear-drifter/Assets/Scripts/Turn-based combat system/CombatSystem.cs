using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public Transform camBattlepos;
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
    public Slot weaponSlot;
    private Inventory inv;
    private Health hpPlayer;
    private BadEnding ending;
    private MapControl map;
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
        battleCanvas.enabled = false;
        enemysObjs = new List<GameObject>();
        enemies = new List<Enemy>();
        BlockPlayer(false);
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
            enemy.Shot(handDamage);
            gUI.AddText(enemy.nameEnemy + " was hit!");
            gUI.AddText(enemy.nameEnemy + " lost " + handDamage + "hp");
        }
        else
        {
            if(weaponSlot.isGun())
            {
                enemy.Shot(currentWeapon.damage);
                inv.RemoveOne(weaponSlot);
                gUI.AddText(enemy.nameEnemy + " was hit!");
                gUI.AddText(enemy.nameEnemy + " lost " + currentWeapon.damage + "hp");
            }
            else if(weaponSlot.isBomb())
            {
                //boomb
            }
            else
            {
                enemy.Shot(currentWeapon.damage);
                gUI.AddText(enemy.nameEnemy + " was hit!");
                gUI.AddText(enemy.nameEnemy + " lost " + currentWeapon.damage + "hp");
            }
        }
    }

    public void SelectWeapon(WeaponItem weapon)
    {
        Slot temp = inv.FindItem(weapon.idItem);
        if (temp != null)
        {
            if(temp.isGun() && temp.ammo <= 0)
            {
                gUI.AddText("Out of ammo!");
                return;
            }
            weaponSlot = temp;
            currentWeapon = weapon;
            ShowWeaponStat();
        }
    }

    public void ShowWeaponDeafault()
    {
        gUI.ClearText();
        gUI.AddText("Current weapon:");
        gUI.AddText("Name: Hand");
        gUI.AddText("Default");
        gUI.AddText("Damage: " + handDamage);
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
    // Update is called once per frame
    void Update()
    {
        if(!playerRound)
        {
            if(!isAttack)
            {
                if(currentIndex < enemies.Count)
                {
                    enemies[currentIndex].Attack();
                    currentIndex += 1;
                    isAttack = true;
                }
                else
                {
                    currentIndex = 0;
                    playerRound = true;
                    BlockPlayer(false);
                    ShowWeaponDeafault();
                }
            }
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
        ClearWeapon();
        playerRound = false;
    }

    public bool Damage(Enemy _enemy)
    {
        float rngChance = Random.Range(0.0f, 1.0f);
        if(rngChance <= _enemy.dmgChance)
        {
            gUI.AddText("You were hit!");
            gUI.AddText("You lost " + _enemy.damageMax + "HP!");
            hpPlayer.Damage(_enemy.damageMax);
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
        currentCamPos = camBattlepos.position;
        camBattlepos.position = transform.position;
        player.SetStop(true);
        typeSc.combatState = true;
        enemyTr = trigger;
        battleCanvas.enabled = true;
        gUI.ActiveBtnPanel(false);
        map.keyActive = false;
        if (map.GetActive()) map.OpenMap();
        SetEnemys();
        ShowWeaponDeafault();
        BlockPlayer(false);
        playerRound = true;
        isAttack = false;
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
            enemy.Init(this);
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
        camBattlepos.position = currentCamPos;
        typeSc.combatState = false;
        battleCanvas.enabled = false;
        gUI.ActiveBtnPanel(true);
        map.keyActive = true;
        ClearArea();
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
