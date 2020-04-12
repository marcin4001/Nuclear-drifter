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
    public Animator anim;
    public Vector3 direct;
    

    // Start is called before the first frame update
    void Start()
    {
        aStar = FindObjectOfType<AStar>();
        path = null;
        stop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D node = Physics2D.Raycast(mousePos, Vector2.zero);

            if(node.collider == null)
            {
                Debug.Log("Ocean");
            }
            else
            {
                RaycastHit2D[] nodes = Physics2D.RaycastAll(mousePos, Vector2.zero);
                foreach (RaycastHit2D n in nodes) if (n.collider.tag == "Obstacle") n.collider.SendMessage("ShowText",SendMessageOptions.DontRequireReceiver);
                target.position = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                path = aStar.FindPath(transform.position, target.position);
                if (path != null)
                {
                    currentIndexPoint = 0;
                    direct = path[currentIndexPoint].pos - transform.position;
                    anim.SetFloat("moveX", direct.x);
                    anim.SetFloat("moveY", direct.y);
                    stop = false;
                }
                else
                {
                    stop = true;
                }
            }
        }
        if(Input.GetMouseButton(1))
        {
            stop = true;
        }
        if (path != null && !stop)
        {
            if (path.Count > 0)
            {
                float distanceToPoint = Vector3.Distance(transform.position, path[currentIndexPoint].pos);
                if (distanceToPoint < 0.001f)
                {
                    if (currentIndexPoint < path.Count - 1)
                    {
                        currentIndexPoint++;
                        direct = path[currentIndexPoint].pos - transform.position;
                        anim.SetFloat("moveX", direct.x);
                         anim.SetFloat("moveY", direct.y);
                    }
                    else
                    {
                        stop = true;
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, path[currentIndexPoint].pos, Time.deltaTime * speed);
                    
                }
            }
        }
        anim.SetBool("walk", !stop);

    }
}
