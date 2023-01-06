using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public MouseLook player;

    Resolution[] resolutions;

    [Header("Video")]
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown GraphicsDropdown;

    [Header("Graficos")]
    public Toggle vSyncToggle;
    public Toggle PostVolToggle;
    public GameObject PostVol;

    [Header("Controles")]
    public Slider sensitivitySlider;

    [Header("Volume")]
    public Slider MasterVolumeSlider;
    public Slider SFXVolumeSlider;
    public Slider MusicVolumeSlider;

    [Header("Values")]
    int Resolution;
    int Quality;
    bool vSync;
    bool PVol;
    int sensivility;
    int MasterV;
    int SFXV;
    int MusicV;

    private void Start()
    {
        player = FindObjectOfType<MouseLook>();

        GraphicsDropdown.value = QualitySettings.GetQualityLevel();

        GetResolutions();
    }

    public void GetDefaultValues()
    {
        string value;

        //-- Resolution # Quality # vSync # PostVol # sensivility # Master Volume # SFX Volume # Music Volume -- //
        if (PlayerPrefs.GetString("Config") == null) { value = "0#0#false#true#100#100#100#100"; } else { value = PlayerPrefs.GetString("Config"); }
    }

    void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            float res = (float)resolutions[i].width / (float)resolutions[i].height;

            if ((res.ToString("F4") == "1,7778" || res.ToString("F4") == "1,6000") && resolutions[i].height > 500) {
                Debug.Log(res.ToString("F4"));
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);
            }

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) { currentResolutionIndex = i; }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume(float volume) { audioMixer.SetFloat("MasterVol", volume); }
    public void SetMusicVolume(float volume) { audioMixer.SetFloat("MusicVol", volume); }
    public void SetSFXVolume(float volume) { audioMixer.SetFloat("SFXVol", volume); }

    public void SetSensiviliti(float sensi) { player.sensitivity = sensi; }

    public void SetFov(float fov) { Camera.main.fieldOfView = fov; }

    public void SetQuality(int qualityIndex) { QualitySettings.SetQualityLevel(qualityIndex); }

    public void SetFullScreen(bool isFullscrenn) { Screen.fullScreen = isFullscrenn; }

    public void VSync(bool isVSync)
    {
        int Acti;
        if (isVSync) { Acti = 1; } else { Acti = 0; }
        if (isVSync) { vSyncToggle.isOn = true; } else { vSyncToggle.isOn = false; }
        QualitySettings.vSyncCount = Acti;
    }

    public void SetPostVol(bool isAct) { PostVol.SetActive(isAct); PostVolToggle.isOn = isAct; }
}
