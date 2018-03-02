using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ListElement : MonoBehaviour, IPointerDownHandler
{
    public Doll doll;
    public Transform tr;
    public GameObject go;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("hi");
    }
}
