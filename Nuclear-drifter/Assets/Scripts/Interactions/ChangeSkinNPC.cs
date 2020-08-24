using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkinNPC : MonoBehaviour
{
    public int idMission;
    public Sprite back;
    public Sprite front;
    public Sprite left;
    public Sprite right;
    public Sprite init;
    private ChangeDirectionNPC changer;

    // Start is called before the first frame update
    void Start()
    {
        changer = GetComponent<ChangeDirectionNPC>();
        MissionObj mission = MissionList.global.GetMission(idMission);
        if(mission != null)
        {
            if (mission.complete) changer.SetSprite(back, front, left, right, init);
        }
    }


}
