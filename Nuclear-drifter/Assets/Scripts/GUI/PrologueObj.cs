using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrologueObj 
{
    public Sprite backgroundImg;
    [TextArea(2,8)]
    public string text;
    public AudioClip clip;
}
