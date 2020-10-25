using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private CombatSystem system;
    private RewardEnemy reward;
    private MissionEnemyTrigger missionTr;
    public GameObject[] enemys;
    // Start is called before the first frame update
    void Start()
    {
        system = FindObjectOfType<CombatSystem>();
        reward = GetComponent<RewardEnemy>();
        missionTr = GetComponent<MissionEnemyTrigger>();
    }

    public void GiveReward()
    {
        if(reward != null)
        {
            reward.GiveItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            system.StartFight(this);
            
        }
    }

    public void Deactive()
    {
        if (missionTr != null) missionTr.SetKill();
        Destroy(gameObject);
    }
}
