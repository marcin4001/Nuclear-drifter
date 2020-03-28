using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 pos;
    public int gCost;
    public int hCost;

    public int x, y;
    public Node parent;

    public int FCost()
    {
        return gCost + hCost;
    }

    public Node(bool _walkable, Vector3 _pos, int _x, int _y)
    {
        this.walkable = _walkable;
        this.pos = _pos;
        this.x = _x;
        this.y = _y;
    }
}
