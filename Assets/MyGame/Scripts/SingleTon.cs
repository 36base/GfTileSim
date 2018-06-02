using UnityEngine;
using System.Collections;

public class SingleTon : MonoBehaviour
{
    public static SingleTon instance;

    public bool IsWindow
    {
        get
        {
            return dollList.isWindow | dollPresetList.isWindow | presetCodeIField.isWindow | damageSim.isWindow;
        }
    }

    public DollManager mgr;

    public DollList dollList;
    public GameObject listTop;
    public DollPresetList dollPresetList;
    public PresetCodeInputField presetCodeIField;
    public DollSelecter dollSelecter;
    public Grid grid;
    public StatusMessage msg;
    public DollDiscripter dollDiscript;
    public Info info;

    public DamageSim damageSim;

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

    public static DollType GetDollType(string type)
    {
        switch(type)
        {
            case "all":
                return DollType.All;
            case "hg":
                return DollType.HG;
            case "smg":
                return DollType.SMG;
            case "rf":
                return DollType.RF;
            case "ar":
                return DollType.AR;
            case "mg":
                return DollType.MG;
            case "sg":
                return DollType.SG;
            default:
                return DollType.All;

        }
    }
}
