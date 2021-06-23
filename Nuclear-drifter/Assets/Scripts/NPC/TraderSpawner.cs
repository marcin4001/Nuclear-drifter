using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderSpawner : MonoBehaviour
{
    public WanderingTrader[] traders;
    private GridNode nodes;
    // Start is called before the first frame update
    void Start()
    {
        nodes = FindObjectOfType<GridNode>();
        if(traders.Length > 1)
        {
            int index = Random.Range(0, traders.Length);
            for(int i = 0; i < traders.Length; i++)
            {
                if(i != index)
                {
                    nodes.SetWalkable(traders[i].trader.transform.position, true);
                    nodes.SetWalkable(traders[i].animal.transform.position, true);
                    traders[i].trader.SetActive(false);
                    traders[i].animal.SetActive(false);
                }
            }
        }
        Destroy(gameObject);
    }

}
