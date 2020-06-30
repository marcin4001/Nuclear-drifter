using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBox : MonoBehaviour
{
    public EqBox[] boxes;

    void Awake()
    {
        SaveAndLoad.LoadTemp(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
