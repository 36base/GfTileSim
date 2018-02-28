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

    public DollType effectType;
    public int effectCenter;
    public int[] effectPos;
    public GridEffect[] gridEffects;

}

[System.Serializable]
public class DollData
{
    public int id;
    public string name;
    public int rank;
    public DollType type;
    public Effect effect;
}
