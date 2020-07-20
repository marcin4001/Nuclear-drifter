using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStart : MonoBehaviour
{
    public List<MissionInit> inits;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(int id)
    {
        MissionInit init = inits.Find(x => x.id == id);
        if(init != null)
        {
            init.box.LockOpen();
        }
    }
}
