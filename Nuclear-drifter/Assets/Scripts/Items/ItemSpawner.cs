using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPref;
    public Vector3 pos;
    public Slot item;

    public void Spawn(Vector3 pos_, GameObject itemPref_, Slot item_)
    {
        itemPref = itemPref_;
        pos = pos_;
        item = item_;
        StartCoroutine(SpawnTime(pos, itemPref, item));
    }

    private IEnumerator SpawnTime(Vector3 pos, GameObject itemPref, Slot item)
    {
        yield return new WaitForSeconds(0.3f);
        GameObject obj = Instantiate(itemPref, pos, Quaternion.identity);
        ItemElement itemEl = obj.GetComponent<ItemElement>();
        if (itemEl != null) itemEl.item = item;
        yield return new WaitForSeconds(0.3f);
        obj = Instantiate(itemPref, pos, Quaternion.identity);
        itemEl = obj.GetComponent<ItemElement>();
        if (itemEl != null) itemEl.item = item;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(SpawnTime(pos, itemPref, item));
        }
    }
}
