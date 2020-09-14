using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCloserController : MonoBehaviour
{
    public NightCloserDoor[] doors;
    // Start is called before the first frame update
    void Start()
    {
        doors = FindObjectsOfType<NightCloserDoor>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
