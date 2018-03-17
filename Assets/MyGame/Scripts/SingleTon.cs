using UnityEngine;
using System.Collections;

public class SingleTon : MonoBehaviour
{
    public static SingleTon instance;

    private bool isWindow;//메뉴 제외 윈도우 확인.
    public bool IsWindow
    {
        get
        {
            return dollList.isWindow | dollPresetList.isWindow;
        }
    }

    public DollManager mgr;

    public DollList dollList;
    public DollPresetList dollPresetList;
    public DollSelecter dollSelecter;
    public Grid grid;
    public StatusMessage msg;
    public DollDiscripter dollDiscript;
    public Info info;

    [Header("Sprites")]
    public Sprite selectedSprite;
    public Sprite buffSprite;
    public Sprite nullSprite;

    public Sprite nullButtonSprite;
    public Sprite nullButtonSprite_dark;

    public Sprite[] gunTypeSprites;
    public Sprite[] statTypeSprites;
    [Header("Prefabs")]
    public GameObject buffPrefab;
    public GameObject indiBuffPrefab;

    [Header("Sounds")]
    public bool mute;
    public AudioSource audioSource;
    public AudioClip uiSound;
    public AudioClip stateSound;

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

    public void StateSound()
    {
        if (mute)
            return;
        audioSource.PlayOneShot(stateSound);
    }

    public void UISound()
    {
        if (mute)
            return;
        audioSource.PlayOneShot(uiSound);
    }

    public void ResetAll(bool showMsg = true)
    {
        instance.grid.ResetAll(showMsg);
        instance.dollSelecter.ResetAll();
        instance.info.OffInfo();
    }
}
