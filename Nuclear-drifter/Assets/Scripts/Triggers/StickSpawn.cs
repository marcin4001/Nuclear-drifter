using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSpawn : MonoBehaviour
{
    public GameObject stick;
    public int indexTrapdoor = 0;
    private GridNode nodes;
    // Start is called before the first frame update
    void Start()
    {
        if (stick == null) Destroy(gameObject);
        nodes = FindObjectOfType<GridNode>();
        if(!PropertyPlayer.property.trapdoorOpened[indexTrapdoor])
        {
            //Debug.Log("stick");
            nodes.SetWalkable(stick.transform.position, true);
            stick.SetActive(false);
        }
    }

}
