using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public Transform sun;
    public Light lightPlayer;
    private TimeGame time;
    public float counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<TimeGame>();
        lightPlayer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (time.hour == 19)
        {
            counter += Time.deltaTime;
            float alfa = counter / 30f; //(float) time.minutes / 59f;
            float x = Mathf.Lerp(0f, 90f, alfa);
            Debug.Log(alfa);
            sun.transform.rotation = Quaternion.Euler(new Vector3(x, 0f, 0f));
            if (time.minutes > 40) lightPlayer.enabled = true;
        }
        else if (time.hour == 4)
        {
            counter += Time.deltaTime;
            float alfa = counter / 30f; //(float) time.minutes / 59f;
            float x = Mathf.Lerp(90f, 0f, alfa);
            sun.rotation = Quaternion.Euler(new Vector3(x, 0f, 0f));
            if (time.minutes > 25) lightPlayer.enabled = false;
        }
        else counter = 0.0f;
    }
}
