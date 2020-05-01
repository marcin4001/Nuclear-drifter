using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCursor : MonoBehaviour
{
    public Transform cursorPlayer;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        cursorPlayer.localPosition = player.position;
    }
}
