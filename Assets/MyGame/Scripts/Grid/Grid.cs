using UnityEngine;
using System.Collections;

public delegate void AllOffHandler();


/// <summary>
/// 타일 9개를 배열로 관리하는 클래스
/// </summary>
public class Grid : MonoBehaviour
{
    /// <summary>
    /// 배열의 순서는 키패드순서
    /// </summary>
    public Tile[] tiles;

    /// <summary>
    /// 현재 클릭또는 터치로 선택하고있는 인형의 위치
    /// </summary>
    public int pickedDoll = 0;

    /// <summary>
    /// 현재 포인터(터치포함)의 타일 위 위치(1~9), 타일이 아니면 위치는 0
    /// </summary>
    public int mousePos = 0;

    /// <summary>
    /// 선택한 인형의 참조
    /// </summary>
    public Doll selectedDoll;

    /// <summary>
    /// 초기화 시 타일에 발생하는 이미지를 전부 비활성(선택, 버프 등)하는 함수를 체인
    /// </summary>
    public AllOffHandler AllImageOff;
    /// <summary>
    /// 인게임과 같이 선택한 인형 버프를 감소할건가? 옵션에서 체크가능
    /// </summary>
    public bool lossBuff = false;

    private void Update()
    {
        if (SingleTon.instance.dollList.isWindow 
            || SingleTon.instance.presetCodeIField.isWindow)
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

    //New Added
    //public void SpawnFromPreset(int num, int pos)
    //{
    //    if (pos < 1 || pos > 9)
    //        return;

    //    if (tiles[pos - 1].doll != null)
    //        Despawn(pos);

    //    Doll doll;

    //    ResetIndiBuff();
    //    AllImageOff();

    //    if (SingleTon.instance.mgr.dollDict.TryGetValue(num, out doll))
    //    {

    //    }
    //}

    /// <summary>
    /// 인형 스폰
    /// </summary>
    /// <param name="doll"></param>
    /// <param name="selecter">인형을 스폰하는 셀렉터</param>
    public void Spawn(Doll doll, DollSelecter.Select selecter)
    {
        if (selecter.gridPos - 1< 0)
            return;
        //셀렉터가 가르키는 타일에 이미 인형이 있으면 해당 타일의 인형을 디스폰
        if (tiles[selecter.gridPos - 1].doll != null)
            Despawn(selecter.gridPos);
        //셀렉터에 인형의 초상화 설정
        selecter.image.sprite = doll.profilePic;
        //인형 개별버프 초기화
        ResetIndiBuff();
        //모든 이미지오프 딜리게이트 실행
        AllImageOff();

        if (doll.go.activeSelf)
        {
            //스폰하려는 인형이 이미 다른타일에 있으면, 해당인형의 셀렉터의 초상화를 지움
            doll.selecter.image.sprite = SingleTon.instance.nullButtonSprite;
            //이미 다른타일에 있는 인형을 스폰하고자하는 셀렉터의 위치로 옮김
            MoveTo(doll.pos, selecter.gridPos, false);
        }
        else
        {
            //그렇지 않으면 일반 스폰 진행
            doll.Spawn(tiles[selecter.gridPos - 1].tr);
            //인형의 Pos에 셀렉터의 gridPos 할당
            doll.pos = selecter.gridPos;
            //해당 타일의 인형에 스폰한 인형, 셀렉터 할당
            //해당 타일의 미니버프 설정
            tiles[selecter.gridPos - 1].doll = doll;
            tiles[selecter.gridPos - 1].selecter = selecter;
            tiles[selecter.gridPos - 1].miniBuff.SetMiniBuff(doll.dollData);
        }
        //인형의 셀렉터에 스폰한 셀렉터 할당.
        doll.selecter = selecter;
        //전체 버프 계산
        CalcBuff();
    }

    public void Despawn(int pos)
    {
        if (pos - 1 < 0)
            return;
        if (tiles[pos - 1].doll == null)
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

        tiles[from - 1].selecter.gridPos = to;

        if (tiles[to - 1].selecter != null)
            tiles[to - 1].selecter.gridPos = from;

        var tempSelecter = tiles[to - 1].selecter;
        tiles[to - 1].selecter = tiles[from - 1].selecter;
        tiles[from - 1].selecter = tempSelecter;
    }

    public void SelectTile(int num)
    {
        if (num < 1)
            return;
        AllImageOff();

        CalcIndiBuff(num);

        if (selectedDoll != null && selectedDoll != tiles[num - 1].doll)
            selectedDoll.SetState(Doll.DollState.Idle);

        SingleTon.instance.info.SetInfo(tiles[num - 1].doll);

        selectedDoll = tiles[num - 1].doll;

        if (lossBuff)
            CalcBuff();

        if (tiles[num - 1].doll == null)
            return;

        tiles[num - 1].SelectImage();

        tiles[num - 1].doll.SetState(Doll.DollState.Selected);

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

            if (lossBuff)
            {
                if (selectedDoll == tiles[i].doll)
                    continue;
            }

            var effect = tiles[i].doll.dollData.effect;
            for (int j = 0; j < effect.effectPos.Length; j++)
            {
                var calcPos = CalcBuffPos(effect.effectPos[j], tiles[i].doll.pos, effect.effectCenter);
                if (calcPos == 0)
                    continue;

                var target = tiles[calcPos - 1];

                for (int k = 0; k < effect.gridEffects.Length; k++)
                {
                    if (effect.effectPos[j] - 1 < 0)
                        continue;

                    if (target == null
                        || target.doll == null)
                        continue;

                    if (effect.effectType == target.doll.dollData.type
                        || effect.effectType == DollType.All)
                    {
                        if (tiles[i].doll.dollData.type == DollType.HG)
                        {
                            target.tileBuff.AddTotalStats(effect.gridEffects[k], 2);
                        }
                        else
                        {
                            target.tileBuff.AddTotalStats(effect.gridEffects[k]);
                        }

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

            int calcPos = CalcBuffPos(effect.effectPos[i], tiles[pos - 1].doll.pos, effect.effectCenter);
            if (calcPos == 0)
                continue;

            var target = tiles[calcPos - 1];

            if (target == null)
                continue;

            if (tiles[pos - 1].doll.dollData.type == DollType.HG)
            {
                target.tileBuff.AddIndiBuff(effect, 2);
            }
            else
            {
                target.tileBuff.AddIndiBuff(effect);
            }


            target.BuffImage();
            if (effect.effectType == DollType.All
                && target.doll != null)
                continue;

            if (target.doll == null
                || effect.effectType != target.doll.dollData.type)
            {
                target.tileBuff.cg.alpha = 0.6f;
            }
        }
    }

    public void ResetIndiBuff()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].tileBuff.ResetIndiBuff();
        }
    }

    public void ResetAll(bool showMsg = true)
    {
        selectedDoll = null;
        ResetIndiBuff();
        AllImageOff();
        if(showMsg)
            SingleTon.instance.msg.SetMsg("초기화");

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].doll == null)
                continue;
            Despawn(i + 1);
        }

        CalcBuff();
    }

    //Util
    private int CalcBuffPos(int effectPos, int dollPos, int center)
    {
        int effect_x = effectPos % 3;
        if (effect_x == 0) effect_x = 3;
        int effect_y = effectPos % 3 == 0 ? effectPos / 3 : effectPos / 3 + 1;

        int doll_x = dollPos % 3;
        if (doll_x == 0) doll_x = 3;
        int doll_y = dollPos % 3 == 0 ? dollPos / 3 : dollPos / 3 + 1;

        int center_x = center % 3;
        if (center_x == 0) center_x = 3;
        int center_y = center % 3 == 0 ? center / 3 : center / 3 + 1;

        var delta_x = doll_x - center_x;
        var delta_y = doll_y - center_y;

        if (effect_x + delta_x > 3 || effect_x + delta_x < 1
            || effect_y + delta_y > 3 || effect_y + delta_y < 1)
            return 0;

        int value = effectPos + dollPos - center;
        if (value < 1 || value > 9)
            return 0;
        else
            return value;
    }
}
