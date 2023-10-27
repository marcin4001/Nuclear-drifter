using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryBoxPlugin : MonoBehaviour
{
    public EqBox newBox;
    public int invBoxIndex = 0;
    private InventoryBox invBox;
    // Start is called before the first frame update
    void Start()
    {
        invBox = FindObjectOfType<InventoryBox>();
        if(invBox == null )
            Destroy(gameObject);
        StartCoroutine(AddNewBox());
    }

    public IEnumerator AddNewBox()
    {
        yield return new WaitForSeconds(0.1f);
        if ((invBox.boxes.Length - 1) >= invBoxIndex)
        {
            Destroy(gameObject);
        }
        else
        {
            List<EqBox> boxes = invBox.boxes.ToList();
            while (boxes.Count <= invBoxIndex)
                boxes.Add(new EqBox());
            foreach (Slot item in newBox.eqSlots)
                item.SetId();
            boxes[invBoxIndex] = newBox;
            invBox.boxes = boxes.ToArray();
        }
    }
}
