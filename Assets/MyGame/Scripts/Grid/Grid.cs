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
        //인형리스트 또는 코드복사불여넣기 + 데미지시뮬 중 인형선택불가
        if (SingleTon.instance.dollList.isWindow 
            || SingleTon.instance.presetCodeIField.isWindow
            || SingleTon.instance.damageSim.isWindow)
            return;

        //터치-업 : 인형 드랍
        if (Input.GetMouseButtonUp(0))
        {
            DropADoll();
            //Debug.Log("Up");
        }

        //마우스 위치가 타일위에 없으면 리턴
        if (mousePos < 1)
            return;

        //터치-다운 : 타일 선택
        if (Input.GetMouseButtonDown(0))
        {
            SelectTile(mousePos);
            //Debug.Log("Down");
        }

        //드래그 : 인형 끌기
        if (Input.GetMouseButton(0))
        {
            PickADoll();
            //Debug.Log("Drag");
        }


    }

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
    /// <summary>
    /// 인형 디스폰
    /// </summary>
    /// <param name="pos">디스폰할 타일의 위치</param>
    public void Despawn(int pos)
    {
        //타일 밖 위치면 리턴
        if (pos - 1 < 0)
            return;
        //해당 타일에 인형이 없으면 리턴
        if (tiles[pos - 1].doll == null)
            return;

        //인형 비활성, 타일에 위치한 인형 null, 해당타일 버프표기 Off
        tiles[pos - 1].doll.Despawn();
        tiles[pos - 1].doll = null;
        tiles[pos - 1].miniBuff.MiniBuffOff();
    }

    /// <summary>
    /// 타일-타일 간에 인형 이동
    /// </summary>
    /// <param name="from">키패드번호기준 타일의 이동하려는 위치</param>
    /// <param name="to">키패드번호기준 타일의 도착하려는 위치</param>
    /// <param name="swap">인형을 새로 스폰할 경우 false, 이미 스폰된 인형들을 움직이는 경우 true</param>
    private void MoveTo(int from, int to, bool swap)
    {
        //타일 밖 위치 리턴
        if (from - 1 < 0 || to - 1 < 0)
            return;

        //from의 인형이 null 이면 리턴
        if (tiles[from - 1].doll == null)
            return;

        //* 인형 데이터 변경 시작*//

        //from타일의 인형의 localPos to타일의 localPos로 이동
        tiles[from - 1].doll.tr.localPosition = tiles[to - 1].tr.localPosition;
        //인형내 타일 위치 to로 할당
        tiles[from - 1].doll.pos = to;
        //from 타일의 인형데이터를 to 타일 미니버프에 설정
        tiles[to - 1].miniBuff.SetMiniBuff(tiles[from - 1].doll.dollData);

        //to 위치에 인형이 있을 경우
        if (tiles[to - 1].doll != null)
        {
            //to 위치의 인형 localPos from 타일의 localPos로 이동
            tiles[to - 1].doll.tr.localPosition = tiles[from - 1].tr.localPosition;
            //인형내 타일 위치 from으로 할당
            tiles[to - 1].doll.pos = from;
            //to 타일의 인형데이터를 from 타일 미니버프에 설정
            tiles[from - 1].miniBuff.SetMiniBuff(tiles[to - 1].doll.dollData);
        }
        else
        {
            //to 위치에 인형이 없으면 미니버프 Off
            tiles[from - 1].miniBuff.MiniBuffOff();
        }

        //* 인형 데이터 변경 끝*//

        //* 타일 데이터 변경 시작*//

        //타일에 참조된 doll swap
        var tempDoll = tiles[to - 1].doll;
        tiles[to - 1].doll = tiles[from - 1].doll;
        tiles[from - 1].doll = tempDoll;

        //인형을 새로 스폰하는 경우 return
        if (!swap)
            return;
        
        //플레이어의 터치 조작 으로 인형을 이동하는 경우 아래 코드 실행

        //셀렉터가 다음에 소폰 할 위치 to로 변경.
        tiles[from - 1].selecter.gridPos = to;

        //to 타일에 셀렉터가 null이 아닐 경우 to 타일의 셀렉터의 다음 스폰위치는 from으로 변경
        if (tiles[to - 1].selecter != null)
            tiles[to - 1].selecter.gridPos = from;

        //타일에 참조된 셀렉터 swap
        var tempSelecter = tiles[to - 1].selecter;
        tiles[to - 1].selecter = tiles[from - 1].selecter;
        tiles[from - 1].selecter = tempSelecter;
    }

    /// <summary>
    /// 포인터가 가르키는 위치의 인형 선택
    /// </summary>
    /// <param name="num">tile 위치</param>
    public void SelectTile(int num)
    {
        //타일 밖이면 리턴
        if (num < 1)
            return;
        //모든 타일에 활성화된 이미지를 off이미지로 변경(체인된 딜리게이트)
        AllImageOff();

        //선택한 인형이 주는 버프 계산
        CalcIndiBuff(num);
        
        //기존에 선택되어있는 인형이 null이 아니고 지금 선택한 타일과 다른곳에 있으면
        //인형상태를 대기로 변경
        if (selectedDoll != null && selectedDoll != tiles[num - 1].doll)
            selectedDoll.SetState(Doll.DollState.Idle);

        //인형 info 말풍선 생성
        SingleTon.instance.info.SetInfo(tiles[num - 1].doll);

        //선택된어있는 인형 해당 타일의 인형으로 변경
        selectedDoll = tiles[num - 1].doll;

        //감산 버프적용시 재계산
        if (lossBuff)
            CalcBuff();

        //선택한 타일에 인형이 null이면 리턴
        if (tiles[num - 1].doll == null)
            return;

        //선택한 타일의 이미지 선택상태로 변경
        tiles[num - 1].SelectImage();
        //선택한 타일의 인형 상태 선택상태로 변경
        tiles[num - 1].doll.SetState(Doll.DollState.Selected);

    }

    /// <summary>
    /// 인형 끌고가기
    /// </summary>
    public void PickADoll()
    {
        //기존에 끌고있는 인형이 있으면 리턴
        if (pickedDoll != 0)
            return;

        //끌고있는 인형이 없을 경우 포인터위치(해당타일위치)로 할당
        pickedDoll = mousePos;

        //해당 타일에 인형이 null이면 끌고있는인형 없음, 리턴
        if (tiles[pickedDoll - 1].doll == null)
        {
            pickedDoll = 0;
            return;
        }
        //인형의 상태 팔로우상태로 변경
        tiles[pickedDoll - 1].doll.SetState(Doll.DollState.Following);
    }

    /// <summary>
    /// 인형 놓기
    /// </summary>
    public void DropADoll()
    {
        //끌고있는 인형이 없으면 리턴
        if (pickedDoll == 0)
            return;

        
        if (mousePos == 0)
        {
            //마우스 위치가 타일 외의 위치면 원위치로 복귀
            MoveTo(pickedDoll, pickedDoll, true);
            tiles[pickedDoll - 1].doll.SetState(Doll.DollState.Idle);
        }
        else
        {
            //해당 마우스 위치로 인형 이동 및 해당 위치 선택
            MoveTo(pickedDoll, mousePos, true);
            SelectTile(mousePos);
        }
        //끌고있는 인형 초기화
        pickedDoll = 0;
        //버프 계산
        CalcBuff();
    }

    /// <summary>
    /// 전체 버프 계산
    /// </summary>
    public void CalcBuff()
    {
        //타일 순회하면서 모든 버프 초기화
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].tileBuff.ResetTotalBuff();
            tiles[i].tileBuff.ResetTotalStats();
        }

        //타일 순회하면서 각 타일마다 버프 계산
        for (int i = 0; i < tiles.Length; i++)
        {
            //타일에 인형 없으면 continue
            if (tiles[i].doll == null)
                continue;

            //인형 버프 감산 옵션 시 선택인형이면 버프계산 안하고 continue
            if (lossBuff)
            {
                if (selectedDoll == tiles[i].doll)
                    continue;
            }

            //인형내 진형버프위치 순회하면서 버프를 줄 위치마다 계산
            var effect = tiles[i].doll.dollData.effect;
            for (int j = 0; j < effect.effectPos.Length; j++)
            {
                //버프를 줄 위치 계산
                var calcPos = CalcBuffPos(effect.effectPos[j], tiles[i].doll.pos, effect.effectCenter);
                if (calcPos == 0)
                    continue;

                //계산된 위치의 타일 target으로 할당
                var target = tiles[calcPos - 1];

                //해당 target에 줄 버프 종류별 순회
                for (int k = 0; k < effect.gridEffects.Length; k++)
                {
                    //데이터상 인형의 버프위치가 잘못 들어갈 경우 continue
                    if (effect.effectPos[j] - 1 < 0)
                        continue;

                    //target이 null이거나 target의 인형이 null일 경우 continue
                    if (target == null
                        || target.doll == null)
                        continue;

                    //해당 타겟의 인형이 버프의 타입과 맞거나, 버프의 타입이 All일경우 만
                    //버프를 누적계산해서 아래 SetTotalBuff에서 출력
                    if (effect.effectType == target.doll.dollData.type
                        || effect.effectType == DollType.All)
                    {
                        if (tiles[i].doll.dollData.type == DollType.HG)
                        {
                            //핸드건의 경우 2배 추가
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

        //타일을 순회하면서 누적된 버프를 전부 출력
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].tileBuff.SetTotalBuff();
        }
    }

    /// <summary>
    /// 선택된 인형이 주는 개별버프 계산
    /// </summary>
    /// <param name="pos">선택한 인형</param>
    public void CalcIndiBuff(int pos)
    {
        //타일 순회하면서 모든 개별버프 초기화
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].tileBuff.ResetIndiBuff();
        }

        //선택된 인형이 null이면 리턴
        if (tiles[pos - 1].doll == null)
            return;

        var effect = tiles[pos - 1].doll.dollData.effect;

        //인형내 진형버프위치 순회하면서 버프를 줄 위치마다 계산
        for (int i = 0; i < effect.effectPos.Length; i++)
        {
            //데이터상 버프위치가 잘못 들어갈 경우 continue
            if (effect.effectPos[i] - 1 < 0)
                continue;

            //실제 버프를 줄 위치 계산
            int calcPos = CalcBuffPos(effect.effectPos[i], tiles[pos - 1].doll.pos, effect.effectCenter);
            if (calcPos == 0)
                continue;

            //실제 버프를 줄 타일, target 으로 할당
            var target = tiles[calcPos - 1];

            if (target == null)
                continue;

            //해당 target에 알맞은 버프 추가 및 출력
            if (tiles[pos - 1].doll.dollData.type == DollType.HG)
            {
                target.tileBuff.AddIndiBuff(effect, 2);
            }
            else
            {
                target.tileBuff.AddIndiBuff(effect);
            }

            //버프를 받는 타일에 버프이미지 표시
            target.BuffImage();

            //버프의 타입이 All이거나, 해당 타일에 인형이 있으면 continue
            if (effect.effectType == DollType.All
                && target.doll != null)
                continue;

            // 인형이 없거나 버프타입이 일치하지 않을경우 버프의 알파값 변경
            if (target.doll == null
                || effect.effectType != target.doll.dollData.type)
            {
                target.tileBuff.cg.alpha = 0.6f;
            }
        }
    }

    /// <summary>
    /// 타일 순회하면서 모든 개별버프 초기화
    /// </summary>
    public void ResetIndiBuff()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].tileBuff.ResetIndiBuff();
        }
    }

    /// <summary>
    /// 전부 초기화
    /// </summary>
    /// <param name="showMsg">초기화 매시지를 출력할까?</param>
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

    /// <summary>
    /// 인형의 위치에 따른 실제 버프를 주는 위치 계산, 3x3 좌표 계산 함.
    /// </summary>
    /// <param name="effectPos">데이터상 버프를 주는 위치</param>
    /// <param name="dollPos">인형의 실제 위치</param>
    /// <param name="center">데이터상 인형의 센터 위치</param>
    /// <returns></returns>
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
