using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCursor : MonoBehaviour
{
    public Transform cursorPlayer;
    public Transform player;
    private TypeScene scene;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        scene = FindObjectOfType<TypeScene>();
        cursorPlayer.localPosition = PropertyPlayer.property.posOutside;
    }

    // Update is called once per frame
    void Update()
    {
        if(!scene.isInterior) cursorPlayer.localPosition = player.position;
    }
}
