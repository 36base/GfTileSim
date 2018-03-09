using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DollPreset : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject deletePanel;
    public bool pressed;

    public float longTouchTime = 1.5f;
    private float currTime;

    private void Update()
    {
        if (pressed)
        {
            currTime += Time.deltaTime;
            if (currTime > longTouchTime)
            {
                deletePanel.SetActive(true);
            }
        }
        else
        {
            currTime = 0f;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
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
    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }
}
