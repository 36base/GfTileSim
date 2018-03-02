using UnityEngine;
using System.Collections;

public class SingleTon : MonoBehaviour
{
    public static SingleTon instance;

    public DollManager mgr;

    public DollList dollList;
    public DollSelecter dollSelecter;
    public Grid grid;
    public StatusMessage msg;

    [Header("Sprites")]
    public Sprite selectedSprite;
    public Sprite buffSprite;
    public Sprite nullSprite;

    public Sprite nullButtonSprite;

    public Sprite[] gunTypeSprites;
    public Sprite[] statTypeSprites;
    [Header("Prefabs")]
    public GameObject buffPrefab;
    public GameObject indiBuffPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
