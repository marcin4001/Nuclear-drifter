using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearNodeEnemy : MonoBehaviour
{
    private GridNode grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridNode>();
        Invoke("Clear", 0.1f);
    }

    private void Clear()
    {
        grid.UpdateGrid();
    }
}
