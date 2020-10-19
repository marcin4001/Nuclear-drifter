using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGame : MonoBehaviour
{
    public int day = 1;
    public int hour = 5;
    public int minutes = 0;

    public float counter = 0.0f;
    public float counterMax = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void SetCounterMax(float value)
    {
        counter = 0.0f;
        counterMax = value;
    }

    public float GetCounterMax()
    {
        return counterMax;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(counter >= counterMax)
        {
            if(minutes >= 59)
            {
                if(hour >= 23)
                {
                    day++;
                    hour = 0;
                }
                else hour++;
                minutes = 0;
            }
            else
            {
                minutes++;
            }
            counter = 0f;
        }
        else
        {
            counter += Time.deltaTime;
        }
    }

}
