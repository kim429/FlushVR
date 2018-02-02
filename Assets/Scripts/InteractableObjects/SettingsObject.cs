using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Settings
{
    TELEPORTMOVEMENT,
    ANTIALIASING,
    START
}

public class SettingsObject : InteractableObject {
    // Private variables visible in the inspector
    [Header("Settings Object")]
    [SerializeField] private Settings setting;
    [SerializeField] private Slider slider;
    [SerializeField] float speed;

    // Private variables hidden in inspector
    private float settingLerp;
    private bool isLerping;

    public override void IsActivated()
    {
        base.IsActivated();
        SwitchSetting();
    }

    public override void Update()
    {
        base.Update();
        if (!slider) return;

        slider.value = Mathf.Lerp(0, 1, settingLerp);

        if (Setting == true && slider.value <= 1)
        {
            LerpSetting += Time.deltaTime * speed;
        }
        else if (Setting == false && slider.value >= 0)
        {
            LerpSetting -= Time.deltaTime * speed;
        }
    }

    public void SwitchSetting()
    {
        switch (setting)
        {

            case Settings.START:
                StartCoroutine(StartGame());
                break;

            default:
                Setting = !Setting;
                break;
        }
    }

    public bool Setting
    {
        get
        {
            switch (setting)
            {
                case Settings.TELEPORTMOVEMENT:
                    return Gaze.playerSettings.useTeleportMove;

                case Settings.ANTIALIASING:
                    return Gaze.playerSettings.useAntiAliasing;

                default:
                    Debug.Log("Setting failed");
                    return false;
            }
        }

        set
        {
            switch(setting)
            {
                case Settings.TELEPORTMOVEMENT:
                    Gaze.playerSettings.useTeleportMove = value;
                    break;

                case Settings.ANTIALIASING:
                    QualitySettings.antiAliasing = value ? 2 : 0; 
                    Gaze.playerSettings.useAntiAliasing = value;
                    break;
            }
        }
    }

    public float LerpSetting
    {
        get
        {
            return settingLerp;
        }
        set
        {
            if (value > 1)
            {
                settingLerp = 1;
            }
            else if (value < 0)
            {
                settingLerp = 0;
            }
            else
            {
                settingLerp = value;
            }
        }
    }

    public IEnumerator StartGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Scene");
        
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
