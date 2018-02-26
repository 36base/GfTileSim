using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Grid grid;
    public Transform tr;

    public int tileNum;

    public Doll doll;
    public DollSelecter.Select selecter;

    public Image image;

    private void Awake()
    {
        tr = transform;
        image = GetComponent<Image>();
        grid.AllImageOff += OffImage;
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Enter " + tileNum);
        grid.mousePos = tileNum;
    }

    private void OnMouseExit()
    {
        //Debug.Log("Exit " + tileNum);
        grid.mousePos = 0;
    }

    private void OffImage()
    {
        image.enabled = false;
    }
}
