using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// 인형 프리셋들 묶음
/// </summary>
public class DollPresetList : UIBackBtnHandle
{
    public DollPreset[] presets;

    public AllOffHandler CloseAllPresets;

    public string[] examples;

    private void Awake()
    {
        //초기화 시 딜리게이트 체인
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

    /// <summary>
    /// 프리셋 초기화
    /// </summary>
    public void InitAllPresets()
    {
        try
        {
            //파일 스트림으로 저장파일 접근
            FileStream fs = new FileStream(Application.persistentDataPath + "/PresetInfo.dat"
                , FileMode.OpenOrCreate, FileAccess.ReadWrite);

            StreamReader sr = new StreamReader(fs);
            StreamWriter sw = new StreamWriter(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < presets.Length; i++)
            {
                //프리셋 개수만큼 순회 하면서 라인 읽어내려감
                var readLine = sr.ReadLine();
                // 해당 라인이 null 이면
                if (readLine == null)
                {
                    //기본 프리셋에 있는 만큼 생성, 기본 프리셋을 다생성하면 빈프리셋 생성
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
                    //해당 라인에 문자열이 있으면, 해당 문자열로 프리셋 생성
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
