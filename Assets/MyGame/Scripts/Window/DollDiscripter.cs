using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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
        if (target.position.x < 0)
            this.tr.position = target.position + new Vector3(
                target.position.x * 0.1f, -0.2f, 0f);
        else
            this.tr.position = target.position + new Vector3(
                target.position.x * -1f, -0.2f, 0f);
    }

    public void Show(Transform tr, Doll doll)
    {
        go.SetActive(true);
        sb.Length = 0;
        string color;
        switch(doll.dollData.rank)
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
                if (doll.dollData.id < 1000)
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
