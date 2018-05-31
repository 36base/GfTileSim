using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class DamageSim : MonoBehaviour
{
    private StatMaker statMaker = new StatMaker();

    public DollData[] targetDolls = new DollData[5];
    private int[] nextAttackFrames = new int[5];
    private List<int>[] damageLists = new List<int>[5];

    public UILineRenderer[] lines;
    public int endOfLineX = 960;
    public int endOfLineY = 550;

    public float yRatio = 0.1f;
    public float xRatio = 3f;

    bool isAnimGraph;


    private void Awake()
    {
        for (int i = 0; i < targetDolls.Length; i++)
        {
            if (targetDolls[i] == null)
            {
                targetDolls[i] = new DollData();
                targetDolls[i].stat = new DollStat();
            }
        }
        for (int i = 0; i < 5; i++)
        {
            damageLists[i] = new List<int>();
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SetTargetDolls();
            MakeGraph();
        }

        if (isAnimGraph)
        {
            currTime += Time.deltaTime;
            if (currTime > 1f)
                isAnimGraph = false;

            AnimGraph();
        }
    }

    private float currTime = 0f;
    private void AnimGraph()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            var y = 0f;
            var index = 0;
            for (int j = 0; j < lines[i].Points.Length; j++)
            {
                if (targetDolls[i].id == -1)
                    continue;

                if (j % 2 == 1 && index < damageLists[i].Count)
                {
                    y += damageLists[i][index];
                    index++;
                }

                lines[i].Points[j].y = Mathf.Lerp(lines[i].Points[j].y, y * yRatio, currTime / 1f);
                lines[i].SetAllDirty();
            }
        }
    }

    public void SetTargetDolls()
    {
        var tiles = SingleTon.instance.grid.tiles;
        int index = 0;
        ResetTargetDolls();

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].doll != null)
            {
                targetDolls[index].id = tiles[i].doll.dollData.id;
                targetDolls[index].krName = tiles[i].doll.dollData.krName;
                targetDolls[index].rank = tiles[i].doll.dollData.rank;
                targetDolls[index].type = tiles[i].doll.dollData.type;
                targetDolls[index].grow = tiles[i].doll.dollData.grow;

                statMaker.CalcStat(tiles[i].doll.dollData, targetDolls[index]);
                statMaker.CalcStatPlusBuff(targetDolls[index], tiles[i].tileBuff);

                SetFrame(index, targetDolls[index].stat.rate);
                index++;
            }
        }
    }
    private void ResetTargetDolls()
    {
        for (int i = 0; i < targetDolls.Length; i++)
        {
            if (targetDolls[i] != null)
            {
                targetDolls[i].id = -1;
            }
            nextAttackFrames[i] = -1;
        }
    }
    private void SetFrame(int index, int rate)
    {
        nextAttackFrames[index] = (int)Mathf.Ceil((1500f / rate) - 1);
    }

    public void MakeGraph()
    {
        ResetDamageList();
        for (int i = 0; i < lines.Length; i++)
        {
            ResetLine(i);

            if(targetDolls[i].id != -1)
            {
                var x = 0;
                for (int j = 0; j < lines[i].Points.Length; j++)
                {
                    if (x > endOfLineX)
                    {
                        lines[i].Points[j].x = endOfLineX;
                        continue;
                    }

                    lines[i].Points[j].x = x;

                    if (j % 2 == 1)
                    {
                        x += (int)(nextAttackFrames[i] * xRatio);
                        if (j < 2)
                            Shoot(i, 0);
                        else
                            Shoot(i, CalcDamage(i));
                    }

                }
            }
        }
        isAnimGraph = true;
        currTime = 0f;
    }
    private void ResetLine(int index)
    {
        for (int i = 0; i < lines[index].Points.Length; i++)
        {
            lines[index].Points[i] = Vector2.zero;
        }
    }
    private void ResetDamageList()
    {
        for (int i = 0; i < damageLists.Length; i++)
        {
            damageLists[i].Clear();
        }
    }
    private void Shoot(int index, int damage)
    {
        damageLists[index].Add(damage);
    }

    /// <summary>
    /// 단순 치명률만 계산한 값, 야간전/장갑 고려X, 치명타데미지고려x
    /// </summary>
    private int CalcDamage(int index)
    {
        if (targetDolls[index] == null)
            return 0;

        int value;

        value = targetDolls[index].stat.pow + 2;


        var crit = (float)targetDolls[index].stat.crit / 100f;
        var critDmg = value * (1.5f + crit);

        return (int)Mathf.Ceil(value * (1 - crit) + critDmg * crit);
    }
}
