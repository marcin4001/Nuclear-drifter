using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEnding : MonoBehaviour
{
    private FadePanel fade;
    private PlayerClickMove move;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadePanel>();
        move = GetComponent<PlayerClickMove>();
    }

    public void End()
    {
        fade.EnableEndImg();
        move.Deathanim();
        Invoke("LoadScene", 3.0f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("DeathScene");
    }
}
