using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 각 Tile오브젝트에 컴포넌트로 붙임, 2017버전 기준 콜라이더 크기초기화 오류로 사용불가
/// </summary>
public class Tile : MonoBehaviour
{
    public MiniBuffSetter miniBuff;
    public Transform tr;

    public int tileNum;

    /// <summary>
    /// 각 타일을 기준점 5로하여 주변 이웃을 할당
    /// </summary>
    public Tile[] neighbors;
    public TileBuff tileBuff;

    [Space]
    public Doll doll;
    public DollSelecter.Select selecter;

    public Image image;

    private void Awake()
    {
        tr = transform;
        image = GetComponent<Image>();
        SingleTon.instance.grid.AllImageOff += OffImage;
        tileBuff = GetComponent<TileBuff>();
    }

    /// <summary>
    /// 해당 타일콜라이더 Enter 시 grid의 mousePos 할당
    /// </summary>
    private void OnMouseEnter()
    {
        //Debug.Log("Enter " + tileNum);
        SingleTon.instance.grid.mousePos = tileNum;
    }

    /// <summary>
    /// Exit 시 mousePos = 0 할당
    /// </summary>
    private void OnMouseExit()
    {
        //Debug.Log("Exit " + tileNum);
        SingleTon.instance.grid.mousePos = 0;
    }

    /// <summary>
    /// 타일의 기본 이미지 상태, grid 에 딜리게이트 체인 해둠
    /// </summary>
    public void OffImage()
    {
        image.sprite = SingleTon.instance.nullSprite;
    }
    /// <summary>
    /// 타일을 선택한 상태 이미지
    /// </summary>
    public void SelectImage()
    {
        image.sprite = SingleTon.instance.selectedSprite;
    }

    /// <summary>
    /// 타일에 버프를 받는 상태 이미지
    /// </summary>
    public void BuffImage()
    {
        image.sprite = SingleTon.instance.buffSprite;
    }

}
