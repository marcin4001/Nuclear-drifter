using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPCbg : MonoBehaviour
{
    public string[] dialogues;
    public string nameNPC;
    public int currentIndex;

    public float counter = 0.0f;
    public float counterMax = 3.5f;
    public bool active = false;
    public TextMeshPro text;

    private PlayerClickMove player;

    private GUIScript gUI;
    private ChangeDirectionNPC dir;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        dir = GetComponent<ChangeDirectionNPC>();
        if (dialogues.Length == 0) Destroy(this);
        text.enabled = false;
    }

    public void ShowText()
    {
        gUI.AddText("This is " + nameNPC);
    }

    public void Use()
    {
        active = true;
        counter = 0.0f;
        text.text = dialogues[currentIndex];
        text.enabled = true;
        if(currentIndex >= dialogues.Length - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
        if (player.ObjIsNearPlayer(transform.parent.position, 1.1f))
        {
            if (dir != null)
            {
                Vector3 dirNPC = player.transform.position - transform.parent.position;
                Debug.Log(dirNPC);
                dir.SetDir((Vector2)dirNPC);
                Vector3 dirPlayer = transform.parent.position - player.transform.position;
                player.SetDir((Vector2)dirPlayer);
                Debug.Log(dirPlayer);
            }
        }
        else
        {
            if (dir != null)
            {
                dir.ResetPos();
            }
        }
    }

    void Update()
    {
        if(active)
        {
            if(counter >= counterMax)
            {
                counter = 0.0f;
                active = false;
                text.enabled = false;
            }
            else
            {
                counter += Time.deltaTime;
            }
        }
        
    }
}
