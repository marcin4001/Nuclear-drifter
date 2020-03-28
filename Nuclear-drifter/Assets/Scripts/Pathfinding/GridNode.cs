using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour
{
    public Vector2 gridSize;
    public float nodeRadius = 0.5f;
    Node[,] nodes;
    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;
    private Vector2[] fourDirection;
    public bool test;
    public GameObject testObj;
    public Transform player;
    private GameObject testGO;
    public List<Node> path;
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
        Node playerN = NodeFromPoint(player.position);
        Debug.Log(playerN.pos);
        fourDirection = new Vector2[4];
        fourDirection[0] = new Vector2(1, 0);
        fourDirection[1] = new Vector2(-1, 0);
        fourDirection[2] = new Vector2(0, 1);
        fourDirection[3] = new Vector2(0, -1);
    }

    public Node NodeFromPoint(Vector3 point)
    {
        float percentX = (point.x) / gridSize.x;
        float percentY = (point.y) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return nodes[x, y];
    }

    public List<Node> GetNeighbours(Node point)
    {
        List<Node> neighbours = new List<Node>();
        for(int i = 0; i < fourDirection.Length; i ++)
        {
            int checkX = point.x + (int) fourDirection[i].x;
            int checkY = point.y + (int)fourDirection[i].y;

            if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
            {
                neighbours.Add(nodes[checkX, checkY]);
            }
        }
        return neighbours;
    }


    private void CreateGrid()
    {
        nodes = new Node[gridSizeX, gridSizeY];
        Vector3 bottonLeft = Vector3.zero - new Vector3(0.5f, 0.5f); //transform.position - Vector3.right * gridSize.x / 2 - Vector3.up * gridSize.y / 2;
        Debug.Log(bottonLeft);
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 pos = bottonLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = (Physics2D.OverlapCircle((Vector2)pos, nodeRadius - 0.3f) != null);
                nodes[x, y] = new Node(walkable, pos, x, y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        
        if (nodes != null)
        {
            Node playerN = NodeFromPoint(player.position);
            //List<Node> nPlayer = GetNeighbours(playerN);
            foreach (Node n in nodes)
            {
                Gizmos.color = (n.walkable) ? Color.yellow : Color.blue;
                if (path != null)
                {
                    if (path.Contains(n)) Gizmos.color = Color.white;
                }
                
                if (playerN == n)
                {
                    Gizmos.color = Color.magenta;
                }
                //if(nPlayer.Contains(n))
                //{
                //    Gizmos.color = Color.green;
                //}

                Gizmos.DrawCube(n.pos, Vector3.one * 0.3f);
                
            }
        }
    }
}
