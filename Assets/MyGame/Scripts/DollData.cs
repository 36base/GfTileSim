using System.Collections.Generic;
using System;
using UnityEngine;

public enum DollType
{
    All = 0,
    HG = 1,
    SMG = 2,
    RF = 3,
    AR = 4,
    MG = 5,
    SG = 6
}

public enum Stats
{
    armor,
    dodge,
    hit,
    hp,
    pow,
    range,
    rate,
    shield,
    speed,
    crit,
    critDmg,
    armorPiercing,
    nightView,
    coolDown,
    bullet
}

[Serializable]
public class DollStat
{
    public int armor;
    public int dodge;
    public int hit;
    public int hp;
    public int pow;
    public int range;
    public int rate;
    public int shield;
    public int speed;
    public int crit;
    public int critDmg;
    public int armorPiercing;
    public int nightView;
    public int coolDown;
    public int bullet;


    public DollStat()
    {
        armor = 0;
        dodge = 0;
        hit = 0;
        hp = 0;
        pow = 0;
        range = 0;
        rate = 0;
        shield = 0;
        speed = 0;
        crit = 0;
        critDmg = 0;
        armorPiercing = 0;
        nightView = 0;
        coolDown = 0;
        bullet = 0;
    }
    public DollStat(DollStat copyStat)
    {
        armor = copyStat.armor;
        dodge = copyStat.dodge;
        hit = copyStat.hit;
        hp = copyStat.hp;
        pow = copyStat.pow;
        range = copyStat.range;
        rate = copyStat.rate;
        shield = copyStat.shield;
        speed = copyStat.speed;
        crit = copyStat.crit;
        critDmg = copyStat.critDmg;
        armorPiercing = copyStat.armorPiercing;
        nightView = copyStat.nightView;
        coolDown = copyStat.coolDown;
        bullet = copyStat.bullet;
    }

    public void SetAllStat(DollStat stat)
    {
        armor = stat.armor;
        dodge = stat.dodge;
        hit = stat.hit;
        hp = stat.hp;
        pow = stat.pow;
        range = stat.range;
        rate = stat.rate;
        shield = stat.shield;
        speed = stat.speed;
        crit = stat.crit;
        critDmg = stat.critDmg;
        armorPiercing = stat.armorPiercing;
        nightView = stat.nightView;
        coolDown = stat.coolDown;
        bullet = stat.bullet;
    }
    public void AddAllStat(DollStat stat)
    {
        armor += stat.armor;
        dodge += stat.dodge;
        hit += stat.hit;
        hp += stat.hp;
        pow += stat.pow;
        range += stat.range;
        rate += stat.rate;
        shield += stat.shield;
        speed += stat.speed;
        crit += stat.crit;
        critDmg += stat.critDmg;
        armorPiercing += stat.armorPiercing;
        nightView += stat.nightView;
        coolDown += stat.coolDown;
        bullet += stat.bullet;
    }

    public void SetStat(string stat, int value)
    {
        switch (stat)
        {
            case "armor":
                armor = value;
                break;
            case "dodge":
                dodge = value;
                break;
            case "hit":
                hit = value;
                break;
            case "hp":
                hp = value;
                break;
            case "pow":
                pow = value;
                break;
            case "range":
                range = value;
                break;
            case "rate":
                rate = value;
                break;
            case "shield":
                shield = value;
                break;
            case "speed":
                speed = value;
                break;
            case "crit":
                crit = value;
                break;
            case "critDmg":
                critDmg = value;
                break;
            case "armorPiercing":
                armorPiercing = value;
                break;
            case "nightView":
                nightView = value;
                break;
            case "coolDown":
                coolDown = value;
                break;
            case "bullet":
                bullet = value;
                break;
        }
    }

    public void SetStat(Stats stat, int value)
    {
        switch (stat)
        {
            case Stats.armor:
                armor = value;
                break;
            case Stats.dodge:
                dodge = value;
                break;
            case Stats.hit:
                hit = value;
                break;
            case Stats.hp:
                hp = value;
                break;
            case Stats.pow:
                pow = value;
                break;
            case Stats.range:
                range = value;
                break;
            case Stats.rate:
                rate = value;
                break;
            case Stats.shield:
                shield = value;
                break;
            case Stats.speed:
                speed = value;
                break;
            case Stats.crit:
                crit = value;
                break;
            case Stats.critDmg:
                critDmg = value;
                break;
            case Stats.armorPiercing:
                armorPiercing = value;
                break;
            case Stats.nightView:
                nightView = value;
                break;
            case Stats.coolDown:
                coolDown = value;
                break;
            case Stats.bullet:
                bullet = value;
                break;
        }
    }

    public void AddStat(Stats stat, int value)
    {
        switch (stat)
        {
            case Stats.armor:
                armor += value;
                break;
            case Stats.dodge:
                dodge += value;
                break;
            case Stats.hit:
                hit += value;
                break;
            case Stats.hp:
                hp += value;
                break;
            case Stats.pow:
                pow += value;
                break;
            case Stats.range:
                range += value;
                break;
            case Stats.rate:
                rate += value;
                break;
            case Stats.shield:
                shield += value;
                break;
            case Stats.speed:
                speed += value;
                break;
            case Stats.crit:
                crit += value;
                break;
            case Stats.critDmg:
                critDmg += value;
                break;
            case Stats.armorPiercing:
                armorPiercing += value;
                break;
            case Stats.nightView:
                nightView += value;
                break;
            case Stats.coolDown:
                coolDown += value;
                break;
            case Stats.bullet:
                bullet += value;
                break;
        }
    }

    public int GetStat(Stats stat)
    {
        switch (stat)
        {
            case Stats.armor:
                return armor;
            case Stats.dodge:
                return dodge;
            case Stats.hit:
                return hit;
            case Stats.hp:
                return hp;
            case Stats.pow:
                return pow;
            case Stats.range:
                return range;
            case Stats.rate:
                return rate;
            case Stats.shield:
                return shield;
            case Stats.speed:
                return speed;
            case Stats.crit:
                return crit;
            case Stats.critDmg:
                return critDmg;
            case Stats.armorPiercing:
                return armorPiercing;
            case Stats.nightView:
                return nightView;
            case Stats.coolDown:
                return coolDown;
            case Stats.bullet:
                return bullet;
            default:
                return 0;
        }
    }

    public void ResetAllStat()
    {
        armor = 0;
        dodge = 0;
        hit = 0;
        hp = 0;
        pow = 0;
        range = 0;
        rate = 0;
        shield = 0;
        speed = 0;
        crit = 0;
        critDmg = 0;
        armorPiercing = 0;
        nightView = 0;
        coolDown = 0;
        bullet = 0;
    }
}

/// <summary>
/// 인형 진형버프 클래스
/// </summary>
[System.Serializable]
public class Effect
{
    public struct GridEffect
    {
        public Stats type;
        public int value;

        public GridEffect(string type, int value)
        {
            switch (type)
            {
                case "armor":
                    this.type = Stats.armor;
                    break;
                case "dodge":
                    this.type = Stats.dodge;
                    break;
                case "hit":
                    this.type = Stats.hit;
                    break;
                case "hp":
                    this.type = Stats.hp;
                    break;
                case "pow":
                    this.type = Stats.pow;
                    break;
                case "range":
                    this.type = Stats.range;
                    break;
                case "rate":
                    this.type = Stats.rate;
                    break;
                case "shield":
                    this.type = Stats.shield;
                    break;
                case "speed":
                    this.type = Stats.speed;
                    break;
                case "crit":
                    this.type = Stats.crit;
                    break;
                case "critDmg":
                    this.type = Stats.critDmg;
                    break;
                case "armorPiercing":
                    this.type = Stats.armorPiercing;
                    break;
                case "nightView":
                    this.type = Stats.nightView;
                    break;
                case "cooldown":
                    this.type = Stats.coolDown;
                    break;
                case "bullet":
                    this.type = Stats.bullet;
                    break;
                default:
                    this.type = Stats.armor;
                    Debug.Log("Error Stat type");
                    break;
            }

            //this.type = (Stats)Enum.Parse(typeof(Stats), type);
            this.value = value;
        }
        public GridEffect(Stats type, int value)
        {
            this.type = type;

            this.value = value;
        }
    }
    /// <summary>
    /// 버프를 줄 인형 타입
    /// </summary>
    public DollType effectType;
    /// <summary>
    /// 버프를 주는 인형의 센터 정보
    /// </summary>
    public int effectCenter;
    /// <summary>
    /// 센터를 기준으로 버프를 줄 인형들의 위치
    /// </summary>
    public int[] effectPos;
    /// <summary>
    /// 버프를 줄 인형들에게 주는 버프들의 수치값 배열
    /// </summary>
    public GridEffect[] gridEffects;

}

/// <summary>
/// 인형 데이터 클래스 Json 데이터 참고.
/// </summary>
[System.Serializable]
public class DollData
{
    public int id;
    public string name;
    public string krName;
    public int rank;
    public DollType type;
    public Effect effect;
    public DollStat stat;
    public int grow;
}

public enum DollFavor
{
    Worst,  // < 10
    Normal, // < 90
    Like,   // < 140
    Love,   // < 190
    MadLove,// >= 190
}

public enum DollEquip
{
    Scope = 0,
    Holo,
    Reddot,
    Nightvision,
    APBullet,
    HPBullet,
    SGBullet_B,
    SGBullet_S,
    HVBullet = 8,
    //Chip,
    //Skeleton,
    //Armor,
    Silencer,
    Ammobox,
    Suit
    //Special = 15
}

public class StatMaker
{
    public class StatConst
    {
        public Stats stat;
        public float value1;
        public float? value2;

        public StatConst(Stats stat, float value)
        {
            this.stat = stat;
            this.value1 = value;
            this.value2 = null;
        }
        public StatConst(Stats stat, float value1, float value2)
        {
            this.stat = stat;
            this.value1 = value1;
            this.value2 = value2;
        }
    }
    public class DollConst
    {
        public DollType type;
        public StatConst[] statConsts;

        public float GetAttribute(Stats stat)
        {
            for (int i = 0; i < statConsts.Length; i++)
            {
                if (stat == statConsts[i].stat)
                    return statConsts[i].value1;
            }
            return 0f;
        }
    }

    /// <summary>
    /// 인형의 병종에따라 기본스텟에 계산되는 상수값.
    /// enum DollType 순서대로 1(HG)부터 시작, 0(ALL)은 없음.
    /// </summary>
    public readonly DollConst[] dollAttributes;

    public readonly StatConst[] statConstNormalBasic;
    public readonly StatConst[] statConstNormalGrow;

    public readonly StatConst[] statConstAfter100Basic;
    public readonly StatConst[] statConstAfter100Grow;

    /// <summary>
    /// 36Base dollAttribute.json/ dollGrow.json참고바랍니다.
    /// </summary>
    public StatMaker()
    {
        dollAttributes = new DollConst[7];

        dollAttributes[0] = null;

        var hg = new DollConst();
        hg.type = DollType.HG;
        hg.statConsts = new StatConst[6];
        hg.statConsts[0] = new StatConst(Stats.hp, 0.6f);
        hg.statConsts[1] = new StatConst(Stats.pow, 0.6f);
        hg.statConsts[2] = new StatConst(Stats.rate, 0.8f);
        hg.statConsts[3] = new StatConst(Stats.speed, 1.5f);
        hg.statConsts[4] = new StatConst(Stats.hit, 1.2f);
        hg.statConsts[5] = new StatConst(Stats.dodge, 1.8f);
        dollAttributes[1] = hg;

        var smg = new DollConst();
        smg.type = DollType.SMG;
        smg.statConsts = new StatConst[6];
        smg.statConsts[0] = new StatConst(Stats.hp, 1.6f);
        smg.statConsts[1] = new StatConst(Stats.pow, 0.6f);
        smg.statConsts[2] = new StatConst(Stats.rate, 1.2f);
        smg.statConsts[3] = new StatConst(Stats.speed, 1.2f);
        smg.statConsts[4] = new StatConst(Stats.hit, 0.3f);
        smg.statConsts[5] = new StatConst(Stats.dodge, 1.6f);
        dollAttributes[2] = smg;

        var rf = new DollConst();
        rf.type = DollType.RF;
        rf.statConsts = new StatConst[6];
        rf.statConsts[0] = new StatConst(Stats.hp, 0.8f);
        rf.statConsts[1] = new StatConst(Stats.pow, 2.4f);
        rf.statConsts[2] = new StatConst(Stats.rate, 0.5f);
        rf.statConsts[3] = new StatConst(Stats.speed, 0.7f);
        rf.statConsts[4] = new StatConst(Stats.hit, 1.6f);
        rf.statConsts[5] = new StatConst(Stats.dodge, 0.8f);
        dollAttributes[3] = rf;

        var ar = new DollConst();
        ar.type = DollType.AR;
        ar.statConsts = new StatConst[6];
        ar.statConsts[0] = new StatConst(Stats.hp, 1f);
        ar.statConsts[1] = new StatConst(Stats.pow, 1f);
        ar.statConsts[2] = new StatConst(Stats.rate, 1f);
        ar.statConsts[3] = new StatConst(Stats.speed, 1f);
        ar.statConsts[4] = new StatConst(Stats.hit, 1f);
        ar.statConsts[5] = new StatConst(Stats.dodge, 1f);
        dollAttributes[4] = ar;

        var mg = new DollConst();
        mg.type = DollType.MG;
        mg.statConsts = new StatConst[6];
        mg.statConsts[0] = new StatConst(Stats.hp, 1.5f);
        mg.statConsts[1] = new StatConst(Stats.pow, 1.8f);
        mg.statConsts[2] = new StatConst(Stats.rate, 1.6f);
        mg.statConsts[3] = new StatConst(Stats.speed, 0.4f);
        mg.statConsts[4] = new StatConst(Stats.hit, 0.6f);
        mg.statConsts[5] = new StatConst(Stats.dodge, 0.6f);
        dollAttributes[5] = mg;

        var sg = new DollConst();
        sg.type = DollType.SG;
        sg.statConsts = new StatConst[7];
        sg.statConsts[0] = new StatConst(Stats.hp, 2.0f);
        sg.statConsts[1] = new StatConst(Stats.pow, 0.7f);
        sg.statConsts[2] = new StatConst(Stats.rate, 0.4f);
        sg.statConsts[3] = new StatConst(Stats.speed, 0.6f);
        sg.statConsts[4] = new StatConst(Stats.hit, 0.3f);
        sg.statConsts[5] = new StatConst(Stats.dodge, 0.3f);
        sg.statConsts[6] = new StatConst(Stats.armor, 1f);
        dollAttributes[6] = sg;

        statConstNormalBasic = new StatConst[7];
        statConstNormalBasic[0] = new StatConst(Stats.armor, 2f, 0.161f);
        statConstNormalBasic[1] = new StatConst(Stats.dodge, 5f);
        statConstNormalBasic[2] = new StatConst(Stats.hit, 5f);
        statConstNormalBasic[3] = new StatConst(Stats.hp, 55f, 0.555f);
        statConstNormalBasic[4] = new StatConst(Stats.pow, 16f);
        statConstNormalBasic[5] = new StatConst(Stats.rate, 45f);
        statConstNormalBasic[6] = new StatConst(Stats.speed, 10f);

        statConstNormalGrow = new StatConst[4];
        statConstNormalGrow[0] = new StatConst(Stats.dodge, 0.303f, 0f);
        statConstNormalGrow[1] = new StatConst(Stats.hit, 0.303f, 0f);
        statConstNormalGrow[2] = new StatConst(Stats.pow, 0.242f, 0f);
        statConstNormalGrow[3] = new StatConst(Stats.rate, 0.181f, 0f);

        statConstAfter100Basic = new StatConst[2];
        statConstAfter100Basic[0] = new StatConst(Stats.armor, 13.979f, 0.04f);
        statConstAfter100Basic[1] = new StatConst(Stats.hp, 96.283f, 0.138f);

        statConstAfter100Grow = new StatConst[4];
        statConstAfter100Grow[0] = new StatConst(Stats.dodge, 0.075f, 22.572f);
        statConstAfter100Grow[1] = new StatConst(Stats.hit, 0.075f, 22.572f);
        statConstAfter100Grow[2] = new StatConst(Stats.pow, 0.06f, 18.018f);
        statConstAfter100Grow[3] = new StatConst(Stats.rate, 0.022f, 15.741f);
    }

    /// <summary>
    /// 인형의 레벨, 호감도에 따른 스텟계산
    /// </summary>
    /// <param name="fromData">인형의 원본 데이터</param>
    /// <param name="toData">계산된 데이터를 담을 데이터</param>
    public void CalcStat(DollData fromData, DollData toData, int level = 100, DollFavor favor = DollFavor.Normal)
    {
        if (toData.stat == null)
        {
            toData.stat = new DollStat(fromData.stat);
        }
        else
        {
            toData.stat.SetAllStat(fromData.stat);
        }



        //기본 스텟 계산
        for (int i = 0; i < statConstNormalBasic.Length; i++)
        {
            int value = 0;
            if (statConstNormalBasic[i].value2 != null)
            {
                float value1 = statConstNormalBasic[i].value1;
                float value2 = (float)statConstNormalBasic[i].value2;
                float attribute = dollAttributes[(int)fromData.type].GetAttribute(statConstNormalBasic[i].stat);
                int dollStat = fromData.stat.GetStat(statConstNormalBasic[i].stat);

                value = CalcBasicStat(value1, value2, attribute, dollStat, level);
            }
            else
            {
                float value1 = statConstNormalBasic[i].value1;
                float attribute = dollAttributes[(int)fromData.type].GetAttribute(statConstNormalBasic[i].stat);
                int dollStat = fromData.stat.GetStat(statConstNormalBasic[i].stat);

                value = CalcBasicStat(value1, attribute, dollStat);
            }

            toData.stat.SetStat(statConstNormalBasic[i].stat, value);
        }

        if (level > 100)
        {
            for (int i = 0; i < statConstAfter100Basic.Length; i++)
            {
                int value = 0;
                if (statConstAfter100Basic[i].value2 != null)
                {
                    float value1 = statConstAfter100Basic[i].value1;
                    float value2 = (float)statConstAfter100Basic[i].value2;
                    float attribute = dollAttributes[(int)fromData.type].GetAttribute(statConstAfter100Basic[i].stat);
                    int dollStat = fromData.stat.GetStat(statConstAfter100Basic[i].stat);

                    value = CalcBasicStat(value1, value2, attribute, dollStat, level);
                }
                else
                {
                    float value1 = statConstAfter100Basic[i].value1;
                    float attribute = dollAttributes[(int)fromData.type].GetAttribute(statConstAfter100Basic[i].stat);
                    int dollStat = fromData.stat.GetStat(statConstAfter100Basic[i].stat);

                    value = CalcBasicStat(value1, attribute, dollStat);
                }

                toData.stat.SetStat(statConstAfter100Basic[i].stat, value);
            }
        }


        // 강화 스텟 계산
        if (level > 100)
        {
            for (int i = 0; i < statConstAfter100Grow.Length; i++)
            {
                int growValue = 0;
                if (statConstAfter100Grow[i].value2 != null)
                {
                    float value1 = statConstAfter100Grow[i].value1;
                    float value2 = (float)statConstAfter100Grow[i].value2;
                    float attribute = dollAttributes[(int)fromData.type].GetAttribute(statConstAfter100Grow[i].stat);
                    int dollStat = fromData.stat.GetStat(statConstAfter100Grow[i].stat);

                    growValue = CalcGrowStat(value1, value2, attribute, dollStat, level, fromData.grow);
                }
                toData.stat.AddStat(statConstAfter100Grow[i].stat, growValue);
            }
        }
        else
        {
            for (int i = 0; i < statConstNormalGrow.Length; i++)
            {
                int growValue = 0;
                if (statConstNormalGrow[i].value2 != null)
                {
                    float value1 = statConstNormalGrow[i].value1;
                    float value2 = (float)statConstNormalGrow[i].value2;
                    float attribute = dollAttributes[(int)fromData.type].GetAttribute(statConstNormalGrow[i].stat);
                    int dollStat = fromData.stat.GetStat(statConstNormalGrow[i].stat);

                    growValue = CalcGrowStat(value1, value2, attribute, dollStat, level, fromData.grow);
                }
                toData.stat.AddStat(statConstNormalGrow[i].stat, growValue);
            }
        }


        //호감도 스텟 계산
        toData.stat.AddStat(Stats.pow, (int)(Mathf.Sign(GetFavorRatio(favor)) * Mathf.Ceil(Mathf.Abs(toData.stat.GetStat(Stats.pow) * GetFavorRatio(favor)))));
        toData.stat.AddStat(Stats.hit, (int)(Mathf.Sign(GetFavorRatio(favor)) * Mathf.Ceil(Mathf.Abs(toData.stat.GetStat(Stats.pow) * GetFavorRatio(favor)))));
        toData.stat.AddStat(Stats.dodge, (int)(Mathf.Sign(GetFavorRatio(favor)) * Mathf.Ceil(Mathf.Abs(toData.stat.GetStat(Stats.pow) * GetFavorRatio(favor)))));
    }

    private int CalcBasicStat(float value1, float value2, float attribute, int dollStat, int level)
    {
        return (int)Mathf.Ceil((((value1 + ((level - 1) * value2))
                                                            * attribute)
                                                                * dollStat) / 100f);
    }

    private int CalcBasicStat(float value1, float attribute, int dollStat)
    {
        return (int)Mathf.Ceil(((value1 * attribute) * dollStat) / 100f);
    }

    private int CalcGrowStat(float value1, float value2, float attribute, int dollStat, int level, int grow)
    {
        return (int)Mathf.Ceil(((((value2 + ((level - 1) * value1)
                                                            * attribute)
                                                                * dollStat)
                                                                    * grow) / 100) / 100);
    }

    private float GetFavorRatio(DollFavor faver)
    {
        switch (faver)
        {
            case DollFavor.Worst:
                return -0.05f;
            case DollFavor.Normal:
                return 0f;
            case DollFavor.Like:
                return 0.05f;
            case DollFavor.Love:
                return 0.1f;
            case DollFavor.MadLove:
                return 0.15f;
            default:
                return 0f;
        }
    }

    public void CalcStatPlusBuff(DollData data, TileBuff tileBuff)
    {
        for (int i = 0; i < tileBuff.totalStats.Length; i++)
        {
            if (tileBuff.totalStats[i] != 0)
            {
                Stats stat = (Stats)i;
                var value = data.stat.GetStat(stat);
                value = (int)Mathf.Ceil(value + value * ((float)tileBuff.totalStats[i]/100f));
                data.stat.SetStat(stat, value);
            }
        }
    }
}