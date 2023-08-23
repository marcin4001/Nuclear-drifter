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

    public int indexTrapdoor = 0;
    public string[] texts;
    public List<string> result;
    public string nextScene;
    public Vector2 posInside;
    public bool inside = false;
    public string location;
    private GUIScript gUI;
    private Inventory inv;
    private PlayerClickMove player;
    private TimeGame time;
    private Health playerHP;
    private SoundsTrigger sound;
    private SoundUse soundUse;

    // Start is called before the first frame update
    void Start()
    {
        holeTrapdoor = GetComponent<SpriteRenderer>();
        gUI = FindObjectOfType<GUIScript>();
        inv = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerClickMove>();
        time = FindObjectOfType<TimeGame>();
        playerHP = player.GetComponent<Health>();
        sound = FindObjectOfType<SoundsTrigger>();
        soundUse = FindObjectOfType<SoundUse>();
        holeTrapdoor.enabled = false;
        upTrapdoor.enabled = false;
    }

    public void Use()
    {
        bool open = true;
        result = new List<string>();
        if(indexTrapdoor < 0)
        {
            if (player.ObjIsNearPlayer(transform.position, 1.1f))
            {
                holeTrapdoor.enabled = true;
                upTrapdoor.enabled = true;
                downTrapdoor.enabled = false;
                player.active = false;
                player.SetStop(true);
                gUI.blockGUI = true;
                sound.UseTrapdoor();
                Invoke("LoadScene", 1.0f);
            }
            else
            {
                gUI.AddText("Trapdoor is too far");
            }
            return;
        }

        if (!PropertyPlayer.property.trapdoorOpened[indexTrapdoor])
        {
            result.Add("Items needed to open:");
            if (key != null)
            {
                if (!inv.FindItemB(key.idItem))
                {
                    result.Add("- " + key.nameItem);
                    open = false;
                }
            }
            if (rope != null)
            {
                if (!inv.FindItemB(rope.idItem))
                {
                    result.Add("- " + rope.nameItem);
                    open = false;
                }
            }
            if (gasMask != null)
            {
                if (!inv.FindItemB(gasMask.idItem))
                {
                    result.Add("- " + gasMask.nameItem);
                    open = false;
                }
            }
        }

        if (open)
        {
            if (player.ObjIsNearPlayer(transform.position, 1.1f))
            {
                if (!PropertyPlayer.property.trapdoorOpened[indexTrapdoor])
                {
                    if (rope != null)
                    {
                        Slot ropeItem = inv.FindItem(rope.idItem);
                        inv.RemoveOne(ropeItem);
                    }
                    if (gasMask != null)
                    {
                        Slot maskItem = inv.FindItem(gasMask.idItem);
                        inv.RemoveOne(maskItem);
                    }
                    PropertyPlayer.property.SetTrapdoorOpened(indexTrapdoor);
                }
                holeTrapdoor.enabled = true;
                upTrapdoor.enabled = true;
                downTrapdoor.enabled = false;
                player.active = false;
                player.SetStop(true);
                gUI.blockGUI = true;
                sound.UseTrapdoor();
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
            soundUse.PlayLock();
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
            foreach (string text in texts)
                gUI.AddText(text);
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
        if(!inside)PropertyPlayer.property.posOutside = player.transform.position;
        if (location != "")
            PropertyPlayer.property.location = location;
        PropertyPlayer.property.SaveTemp();
    }
}
