using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockpickingPanel : MonoBehaviour
{
    public Text chanceText;
    public int chance = 3;
    public int maxChance = 3;
    public string sequence = "RLRL";
    public int currentSeqIndex = 0;
    public Image padlockSignImg;
    public Sprite greyPadlock;
    public Sprite greenPadlock;
    public Sprite redPadlock;
    public bool active = false;
    public GameObject panel;
    public Container container;
    private MapControl map;
    private PauseMenu menu;
    private TypeScene typeSc;
    private SoundUse soundUse;
    private SoundsTrigger soundsTrigger;
    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<MapControl>();
        menu = FindObjectOfType<PauseMenu>();
        typeSc = FindObjectOfType<TypeScene>();
        soundUse = FindObjectOfType<SoundUse>();
        soundsTrigger = FindObjectOfType<SoundsTrigger>();
        padlockSignImg.overrideSprite = greyPadlock;
        chanceText.text = "Chance: " + chance;
        active = false;
        panel.SetActive(active);
    }

    public void Open(Container _container)
    {
        container = _container;
        active = true;
        sequence = container.sequence;
        if(string.IsNullOrEmpty(sequence))
            sequence = "RLRL";
        panel.SetActive(active);
        currentSeqIndex = 0;
        chance = maxChance;
        chanceText.text = "Chance: " + chance;
        padlockSignImg.overrideSprite = greyPadlock;
        map.keyActive = false;
        menu.activeEsc = false;
        typeSc.inMenu = true;
    }

    public void Hide()
    {
        active = false;
        sequence = "";
        panel.SetActive(active);
        container = null;
    }

    public void Close()
    {
        Hide();
        map.keyActive = true;
        menu.activeEsc = true;
        typeSc.inMenu = false;
        soundsTrigger.PlayClickButton();
    }

    public void RightButton()
    {
        CheckSequence('R');
    }

    public void LeftButton()
    {
        CheckSequence('L');
    }

    private void CheckSequence(char letter)
    {
        if (sequence[currentSeqIndex] == letter)
        {
            currentSeqIndex++;
            if(currentSeqIndex >= sequence.Length)
            {
                Debug.Log("Open Lock!!!");
                container.OpenWithLockpick();
                typeSc.inMenu = false;
                Hide();
                return;
            }
            soundUse.PlayLockpickingGood();
            padlockSignImg.overrideSprite = greenPadlock;
        }
        else
        {
            chance -= 1;
            if (chance <= 0)
            {
                container.BrokenLockpick();
                Debug.Log("Failed!!! Close Panel");
                Close();
            }
            else
            {
                padlockSignImg.overrideSprite = redPadlock;
                chanceText.text = "Chance: " + chance;
                currentSeqIndex = 0;
                soundUse.PlayLock();
            }
        }
    }

    private void Update()
    {
        if(active)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow)) 
                LeftButton();
            if(Input.GetKeyDown(KeyCode.RightArrow))
                RightButton();
        }
    }
}
