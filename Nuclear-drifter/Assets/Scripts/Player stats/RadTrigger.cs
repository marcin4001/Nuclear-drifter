using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadTrigger : MonoBehaviour
{
    public Health health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        health = collision.GetComponentInParent<Health>();
        if (health != null) health.isRad = true;
    }
}
