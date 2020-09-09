using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrade : MonoBehaviour
{
    public string nameNPC;
    public float distance = 1.1f;
    private PlayerClickMove player;
    private GUIScript gUI;
    private ChangeDirectionNPC dir;
    private TradeSystem trade;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        dir = GetComponent<ChangeDirectionNPC>();
        trade = FindObjectOfType<TradeSystem>();
    }

    public void ShowText()
    {
        gUI.AddText("This is " + nameNPC);
    }

    public void Use()
    {

        if (player.ObjIsNearPlayer(transform.parent.position, distance))
        {
            if (dir != null)
            {
                Vector3 dirNPC = player.transform.position - transform.parent.position;
                Debug.Log(dirNPC);
                dir.SetDir((Vector2)dirNPC);
                Vector3 dirPlayer = transform.parent.position - player.transform.position;
                player.SetDir((Vector2)dirPlayer);
                Debug.Log(dirPlayer);
            }
            trade.Open();
        }
        else gUI.AddText(nameNPC + " is too far away");
    }
}
