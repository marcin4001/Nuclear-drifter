using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPrize : MonoBehaviour
{
    public List<Prize> prizes;
    private Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
    }

    public void GivePrize(int idMission)
    {
        Prize p = prizes.Find(x => x.idMission == idMission);
        if(p != null)
        {
            if (p.chest == null)
            {
                if (!inv.IsFull())
                {
                    inv.Add(p.itemPrize);
                }
                else
                {
                    GameObject obj = Instantiate(p.prefItem, p.spawnPos, Quaternion.identity);
                    ItemElement item = obj.GetComponent<ItemElement>();
                    if (item != null) item.item = p.itemPrize;
                }
            }
            else
            {
                p.chest.LockOpen();
            }
        }
    }
}
