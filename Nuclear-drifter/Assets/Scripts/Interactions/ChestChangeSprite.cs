using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestChangeSprite : MonoBehaviour
{
    public Sprite openChest;
    public Sprite closeChest;
    private SpriteRenderer render;

    public void Open()
    {
        render.sprite = openChest;
    }

    public void Close()
    {
        render.sprite = closeChest;
    }
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }
}
