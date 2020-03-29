using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickMove : MonoBehaviour
{
    public Vector2 mousePos;
    public Transform target;
    private AStar aStar;
    public List<Node> path;
    public int currentIndexPoint;
    public float speed = 2.0f;
    private bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        aStar = FindObjectOfType<AStar>();
        path = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D node = Physics2D.Raycast(mousePos, Vector2.zero);

            if(node.collider == null)
            {
                Debug.Log("Ocean");
            }
            else
            {
                target.position = mousePos;
                path = aStar.FindPath(transform.position, target.position);
                currentIndexPoint = 0;
                stop = false;
            }
        }
        if(Input.GetMouseButton(1))
        {
            stop = true;
        }
        if (path != null && !stop)
        {
            float distanceToPoint = Vector3.Distance(transform.position, path[currentIndexPoint].pos);
            if(distanceToPoint < 0.1f)
            {
                if(currentIndexPoint < path.Count -1)currentIndexPoint++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, path[currentIndexPoint].pos, Time.deltaTime * speed);
            }
        }
    }
}
