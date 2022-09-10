using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public Transform sun;
    public Light lightPlayer;
    private TimeGame time;
    private TypeScene typeSc;
    public float counter = 0f;
    private float timeOneHour = 0f;
    private float normalCounter = 0.5f;
    private float slowCounter = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<TimeGame>();
        typeSc = FindObjectOfType<TypeScene>();
        normalCounter = time.counterMax;
        timeOneHour = 60 * time.GetCounterMax();
    }

    public void SetTime(int _day, int _hour, int minutes)
    {
        typeSc = FindObjectOfType<TypeScene>();
        time = FindObjectOfType<TimeGame>();
        time.day = _day;
        time.hour = _hour;
        time.minutes = minutes;
        if(time.hour == 20 || time.hour == 5)
        {
            if (minutes > 30)
            {
                PropertyPlayer.property.AddDehydration(60 - minutes);
                time.hour = _hour + 1;
            }
            else
            {
                PropertyPlayer.property.AddDehydration(-minutes);
                time.hour = _hour;
            }
            time.minutes = 0;
        }
        if(time.hour > 20 || time.hour < 6)
        {
            sun.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            if (lightPlayer != null) lightPlayer.enabled = true;
            typeSc.lightNight = true;
        }
        else
        {
            sun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            if (lightPlayer != null) lightPlayer.enabled = false;
            typeSc.lightNight = false;
        }
    }

    public void SetSlowTime()
    {
        time.SetCounterMax(slowCounter);
        SetTime(time.day, time.hour, time.minutes);
        timeOneHour = 60 * time.GetCounterMax();
        counter = 0.0f;
    }

    public void SetNormalTime()
    {
        time.SetCounterMax(normalCounter);
        SetTime(time.day, time.hour, time.minutes);
        timeOneHour = 60 * time.GetCounterMax();
        counter = 0.0f;
    }

    public int GetMinutes()
    {
        return time.minutes;
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
        if (time.hour == 20)
        {
            counter += Time.deltaTime;
            float alfa = counter / timeOneHour; //(float) time.minutes / 59f;
            float x = Mathf.Lerp(0f, 90f, alfa);
            //Debug.Log(alfa);
            sun.transform.rotation = Quaternion.Euler(new Vector3(x, 0f, 0f));
            if (time.minutes > 40 && lightPlayer != null)
            {
                typeSc.lightNight = true;
                lightPlayer.enabled = true;
            }
        }
        else if (time.hour == 5)
        {
            counter += Time.deltaTime;
            float alfa = counter / timeOneHour; //(float) time.minutes / 59f;
            float x = Mathf.Lerp(90f, 0f, alfa);
            sun.rotation = Quaternion.Euler(new Vector3(x, 0f, 0f));
            if (time.minutes > 25 && lightPlayer != null)
            {
                typeSc.lightNight = false;
                lightPlayer.enabled = false;
            }
        }
        else counter = 0.0f;
    }
}
