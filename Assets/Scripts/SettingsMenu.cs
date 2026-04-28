using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour {

    public GameObject settingMenu;

    public vThirdPersonCamera vTPC;

    public AudioMixer aM; 

    public Slider slider;

    public Toggle myToggle;

    public TMPro.TMP_Dropdown resolutionDropdown;



    Resolution[] resolutions;

    private bool openMenu = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        myToggle.isOn = false;

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        int currentResoIndex = 0;

        List<string> options = new List<string>();

        for(int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && 
            resolutions[i].height == Screen.currentResolution.height) {
                currentResoIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResoIndex;
        resolutionDropdown.RefreshShownValue();

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("Escape Pressed");
            SettingMenu();
        }
    }

    public void SetVolume(float v) {
        Debug.Log(v);
        aM.SetFloat("volume", v);
    }

    public void SetResolution(int resoIndex) {

        Resolution reso = resolutions[resoIndex];
        Screen.SetResolution(reso.width, reso.height, false);
    }

    public void SetYInversion(bool x) {
        vTPC.yInvert = x;
        Debug.Log("value of x is: " + x);
    }

    public void SettingMenu() {
        openMenu = !openMenu;
        settingMenu.SetActive(openMenu);

    }

    public void SetDefaultSettings() {
        aM.SetFloat("volume", -40.0f);
        slider.value = -40.0f;

        myToggle.isOn = false;

        Resolution resols = resolutions[0];
        Screen.SetResolution(resols.width, resols.height, false);

        resolutionDropdown.value = 0;
        resolutionDropdown.RefreshShownValue();
        

        Debug.Log("button is pressed"); 
    }

}
