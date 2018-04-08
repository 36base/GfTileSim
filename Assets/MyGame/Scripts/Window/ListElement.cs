using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 인형 리스트 스크롤뷰에 사용하는 엘리먼트들, 포인터 다운 시 DollDiscripter 보여줌
/// </summary>
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
