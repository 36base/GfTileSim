using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DollList : UIBackBtnHandle
{
    public GameObject dollButtonPrefab;
    public Grid grid;

    public Transform content;

    public GameObject[] filters;
    public int filterIndex = 0;

    public int noSortValue = 1;
    public int rareSortValue = -1;
    public int nameSortValue = 1;
    public int gunBitFilterValue = 0;
    public int rareBitFilterValue = 0;
    public Image[] gunFilterImages;
    public Image[] rareFilterImages;

    public List<ListElement> elements = new List<ListElement>();

    public int currentSelection;

    public void AddContent(Doll doll)
    {
        var go = Instantiate(dollButtonPrefab, content.position, Quaternion.identity)
            as GameObject;
        go.transform.SetParent(content, false);
        go.GetComponent<Button>().onClick.AddListener(() => SelectDoll(doll.id));
        go.GetComponent<Image>().sprite = doll.profilePic;
        var ele = go.GetComponent<ListElement>();
        ele.doll = doll;
        elements.Add(ele);
    }

    public void SelectDoll(int num)
    {
        if (!isWindow)
            return;
        if (currentSelection < 1)
            return;

        Doll doll;
        if (SingleTon.instance.mgr.dollDict.TryGetValue(num, out doll))
        {
            grid.Spawn(doll
                , SingleTon.instance.dollSelecter.selects[currentSelection - 1]);
        }
        Close();
    }



    public void NoSort()
    {
        elements.Sort((a, b) =>
        {
            if (a.doll.dollData.id > b.doll.dollData.id)
                return 1 * noSortValue;
            else if (a.doll.dollData.id < b.doll.dollData.id)
                return -1 * noSortValue;
            else
                return 0;
        });

        SortAllElements();
        noSortValue *= -1;
    }

    public void RareSort()
    {
        elements.Sort((a, b) =>
        {
            var temp_a = (a.doll.dollData.id > 999) ? 4.5 : a.doll.dollData.rank;
            var temp_b = (b.doll.dollData.id > 999) ? 4.5 : b.doll.dollData.rank;

            if (temp_a > temp_b)
                return 1 * rareSortValue;
            else if (temp_a < temp_b)
                return -1 * rareSortValue;
            else
                return 0;
        });

        SortAllElements();
        rareSortValue *= -1;
    }

    public void NameSort()
    {
        elements.Sort((a, b) =>
        {
            return nameSortValue * String.Compare(a.doll.dollData.name, b.doll.dollData.name);
        });

        SortAllElements();
        nameSortValue *= -1;
    }

    private void SortAllElements()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].tr.SetSiblingIndex(i);
        }
    }

    public void AllFilter()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].go.SetActive(true);
        }
        for (int i = 0; i < rareFilterImages.Length; i++)
        {
            rareFilterImages[i].color = Color.white;
        }
        for (int i = 0; i < gunFilterImages.Length; i++)
        {
            gunFilterImages[i].color = Color.white;
        }
    }

    public void GunFilter(int bit)
    {
        gunBitFilterValue ^= (1 << bit - 1);
        if ((gunBitFilterValue & (1 << bit - 1)) > 0)
        {
            gunFilterImages[bit - 1].color = Color.yellow;
        }
        else
        {
            gunFilterImages[bit - 1].color = Color.white;
        }

        Filtering();
    }
    public void RareFilter(int bit)
    {
        rareBitFilterValue ^= (1 << bit - 2);
        if ((rareBitFilterValue & (1 << bit - 2)) > 0)
        {
            rareFilterImages[bit - 2].color = Color.yellow;
        }
        else
        {
            rareFilterImages[bit - 2].color = Color.white;
        }
        Filtering();
    }
    private void Filtering()
    {
        bool value;

        for (int i = 0; i < elements.Count; i++)
        {
            value = false;

            if (gunBitFilterValue == 0)
                value = true;

            int type = (int)elements[i].doll.dollData.type;

            if ((gunBitFilterValue & (1 << (type - 1))) > 0)
                value = true;

            if (!value)
            {
                elements[i].go.SetActive(value);
                continue;
            }

            value = false;

            if (rareBitFilterValue == 0)
                value = true;

            int rank = elements[i].doll.dollData.rank;

            if(elements[i].doll.id < 999)
            {
                if ((rareBitFilterValue & (1 << (rank - 2))) > 0)
                    value = true;
            }
            else if((rareBitFilterValue & (1 << 4)) > 0)
            {
                value = true;
            }

            elements[i].go.SetActive(value);
        }
    }


    //private void _Filtering()
    //{
    //    bool value;

    //    for (int i = 0; i < elements.Count; i++)
    //    {
    //        value = false;

    //        if (gunBitFilterValue == 0)
    //            value = true;
    //        else
    //            for (int j = 1; j < 7; j++)
    //            {
    //                if ((gunBitFilterValue & (1 << (j - 1))) > 0)
    //                {
    //                    if ((int)elements[i].doll.dollData.type == j)
    //                    {
    //                        value = true;
    //                        break;
    //                    }
    //                }
    //            }

    //        if (!value)
    //            continue;

    //        value = false;

    //        if (rareBitFilterValue == 0)
    //            value = true;
    //        else if ((rareBitFilterValue & (1 << 4)) > 0)
    //        {
    //            if (elements[i].doll.id > 999)
    //            {
    //                value = true;
    //            }
    //        }
    //        else
    //            for (int j = 2; j < 6; j++)
    //            {
    //                if ((rareBitFilterValue & (1 << (j - 2))) > 0)
    //                {
    //                    if (elements[i].doll.dollData.rank == j)
    //                    {
    //                        value = true;
    //                        break;
    //                    }
    //                }
    //            }

    //    }
    //}


    //public void GunBitFilter(int bit)
    //{
    //    gunBitFilterValue ^= (1 << bit - 1);

    //    for (int i = 0; i < 6; i++)
    //    {
    //        if ((gunBitFilterValue & (1 << i)) > 0)
    //        {
    //            GunFilter(i + 1, true);
    //            gunFilterImages[i].color = Color.yellow;
    //        }
    //        else
    //        {
    //            GunFilter(i + 1, false);
    //            gunFilterImages[i].color = Color.white;
    //        }

    //    }

    //    if (gunBitFilterValue == 0)
    //    {
    //        for (int i = 1; i < 7; i++)
    //        {
    //            GunFilter(i, true);
    //        }
    //    }
    //}

    //private void GunFilter(int type, bool on)
    //{
    //    for (int i = 0; i < elements.Count; i++)
    //    {
    //        if (elements[i].doll.dollData.type == (DollType)type)
    //            elements[i].go.SetActive(on);
    //    }
    //}

    //public void RareBitFilter(int bit)
    //{
    //    rareBitFilterValue ^= (1 << bit - 2);

    //    for (int i = 0; i < 5; i++)
    //    {
    //        if ((rareBitFilterValue & (1 << i)) > 0)
    //        {
    //            RareFilter(i + 2, true);
    //            rareFilterImages[i].color = Color.yellow;
    //        }
    //        else
    //        {
    //            RareFilter(i + 2, false);
    //            rareFilterImages[i].color = Color.white;
    //        }

    //    }

    //    if (rareBitFilterValue == 0)
    //    {
    //        for (int i = 2; i < 7; i++)
    //        {
    //            RareFilter(i, true);
    //        }
    //    }
    //}

    //private void RareFilter(int rare, bool on)
    //{
    //    for (int i = 0; i < elements.Count; i++)
    //    {
    //        if (rare != 6)
    //        {
    //            if (elements[i].doll.dollData.rank == rare)
    //                elements[i].go.SetActive(on);
    //        }
    //        else
    //        {
    //            if (elements[i].doll.dollData.id > 999)
    //                elements[i].go.SetActive(on);
    //        }
    //    }
    //}

    public void NextButton()
    {
        filters[filterIndex].SetActive(false);
        filterIndex++;
        if (filterIndex >= filters.Length)
            filterIndex = 0;
        filters[filterIndex].SetActive(true);
    }
}
