using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public bool active = false;
    public Texture2D cursor;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        Invoke("SetActive", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(active && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SetActive()
    {
        active = true;
    }
}
