using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEnemyFoodmart : MonoBehaviour
{
    public GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        if (PropertyPlayer.property.afterFoodmart)
            enemies.SetActive(false);
        Destroy(this);
    }
}
