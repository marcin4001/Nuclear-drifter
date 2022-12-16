using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public Transform text;
    public float speed = 40f;
    public float posY = 0f;
    public float maxPosY = 4000f;

    // Update is called once per frame
    void Update()
    {
        if (maxPosY > posY)
        {
            text.localPosition = new Vector3(0f, posY, 0f);
            posY += speed * Time.deltaTime;
        }

        if(maxPosY < posY || Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("end");
            Application.Quit();
        }
    }
}
