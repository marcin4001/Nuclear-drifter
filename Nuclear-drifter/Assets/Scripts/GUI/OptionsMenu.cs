using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json.Linq;

public class OptionsMenu : MonoBehaviour
{
    public Dropdown resDropdown;
    public Text resLabel;
    public Toggle fullScreenToggle;
    public Toggle talkingHeadToogle;
    public List<string> resText;
    public int currentResolution = 0;
    public int currentResolutionSelect = 0;
    public bool fullscreen = true;
    public Scrollbar musicScroll;
    public Scrollbar sfxScroll;
    public Scrollbar speedScroll;
    private Resolution[] resolutions;
    private Canvas canvasOpt;
    private MusicController music;
    private SoundsTrigger sfxSound;
    private FightSound fightSound;
    private CanvasScaler600p canvasScaler;
    private SoundUse soundUse;
    private TalkingHeadController head;
    // Start is called before the first frame update
    void Start()
    {
        if (Screen.currentResolution.width > 1920 && Screen.currentResolution.width > 1080)
            Screen.SetResolution(1920, 1080, fullscreen);
        canvasOpt = GetComponent<Canvas>();
        resDropdown.ClearOptions();
        Resolution[] temp = Screen.resolutions;
        resolutions = temp.Where(r => r.width >= 800 && r.width <= 1920 && r.refreshRate == 60).ToArray();
        fullscreen = Screen.fullScreen;
        fullScreenToggle.isOn = fullscreen;
        talkingHeadToogle.isOn = PlayerPrefs.GetInt("talkingHead", 1) != 0;
        head = FindObjectOfType<TalkingHeadController>();
        if (head != null)
            head.SetSwitchOn(PlayerPrefs.GetInt("talkingHead", 1) != 0);
        int i = 0;
        foreach(Resolution res in resolutions)
        {
            string currentRes = res.width + "x" + res.height;
            resText.Add(currentRes);
            if(res.width == Screen.width && res.height == Screen.height)
            {
                currentResolution = i;
                currentResolutionSelect = i;
                if(resLabel != null)
                    resLabel.text = currentRes;
            }
            i++;
        }

        resDropdown.AddOptions(resText);
        resDropdown.value = currentResolution;
        resDropdown.RefreshShownValue();


        canvasOpt.enabled = false;
        musicScroll.value = PlayerPrefs.GetFloat("mainMusic", 1.0f);
        sfxScroll.value = PlayerPrefs.GetFloat("sfxSound", 1.0f);
        if (speedScroll != null)
        {
            speedScroll.value = PlayerPrefs.GetFloat("moveSpeed", 0.6f);
            PlayerClickMove move = FindObjectOfType<PlayerClickMove>();
            if (move != null)
                move.SetSpeed(speedScroll.value);
        }

        music = FindObjectOfType<MusicController>();
        if (music != null)
            music.SetVolume(musicScroll.value);
        sfxSound = FindObjectOfType<SoundsTrigger>();
        if (sfxSound != null)
            sfxSound.SetVolume(sfxScroll.value);
        fightSound = FindObjectOfType<FightSound>();
        if (fightSound != null)
            fightSound.SetVolume(sfxScroll.value);
        soundUse = FindObjectOfType<SoundUse>();
        if (soundUse != null)
            soundUse.SetVolume(sfxScroll.value);
        canvasScaler = FindObjectOfType<CanvasScaler600p>();
        if (canvasScaler != null)
            canvasScaler.ChangeScale(new Vector2 (Screen.width, Screen.height));
    }

    public void ChangeVolumeMusic()
    {
        PlayerPrefs.SetFloat("mainMusic", musicScroll.value);
        if (music != null)
            music.SetVolume(musicScroll.value);
    }

    public void ChangeVolumeSFX()
    {
        PlayerPrefs.SetFloat("sfxSound", sfxScroll.value);
        if (sfxSound != null)
            sfxSound.SetVolume(sfxScroll.value);
        if (fightSound != null)
            fightSound.SetVolume(sfxScroll.value);
        if (soundUse != null)
            soundUse.SetVolume(sfxScroll.value);
    }

    public void ChangeFullscreen(bool value)
    {
        Screen.SetResolution(resolutions[currentResolution].width, resolutions[currentResolution].height, value);
        fullscreen = value;
        if (sfxSound != null)
            sfxSound.PlayClickButton();
    }

    public void ChangeTalkingHead(bool value)
    {
        PlayerPrefs.SetInt("talkingHead", value ? 1 : 0);
        if (head != null)
            head.SetSwitchOn(value);
        if (sfxSound != null)
            sfxSound.PlayClickButton();
    }

    public void ChangeResolution(int value)
    {
        Screen.SetResolution(resolutions[value].width, resolutions[value].height, fullscreen);
        currentResolution = value;
        if(canvasScaler != null)
            canvasScaler.ChangeScale(new Vector2 (resolutions[value].width, resolutions[value].height));
    }

    public void RightButton()
    {
        currentResolutionSelect += 1;
        if(currentResolutionSelect >= resolutions.Length)
            currentResolutionSelect = 0;
        ChangeResolutionText();
        if (sfxSound != null)
            sfxSound.PlayClickButton();
    }

    public void LeftButton()
    {
        currentResolutionSelect -= 1;
        if (currentResolutionSelect < 0)
            currentResolutionSelect = resolutions.Length - 1;
        ChangeResolutionText();
        if (sfxSound != null)
            sfxSound.PlayClickButton();
    }

    public void ChangeResolutionText()
    {
        resLabel.text = resText[currentResolutionSelect];
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(resolutions[currentResolutionSelect].width, resolutions[currentResolutionSelect].height, fullscreen);
        currentResolution = currentResolutionSelect;
        if (canvasScaler != null)
            canvasScaler.ChangeScale(new Vector2(resolutions[currentResolutionSelect].width, resolutions[currentResolutionSelect].height));
        if (sfxSound != null)
            sfxSound.PlayClickButton();
    }

    public void ChangeSpeed()
    {
        double speed = speedScroll.value;
        speed = System.Math.Floor(speed * 10);
        if ((int)speed % 2 == 0)
            speed = speed * 0.1;
        else
            speed = (speed + 1) * 0.1;
        speedScroll.value = (float) speed;
        PlayerClickMove move = FindObjectOfType<PlayerClickMove>();
        if (move != null)
            move.SetSpeed((float)speed);
        PlayerPrefs.SetFloat("moveSpeed", (float)speed);
    }

    public void OpenOptions()
    {
        canvasOpt.enabled = true;
        ChangeResolutionText();
    }

    public void CloseOptions()
    {
        canvasOpt.enabled = false;
        if (sfxSound != null)
            sfxSound.PlayClickButton();
        currentResolutionSelect = currentResolution;
    }
}
