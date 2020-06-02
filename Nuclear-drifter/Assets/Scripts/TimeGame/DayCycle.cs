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
        
    }

    public void SetTime(int _day, int _hour, int minutes)
    {
        time = FindObjectOfType<TimeGame>();
        time.day = _day;
        if (minutes > 30) time.hour = _hour + 1;
        else time.hour = _hour;
        if (time.hour > 23) _hour = 0;
        if(time.hour > 19 || time.hour < 5)
        {
            sun.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            if (lightPlayer != null) lightPlayer.enabled = true;
        }
        else
        {
            sun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            if (lightPlayer != null) lightPlayer.enabled = false;
        }
        time.minutes = 0;
    }

    public int GetHour()
    {
        return time.hour;
    }

    public int GetDay()
    {
        return time.day;
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
            if (time.minutes > 40 && lightPlayer != null) lightPlayer.enabled = true;
        }
        else if (time.hour == 4)
        {
            counter += Time.deltaTime;
            float alfa = counter / 30f; //(float) time.minutes / 59f;
            float x = Mathf.Lerp(90f, 0f, alfa);
            sun.rotation = Quaternion.Euler(new Vector3(x, 0f, 0f));
            if (time.minutes > 25 && lightPlayer != null) lightPlayer.enabled = false;
        }
        else counter = 0.0f;
    }
}
