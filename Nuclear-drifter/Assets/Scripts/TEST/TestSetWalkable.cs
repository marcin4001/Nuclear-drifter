using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetWalkable : MonoBehaviour
{
    private GridNode grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridNode>();
        Node n = grid.NodeFromPoint(transform.position);
        n.walkable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
