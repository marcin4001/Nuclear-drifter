using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    public string nameNPC;
    public float distance = 1.1f;
    private PlayerClickMove player;
    private GUIScript gUI;
    private ChangeDirectionNPC dir;
    public List<LocObj> locObjs;
    private FastTravel fastTravel;
    private CarrierCanvas canvas;
    public bool unlockAll = false;
    private Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        fastTravel = FindObjectOfType<FastTravel>();
        canvas = FindObjectOfType<CarrierCanvas>();
        gUI = FindObjectOfType<GUIScript>();
        dir = GetComponent<ChangeDirectionNPC>();
        player = FindObjectOfType<PlayerClickMove>();
        inv = FindObjectOfType<Inventory>();
        foreach (LocObj obj in locObjs)
        {
            obj.distance = Vector2.Distance(obj.startPos, transform.position);
            float hour = obj.distance / 60f;
            obj.hour = Mathf.RoundToInt(hour);
            obj.cost = obj.hour * 10;
        }
        UpdateLocObj();
    }

    public void UpdateLocObj()
    {
        if(unlockAll)
        {
            foreach(LocObj obj in locObjs)
            {
                obj.unlocked = true;
            }
            return;
        }
        foreach(LocObj obj in locObjs)
        {
            obj.unlocked = AchievementCounter.global.ContainsArea(obj.name);
        }
    }

    public void ShowText()
    {
        gUI.AddText("NPC: " + nameNPC);
    }

    public void Use()
    {
        if (player.ObjIsNearPlayer(transform.parent.position, distance))
        {
            if (dir != null)
            {
                Vector3 dirNPC = player.transform.position - transform.parent.position;
                dir.SetDir((Vector2)dirNPC);
                Vector3 dirPlayer = transform.parent.position - player.transform.position;
                player.SetDir((Vector2)dirPlayer);
            }
            canvas.carrier = this;
            canvas.Open();
        }
        else
        {
            gUI.AddText(nameNPC + " is too far");
            gUI.AddText("away");
        }
    }

    public void WalkTo(int location)
    {
        if (locObjs == null)
            return;
        if (locObjs.Count == 0)
            return;
        if(location >= 0 && location < locObjs.Count)
        {
            LocObj obj = locObjs[location];
            Slot money = inv.FindItem(300);
            if(money != null)
            {
                int amount = money.amountItem;
                amount = amount - obj.cost;
                if(amount >= 0)
                {
                    Slot newMoney = new Slot(money.itemElement, obj.cost, 0);
                    inv.RemoveFew(newMoney);
                }
                else
                {
                    gUI.AddText("You don't have enough");
                    gUI.AddText("money");
                    return;
                }
            }
            else
            {
                gUI.AddText("You don't have enough");
                gUI.AddText("money");
                return;
            }
            fastTravel.playerPos = obj.startPos;
            fastTravel.location = obj.location;
            fastTravel.Walk(obj.hour);
        }
    }
}
