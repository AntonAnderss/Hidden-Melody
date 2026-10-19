using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionsDropdown;
    public Toggle fullscreen;
    public Toggle vSync;

    private Resolution[] resolutions;
    void Start()
    {
        CreateResDropdown();
    }
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    void CreateResDropdown()
    {
        int currentIndex = 0;
        List<string> options = new List<string>();
        resolutions = Screen.resolutions.Distinct().ToArray();
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentIndex = i;
            }
        }
        resolutionsDropdown.ClearOptions();
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentIndex;
        resolutionsDropdown.RefreshShownValue();
    }
    public void ChangeResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
