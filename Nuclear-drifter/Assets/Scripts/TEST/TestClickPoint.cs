using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClickPoint : MonoBehaviour
{
    public Vector2 posMouse;
    public GameObject prefabs;
    public Transform player;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            posMouse = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(posMouse, Vector2.zero);

            if(hit.collider == null)
            {
                Debug.Log("Water");
            }
            else
            {
                Debug.Log(hit.collider.name);
            }
            Instantiate(prefabs, new Vector3(posMouse.x, posMouse.y), Quaternion.identity);
            Debug.Log("Direction: " + (new Vector3(posMouse.x, posMouse.y) - player.position));
        }
    }
}
