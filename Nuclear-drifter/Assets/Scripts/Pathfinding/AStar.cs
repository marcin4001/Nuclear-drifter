using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    GridNode grid;

    //public Transform player, target;
    private void Awake()
    {
        grid = GetComponent<GridNode>();
    }

    //private void Update()
    //{
    //    if(player != null && target != null)
    //    {
    //        FindPath(player.position, target.position);
    //    }
    //}
    public List<Node> FindPath(Vector3 start, Vector3 end)
    {
        Node startNode = grid.NodeFromPoint(start);
        Node endNode = grid.NodeFromPoint(end);
        if (startNode == endNode) return null;
        if (!endNode.walkable) return null;
        List<Node> open = new List<Node>();
        HashSet<Node> close = new HashSet<Node>();
        open.Add(startNode);

        while(open.Count > 0)
        {
            Node currentNode = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if(open[i].FCost() < currentNode.FCost() || open[i].FCost() == currentNode.FCost() && open[i].hCost < currentNode.hCost)
                {
                    currentNode = open[i];
                }
            }

            open.Remove(currentNode);
            close.Add(currentNode);

            if(currentNode == endNode)
            {
               return RePath(startNode, endNode);
                
            }

            foreach(Node n in grid.GetNeighbours(currentNode))
            {
                if(!n.walkable || close.Contains(n))
                {
                    continue;
                }
                int newMoveCost = currentNode.gCost + GetDistance(currentNode, n);
                if(newMoveCost < n.gCost || !open.Contains(n))
                {
                    n.gCost = newMoveCost;
                    n.hCost = GetDistance(n, endNode);
                    n.parent = currentNode;

                    if(!open.Contains(n))
                    {
                        open.Add(n);
                    }
                }
            }
        }
        return null;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int h = Mathf.Abs(nodeA.x - nodeB.x) + Mathf.Abs(nodeA.y - nodeB.y);//Mathf.Max(Mathf.Abs(nodeA.x - nodeB.x), Mathf.Abs(nodeA.y - nodeB.y));
        return h;
    }

    List<Node> RePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;
        while(currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        //path.Add(currentNode);
        path.Reverse();
        grid.path = path;
        return path;
    }
}
