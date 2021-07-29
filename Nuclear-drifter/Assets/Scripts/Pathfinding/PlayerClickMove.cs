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
    remove,
    look,
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
    public Texture2D all;
    public Texture2D head;
    public Texture2D tool;
    public Texture2D gunpoint;
    public bool active = true;
    private GUIScript gUI;
    public Node checkNode;
    private TypeScene typeSc;
    private Health hp;
    public GameObject sqrLocPref;
    public Camera mainCam;
    private Vector3 localPosCam;
    private GameObject sqrLoc;
    private CombatSystem combat;
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
        hp = GetComponent<Health>();
        if(sqrLocPref != null)
        {
            sqrLoc = Instantiate(sqrLocPref);
        }
        mainCam = GetComponentInChildren<Camera>();
        localPosCam = mainCam.transform.localPosition;
        combat = FindObjectOfType<CombatSystem>();
    }

    public void RoundPosPlayer()
    {
        float x_pos = Mathf.Floor(transform.position.x);
        float y_pos = Mathf.Floor(transform.position.y);

        transform.position = new Vector3(x_pos, y_pos, transform.position.z);
    }

    public void SetLocalPosCamera()
    {
        mainCam.transform.localPosition = localPosCam;
    }

    public void SetDir(Vector2 pos)
    {
        anim.SetFloat("moveX", pos.x);
        anim.SetFloat("moveY", pos.y);
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

    public bool ObjIsNearPlayer(Vector3 pos, float dis)
    {
        float distance = Vector3.Distance(pos, transform.position);
        Debug.Log(distance);
        return distance <= dis;
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
            foreach (RaycastHit2D n in nodes)
            {
                if (n.collider.tag == "Border")
                {
                    gUI.ClearText();
                    gUI.AddText("You can't cross the");
                    gUI.AddText("bridge because you don't");
                    gUI.AddText("have a passport!");//
                    gUI.AddText("US territory");
                    return;
                }
                if (n.collider.tag == "WaterCol")
                {
                    gUI.AddText("I can not swim!");
                    return;
                }
            }
            Vector3 player_pos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
            path = aStar.FindPath(player_pos, target.position, transform.position);
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
        
        foreach(RaycastHit2D n in nodes)
        {
            Collider2D n_col = n.collider;
            if(n_col.tag == "Item" || n_col.tag == "Bed" || n_col.tag == "Stove" || n_col.tag == "Chest" || n_col.tag == "NPC" || n_col.tag == "Device" || n_col.tag == "Trapdoor" || n_col.tag == "Well" || n_col.tag == "FirePlace")
            {
                n.collider.SendMessage("Use", SendMessageOptions.DontRequireReceiver);
                return;
            }
        }
         gUI.AddText("This can't be used");
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
                if (n.collider.tag == "Obstacle" || n.collider.tag == "Bed" || n.collider.tag == "Info" || n.collider.tag == "Chest" || n.collider.tag == "Trapdoor" || n.collider.tag == "Well" || n.collider.tag == "EnemyInfo" || n.collider.tag == "FirePlace") 
                {
                    n.collider.SendMessage("ShowText", SendMessageOptions.DontRequireReceiver);
                    wall = null;
                    isWall = false;
                    //Debug.Log("Obstacle");
                }
                if(n.collider.tag == "Item" || n.collider.tag == "WaterCol" || n.collider.tag == "NPC")
                {
                    n.collider.SendMessage("ShowText", SendMessageOptions.DontRequireReceiver);
                    return;
                }
                if (n.collider.tag == "Wall" || n.collider.tag == "Player")
                {
                    //Debug.Log("Wall");
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
            if (!typeSc.combatState)
            {
                mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                nodes = Physics2D.RaycastAll(mousePos, Vector2.zero);
                if (mode == Mouse_mode.move)
                {
                    target.position = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                    if (sqrLoc != null)
                    {
                        sqrLoc.transform.position = target.position;
                        sqrLoc.SetActive(true);
                    }
                    checkNode = grid.NodeFromPoint(target.position);

                    if (checkNode.walkable && !grid.isPlayerNode(target.position)) Cursor.SetCursor(goodWay, Vector2.one, CursorMode.ForceSoftware);
                    else Cursor.SetCursor(noWay, Vector2.one, CursorMode.ForceSoftware);
                }
                if (mode == Mouse_mode.use)
                {
                    if (sqrLoc != null) sqrLoc.SetActive(false);
                    bool isItems = false;
                    bool isNPC = false;
                    bool isDevice = false;
                    nodes = Physics2D.RaycastAll(mousePos, Vector2.zero);
                    foreach (RaycastHit2D n in nodes)
                    {
                        if (n.collider.tag == "Item" || n.collider.tag == "Bed" || n.collider.tag == "Stove" || n.collider.tag == "Chest" || n.collider.tag == "Trapdoor" || n.collider.tag == "Well" || n.collider.tag == "FirePlace")
                            isItems = true;
                        if (n.collider.tag == "NPC") isNPC = true;
                        if (n.collider.tag == "Device") isDevice = true;
                    }
                    if (isItems) Cursor.SetCursor(hand, Vector2.zero, CursorMode.ForceSoftware);
                    else if (isNPC) Cursor.SetCursor(head, Vector2.zero, CursorMode.ForceSoftware);
                    else if (isDevice) Cursor.SetCursor(tool, Vector2.zero, CursorMode.ForceSoftware);
                    else Cursor.SetCursor(noUse, Vector2.zero, CursorMode.ForceSoftware);
                }
                if (mode == Mouse_mode.look)
                {
                    if (sqrLoc != null) sqrLoc.SetActive(false);
                    bool isObstacle = false;
                    nodes = Physics2D.RaycastAll(mousePos, Vector2.zero);
                    if (nodes.Length == 0) isObstacle = true;
                    foreach (RaycastHit2D n in nodes)
                        if (n.collider.tag != "Untagged" && n.collider.tag != "Hero")
                            isObstacle = true;
                    if (isObstacle) Cursor.SetCursor(look, Vector2.zero, CursorMode.ForceSoftware);
                    else Cursor.SetCursor(arrow, Vector2.zero, CursorMode.ForceSoftware);
                }
            }
            else
            {
                mousePos = (Vector2)combat.camBattle.ScreenToWorldPoint(Input.mousePosition);
                nodes = Physics2D.RaycastAll(mousePos, Vector2.zero);
                bool isEnemy = false;
                if (nodes.Length == 0) isEnemy = false;
                foreach (RaycastHit2D n in nodes)
                    if (n.collider.tag == "Enemy") isEnemy = true;

                if (isEnemy)
                {
                    Cursor.SetCursor(gunpoint, Vector2.zero, CursorMode.ForceSoftware);
                }
                else
                {
                    Cursor.SetCursor(arrow, Vector2.zero, CursorMode.ForceSoftware);
                }
            }




            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit2D node = Physics2D.Raycast(mousePos, Vector2.zero);
                if (!typeSc.combatState)
                {
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
                else
                {
                    //Debug.Log(node.collider);
                    if(node.collider != null) node.collider.SendMessage("Damage", SendMessageOptions.DontRequireReceiver);
                }
            }
            if (Input.GetMouseButtonDown(1) && !typeSc.combatState)
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
            if (sqrLoc != null) sqrLoc.SetActive(false);
            Cursor.SetCursor(arrow, Vector2.zero, CursorMode.ForceSoftware);
            if (gUI.CursorOnSlot() && !hp.isDead() && !typeSc.inMenu)
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
                        if(typeSc.inBox == 0)Cursor.SetCursor(bin, Vector2.zero, CursorMode.ForceSoftware);
                        else Cursor.SetCursor(all, Vector2.zero, CursorMode.ForceSoftware);
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
        //Move 
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

    public void Deathanim()
    {
        stop = true;
        anim.SetTrigger("death");
    }
}
