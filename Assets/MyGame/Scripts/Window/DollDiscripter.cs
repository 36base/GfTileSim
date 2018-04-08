using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인형 리스트에서 포인터 터치 시 표현되는 정보
/// </summary>
public class DollDiscripter : MonoBehaviour
{
    public Text text;
    public Transform tr;
    public GameObject go;

    private StringBuilder sb;
    private Transform target;

    private void Awake()
    {
        sb = new StringBuilder();
    }

    private void Update()
    {
        // 리스트 중앙을 기준으로 왼쪽, 오른쪽 표시되는 위치 조절
        // 모바일기준 유저 손가락 위치 가림 대비
        if (target.position.x < 0)
            this.tr.position = target.position + new Vector3(
                target.position.x * 0.1f, -0.2f, 0f);
        else
            this.tr.position = target.position + new Vector3(
                target.position.x * -1f, -0.2f, 0f);
    }

    /// <summary>
    /// 정보 표시
    /// </summary>
    /// <param name="tr">표시될 위치 트랜스폼</param>
    /// <param name="doll">표시될 인형 정보</param>
    public void Show(Transform tr, Doll doll)
    {
        go.SetActive(true);
        sb.Length = 0;
        string color;
        switch (doll.dollData.rank)
        {
            case 2:
                color = "#DCEBFF";
                break;
            case 3:
                color = "#A7FAEB";
                break;
            case 4:
                color = "#8AE634";
                break;
            case 5:
                if ((doll.dollData.id < 1000) || (doll.dollData.id > 9999))
                    color = "#FFD73C";
                else
                    color = "#FF98A3";
                break;
            default:
                color = "white";
                break;
        }

        sb.Append("<Color=");
        sb.Append(color);
        sb.Append(">");
        sb.Append(doll.dollData.krName);
        sb.Append("</color>");
        sb.Append("\n");
        sb.Append(doll.dollData.rank);
        sb.Append("성 ");
        sb.Append(doll.dollData.type);
        text.text = sb.ToString();

        target = tr;
    }

    public void Off()
    {
        go.SetActive(false);
    }
}
