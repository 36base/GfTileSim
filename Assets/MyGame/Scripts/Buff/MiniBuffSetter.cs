using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 각 타일에 보여주는 9칸의 타일버프,
/// 인형의 위치, 나머지위치, 버프를 주는위치 칼라를 인스펙터에서 초기화해야한다
/// </summary>
public class MiniBuffSetter : MonoBehaviour
{
    /// <summary>
    /// 1~9 각 타일의 위치에 알맞게 할당해줘야함
    /// </summary>
    public Image[] miniBfs;

    public Color centerColor;
    public Color nothingColor;
    public Color buffColor;

    public GameObject go;

    private void Awake()
    {
        MiniBuffOff();
    }
    private void SetCenter(int num)
    {
        if (num < 0)
            return;
        miniBfs[num].color = centerColor;
    }
    private void SetNothing(int num)
    {
        if (num < 0)
            return;
        miniBfs[num].color = nothingColor;
    }
    private void SetBuff(int num)
    {
        if (num < 0)
            return;
        miniBfs[num].color = buffColor;
    }

    public void SetMiniBuff(DollData dolldata)
    {
        if (!go.activeSelf)
            MiniBuffOn();

        for (int i = 0; i < miniBfs.Length; i++)
        {
            SetNothing(i);
        }

        SetCenter(dolldata.effect.effectCenter - 1);

        for (int i = 0; i < dolldata.effect.effectPos.Length; i++)
        {
            SetBuff(dolldata.effect.effectPos[i] - 1);
        }
    }

    public void MiniBuffOn()
    {
        go.SetActive(true);
    }
    public void MiniBuffOff()
    {
        go.SetActive(false);
    }
}
