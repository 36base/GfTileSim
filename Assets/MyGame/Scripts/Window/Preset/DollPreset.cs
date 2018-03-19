using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

public class DollPreset : MonoBehaviour
{
    public GameObject presetPanel;
    public GameObject savePanel;
    public GameObject deletePanel;
    public int presetId;

    public Image[] dollPics;

    //public bool pressed;

    //public float longTouchTime = 1f;
    //private float currTime;

    //private void Update()
    //{
    //    if (pressed)
    //    {
    //        currTime += Time.deltaTime;
    //        if (currTime > longTouchTime)
    //        {
    //            deletePanel.SetActive(true);
    //        }
    //    }
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    pressed = true;
    //}
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    pressed = false;
    //    currTime = 0f;
    //}

    public void CloseAllPanel()
    {
        ClosePresetPanel();
        CloseDeletePanel();
        CloseSavePanel();
    }

    public void SavePreset()
    {
        string plainText = "";
        for (int i = 0; i < dollPics.Length; i++)
        {
            int pos = SingleTon.instance.dollSelecter.selects[i].gridPos;
            int id;
            if (SingleTon.instance.grid.tiles[pos - 1].doll == null)
            {
                id = 0;
                dollPics[i].sprite = SingleTon.instance.nullButtonSprite_dark;
            }
            else
            {
                id = SingleTon.instance.grid.tiles[pos - 1].doll.id;
                dollPics[i].sprite = SingleTon.instance.mgr.dollDict[id].profilePic;
            }

            plainText += "ID" + id.ToString("D5") + "Pos" + pos;
        }
        string cipherText = DataManager.Encrypt(plainText);

        try
        {
            if (!File.Exists(Application.persistentDataPath + "/PresetInfo.dat"))
            {
                SingleTon.instance.msg.SetMsg("파일이 존재하지 않음");
                SingleTon.instance.dollPresetList.InitAllPresets();
            }

            var data = File.ReadAllLines(Application.persistentDataPath + "/PresetInfo.dat");

            data[presetId] = cipherText;
            File.WriteAllLines(Application.persistentDataPath + "/PresetInfo.dat", data);
        }
        catch
        {
            SingleTon.instance.msg.SetMsg("파일 저장 오류");
            GC.Collect();
        }
#if UNITY_WEBGL
        SingleTon.instance.msg.SetMsg("Web플레이어 종료 시 데이터가 삭제됩니다!");
#endif

        savePanel.SetActive(false);
    }

    public void LoadPreset()
    {
        string cipherText = "";

        using (StreamReader streamReader
            = new StreamReader(Application.persistentDataPath + "/PresetInfo.dat"))
        {
            for (int i = 0; i < presetId; i++)
            {
                streamReader.ReadLine();
            }

            cipherText = streamReader.ReadLine();
            string plainText = DataManager.Decrypt(cipherText);

            DataToGrid(plainText);

            streamReader.Close();
        }

        presetPanel.SetActive(false);
    }

    public void DeletePreset()
    {
        try
        {
            if (!File.Exists(Application.persistentDataPath + "/PresetInfo.dat"))
            {
                SingleTon.instance.msg.SetMsg("파일이 존재하지 않음");
                SingleTon.instance.dollPresetList.InitAllPresets();
            }

            var data = File.ReadAllLines(Application.persistentDataPath + "/PresetInfo.dat");

            data[presetId] = "7qnzva8CTUHjOZeZ0lsGe+nUIkpDaO/FqQHaWAyfUmpUHj5SW+7Ri7fgbx2DXXE1hL8k7xz1pCiWrDpSCWMWfA==";
            File.WriteAllLines(Application.persistentDataPath + "/PresetInfo.dat", data);
            for (int i = 0; i < dollPics.Length; i++)
            {
                dollPics[i].sprite = SingleTon.instance.nullButtonSprite_dark;
            }
        }
        catch
        {
            SingleTon.instance.msg.SetMsg("파일 삭제 오류");
            GC.Collect();
        }

        deletePanel.SetActive(false);
    }

    public void MakePreset(string plainText)
    {
        StringBuilder sb = new StringBuilder();
        if (plainText.Length == 55)
        {
            for (int i = 0; i < 5; i++)
            {
                sb.Length = 0;
                sb.Append(plainText[2 + 11 * i]);
                sb.Append(plainText[3 + 11 * i]);
                sb.Append(plainText[4 + 11 * i]);
                sb.Append(plainText[5 + 11 * i]);
                sb.Append(plainText[6 + 11 * i]);
                int num;
                num = Convert.ToInt32(sb.ToString());
                Doll doll;
                if (SingleTon.instance.mgr.dollDict.TryGetValue(num, out doll))
                {
                    dollPics[i].sprite = doll.profilePic;
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                dollPics[i].sprite = SingleTon.instance.nullButtonSprite_dark;
            }
        }
    }

    public void DataToGrid(string plainText)
    {
        StringBuilder sb = new StringBuilder();
        if (plainText.Length == 55)
        {
            SingleTon.instance.ResetAll(false);

            for (int i = 0; i < SingleTon.instance.grid.tiles.Length; i++)
            {
                SingleTon.instance.grid.tiles[i].selecter = null;
            }

            for (int i = 0; i < 5; i++)
            {
                int pos = Convert.ToInt32(plainText[10 + 11 * i] - 48);
                if (pos < 1 || pos > 9)
                {
                    continue;
                }
                SingleTon.instance.dollSelecter.selects[i].gridPos = pos;
                SingleTon.instance.grid.tiles[pos - 1].selecter 
                    = SingleTon.instance.dollSelecter.selects[i];
                sb.Length = 0;
                sb.Append(plainText[2 + 11 * i]);
                sb.Append(plainText[3 + 11 * i]);
                sb.Append(plainText[4 + 11 * i]);
                sb.Append(plainText[5 + 11 * i]);
                sb.Append(plainText[6 + 11 * i]);
                int num;
                num = Convert.ToInt32(sb.ToString());
                if (num == 0)
                    continue;

                Doll doll;
                if (SingleTon.instance.mgr.dollDict.TryGetValue(num, out doll))
                {
                    SingleTon.instance.grid.Spawn(doll
                        , SingleTon.instance.dollSelecter.selects[i]);
                }
                else
                {
                    SingleTon.instance.msg.SetMsg("인형 번호 오류");
                }
            }
        }
        else
        {
            SingleTon.instance.msg.SetMsg("불러올 데이터가 없습니다!");
        }
    }

    public void OpenPresetPanel()
    {
        if (savePanel.activeSelf || deletePanel.activeSelf)
            return;
        SingleTon.instance.dollPresetList.CloseAllPresets();
        presetPanel.SetActive(true);
    }

    public void ClosePresetPanel()
    {
        presetPanel.SetActive(false);
    }

    public void OpenSavePanel()
    {
        deletePanel.SetActive(false);
        presetPanel.SetActive(false);
        savePanel.SetActive(true);
    }
    public void CloseSavePanel()
    {
        savePanel.SetActive(false);
    }

    public void OpenDeletePanel()
    {
        presetPanel.SetActive(false);
        savePanel.SetActive(false);
        deletePanel.SetActive(true);
    }
    public void CloseDeletePanel()
    {
        deletePanel.SetActive(false);
    }
}
