using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DollSettingToggle
{
    public Toggle[] favors;
    public Toggle[] equips;
}

public class DamageSimSetting : MonoBehaviour
{
    public DollSettingToggle[] dollSettingToggles;

    public InputField enemyDodge;
    public InputField enemyArmor;

    public Animator anim;

    public void Open()
    {
        anim.SetBool("open", true);
        ShowEquipByType();
    }
    public void Close()
    {
        anim.SetBool("open", false);
    }
    public void OKButton()
    {
        SetAllEquipToStat();
        SingleTon.instance.damageSim.SetTargetDolls();
        SetEnemy();
        SingleTon.instance.damageSim.MakeGraph(true);
    }

    public void SetFaverWorst(int index)
    {
        if (dollSettingToggles[index].favors[0].isOn)
            SingleTon.instance.damageSim.dollSettings[index].favor = DollFavor.Worst;
    }
    public void SetFavorNormal(int index)
    {
        if (dollSettingToggles[index].favors[1].isOn)
            SingleTon.instance.damageSim.dollSettings[index].favor = DollFavor.Normal;
    }
    public void SetFavorLike(int index)
    {
        if (dollSettingToggles[index].favors[2].isOn)
            SingleTon.instance.damageSim.dollSettings[index].favor = DollFavor.Like;
    }
    public void SetFavorLove(int index)
    {
        if (dollSettingToggles[index].favors[3].isOn)
            SingleTon.instance.damageSim.dollSettings[index].favor = DollFavor.Love;
    }
    public void SetFavorMadLove(int index)
    {
        if (dollSettingToggles[index].favors[4].isOn)
            SingleTon.instance.damageSim.dollSettings[index].favor = DollFavor.MadLove;
    }

    public void SetAllEquipToStat()
    {
        var dollSettings = SingleTon.instance.damageSim.dollSettings;
        for (int i = 0; i < dollSettings.Length; i++)
        {
            dollSettings[i].equipmentStat.ResetAllStat();
            dollSettings[i].slug = false;
        }

        for (int i = 0; i < dollSettingToggles.Length; i++)
        {
            for (int j = 0; j < dollSettingToggles[i].equips.Length; j++)
            {
                if (dollSettingToggles[i].equips[j].isOn)
                {
                    AddEquipValue(j, dollSettings[i].equipmentStat);
                    if (j == 7)
                        dollSettings[i].slug = true;
                }
            }
        }
    }
    private void AddEquipValue(int equipNum, DollStat targetStat)
    {
        switch (equipNum)
        {
            case 0:
                targetStat.crit += 48;
                break;
            case 1:
                targetStat.hit += 14;
                targetStat.pow += 8;
                targetStat.rate -= 4;
                break;
            case 2:
                targetStat.hit += 30;
                targetStat.rate -= 1;
                break;
            case 3:
                targetStat.nightView += 100;
                break;
            case 4:
                targetStat.armorPiercing += 120;
                break;
            case 5:
                targetStat.pow += 15;
                targetStat.armorPiercing -= 7;
                break;
            case 6:
                targetStat.pow += 15;
                targetStat.critDmg += 22;
                break;
            case 7:
                targetStat.hit += 20;
                break;
            case 8:
                targetStat.pow += 20;
                break;
            case 9:
                targetStat.crit += 20;
                break;
            case 10:
                targetStat.bullet += 5;
                break;
            case 11:
                targetStat.critDmg += 25;
                break;
        }
    }

    public void SetEnemy()
    {
        int dodge;
        if(!int.TryParse(enemyDodge.text, out dodge))
        {
            enemyDodge.text = 0.ToString();
            dodge = 0;
        }

        int armor;
        if(!int.TryParse(enemyArmor.text, out armor))
        {
            enemyArmor.text = 0.ToString();
            armor = 0;
        }

        SingleTon.instance.damageSim.enemy.dodge = dodge;
        SingleTon.instance.damageSim.enemy.armor = armor;
    }

    //0-2 베스피드
    public void SetEnemy_Default1()
    {
        enemyDodge.text = "10";
        enemyArmor.text = "0";
    }
    //7-2N 야간 박쥐
    public void SetEnemy_Default2()
    {
        enemyDodge.text = "80";
        enemyArmor.text = "0";
    }
    //7-3N 장갑병
    public void SetEnemy_Default3()
    {
        enemyDodge.text = "0";
        enemyArmor.text = "104";
    }
    //10-6 법관
    public void SetEnemy_Default4()
    {
        enemyDodge.text = "10";
        enemyArmor.text = "25";
    }

    private void ShowEquipByType()
    {
        var damageSim = SingleTon.instance.damageSim;
        for (int i = 0; i < damageSim.targetDolls.Length; i++)
        {
            for (int j = 0; j < dollSettingToggles[i].equips.Length; j++)
            {
                dollSettingToggles[i].equips[j].gameObject.SetActive(false);
            }

            if (damageSim.targetDolls[i].id == -1)
                continue;

            switch (damageSim.targetDolls[i].type)
            {
                case DollType.HG:
                    dollSettingToggles[i].equips[3].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[5].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[9].gameObject.SetActive(true);
                    if(damageSim.targetDolls[i].id == 183)
                        dollSettingToggles[i].equips[4].gameObject.SetActive(true);
                    break;
                case DollType.SMG:
                    dollSettingToggles[i].equips[0].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[1].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[2].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[3].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[5].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[9].gameObject.SetActive(true);
                    if(damageSim.targetDolls[i].id == 213)
                        dollSettingToggles[i].equips[4].gameObject.SetActive(true);
                    break;
                case DollType.RF:
                    dollSettingToggles[i].equips[0].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[1].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[2].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[4].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[9].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[11].gameObject.SetActive(true);
                    break;
                case DollType.AR:
                    dollSettingToggles[i].equips[0].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[1].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[2].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[3].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[8].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[9].gameObject.SetActive(true);
                    if(damageSim.targetDolls[i].id == 138)
                        dollSettingToggles[i].equips[4].gameObject.SetActive(true);
                    break;
                case DollType.MG:
                    dollSettingToggles[i].equips[0].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[1].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[2].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[4].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[10].gameObject.SetActive(true);
                    break;
                case DollType.SG:
                    dollSettingToggles[i].equips[0].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[1].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[2].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[6].gameObject.SetActive(true);
                    dollSettingToggles[i].equips[7].gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void ResetAllEquipToggles()
    {
        for (int i = 0; i < dollSettingToggles.Length; i++)
        {
            for (int j = 0; j < dollSettingToggles[i].equips.Length; j++)
            {
                dollSettingToggles[i].equips[j].isOn = false;
            }
        }
    }
    public void ResetAllFavorToggles()
    {
        for (int i = 0; i < 5; i++)
        {
            dollSettingToggles[i].favors[0].isOn = false;
            dollSettingToggles[i].favors[1].isOn = true;
            dollSettingToggles[i].favors[2].isOn = false;
            dollSettingToggles[i].favors[3].isOn = false;
            dollSettingToggles[i].favors[4].isOn = false;
            //SetFavorNormal(i);
        }
    }
    public void ResetEnemySetting()
    {
        enemyArmor.text = "0";
        enemyDodge.text = "0";
    }
    public void ResetLevelSetting()
    {

    }


    public void ResetAllSettings()
    {
        ResetAllEquipToggles();
        ResetAllFavorToggles();
        ResetEnemySetting();
        ResetLevelSetting();
    }
}
