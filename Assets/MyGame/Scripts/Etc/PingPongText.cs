using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 로드 중 텍스트 칼라 변경
/// </summary>
public class PingPongText : MonoBehaviour
{
    public Text loadText;

    private void Update()
    {
        loadText.color
            = new Color(loadText.color.r, loadText.color.g, loadText.color.b, Mathf.PingPong(Time.time, 1));
    }

}
