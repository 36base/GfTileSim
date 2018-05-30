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
            switch(type)
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
}
