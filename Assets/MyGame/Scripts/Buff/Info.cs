using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 인형선택시 인형정보표시
/// </summary>
public class Info : MonoBehaviour
{
    public bool infoOn = true;

    public GameObject go;

    public MiniBuffSetter miniBuff;
    public Text text;

    private StringBuilder sb = new StringBuilder();
    private StringBuilder sb2 = new StringBuilder();

    private void Awake()
    {
        go.SetActive(false);
    }

    /// <summary>
    /// 해당하는 인형에대한 버프정보를 설정한다.
    /// </summary>
    /// <param name="doll">인형</param>
    public void SetInfo(Doll doll)
    {
        //옵션에서 On/Off 가능
        if (!infoOn)
            return;

        if (doll == null)
        {
            go.SetActive(false);
            return;
        }
        else
            go.SetActive(true);

        miniBuff.SetMiniBuff(doll.dollData);

        sb.Length = 0;
        sb.Append("버프칸의 ");

        switch (doll.dollData.effect.effectType)
        {
            case DollType.All:
                sb.Append("<Color=#DAA520>모든총기</color> 에게\n");
                sb.Append(ReturnStatName(doll));
                break;
            case DollType.HG:
                sb.Append("<Color=#DAA520>권총</color> 에게\n");
                sb.Append(ReturnStatName(doll));
                break;
            case DollType.SMG:
                sb.Append("<Color=#DAA520>기관단총</color> 에게\n");
                sb.Append(ReturnStatName(doll));
                break;
            case DollType.RF:
                sb.Append("<Color=#DAA520>소총</color> 에게\n");
                sb.Append(ReturnStatName(doll));
                break;
            case DollType.AR:
                sb.Append("<Color=#DAA520>돌격소총</color> 에게\n");
                sb.Append(ReturnStatName(doll));
                break;
            case DollType.MG:
                sb.Append("<Color=#DAA520>기관총</color> 에게\n");
                sb.Append(ReturnStatName(doll));
                break;
            case DollType.SG:
                sb.Append("<Color=#DAA520>산탄총</color> 에게\n");
                sb.Append(ReturnStatName(doll));
                break;
        }

        text.text = sb.ToString();
    }

    /// <summary>
    /// 인형정보에서 gridEffect를 읽어들여 버프를 문자열화,
    /// 권총의 경우 버프2배
    /// </summary>
    /// <param name="doll"></param>
    /// <returns></returns>
    private string ReturnStatName(Doll doll)
    {
        sb2.Length = 0;

        for (int i = 0; i < doll.dollData.effect.gridEffects.Length; i++)
        {
            switch (doll.dollData.effect.gridEffects[i].type)
            {
                case Stats.armor:
                    sb2.Append("장갑상승");
                    break;
                case Stats.armorPiercing:
                    sb2.Append("관통력상승");
                    break;
                case Stats.bullet:
                    sb2.Append("장탄수상승");
                    break;
                case Stats.coolDown:
                    sb2.Append("쿨타임 감소율상승");
                    break;
                case Stats.crit:
                    sb2.Append("치명률상승");
                    break;
                case Stats.critDmg:
                    sb2.Append("치명타 데미지상승");
                    break;
                case Stats.dodge:
                    sb2.Append("회피상승");
                    break;
                case Stats.hit:
                    sb2.Append("명중상승");
                    break;
                case Stats.hp:
                    sb2.Append("체력상승");
                    break;
                case Stats.nightView:
                    sb2.Append("야간능력상승");
                    break;
                case Stats.pow:
                    sb2.Append("화력상승");
                    break;
                case Stats.range:
                    sb2.Append("사거리상승");
                    break;
                case Stats.rate:
                    sb2.Append("사속상승");
                    break;
                case Stats.shield:
                    sb2.Append("보호막추가");
                    break;
                case Stats.speed:
                    sb2.Append("이속상승");
                    break;
                default:
                    break;
            }

            sb2.Append("<Color=#DAA520>");

            if(doll.dollData.type == DollType.HG)
            {
                sb2.Append(doll.dollData.effect.gridEffects[i].value * 2);
            }
            else
            {
                sb2.Append(doll.dollData.effect.gridEffects[i].value);
            }

            sb2.Append("%</color> ");
        }

        return sb2.ToString();
    }

    public void OffInfo()
    {
        go.SetActive(false);
    }
}
