using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

/// <summary>
/// 인형 프리셋 저장불러오기 및 관리 클래스
/// </summary>
public class DollPreset : MonoBehaviour
{
    public GameObject presetPanel;
    public GameObject savePanel;
    public GameObject deletePanel;
    /// <summary>
    /// 0~9까지 사용
    /// </summary>
    public int presetId;

    /// <summary>
    /// 각 프리셋에 표현될 인형들 이미지배열
    /// </summary>
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
        
        //프리셋 내 인형이미지를 순회하면서 알맞은 이미지 할당 및 플레인텍스트 연결
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

        //연결된 플레인텍스트 암호화
        string cipherText = DataManager.Encrypt(plainText);

        try
        {
            //저장할 파일이 존재하지 않을경우 초기화 호출
            if (!File.Exists(Application.persistentDataPath + "/PresetInfo.dat"))
            {
                SingleTon.instance.msg.SetMsg("파일이 존재하지 않음");
                SingleTon.instance.dollPresetList.InitAllPresets();
            }

            //저장할 파일에 모든 Line을 읽어 문자열 배열로 반환
            var data = File.ReadAllLines(Application.persistentDataPath + "/PresetInfo.dat");

            //알맞은 presetId의 배열 위치에 암호화된 텍스트 할당
            data[presetId] = cipherText;
            //수정된 배열을 다시 쓰기
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
            //PresetId에 알맞은 라인을 읽어옴
            cipherText = streamReader.ReadLine();
            //복호화
            string plainText = DataManager.Decrypt(cipherText);
            //복호화 한 문자열을 Grid에 표현
            DataToGrid(plainText);

            streamReader.Close();
        }

        presetPanel.SetActive(false);
    }

    public void DeletePreset()
    {
        try
        {
            //파일이 존재하지 않을경우 초기화 호출
            if (!File.Exists(Application.persistentDataPath + "/PresetInfo.dat"))
            {
                SingleTon.instance.msg.SetMsg("파일이 존재하지 않음");
                SingleTon.instance.dollPresetList.InitAllPresets();
            }

            //SavePreset과 동일기능, 저장할 line에 빈데이터 저장
            var data = File.ReadAllLines(Application.persistentDataPath + "/PresetInfo.dat");

            data[presetId] = "7qnzva8CTUHjOZeZ0lsGe+nUIkpDaO/FqQHaWAyfUmpUHj5SW+7Ri7fgbx2DXXE1hL8k7xz1pCiWrDpSCWMWfA==";
            File.WriteAllLines(Application.persistentDataPath + "/PresetInfo.dat", data);
            for (int i = 0; i < dollPics.Length; i++)
            {
                //빈 이미지 할당
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

    /// <summary>
    /// 플레인 텍스트를 받아 프리셋 생성
    /// </summary>
    /// <param name="plainText"></param>
    public void MakePreset(string plainText)
    {
        StringBuilder sb = new StringBuilder();
        //ID00000POS0 x5
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

    /// <summary>
    /// 플레인 텍스트를 받아 그리드에 인형 출력
    /// </summary>
    /// <param name="plainText"></param>
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
            SingleTon.instance.damageSim.ResetSetting();
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
