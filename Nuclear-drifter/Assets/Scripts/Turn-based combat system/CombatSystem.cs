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
    // Start is called before the first frame update
    void Start()
    {
        typeSc = FindObjectOfType<TypeScene>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        battleCanvas.enabled = false;
        enemysObjs = new List<GameObject>();
        enemies = new List<Enemy>();
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
        ClearArea();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
