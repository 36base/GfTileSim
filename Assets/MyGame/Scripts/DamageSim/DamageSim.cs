using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

[System.Serializable]
public class LegendToggle
{
    public Image pic;
    public Toggle toggle;
    public Text text;

    public void Set(Sprite sprite, string text)
    {
        pic.sprite = sprite;
        this.text.text = text;
    }
}

public class DollSetting
{
    public DollStat equipmentStat = new DollStat();
    public bool slug = false; // 하 슬러그탄... 때문에 늘림 ㅅㅂ
    public DollFavor favor = DollFavor.Normal;
    public int level = 100;
}

[System.Serializable]
public class Enemy
{
    public int dodge = 0;
    public int armor = 0;
}

public class DamageSim : UIBackBtnHandle
{
    private StatMaker statMaker = new StatMaker();

    public LegendToggle[] legends;
    public Slider sliderX;
    public Slider sliderY;

    public Enemy enemy;

    public DollSetting[] dollSettings = new DollSetting[5];
    public DollData[] targetDolls = new DollData[5];
    //private int[] nextAttackFrames = new int[5];
    private List<int>[] damageLists = new List<int>[5];

    public UILineRenderer[] lines;
    public int endOfLineX = 960;
    public int endOfLineY = 550;

    public float yRatio = 0.1f;
    public float xRatio = 3f;

    bool isAnimGraph;
    public bool nightOperation;
    [Space]
    public DamageSimSetting setting;
    public DamageSimToolTip toolTip;

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
            dollSettings[i] = new DollSetting();
        }
    }

    public void OnChangeSlider()
    {
        xRatio = sliderX.value;
        yRatio = sliderY.value;
        MakeGraph(false);
    }
    public void OnChangeLegendToggle(int index)
    {
        if (legends[index].toggle.isOn)
        {
            lines[index].gameObject.SetActive(true);
            legends[index].pic.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            lines[index].gameObject.SetActive(false);
            legends[index].pic.color = new Color(1f, 1f, 1f, 0.5f);
        }
        if (toolTip.gameObject.activeSelf)
            toolTip.SetText();
    }


    protected override void Update()
    {
        base.Update();
        if (isAnimGraph)
        {
            currTime += Time.deltaTime;
            if (currTime > 2f)
            {
                isAnimGraph = false;
                if (toolTip.gameObject.activeSelf)
                    toolTip.SetText();
            }

            if (currTime > 1f)
                AnimGraph();
        }
    }
    private void OnDisable()
    {
        setting.ResetAllSettings();
        for (int i = 0; i < dollSettings.Length; i++)
        {
            dollSettings[i].slug = false;
            dollSettings[i].equipmentStat.ResetAllStat();
        }
    }
    public override void Open()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        base.Open();
        SetTargetDolls();
        MakeGraph(true);
        SetLegend();
        //인형 List와 같이 켤수 없게 함.
        SingleTon.instance.listTop.SetActive(false);
    }
    public override void Close()
    {
        base.Close();
        setting.Close();
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

                lines[i].Points[j].y = Mathf.Lerp(lines[i].Points[j].y, y * yRatio, currTime - 1f / 1f);
            }
            lines[i].SetAllDirty();
        }
    }
    private void MakeGraphDirectly()
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

                lines[i].Points[j].y = y * yRatio;
            }
            lines[i].SetAllDirty();
        }
    }

    private void SetLegend()
    {
        for (int i = 0; i < targetDolls.Length; i++)
        {
            if (targetDolls[i].id == -1)
            {
                legends[i].toggle.gameObject.SetActive(false);
                continue;
            }
            legends[i].toggle.gameObject.SetActive(true);
            legends[i].toggle.isOn = true;
            legends[i].Set(SingleTon.instance.mgr.dollDict[targetDolls[i].id].profilePic, targetDolls[i].krName);
        }
    }

    /// <summary>
    /// 타일에 있는 인형들 초기 설정.
    /// </summary>
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

                statMaker.CalcStat(tiles[i].doll.dollData, targetDolls[index], dollSettings[index].level, dollSettings[index].favor);
                statMaker.CalcStatPlusBuff(targetDolls[index], tiles[i].tileBuff);

                //nextAttackFrames[index] = CalcFrame(index, targetDolls[index].stat.rate);
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
            //nextAttackFrames[i] = -1;
        }
    }

    private int CalcFrame(int index, int rate)
    {
        if (targetDolls[index].id == 208)
        {
            return 9;
        }
        else if (targetDolls[index].type == DollType.MG)
        {
            if (targetDolls[index].id == 77 || targetDolls[index].id == 85 || targetDolls[index].id == 109 || targetDolls[index].id == 173)
            {
                return 11;
            }
            return 10;
        }
        else if (targetDolls[index].type == DollType.SG)
        {
            if (rate > 60)
                rate = 60;
        }

        if (rate > 116)
            rate = 116;
        return (int)Mathf.Ceil((1500f / rate) - 1);
    }

    /// <summary>
    /// 그래프의 X좌표값 입력, Y좌표값은 Shoot함수를 통해 리스트에 저장, Update에서 애니메이션으로 출력.
    /// </summary>
    public void MakeGraph(bool anim)
    {
        ResetDamageList();
        for (int i = 0; i < lines.Length; i++)
        {
            ResetLine(i);

            if (targetDolls[i].id != -1)
            {
                var x = 0f;
                //레벨/호감도로 계산된 인형스텟 + 장비스텟 더해요
                var bullet = targetDolls[i].stat.bullet + dollSettings[i].equipmentStat.bullet;
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
                        x += CalcFrame(i, targetDolls[i].stat.rate + dollSettings[i].equipmentStat.rate) * xRatio;
                        if (j < 2)
                            Shoot(i, 0);
                        else
                        {
                            Shoot(i, CalcDamage(i));
                            //샷건/망가 탄약계산 및 전탄소비시 재장전시간 추가.
                            if (targetDolls[i].type == DollType.MG || targetDolls[i].type == DollType.SG)
                            {
                                bullet--;
                                if (bullet == 0)
                                {
                                    x += CalcReloadFrame(i) * xRatio;
                                    bullet = targetDolls[i].stat.bullet + dollSettings[i].equipmentStat.bullet;
                                }
                            }
                        }
                    }

                }
            }
        }
        if (anim)
        {
            isAnimGraph = true;
            currTime = 0f;
        }
        else
        {
            MakeGraphDirectly();
            if (toolTip.gameObject.activeSelf)
                toolTip.SetText();
        }
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
    private float CalcReloadFrame(int index)
    {
        if (targetDolls[index].type == DollType.SG)
        {
            return (1.5f + (0.5f * targetDolls[index].stat.bullet + dollSettings[index].equipmentStat.bullet)) * 29.999994f;
        }
        else if (targetDolls[index].type == DollType.MG)
        {
            return (4 + (200 / targetDolls[index].stat.rate + dollSettings[index].equipmentStat.rate)) * 29.999994f;
        }
        return 0f;
    }

    /// <summary>
    /// 단순 치명률만 계산한 값, 야간전/장갑 고려X, 치명타데미지고려x
    /// </summary>
    private int CalcDamage(int index, int link = 5)
    {
        if (targetDolls[index] == null)
            return 0;

        int value;
        int armorDamage = targetDolls[index].stat.armorPiercing - enemy.armor;
        armorDamage = Mathf.Min(armorDamage, 2);

        value = Mathf.Max(targetDolls[index].stat.pow + dollSettings[index].equipmentStat.pow + armorDamage,1);


        var crit = Mathf.Clamp(
            (float)(targetDolls[index].stat.crit + dollSettings[index].equipmentStat.crit) / 100f, 0f, 1f);
        var critDmg = value * (1.5f
            + (float)(targetDolls[index].stat.critDmg + dollSettings[index].equipmentStat.critDmg) / 100f);

        var hit = targetDolls[index].stat.hit / (float)(targetDolls[index].stat.hit + enemy.dodge);

        if (dollSettings[index].slug)
            return (int)(Mathf.Ceil(value * (1 - crit) + critDmg * crit) * link * hit * 3) ;
        else
            return (int)(Mathf.Ceil(value * (1 - crit) + critDmg * crit) * link * hit);
    }
}
