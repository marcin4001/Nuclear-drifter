using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private CombatSystem system;
    public GameObject[] enemys;
    // Start is called before the first frame update
    void Start()
    {
        system = FindObjectOfType<CombatSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            system.StartFight(this);
            
        }
    }
}
