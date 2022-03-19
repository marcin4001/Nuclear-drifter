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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            WalkTo(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            WalkTo(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            WalkTo(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            WalkTo(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            WalkTo(4);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            WalkTo(5);
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
