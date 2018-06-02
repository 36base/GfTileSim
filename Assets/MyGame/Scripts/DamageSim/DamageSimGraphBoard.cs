using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DamageSimGraphBoard : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    public DamageSimToolTip toolTip;

    public Toggle holdToolTip;

    public void OnPointerDown(PointerEventData eventData)
    {
        toolTip.SetTransform();
        toolTip.SetText();
        toolTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!holdToolTip.isOn)
            toolTip.SetActive(false);
    }

    private void OnDisable()
    {
        toolTip.SetActive(false);
    }
}
