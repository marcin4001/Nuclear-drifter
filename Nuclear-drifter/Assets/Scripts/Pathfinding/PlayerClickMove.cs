using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mouse_mode
{
    move,
    look
}

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
    public Mouse_mode mode = 0;
    public int maxMode = 0;
    public RaycastHit2D[] nodes;
    private GridNode grid;
    public Texture2D arrow;
    public Texture2D goodWay;
    public Texture2D noWay;
    public Texture2D look;
    public Texture2D hand;
    public bool active = true;
    private GUIScript gUI;
    public Node checkNode;
    // Start is called before the first frame update
    void Start()
    {
        aStar = FindObjectOfType<AStar>();
        path = null;
        stop = true;
        maxMode = System.Enum.GetNames(typeof(Mouse_mode)).Length;
        grid = FindObjectOfType<GridNode>();
        gUI = FindObjectOfType<GUIScript>();
        Cursor.SetCursor(arrow, Vector2.zero, CursorMode.ForceSoftware);
    }


    private void Move(RaycastHit2D hit)
    {
        if (hit.collider == null)
        {
            gUI.AddText("I can not swim!");
        }
        else
        {
            path = aStar.FindPath(transform.position, target.position);
            if(!checkNode.walkable) gUI.AddText("I can't go there!");
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

    private void Look(RaycastHit2D hit)
    {
        if (hit.collider == null)
        {
            gUI.AddText("This is the ocean");
        }
        else
        {
            foreach (RaycastHit2D n in nodes) if (n.collider.tag == "Obstacle") n.collider.SendMessage("ShowText", SendMessageOptions.DontRequireReceiver);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mode == Mouse_mode.move)
            {
                target.position = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                checkNode = grid.NodeFromPoint(target.position);

                if (checkNode.walkable) Cursor.SetCursor(goodWay, Vector2.one, CursorMode.ForceSoftware);
                else Cursor.SetCursor(noWay, Vector2.one, CursorMode.ForceSoftware);
            }
            if (mode == Mouse_mode.look)
            {
                bool isObstacle = false;
                nodes = Physics2D.RaycastAll(mousePos, Vector2.zero);
                if (nodes.Length == 0) isObstacle = true;
                foreach (RaycastHit2D n in nodes) if (n.collider.tag == "Obstacle") isObstacle = true;
                if (isObstacle) Cursor.SetCursor(look, Vector2.zero, CursorMode.ForceSoftware);
                else Cursor.SetCursor(arrow, Vector2.zero, CursorMode.ForceSoftware);
            }




            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit2D node = Physics2D.Raycast(mousePos, Vector2.zero);

                switch (mode)
                {
                    case Mouse_mode.move:
                        Move(node);
                        break;
                    case Mouse_mode.look:
                        Look(node);
                        break;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (!stop) stop = true;
                else
                {
                    if ((int)mode < maxMode - 1)
                    {
                        mode++;
                    }
                    else
                    {
                        mode = 0;
                    }
                }
            }
        }
        else
        {
            Cursor.SetCursor(arrow, Vector2.zero, CursorMode.ForceSoftware);
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
