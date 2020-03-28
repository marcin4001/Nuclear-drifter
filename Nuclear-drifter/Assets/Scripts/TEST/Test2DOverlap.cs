using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2DOverlap : MonoBehaviour
{
    public Collider2D[] col;


    // Update is called once per frame
    void Update()
    {
        col = Physics2D.OverlapCircleAll((Vector2)transform.position, 0.7f);
    }
}
