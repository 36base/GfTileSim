using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : UIBackBtnHandle
{
    public Toggle infoToggle;
    public Toggle soundToggle;

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && backBtnHandle 
            && !SingleTon.instance.IsWindow)
        {
            OpenOrClose();
        }
    }

    public void SetInfo()
    {
        if(infoToggle.isOn)
        {
            SingleTon.instance.info.infoOn = true;
        }
        else
        {
            SingleTon.instance.info.go.SetActive(false);
            SingleTon.instance.info.infoOn = false;
        }
    }

    public void SetSound()
    {
        if (soundToggle.isOn)
        {
            SingleTon.instance.mute = false;
        }
        else
        {
            SingleTon.instance.mute = true;
        }
    }
}
