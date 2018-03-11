using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPresetList : UIBackBtnHandle
{
    public DollPreset[] presets;

    public AllOffHandler CloseAllPresets;

    private void Awake()
    {
        for (int i = 0; i < presets.Length; i++)
        {
            if(presets[i] != null)
                CloseAllPresets += presets[i].CloseAllPanel;
        }
    }

    private void OnMouseExit()
    {
        CloseAllPresets();
    }
}
