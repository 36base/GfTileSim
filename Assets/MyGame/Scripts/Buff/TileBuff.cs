using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 각 타일에 프리팹화된 버프클래스 오브젝트들을 들고 관리하는 클래스
/// </summary>
public class TileBuff : MonoBehaviour
{
    /// <summary>
    /// 버프를 주는 타겟의 Type종류
    /// </summary>
    public Image buffGunType;

    //만약 인형 개별/총합 버프가 해당 개수보다 많으면 늘려줘야한다
    public int indiBuffCount = 4;
    public int totalBuffCount = 8;

    public Buff[] indiBuffs;
    public Buff[] totalBuffs;

    public CanvasGroup cg;

    //초기화 시 프리팹들을 위치시킬 트랜스폼
    public Transform indiBuffTileTr;
    public Transform totalBuffTileTr;

    /// <summary>
    /// 스탯의 배열 순서는 Stat Enum의 순서를 따름
    /// totalStat의 배열에 정해진 값대로 총합버프의 수치가 정해짐
    /// </summary>
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

    /// <summary>
    /// totalStats배열에 접근, Stat Enum의 순서에따라 배열에 값 추가할당
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="multiple">기본값 1, 권총의 경우 2</param>
    public void AddTotalStats(Effect.GridEffect effect, int multiple = 1)
    {
        totalStats[(int)effect.type] += effect.value * multiple;
    }
    /// <summary>
    /// totalStas배열 초기화
    /// </summary>
    public void ResetTotalStats()
    {
        for (int i = 0; i < totalStats.Length; i++)
        {
            totalStats[i] = 0;
        }
    }
    /// <summary>
    /// totalStats 순회하면서 값이 0이 아닌경우 버프오브젝트 하나씩 활성화
    /// </summary>
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

    /// <summary>
    /// 인형 개별버프 표시
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="multiple"></param>
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
