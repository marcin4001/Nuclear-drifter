using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMartColliderMap : MonoBehaviour
{
    public FoodMartSign martSign;
    // Start is called before the first frame update
    void Start()
    {
        martSign = FindObjectOfType<FoodMartSign>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hero")
        {
            martSign.ActiveText();
        }
    }
}
