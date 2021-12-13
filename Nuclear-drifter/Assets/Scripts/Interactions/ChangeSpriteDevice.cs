using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteDevice : MonoBehaviour
{
    public Sprite oldSprite;
    public Sprite fixedSprite;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    public void Change(bool _fixed)
    {
        if (_fixed)
            render.sprite = fixedSprite;
        else
            render.sprite = oldSprite;
    }
}
