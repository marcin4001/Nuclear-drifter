using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOTriggerTextMap : MonoBehaviour
{
    private UFOMapText text;
    // Start is called before the first frame update
    void Start()
    {
        text = FindObjectOfType<UFOMapText>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            text.SetActive();
            Destroy(gameObject);
        }
    }
}
