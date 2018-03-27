using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
