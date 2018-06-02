using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/// <summary>
/// 인형 선택 리스트
/// </summary>
public class DollList : UIBackBtnHandle
{
    public GameObject dollButtonPrefab;
    public Grid grid;

    /// <summary>
    /// 인형 버튼들이 들어갈 스크롤뷰 콘텐츠
    /// </summary>
    public Transform content;

    //다음 버튼 누르면 새로운 필터 보임, 지금은 사용하지 않습니다.
    public int filterIndex = 0;

    //Sort Value = 정렬 값 1 또는 -1 순, 역순 
    public int noSortValue = 1;
    public int rareSortValue = -1;
    public int nameSortValue = 1;
    //Filter Value = 필터링할 비트플래그 값 이진 계산함
    public int gunBitFilterValue = 0;
    public int rareBitFilterValue = 0;

    //Images = 해당 필터버튼들의 이미지, 필터 활성시 이미지 색상 변경
    public Image[] gunFilterImages;
    public Image[] rareFilterImages;

    /// <summary>
    /// 스크롤 뷰에 들어갈 버튼 엘리먼트 리스트
    /// </summary>
    public List<ListElement> elements = new List<ListElement>();

    /// <summary>
    /// 선택한 셀렉터 num
    /// </summary>
    public int currentSelection;

    public override void Open()
    {
        if (!SingleTon.instance.listTop.activeSelf)
            SingleTon.instance.listTop.SetActive(true);
        base.Open();
        //데미지 Sim과 같이 켤수 없게 함.
        SingleTon.instance.damageSim.gameObject.SetActive(false);
    }

    /// <summary>
    /// 스크롤뷰 콘텐츠에 인형버튼 추가 및 버튼 이벤트 할당
    /// </summary>
    /// <param name="doll"></param>
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

    /// <summary>
    /// 스크롤 뷰 내의 인형 버튼 클릭시 인형 스폰
    /// </summary>
    /// <param name="num">스폰할 인형 id</param>
    public void SelectDoll(int num)
    {
        if (!isWindow)
            return;
        if (currentSelection < 1)
            return;

        Doll doll;
        if (SingleTon.instance.mgr.dollDict.TryGetValue(num, out doll))
        {
            SingleTon.instance.info.OffInfo();
            grid.Spawn(doll
                , SingleTon.instance.dollSelecter.selects[currentSelection - 1]);
        }
        Close();
    }

    /// <summary>
    /// 해당 셀렉터에 위치한 타일의 인형 해제
    /// </summary>
    public void DeSelectDoll()
    {
        if (!isWindow)
            return;
        if (currentSelection < 1)
            return;

        SingleTon.instance.info.OffInfo();
        var select = SingleTon.instance.dollSelecter.selects[currentSelection - 1];
        grid.Despawn(select.gridPos);
        select.image.sprite = SingleTon.instance.nullButtonSprite;
        grid.CalcBuff();
        grid.CalcIndiBuff(select.gridPos);
        grid.AllImageOff();
        Close();
    }

    /// <summary>
    /// 인형 번호 순 정렬
    /// </summary>
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

    /// <summary>
    /// 인형 레어도 순 정렬
    /// </summary>
    public void RareSort()
    {
        elements.Sort((a, b) =>
        {
            var temp_a = ((a.doll.dollData.id > 999) && (a.doll.dollData.id < 10000))
            ? 4.5 : a.doll.dollData.rank;
            var temp_b = ((b.doll.dollData.id > 999) && (b.doll.dollData.id < 10000))
            ? 4.5 : b.doll.dollData.rank;

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

    /// <summary>
    /// 인형 이름 순 정렬
    /// </summary>
    public void NameSort()
    {
        elements.Sort((a, b) =>
        {
            return nameSortValue * String.Compare(a.doll.dollData.krName, b.doll.dollData.krName);
        });

        SortAllElements();
        nameSortValue *= -1;
    }

    /// <summary>
    /// 정렬된 리스트에 맞춰서 SetSiblingIndex실행, 하이어아키 뷰에서 순서변경
    /// </summary>
    private void SortAllElements()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].tr.SetSiblingIndex(i);
        }
    }

    /// <summary>
    /// 필터 초기화
    /// </summary>
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

        gunBitFilterValue = 0;
        rareBitFilterValue = 0;
    }

    /// <summary>
    /// 총기 타입에 따른 필터
    /// </summary>
    /// <param name="bit">인스펙터에서 할당, 비트단위 이동</param>
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

    /// <summary>
    /// 레어도에 따른 필터
    /// </summary>
    /// <param name="bit">인스펙터에서 할당, 비트단위 이동</param>
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

    /// <summary>
    /// 세팅된 필터에 따른 게임오브젝트 비활성화를 통한 필터링
    /// </summary>
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

            if ((elements[i].doll.id < 999) || (elements[i].doll.id > 10000))
            {
                if ((rareBitFilterValue & (1 << (rank - 2))) > 0)
                    value = true;
            }
            else if ((rareBitFilterValue & (1 << 4)) > 0)
            {
                value = true;
            }

            elements[i].go.SetActive(value);
        }
    }

    //public void NextButton()
    //{
    //    filters[filterIndex].SetActive(false);
    //    filterIndex++;
    //    if (filterIndex >= filters.Length)
    //        filterIndex = 0;
    //    filters[filterIndex].SetActive(true);
    //}
}
