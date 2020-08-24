using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionNPC : MonoBehaviour
{
    public Sprite back;
    public Sprite forward;
    public Sprite left;
    public Sprite right;
    private Sprite prevState;
    private SpriteRenderer renderSp;
    // Start is called before the first frame update
    void Start()
    {
        renderSp = GetComponent<SpriteRenderer>();
        prevState = renderSp.sprite;
    }

    public void SetSprite(Sprite _back, Sprite _forward, Sprite _left, Sprite _right, Sprite init)
    {
        back = _back;
        forward = _forward;
        left = _left;
        right = _right;
        prevState = init;
        renderSp = GetComponent<SpriteRenderer>();
        renderSp.sprite = init;
    }

    public void SetDir(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        
        Debug.Log("x: " + x + " y: " + y);

        if(x > 0)
        {
            renderSp.sprite = right;
        }
        else if(x < 0)
        {
            renderSp.sprite = left;
        }
        else
        {
            if(y > 0)
            {
                renderSp.sprite = back;
            }
            else if(y < 0)
            {
                renderSp.sprite = forward;
            }
        }
    }

    public void ResetPos()
    {
        if(prevState != null)
        {
            renderSp.sprite = prevState;
        }
    }
}
