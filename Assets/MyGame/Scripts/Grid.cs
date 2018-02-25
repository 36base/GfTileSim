using UnityEngine;
using System.Collections;

public delegate void ImageOffHandler();

public class Grid : MonoBehaviour
{
    public Tile[] tiles;

    public int pickedDoll = 0;

    public int mousePos = 0;

    public event ImageOffHandler AllImageOff;

    private void Update()
    {
        if (SingleTon.instance.dollList.isWindow)
            return;

        if (Input.GetMouseButtonUp(0))
        {
            DropADoll();
            //Debug.Log("Up");
        }

        if (mousePos < 1)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            SelectTile(mousePos);
            //Debug.Log("Down");
        }

        if (Input.GetMouseButton(0))
        {
            PickADoll();
            //Debug.Log("Drag");
        }


    }


    public void Spawn(Doll doll, int pos)
    {
        if (pos - 1 < 0)
            return;
        doll.Spawn(tiles[pos - 1].tr);
        tiles[pos - 1].doll = doll;
    }

    public void Despawn(int pos)
    {
        if (pos - 1 < 0)
            return;
        tiles[pos - 1].doll.Despawn();
        tiles[pos - 1].doll = null;
    }

    private void MoveTo(int from, int to)
    {
        if (from - 1 < 0 || to - 1 < 0)
            return;

        if (tiles[from - 1].doll == null)
            return;

        tiles[from - 1].doll.tr.localPosition = tiles[to - 1].tr.localPosition;

        var temp = tiles[to - 1].doll;
        tiles[to - 1].doll = tiles[from - 1].doll;
        tiles[from - 1].doll = temp;
    }

    public void SelectTile(int num)
    {
        if (num < 1)
            return;
        AllImageOff();
        tiles[num - 1].image.enabled = true;

        if (tiles[num - 1].doll == null)
            return;
        tiles[num - 1].doll.SetState(Doll.DollState.Selected);
    }

    public void PickADoll()
    {
        if (pickedDoll != 0)
            return;
        pickedDoll = mousePos;
        if (tiles[pickedDoll - 1].doll == null)
            return;

        tiles[pickedDoll - 1].doll.SetState(Doll.DollState.Following);
    }
    public void DropADoll()
    {
        if (pickedDoll == 0)
            return;

        if (mousePos == 0)
        {
            MoveTo(pickedDoll, pickedDoll);
            tiles[pickedDoll - 1].doll.SetState(Doll.DollState.Idle);
        }
        else
        {
            MoveTo(pickedDoll, mousePos);
            SelectTile(mousePos);
        }
        pickedDoll = 0;
    }
}
