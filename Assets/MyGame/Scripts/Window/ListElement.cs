using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ListElement : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    public Doll doll;
    public Transform tr;
    public GameObject go;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        SingleTon.instance.dollDiscript.Show(tr, doll);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        SingleTon.instance.dollDiscript.Off();
    }
}
