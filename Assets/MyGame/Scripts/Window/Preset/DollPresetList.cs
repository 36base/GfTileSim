using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DollPresetList : UIBackBtnHandle
{
    public DollPreset[] presets;

    public AllOffHandler CloseAllPresets;

    public string[] examples;

    private void Awake()
    {
        for (int i = 0; i < presets.Length; i++)
        {
            if (presets[i] != null)
                CloseAllPresets += presets[i].CloseAllPanel;
        }
    }

    private void OnMouseExit()
    {
        CloseAllPresets();
    }

    public void InitAllPresets()
    {
        try
        {
            FileStream fs = new FileStream(Application.persistentDataPath + "/PresetInfo.dat"
                , FileMode.OpenOrCreate, FileAccess.ReadWrite);

            StreamReader sr = new StreamReader(fs);
            StreamWriter sw = new StreamWriter(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < presets.Length; i++)
            {
                var readLine = sr.ReadLine();
                if (readLine == null)
                {
                    if (i < examples.Length)
                    {
                        sw.WriteLine(examples[i]);
                        presets[i].MakePreset(DataManager.Decrypt(examples[i]));
                    }
                    else
                        sw.WriteLine("7qnzva8CTUHjOZeZ0lsGe+nUIkpDaO/FqQHaWAyfUmpUHj5SW+7Ri7fgbx2DXXE1hL8k7xz1pCiWrDpSCWMWfA==");

                }
                else
                {
                    presets[i].MakePreset(DataManager.Decrypt(readLine));
                }

            }

            sw.Close();
            sr.Close();
            fs.Close();
        }
        catch
        {
            SingleTon.instance.msg.SetMsg("프리셋 초기화 오류");
            GC.Collect();
        }
    }
}
