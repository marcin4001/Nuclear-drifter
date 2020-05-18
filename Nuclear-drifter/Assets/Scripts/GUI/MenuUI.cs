using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        ResetProperty();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit(0);
        Debug.Log("Exit");
    }

    private void ResetProperty()
    {
        PropertyPlayer.property.currentHealth = 100;
        PropertyPlayer.property.maxHealth = 100;
        PropertyPlayer.property.isPoison = false;
        PropertyPlayer.property.isRad = false;

        PropertyPlayer.property.day = 1;
        PropertyPlayer.property.hour = 5;
        PropertyPlayer.property.minutes = 0;

        PropertyPlayer.property.startPos = new Vector2(235f, 24f);
        PropertyPlayer.property.posOutside = Vector2.zero;
        PropertyPlayer.property.foundArea = new bool[9];
        PropertyPlayer.property.foundArea[0] = true; 
    }
}
