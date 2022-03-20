using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    public List<LocObj> locObjs;
    private FastTravel fastTravel;
    // Start is called before the first frame update
    void Start()
    {
        fastTravel = FindObjectOfType<FastTravel>();
        foreach(LocObj obj in locObjs)
        {
            obj.distance = Vector2.Distance(obj.startPos, transform.position);
            float hour = obj.distance / 60f;
            obj.hour = Mathf.RoundToInt(hour);
            obj.cost = obj.hour * 10;
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
            fastTravel.playerPos = obj.startPos;
            fastTravel.Walk(obj.hour);
        }
    }
}
