using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNeraObj : MonoBehaviour
{
    private PlayerClickMove player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerClickMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(player.ObjIsNear("Bed", 1.0f));
        }
    }
}
