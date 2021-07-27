using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSpawn : MonoBehaviour
{
    public GameObject stick;
    private GridNode nodes;
    // Start is called before the first frame update
    void Start()
    {
        if (stick == null) Destroy(gameObject);
        nodes = FindObjectOfType<GridNode>();
        if(!PropertyPlayer.property.trapdoorOpened)
        {
            Debug.Log("stick");
            nodes.SetWalkable(stick.transform.position, true);
            stick.SetActive(false);
        }
    }

}
