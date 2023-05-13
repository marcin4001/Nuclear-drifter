using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    public string location;
    public string locationExit = "Wasteland";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            PropertyPlayer.property.location = location;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            PropertyPlayer.property.location = locationExit;
        }
    }
}
