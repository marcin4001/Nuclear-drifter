using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private Transform player;
    private DayCycle time;
    private Health playerHP;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHP = player.GetComponent<Health>();
        time = FindObjectOfType<DayCycle>();

        player.position = PropertyPlayer.property.startPos;
        Collider2D[] col = Physics2D.OverlapCircleAll((Vector2)player.position, 0.5f);
        foreach(Collider2D c in col)
        {
            if (c.tag == "EnemyTr") Destroy(c.gameObject);
        }
        playerHP.currentHealth = PropertyPlayer.property.currentHealth;
        playerHP.maxHealth = PropertyPlayer.property.maxHealth;
        playerHP.maxAfterRad = PropertyPlayer.property.maxHealth;
        playerHP.levelRad = PropertyPlayer.property.levelRad;
        playerHP.SetRadStart(PropertyPlayer.property.isRad, PropertyPlayer.property.levelRad);
        playerHP.isPoison = false;
        playerHP.SetPoison(PropertyPlayer.property.isPoison);
        //Debug.Log(PropertyPlayer.property.day);
        time.SetTime(PropertyPlayer.property.day, PropertyPlayer.property.hour, PropertyPlayer.property.minutes);
    }

}
