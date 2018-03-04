using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : UIBackBtnHandle
{
    public Toggle infoToggle;

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && backBtnHandle 
            && !SingleTon.instance.dollList.isWindow)
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
}
