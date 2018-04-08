using UnityEngine;
using System.Collections;

/// <summary>
/// 인형의 기본 클래스
/// </summary>
public class Doll : MonoBehaviour
{
    /// <summary>
    /// 인형 상태 구분
    /// </summary>
    public enum DollState
    {
        Idle,
        Selected,
        Following
    }
    public DollState state;

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

    private void Awake()
    {
        go = gameObject;
        tr = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        //팔로우 상태 시 포인터를 따라가게 끔
        if(state == DollState.Following)
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
        switch(state)
        {
            case DollState.Idle:
                skelAnim.AnimationName = "wait";
                break;
            case DollState.Following:
                skelAnim.AnimationName = "move";
                break;
            case DollState.Selected:
                skelAnim.AnimationName = "attack";
                break;
            default:
                skelAnim.AnimationName = "wait";
                break;
        }
    }
}
