using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileBuff : MonoBehaviour
{
    public Image buffGunType;

    public int indiBuffCount = 4;
    public int totalBuffCount = 8;

    public Buff[] indiBuffs;
    public Buff[] totalBuffs;

    public CanvasGroup cg;

    public Transform indiBuffTileTr;
    public Transform totalBuffTileTr;

    [Header("Stats")]
    public int[] totalStats = new int[15];

    private void Awake()
    {
        Init();
        ResetIndiBuff();
    }

    private void Init()
    {
        indiBuffs = new Buff[indiBuffCount];
        totalBuffs = new Buff[totalBuffCount];

        for (int i = 0; i < indiBuffCount; i++)
        {
            var go = Instantiate(SingleTon.instance.indiBuffPrefab);
            go.transform.SetParent(indiBuffTileTr, false);
            indiBuffs[i] = go.GetComponent<Buff>();
            go.SetActive(false);
        }
        for (int i = 0; i < totalBuffCount; i++)
        {
            var go = Instantiate(SingleTon.instance.buffPrefab);
            go.transform.SetParent(totalBuffTileTr, false);
            totalBuffs[i] = go.GetComponent<Buff>();
            go.SetActive(false);
        }

    }

    public void AddTotalStats(Effect.GridEffect effect, int multiple = 1)
    {
        totalStats[(int)effect.type] += effect.value * multiple;
    }

    public void ResetTotalStats()
    {
        for (int i = 0; i < totalStats.Length; i++)
        {
            totalStats[i] = 0;
        }
    }

    public void SetTotalBuff()
    {
        int index = 0;
        for (int i = 0; i < totalStats.Length; i++)
        {
            if (totalStats[i] == 0)
                continue;

            totalBuffs[index].SetBuff(SingleTon.instance.statTypeSprites[i]
                , totalStats[i]);

            totalBuffs[index].go.SetActive(true);
            index++;
        }
    }

    public void ResetTotalBuff()
    {
        for (int i = 0; i < totalBuffs.Length; i++)
        {
            totalBuffs[i].go.SetActive(false);
        }
    }

    public void AddIndiBuff(Effect effect, int multiple = 1)
    {
        buffGunType.enabled = true;
        buffGunType.sprite = SingleTon.instance.gunTypeSprites[(int)effect.effectType];

        for (int i = 0; i < effect.gridEffects.Length; i++)
        {
            indiBuffs[i].SetBuff(SingleTon.instance.statTypeSprites[(int)effect.gridEffects[i].type]
                , effect.gridEffects[i].value * multiple);

            indiBuffs[i].go.SetActive(true);
        }
    }

    public void ResetIndiBuff()
    {
        buffGunType.enabled = false;
        for (int i = 0; i < indiBuffs.Length; i++)
        {
            indiBuffs[i].go.SetActive(false);
            cg.alpha = 1f;
        }
    }
}
