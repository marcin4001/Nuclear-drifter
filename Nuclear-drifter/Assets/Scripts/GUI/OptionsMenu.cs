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
    private Resolution[] resolutions;
    private Canvas canvasOpt;
    // Start is called before the first frame update
    void Start()
    {
        canvasOpt = GetComponent<Canvas>();
        resDropdown.ClearOptions();
        Resolution[] temp = Screen.resolutions;
        resolutions = temp.Where(r => r.width >= 1024 && r.refreshRate >= 60).ToArray();
        fullscreen = Screen.fullScreen;
        fullScreenToggle.isOn = fullscreen;  
        int i = 0;
        foreach(Resolution res in resolutions)
        {
            string currentRes = res.width + "x" + res.height + " (" +res.refreshRate + "Hz)";
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
