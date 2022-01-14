using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNPCTrapdoor : MonoBehaviour
{
    public GameObject npc;
    public int indexTrapdoor = 1;
    private GridNode nodes;
    // Start is called before the first frame update
    void Start()
    {
        if (npc == null) Destroy(gameObject);
        nodes = FindObjectOfType<GridNode>();
        bool openedTrapdoor = PropertyPlayer.property.trapdoorOpened[1];
        if (openedTrapdoor)
        {
            nodes.SetWalkable(npc.transform.position, true);
            npc.SetActive(false);
        }
    }
}
