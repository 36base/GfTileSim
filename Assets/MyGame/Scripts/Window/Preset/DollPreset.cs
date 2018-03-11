using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DollPreset : MonoBehaviour
{
    public GameObject presetPanel;
    public GameObject savePanel;
    public GameObject deletePanel;

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

    public void Preset2Grid()
    {

    }

    public void Grid2Preset()
    {

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
