using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    private List<Resolution> resolutions = new List<Resolution>();
    public TMP_Dropdown resolutionDropdown;
    public AudioManager audioManager;

    private void Awake()
    {
        #region resolutions and fullscreen
        triggerResolution = true;

        if (!PlayerPrefs.HasKey("saveIndex"))
        {
            FindResolutionIndex();
        }
        else
        {
            resolutionIndexSave = PlayerPrefs.GetInt("saveIndex");
        }

        if (!PlayerPrefs.HasKey("SaveFullScreen"))
        {
            saveFullsScreen = 0;
        }
        else
        {
            saveFullsScreen = PlayerPrefs.GetInt("SaveFullScreen");
        }

        if (saveFullsScreen == 1)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }

        if (!PlayerPrefs.HasKey("ScreenWidth"))
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            saveWidth = PlayerPrefs.GetInt("ScreenWidth");
            saveHeight = PlayerPrefs.GetInt("ScreenHeight");
            Screen.SetResolution(saveWidth, saveHeight, Screen.fullScreenMode);
        }
        #endregion

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);

        if (!PlayerPrefs.HasKey("SaveFullScreen"))
        {
            fullscreenText.text = $"{LocalizationSCRIPT.fullscreen}{LocalizationSCRIPT.OFF}";
        }

        if (saveFullsScreen == 1)
        {
            fullscreenText.text = $"{LocalizationSCRIPT.fullscreen}{LocalizationSCRIPT.OFF}";
        }
        else
        {
            fullscreenText.text = $"{LocalizationSCRIPT.fullscreen}{LocalizationSCRIPT.ON}";
        }


        triggerResolution = false;
    }

    private void Start()
    {
        #region Resolution
        // Define a list of supported resolutions
        resolutions = new List<Resolution>
        {
            new Resolution { width = 800, height = 600 },
            new Resolution { width = 1024, height = 768 },
            new Resolution { width = 1280, height = 720 },
            new Resolution { width = 1280, height = 800 },
            new Resolution { width = 1280, height = 1024 },
            new Resolution { width = 1366, height = 768 },
            new Resolution { width = 1600, height = 900 },
            new Resolution { width = 1920, height = 1080 },
            new Resolution { width = 1920, height = 1200 },
            new Resolution { width = 2560, height = 1440 },
            new Resolution { width = 2560, height = 1600 },
            new Resolution { width = 2560, height = 1080 },
            new Resolution { width = 3440, height = 1440 },
            new Resolution { width = 3840, height = 1440 },
            new Resolution { width = 3840, height = 2160 },
            new Resolution { width = 3840, height = 2400 }
            // Add any other resolutions you want to support here
        };

        // Add the supported resolutions to the dropdown
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.value = resolutionIndexSave;
        #endregion
    }

    #region resolution 
    public int resolutionIndexSave;
    public bool triggerResolution;
    public int saveHeight, saveWidth;
    public static int saveFullsScreen;

    public void SetResolution(int resolutionIndex)
    {
        if (triggerResolution == false)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

            saveWidth = resolution.width;
            saveHeight = resolution.height;

            PlayerPrefs.SetInt("ScreenWidth", saveWidth);
            PlayerPrefs.SetInt("ScreenHeight", saveHeight);

            resolutionIndexSave = resolutionIndex;
            PlayerPrefs.SetInt("saveIndex", resolutionIndexSave);
        }
    }

    public TextMeshProUGUI fullscreenText;

    public void SetFullSCreen()
    {
        audioManager.Play("Ui_click1");

        if (saveFullsScreen == 0)
        {
            fullscreenText.text = $"{LocalizationSCRIPT.fullscreen}{LocalizationSCRIPT.OFF}";
            Screen.fullScreenMode = FullScreenMode.Windowed;

            saveFullsScreen = 1;
            PlayerPrefs.SetInt("SaveFullScreen", saveFullsScreen);

        }
        else
        {
            fullscreenText.text = $"{LocalizationSCRIPT.fullscreen}{LocalizationSCRIPT.ON}";
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;

            saveFullsScreen = 0;
            PlayerPrefs.SetInt("SaveFullScreen", saveFullsScreen);
        }
    }

    public void FindResolutionIndex()
    {
        if (Screen.width == 600 && Screen.height == 800) { resolutionIndexSave = 0; }
        if (Screen.width == 1024 && Screen.height == 768) { resolutionIndexSave = 1; }
        if (Screen.width == 1280 && Screen.height == 720) { resolutionIndexSave = 2; }
        if (Screen.width == 1280 && Screen.height == 800) { resolutionIndexSave = 3; }
        if (Screen.width == 1280 && Screen.height == 1024) { resolutionIndexSave = 4; }
        if (Screen.width == 1366 && Screen.height == 768) { resolutionIndexSave = 5; }
        if (Screen.width == 1600 && Screen.height == 900) { resolutionIndexSave = 6; }
        if (Screen.width == 1920 && Screen.height == 1080) { resolutionIndexSave = 7; }
        if (Screen.width == 1920 && Screen.height == 1200) { resolutionIndexSave = 8; }
        if (Screen.width == 2560 && Screen.height == 1440) { resolutionIndexSave = 9; }
        if (Screen.width == 2560 && Screen.height == 1600) { resolutionIndexSave = 10; }
        if (Screen.width == 2560 && Screen.height == 1080) { resolutionIndexSave = 11; }
        if (Screen.width == 3440 && Screen.height == 1440) { resolutionIndexSave = 12; }
        if (Screen.width == 3840 && Screen.height == 1440) { resolutionIndexSave = 13; }
        if (Screen.width == 3840 && Screen.height == 2160) { resolutionIndexSave = 14; }
        if (Screen.width == 3840 && Screen.height == 2400) { resolutionIndexSave = 15; }
    }
    #endregion
}
