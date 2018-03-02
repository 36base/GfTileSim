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

    public void Filter(int type)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].doll.dollData.type == (DollType)type)
                elements[i].go.SetActive(!elements[i].go.activeSelf);
        }
    }

    public void NextButton()
    {
        filters[filterIndex].SetActive(false);
        filterIndex++;
        if (filterIndex >= filters.Length)
            filterIndex = 0;
        filters[filterIndex].SetActive(true);
    }
}
