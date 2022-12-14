using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameDialogue : MonoBehaviour
{
    public string sceneName;

    public void LoadEnd()
    {
        SceneManager.LoadScene(sceneName);
    }
}
