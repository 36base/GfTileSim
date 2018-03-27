using UnityEngine;
using System.Collections;

public class Doll : MonoBehaviour
{
    public enum DollState
    {
        Idle,
        Selected,
        Following
    }
    public DollState state;

    private Camera cam;

    public int id;

    public DollData dollData;
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
