using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class DamageSimToolTip : MonoBehaviour
{
    public string[] colors;
    public Transform tr;
    public Text toolTipText;
    private StringBuilder sb = new StringBuilder();

    public Camera cam;
    public Transform toolTipAxis;
    public float width = 478.5f;

    bool right;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        toolTipAxis.gameObject.SetActive(value);
    }

    public void SetTransform()
    {
        var pointerPos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (pointerPos.x > 1.5f)
        {
            if (!right)
            {
                right = true;
                var rot = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                tr.rotation = rot;
                toolTipText.transform.localRotation = rot;
            }
        }
        else
        {
            if (right)
            {
                right = false;
                var rot = Quaternion.identity;
                tr.rotation = rot;
                toolTipText.transform.localRotation = rot;
            }
        }

        tr.position = pointerPos;
        toolTipAxis.position = pointerPos;
    }

    public void SetText()
    {
        var damageSim = SingleTon.instance.damageSim;
        sb.Length = 0;
        var x = (toolTipAxis.localPosition.x + width);
        sb.Append("<i>")
            .Append(((x/damageSim.xRatio)/30f).ToString("F1"))
            .Append("초</i>\n");
        for (int i = 0; i < damageSim.targetDolls.Length; i++)
        {
            if (damageSim.targetDolls[i].id == -1 || !damageSim.lines[i].gameObject.activeSelf)
            {
                continue;
            }

            var damage =Find_yValue(i,x);
            damage = damage / damageSim.yRatio;
            sb.Append("<Color=")
                .Append(colors[i])
                .Append(">")
                .Append(damageSim.targetDolls[i].krName)
                .Append(" : ")
                .Append((int)damage)
                .Append("</Color>\n");
        }



        toolTipText.text = sb.ToString();
    }

    private float Find_yValue(int index, float xValue)
    {
        var value = 0f;
        for (int i = 0; i < SingleTon.instance.damageSim.lines[index].Points.Length; i++)
        {
            if (SingleTon.instance.damageSim.lines[index].Points[i].x < xValue)
                value = SingleTon.instance.damageSim.lines[index].Points[i].y;
            else
                return value;
        }
        return value;
    }
}
