using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인형의 기본 클래스
/// </summary>
public class Doll : MonoBehaviour
{
    /// <summary>
    /// 인형 기본 상태 구분
    /// </summary>
    public enum DollState
    {
        Idle,
        Selected,
        Following
    }
    public DollState state;

    /// <summary>
    /// 인형 모션 재생 상태
    /// </summary>
    public enum DollAnimState
    {
        attack,
        die,
        move,
        s,
        victory,
        wait
    }
    public DollAnimState animState;

    private Camera cam;

    public int id;
    /// <summary>
    /// 인형 데이터
    /// </summary>
    public DollData dollData;
    /// <summary>
    /// 인형 스켈레톤 애니메이션
    /// </summary>
    public SkeletonAnimation skelAnim;
    public Sprite profilePic;

    [HideInInspector]
    public GameObject go;
    [HideInInspector]
    public Transform tr;

    public int pos = 0;
    public DollSelecter.Select selecter;

    private bool initializedEvent = false;

    private void Awake()
    {
        go = gameObject;
        tr = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        //팔로우 상태 시 포인터를 따라가게 끔
        if (state == DollState.Following)
        {
            var z = tr.position.z;
            var followPos = cam.ScreenToWorldPoint(Input.mousePosition);
            followPos.z = z;
            tr.position = followPos;
        }
    }

    public void Spawn(Transform tr)
    {
        this.tr.localPosition = tr.localPosition;
        go.SetActive(true);
    }

    public void Despawn()
    {
        go.SetActive(false);
    }

    /// <summary>
    /// 인형 상태 변경
    /// </summary>
    /// <param name="state"></param>
    public void SetState(DollState state)
    {
        this.state = state;
        skelAnim.loop = true;
        switch (state)
        {
            case DollState.Idle:
                skelAnim.AnimationName = "wait";
                animState = DollAnimState.wait;
                break;
            case DollState.Following:
                skelAnim.AnimationName = "move";
                animState = DollAnimState.move;
                break;
            case DollState.Selected:
                skelAnim.AnimationName = "attack";
                animState = DollAnimState.attack;
                break;
            default:
                skelAnim.AnimationName = "wait";
                animState = DollAnimState.wait;
                break;
        }
    }

    /// <summary>
    /// 애니메이션 순차 변경
    /// 없는 애니메이션은 안나오게 처리
    /// attack(loop) -> s(loop) -> move(loop) -> die(nonloop) -> victory(->victoryloop) -> wait(loop) -> attack(loop)
    /// 카리나 예외
    /// </summary>
    public void ChangeDollAnimation()
    {
        if (id == 999)
            return;

        if (!initializedEvent) InitDollEvent();


        switch(animState)
        {
            case DollAnimState.attack:
                skelAnim.loop = true;
                if (FindAnimation("s"))
                {
                    skelAnim.AnimationName = "s";
                    animState = DollAnimState.s;
                }
                else
                {
                    skelAnim.AnimationName = "move";
                    animState = DollAnimState.move;
                }
                break;
            case DollAnimState.s:
                skelAnim.loop = true;
                skelAnim.AnimationName = "move";
                animState = DollAnimState.move;
                break;
            case DollAnimState.move:
                skelAnim.loop = false;
                skelAnim.AnimationName = "die";
                animState = DollAnimState.die;
                break;
            case DollAnimState.die:
                if(FindAnimation("victoryloop"))
                {
                    skelAnim.loop = false;
                }
                else
                {
                    skelAnim.loop = true;
                }
                skelAnim.AnimationName = "victory";
                animState = DollAnimState.victory;
                break;
            case DollAnimState.victory:
                skelAnim.loop = true;
                skelAnim.AnimationName = "wait";
                animState = DollAnimState.wait;
                break;
            case DollAnimState.wait:
                skelAnim.loop = true;
                skelAnim.AnimationName = "attack";
                animState = DollAnimState.attack;
                break;
        }


    }

    private void InitDollEvent()
    {
        initializedEvent = true;
        skelAnim.state.Event += CallBackOnComplete;
    }

    private void CallBackOnComplete(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {
        if (animState == DollAnimState.victory)
        {
            skelAnim.loop = true;
            skelAnim.AnimationName = "victoryloop";
        }
    }
    /// <summary>
    /// 애니메이션 검색
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private bool FindAnimation(string name)
    {
        var animations = skelAnim.state.Data.skeletonData.animations;
        for (int i = 0; i < animations.Count; i++)
        {
            var ani = animations[i];
            if (ani.name == name) return true;
        }
        return false;
    }
}
