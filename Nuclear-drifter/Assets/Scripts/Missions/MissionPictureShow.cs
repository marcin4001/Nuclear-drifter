using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPictureShow : MonoBehaviour
{
    public int idMission = 35;
    public GameObject pictureObj;
    private MissionObj mission;
    // Start is called before the first frame update
    void Start()
    {
        mission = MissionList.global.GetMission(idMission);
        if (mission == null)
        {
            Destroy(gameObject);
        }
        else
        {
            if(!mission.complete)
                pictureObj.SetActive(false);
            else
                Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(mission.complete)
        {
            pictureObj.SetActive(true);
            Destroy(gameObject);
        }
    }
}
