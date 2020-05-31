using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyPlayer : MonoBehaviour
{
    public static PropertyPlayer property;

    public int currentHealth = 0;
    public int maxHealth = 0;
    public bool isRad = false;
    public bool isPoison = false;

    public int day = 0;
    public int hour = 0;
    public int minutes = 0;

    public Vector2 startPos;
    public Vector2 posOutside;
    public bool[] foundArea;
    public List<Slot> inv;

    // Start is called before the first frame update
    void Awake()
    {
        if (!property)
        {
            DontDestroyOnLoad(this.gameObject);
            property = this;
        }
        else Destroy(gameObject);
        
    }


}
