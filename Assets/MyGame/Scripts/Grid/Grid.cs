using UnityEngine;
using System.Collections;

public delegate void ImageOffHandler();

public class Grid : MonoBehaviour
{
    public Tile[] tiles;

    public int pickedDoll = 0;

    public int mousePos = 0;

    public Doll selectedDoll;

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

    public void Spawn(Doll doll, DollSelecter.Select selecter)
    {
        if (selecter.gridPos - 1 < 0)
            return;
        if (tiles[selecter.gridPos - 1].doll != null)
            Despawn(selecter.gridPos);

        if (doll.go.activeSelf)
        {
            MoveTo(doll.pos, selecter.gridPos, false);
        }
        else
        {
            doll.Spawn(tiles[selecter.gridPos - 1].tr);
            doll.pos = selecter.gridPos;
            tiles[selecter.gridPos - 1].doll = doll;
            tiles[selecter.gridPos - 1].selecter = selecter;
            tiles[selecter.gridPos - 1].miniBuff.SetMiniBuff(doll.dollData);
        }


        CalcBuff();
    }

    public void Despawn(int pos)
    {
        if (pos - 1 < 0)
            return;
        tiles[pos - 1].doll.Despawn();
        tiles[pos - 1].doll = null;
        tiles[pos - 1].miniBuff.MiniBuffOff();

    }

    private void MoveTo(int from, int to, bool swap)
    {
        if (from - 1 < 0 || to - 1 < 0)
            return;

        if (tiles[from - 1].doll == null)
            return;

        tiles[from - 1].doll.tr.localPosition = tiles[to - 1].tr.localPosition;
        tiles[from - 1].doll.pos = to;
        tiles[to - 1].miniBuff.SetMiniBuff(tiles[from - 1].doll.dollData);

        if (tiles[to - 1].doll != null)
        {
            tiles[to - 1].doll.tr.localPosition = tiles[from - 1].tr.localPosition;
            tiles[to - 1].doll.pos = from;
            tiles[from - 1].miniBuff.SetMiniBuff(tiles[to - 1].doll.dollData);
        }
        else
        {
            tiles[from - 1].miniBuff.MiniBuffOff();
        }

        var tempDoll = tiles[to - 1].doll;
        tiles[to - 1].doll = tiles[from - 1].doll;
        tiles[from - 1].doll = tempDoll;

        if (!swap)
            return;

        tiles[to - 1].selecter.gridPos = from;
        if (tiles[from - 1].selecter != null)
            tiles[from - 1].selecter.gridPos = to;

        var tempSelecter = tiles[to - 1].selecter;
        tiles[to - 1].selecter = tiles[from - 1].selecter;
        tiles[from - 1].selecter = tempSelecter;
    }

    public void SelectTile(int num)
    {
        if (num < 1)
            return;
        AllImageOff();
        tiles[num - 1].SelectImage();

        CalcIndiBuff(num);

        if (tiles[num - 1].doll == null)
            return;

        if (selectedDoll != null && selectedDoll != tiles[num - 1].doll)
            selectedDoll.SetState(Doll.DollState.Idle);

        tiles[num - 1].doll.SetState(Doll.DollState.Selected);
        selectedDoll = tiles[num - 1].doll;
    }

    public void PickADoll()
    {
        if (pickedDoll != 0)
            return;

        pickedDoll = mousePos;

        if (tiles[pickedDoll - 1].doll == null)
        {
            pickedDoll = 0;
            return;
        }

        tiles[pickedDoll - 1].doll.SetState(Doll.DollState.Following);
    }
    public void DropADoll()
    {
        if (pickedDoll == 0)
            return;

        if (mousePos == 0)
        {
            MoveTo(pickedDoll, pickedDoll, true);
            tiles[pickedDoll - 1].doll.SetState(Doll.DollState.Idle);
        }
        else
        {
            MoveTo(pickedDoll, mousePos, true);
            SelectTile(mousePos);
        }
        pickedDoll = 0;

        CalcBuff();
    }

    public void CalcBuff()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].tileBuff.ResetTotalBuff();
            tiles[i].tileBuff.ResetTotalStats();
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].doll == null)
                continue;

            var effect = tiles[i].doll.dollData.effect;
            for (int j = 0; j < effect.effectPos.Length; j++)
            {
                for (int k = 0; k < effect.gridEffects.Length; k++)
                {
                    if (effect.effectPos[j] - 1 < 0)
                        continue;

                    var naver = tiles[i].neighbors[effect.effectPos[j] - 1];

                    if (naver == null
                        || naver.doll == null)
                        continue;


                    if (effect.effectType == naver.doll.dollData.type
                        || effect.effectType == DollType.All)
                    {
                        tiles[i].neighbors[effect.effectPos[j] - 1].tileBuff
                            .AddTotalStats(effect.gridEffects[k]);
                    }
                }
            }
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].tileBuff.SetTotalBuff();
        }
    }

    public void CalcIndiBuff(int pos)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].tileBuff.ResetIndiBuff();
        }

        if (tiles[pos - 1].doll == null)
            return;

        var effect = tiles[pos - 1].doll.dollData.effect;

        for (int i = 0; i < effect.effectPos.Length; i++)
        {
            if (effect.effectPos[i] - 1 < 0)
                continue;

            var naver = tiles[pos - 1].neighbors[effect.effectPos[i] - 1];

            if (naver == null
                || naver.doll == null)
                continue;

            naver.tileBuff.AddIndiBuff(effect);

            if (effect.effectType == DollType.All)
                continue;

            if (naver.doll == null
                || effect.effectType != naver.doll.dollData.type)
            {
                naver.tileBuff.cg.alpha = 0.6f;
            }
        }
    }
}
