using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    private LineRenderer render;
    public int pointsCount = 50;
    public Transform point_0;
    public Transform point_1;
    public Transform point_2;
    private Vector3[] pos;
    
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<LineRenderer>();
        pos = new Vector3[pointsCount+1];
        render.positionCount = pointsCount;
        DrawLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawLine()
    {
        pos[0] = point_0.position;
        for(int i = 1; i < pointsCount + 1; i++)
        {
            float time = i / (float)pointsCount;
            pos[i] = CalculateQBezierPoint(time, point_0.position, point_1.position, point_2.position);
        }
        pos[pointsCount - 1] = point_2.position;
        render.SetPositions(pos);
    }

    private Vector3 CalculateLinearBezierPoint(float time, Vector3 point0, Vector3 point1)
    {
        return point0 + time * (point1 - point0);
    }

    private Vector3 CalculateQBezierPoint(float time, Vector3 point0, Vector3 point1, Vector3 point2)
    {
        float u = 1 - time;
        float tSq = time * time;
        float uSq = u * u;
        Vector3 newPoint = uSq * point0;
        newPoint += 2 * u * time * point1;
        newPoint += tSq * point2;
        return newPoint;
    }
}
