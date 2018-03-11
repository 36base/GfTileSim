using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : UIBackBtnHandle
{
    public Toggle infoToggle;
    public Toggle soundToggle;
    public Toggle lossBuffToggle;

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && backBtnHandle 
            && !SingleTon.instance.IsWindow)
        {
            OpenOrClose();
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if(PlayerPrefs.HasKey("info"))
        {
            infoToggle.isOn = ReturnBool(PlayerPrefs.GetInt("info"));
        }
        else
        {
            infoToggle.isOn = true;
        }

        if(PlayerPrefs.HasKey("sound"))
        {
            soundToggle.isOn = ReturnBool(PlayerPrefs.GetInt("sound"));
        }
        else
        {
            soundToggle.isOn = true;
        }

        if(PlayerPrefs.HasKey("lossBuff"))
        {
            lossBuffToggle.isOn = ReturnBool(PlayerPrefs.GetInt("lossBuff"));
        }
        else
        {
            lossBuffToggle.isOn = false;
        }
        SetInfo();
        SetSound();
        SetLossBuff();

    }
    private bool ReturnBool(int value)
    {
        if (value == 0)
            return false;
        else
            return true;
    }


    public void SetInfo()
    {
        if(infoToggle.isOn)
        {
            SingleTon.instance.info.infoOn = true;
            PlayerPrefs.SetInt("info", 1);
        }
        else
        {
            SingleTon.instance.info.go.SetActive(false);
            SingleTon.instance.info.infoOn = false;
            PlayerPrefs.SetInt("info", 0);
        }
    }

    public void SetSound()
    {
        if (soundToggle.isOn)
        {
            SingleTon.instance.mute = false;
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            SingleTon.instance.mute = true;
            PlayerPrefs.SetInt("sound", 0);
        }


    }

    public void SetLossBuff()
    {
        if(lossBuffToggle.isOn)
        {
            SingleTon.instance.grid.lossBuff = true;
            PlayerPrefs.SetInt("lossBuff", 1);
        }
        else
        {
            SingleTon.instance.grid.lossBuff = false;
            PlayerPrefs.SetInt("lossBuff", 0);
        }
    }
}
