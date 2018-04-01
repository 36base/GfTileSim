using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Image, Text컴포넌트와 함께 사용.
/// 이미지 스프라이트의경우 싱글턴에서 참조, 버프의 스프라이트와 값을 지정해준다.
/// </summary>
[RequireComponent(typeof(Text))]
public class Buff : MonoBehaviour
{
    public Image buffImage;
    public Text buffText;

    public GameObject go;

    public void SetBuff(Sprite sprite, int value)
    {
        buffImage.sprite = sprite;
        buffText.text = value.ToString() + "%";
    }
}
