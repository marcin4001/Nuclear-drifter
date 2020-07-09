using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewModule", menuName = "Dialogue/DialogueModule")]
public class DialogueModule : ScriptableObject
{
    public Dialogue[] dialogues;
}
