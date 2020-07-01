using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    public Sheep[] sheeps;
    public int prefIndex = -1;
    public int currentIndex = -1;

    public float counter = 0;
    public float counterMax = 0;
    // Start is called before the first frame update
    void Start()
    {
        sheeps = GetComponentsInChildren<Sheep>();
        prefIndex = -1;
        currentIndex = -1;
        counter = 0;
        counterMax = Random.Range(3, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if(counter < counterMax)
        {
            counter += Time.deltaTime;
        }
        else
        {
            currentIndex = Random.Range(0, sheeps.Length);
            if(currentIndex != prefIndex)
            {
                prefIndex = currentIndex;
                counterMax = Random.Range(3, 6);
                sheeps[currentIndex].ChangeState();
                counter = 0;
            }
        }
    }
}
