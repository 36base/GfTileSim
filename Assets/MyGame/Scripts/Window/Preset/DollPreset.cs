using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class DollPreset : MonoBehaviour
{
    public GameObject presetPanel;
    public GameObject savePanel;
    public GameObject deletePanel;
    public int presetId;

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
        for(int i = 0; i < 5; i++)
        {
            int id = SingleTon.instance.dollSelecter.selects[i].dollNum;
            int pos = SingleTon.instance.dollSelecter.selects[i].gridPos;
            plainText += "ID" + id + "Pos" + pos;
        }

        string cipherText = DataManager.Encrypt(plainText);

        FileStream file;
        if(File.Exists(Application.persistentDataPath + "/PresetInfo.dat"))
        {
            file = File.Open(Application.persistentDataPath + "/PresetInfo.dat", FileMode.Open);
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/PresetInfo.dat");
        }
    
        StreamReader streamReader = new StreamReader(file);
        StreamWriter streamWriter = new StreamWriter(file);
        streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

        for (int i = 0; i < presetId; i++)
        {
            string lineRead = streamReader.ReadLine();
            if (lineRead == null)
            {
                streamWriter.WriteLine();
            }
        }

        streamWriter.WriteLine(cipherText);
        streamWriter.Close();
        streamReader.Close();
        file.Close();

        savePanel.SetActive(false);
    }

    public void LoadPreset()
    {
        string cipherText = "";

        FileStream file = File.Open(Application.persistentDataPath + "/PresetInfo.dat", FileMode.Open);
        StreamReader streamReader = new StreamReader(file);

        for (int i = 0; i < presetId; i++)
        {
            streamReader.ReadLine();
        }

        cipherText = streamReader.ReadLine();

        string plainText = DataManager.Decrypt(cipherText);

        streamReader.Close();
        file.Close();

        //여기에 제대 불러오기 후 Spawn하는 함수를 만들어주세욤

        presetPanel.SetActive(false);
    }

    public void DeletePreset()
    {
        deletePanel.SetActive(false);
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
