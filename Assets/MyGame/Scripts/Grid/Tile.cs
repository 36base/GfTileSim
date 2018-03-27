using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public MiniBuffSetter miniBuff;
    public Transform tr;

    public int tileNum;

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

    private void OnMouseEnter()
    {
        //Debug.Log("Enter " + tileNum);
        SingleTon.instance.grid.mousePos = tileNum;
    }

    private void OnMouseExit()
    {
        //Debug.Log("Exit " + tileNum);
        //Grids.mousePos = 0;
        SingleTon.instance.grid.mousePos = 0;
    }

    public void OffImage()
    {
        image.sprite = SingleTon.instance.nullSprite;
    }
    public void SelectImage()
    {
        image.sprite = SingleTon.instance.selectedSprite;
    }
    public void BuffImage()
    {
        image.sprite = SingleTon.instance.buffSprite;
    }

}
