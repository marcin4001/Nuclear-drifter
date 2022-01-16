using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OptionsMenu : MonoBehaviour
{
    public Dropdown resDropdown;
    public Toggle fullScreenToggle;
    public List<string> resText;
    public int currentResolution = 0;
    public bool fullscreen = true;
    public Scrollbar musicScroll;
    public Scrollbar sfxScroll;
    private Resolution[] resolutions;
    private Canvas canvasOpt;
    private MusicController music;
    private SoundsTrigger sfxSound;
    private FightSound fightSound;
    // Start is called before the first frame update
    void Start()
    {
        if (Screen.currentResolution.width > 1920 && Screen.currentResolution.width > 1080)
            Screen.SetResolution(1920, 1080, fullscreen);
        canvasOpt = GetComponent<Canvas>();
        resDropdown.ClearOptions();
        Resolution[] temp = Screen.resolutions;
        resolutions = temp.Where(r => r.width >= 1024 && r.width <= 1920 && r.refreshRate == 60).ToArray();
        fullscreen = Screen.fullScreen;
        fullScreenToggle.isOn = fullscreen;  
        int i = 0;
        foreach(Resolution res in resolutions)
        {
            string currentRes = res.width + "x" + res.height;
            resText.Add(currentRes);
            if(res.width == Screen.width && res.height == Screen.height)
            {
                currentResolution = i;
            }
            i++;
        }

        resDropdown.AddOptions(resText);
        resDropdown.value = currentResolution;
        resDropdown.RefreshShownValue();
        canvasOpt.enabled = false;
        musicScroll.value = PlayerPrefs.GetFloat("mainMusic", 1.0f);
        sfxScroll.value = PlayerPrefs.GetFloat("sfxSound", 1.0f);

        music = FindObjectOfType<MusicController>();
        if (music != null)
            music.SetVolume(musicScroll.value);
        sfxSound = FindObjectOfType<SoundsTrigger>();
        if (sfxSound != null)
            sfxSound.SetVolume(sfxScroll.value);
        fightSound = FindObjectOfType<FightSound>();
        if (fightSound != null)
            fightSound.SetVolume(sfxScroll.value);
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
    }

    public void ChangeFullscreen(bool value)
    {
        Screen.SetResolution(resolutions[currentResolution].width, resolutions[currentResolution].height, value);
        fullscreen = value;
    }

    public void ChangeResolution(int value)
    {
        Screen.SetResolution(resolutions[value].width, resolutions[value].height, fullscreen);
        currentResolution = value;
    }

    public void OpenOptions()
    {
        canvasOpt.enabled = true;
    }

    public void CloseOptions()
    {
        canvasOpt.enabled = false;
    }
}
