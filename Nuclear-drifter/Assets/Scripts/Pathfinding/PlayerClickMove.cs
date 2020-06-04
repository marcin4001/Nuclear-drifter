using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mouse_mode
{
    move,
    use,
    look
}

public enum inv_mode
{
    use,
    look,
    remove
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
    public inv_mode modeGui = 0;
    public int maxModeGui = 0;
    public RaycastHit2D[] nodes;
    private GridNode grid;
    public Texture2D arrow;
    public Texture2D goodWay;
    public Texture2D noWay;
    public Texture2D look;
    public Texture2D hand;
    public Texture2D noUse;
    public Texture2D bin;
    public bool active = true;
    private GUIScript gUI;
    public Node checkNode;
    private TypeScene typeSc;
    // Start is called before the first frame update
    void Start()
    {
        aStar = FindObjectOfType<AStar>();
        path = null;
        stop = true;
        maxMode = System.Enum.GetNames(typeof(Mouse_mode)).Length;
        maxModeGui = System.Enum.GetNames(typeof(inv_mode)).Length;
        grid = FindObjectOfType<GridNode>();
        gUI = FindObjectOfType<GUIScript>();
        Cursor.SetCursor(arrow, Vector2.zero, CursorMode.ForceSoftware);
        typeSc = FindObjectOfType<TypeScene>();
    }

    public bool ObjIsNear(string tag, float r)
    {
        bool result = false;
        Collider2D[] col = Physics2D.OverlapCircleAll((Vector2)transform.position, r);
        foreach (Collider2D c in col) if (c.tag == tag) result = true;
        return result;
    }

    public Vector3 GetPosPlayer()
    {
        float posX = Mathf.Ceil(transform.position.x);
        float posY = Mathf.Ceil(transform.position.y);
        return new Vector3(posX, posY, 0);
    }

    private void Move(RaycastHit2D hit)
    {
        if (hit.collider == null)
        {
            if (!typeSc.isInterior) gUI.AddText("I can not swim!");
            else gUI.AddText("I can't go there!");
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

    public void SetStop(bool value)
    {
        stop = value;
    }

    private void Use()
    {
        bool isItem = false;
        foreach(RaycastHit2D n in nodes)
        {
            if(n.collider.tag == "Item" || n.collider.tag == "Bed" || n.collider.tag == "Stove")
            {
                n.collider.SendMessage("Use", SendMessageOptions.DontRequireReceiver);
                return;
                isItem = true;
            }
        }
        if(!isItem) gUI.AddText("This can't be used");
    }

    private void Look(RaycastHit2D hit)
    {
        if (hit.collider == null)
        {
            if(!typeSc.isInterior)gUI.AddText("This is the ocean");
            else gUI.AddText("Nothing");
        }
        else
        {
            GameObject wall = null;
            bool isWall = true;
            foreach (RaycastHit2D n in nodes)
            {
                if (n.collider.tag == "Obstacle" || n.collider.tag == "Bed" || n.collider.tag == "Info")
                {
                    n.collider.SendMessage("ShowText", SendMessageOptions.DontRequireReceiver);
                    wall = null;
                    isWall = false;
                    Debug.Log("Obstacle");
                }
                if(n.collider.tag == "Item")
                {
                    n.collider.SendMessage("ShowText", SendMessageOptions.DontRequireReceiver);
                    return;
                }
                if (n.collider.tag == "Wall" || n.collider.tag == "Player")
                {
                    Debug.Log("Wall");
                    wall = n.collider.gameObject;
                    //isWall = true;
                }
            }
            if(wall != null && isWall) wall.SendMessage("ShowText", SendMessageOptions.DontRequireReceiver);
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
            if(mode == Mouse_mode.use)
            {
                bool isItems = false;
                nodes = Physics2D.RaycastAll(mousePos, Vector2.zero);
                foreach(RaycastHit2D n in nodes)
                {
                    if (n.collider.tag == "Item" || n.collider.tag == "Bed" || n.collider.tag == "Stove") isItems = true;
                }
                if (isItems) Cursor.SetCursor(hand, Vector2.zero, CursorMode.ForceSoftware);
                else Cursor.SetCursor(noUse, Vector2.zero, CursorMode.ForceSoftware);
            }
            if (mode == Mouse_mode.look)
            {
                bool isObstacle = false;
                nodes = Physics2D.RaycastAll(mousePos, Vector2.zero);
                if (nodes.Length == 0) isObstacle = true;
                foreach (RaycastHit2D n in nodes)
                    if (n.collider.tag == "Obstacle" || n.collider.tag == "Wall" || n.collider.tag == "Bed" || n.collider.tag == "Info" || n.collider.tag == "Player" || n.collider.tag == "Item")
                        isObstacle = true;
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
                    case Mouse_mode.use:
                        Use();
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
            if (gUI.CursorOnSlot())
            {
                switch(modeGui)
                {
                    case inv_mode.use:
                        Cursor.SetCursor(hand, Vector2.zero, CursorMode.ForceSoftware);
                        break;
                    case inv_mode.look:
                        Cursor.SetCursor(look, Vector2.zero, CursorMode.ForceSoftware);
                        break;
                    case inv_mode.remove:
                        Cursor.SetCursor(bin, Vector2.zero, CursorMode.ForceSoftware);
                        break;
                }
            }
            else
            {
                Cursor.SetCursor(arrow, Vector2.zero, CursorMode.ForceSoftware);
            }

            if (Input.GetMouseButtonDown(1))
            {
                if((int)modeGui < maxModeGui - 1)
                {
                    modeGui++;
                } else
                {
                    modeGui = 0;
                }
            }
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
