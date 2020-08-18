using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public int idMission = 0;
    private PlayerClickMove move;
    private GUIScript gUI;
    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
    }

    private void OnMouseDown()
    {
        move.SetStop(true);
        gUI.AddText("Border");
        Debug.Log("FFF");
    }
    
}
