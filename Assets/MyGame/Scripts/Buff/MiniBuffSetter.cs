using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniBuffSetter : MonoBehaviour
{
    public Image[] miniBfs;

    public Color centerColor;
    public Color nothingColor;
    public Color buffColor;

    public GameObject go;

    private void Start()
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
