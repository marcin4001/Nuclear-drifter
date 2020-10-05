using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamTp : MonoBehaviour
{
    public Transform camBattlepos;
    private TypeScene typeSc;
    private PlayerClickMove player;
    private Vector3 currentCamPos;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        typeSc = FindObjectOfType<TypeScene>();
        player = FindObjectOfType<PlayerClickMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            if (!active)
            {
                currentCamPos = camBattlepos.position;
                camBattlepos.position = transform.position;
                player.SetStop(true);
                typeSc.combatState = true;
                active = true;
            }
            else
            {
                camBattlepos.position = currentCamPos;
                typeSc.combatState = false;
                active = false;
            }
        }
    }
}
