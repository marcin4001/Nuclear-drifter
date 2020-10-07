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
    private bool isAttack = false;
    public int currentIndex = 0;
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
        battleCanvas.enabled = false;
        enemysObjs = new List<GameObject>();
        enemies = new List<Enemy>();
        BlockPlayer(false);
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
                }
            }
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
        playerRound = false;
    }

    public bool Damage(Enemy _enemy)
    {
        float rngChance = Random.Range(0.0f, 1.0f);
        if(rngChance <= _enemy.dmgChance)
        {
            gUI.AddText("The " + _enemy.nameEnemy + " bit you!");
            gUI.AddText("You lost " + _enemy.damageMax + "HP!");
            hpPlayer.Damage(_enemy.damageMax);
            return true;
        }
        else
        {
            gUI.AddText("The " + _enemy.nameEnemy + " missed!");
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
