using System.Collections.Generic;
using System;

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

            this.type = (Stats)Enum.Parse(typeof(Stats), type);

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
}
