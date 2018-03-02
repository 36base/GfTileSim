using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : UIBackBtnHandle
{
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && backBtnHandle)
        {
            OpenOrClose();
        }
    }
}
