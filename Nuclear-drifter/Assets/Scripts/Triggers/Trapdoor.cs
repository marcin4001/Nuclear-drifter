using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trapdoor : MonoBehaviour
{
    public SpriteRenderer holeTrapdoor;
    public SpriteRenderer upTrapdoor;
    public SpriteRenderer downTrapdoor;

    public Item key;
    public Item rope;
    public Item gasMask;

    public List<string> result;
    public string nextScene;
    public Vector2 posInside;
    private GUIScript gUI;
    private Inventory inv;
    private PlayerClickMove player;
    private TimeGame time;
    private Health playerHP;
    // Start is called before the first frame update
    void Start()
    {
        holeTrapdoor = GetComponent<SpriteRenderer>();
        gUI = FindObjectOfType<GUIScript>();
        inv = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerClickMove>();
        time = FindObjectOfType<TimeGame>();
        playerHP = player.GetComponent<Health>();
        holeTrapdoor.enabled = false;
        upTrapdoor.enabled = false;
    }

    public void Use()
    {
        bool open = true;
        result = new List<string>();
        if (!PropertyPlayer.property.trapdoorOpened)
        {
            result.Add("Items needed to open:");
            if (!inv.FindItemB(key.idItem))
            {
                result.Add("- " + key.nameItem);
                open = false;
            }
            if (!inv.FindItemB(rope.idItem))
            {
                result.Add("- " + rope.nameItem);
                open = false;
            }
            if (!inv.FindItemB(gasMask.idItem))
            {
                result.Add("- " + gasMask.nameItem);
                open = false;
            }
        }

        if (open)
        {
            if (player.ObjIsNearPlayer(transform.position, 1.1f))
            {
                if (!PropertyPlayer.property.trapdoorOpened)
                {
                    Slot ropeItem = inv.FindItem(rope.idItem);
                    inv.RemoveOne(ropeItem);
                    Slot maskItem = inv.FindItem(gasMask.idItem);
                    inv.RemoveOne(maskItem);
                    PropertyPlayer.property.SetTrapdoorOpened();
                }
                holeTrapdoor.enabled = true;
                upTrapdoor.enabled = true;
                downTrapdoor.enabled = false;
                player.active = false;
                player.SetStop(true);
                gUI.blockGUI = true;
                Invoke("LoadScene", 1.0f);
            }
            else
            {
                gUI.AddText("Trapdoor is too far");
            }
        }
        else
        {
            foreach (string res in result)
            {
                gUI.AddText(res);
            }
        }
    }

    private void LoadScene()
    {
        SetProperty();
        SceneManager.LoadScene(nextScene);
    }

    public void ShowText()
    {
        if (gUI == null) gUI = FindObjectOfType<GUIScript>();
        if (gUI != null)
        {
            gUI.AddText("This is the subway");
            gUI.AddText("entrance");
        }
    }

    private void SetProperty()
    {
        PropertyPlayer.property.currentHealth = playerHP.currentHealth;
        PropertyPlayer.property.maxHealth = playerHP.maxAfterRad;
        PropertyPlayer.property.isPoison = playerHP.isPoison;
        PropertyPlayer.property.isRad = playerHP.isRad;
        PropertyPlayer.property.levelRad = playerHP.levelRad;

        PropertyPlayer.property.day = time.day;
        PropertyPlayer.property.hour = time.hour;
        PropertyPlayer.property.minutes = time.minutes;

        PropertyPlayer.property.startPos = posInside;
        PropertyPlayer.property.posOutside = player.transform.position;
        PropertyPlayer.property.SaveTemp();
    }
}
