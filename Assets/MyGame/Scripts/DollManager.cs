using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using LitJson;

/// <summary>
/// 초기화 시 Json 데이터를 이용 인형데이터를 가공하는 클래스
/// </summary>
public class DollManager : MonoBehaviour
{
    public TextAsset dollJson;

    /// <summary>
    /// 인형 프리팹
    /// </summary>
    public GameObject dollPrefab;
    /// <summary>
    /// 생성된 인형들을 담아둘 곳
    /// </summary>
    public Transform dollPool;
    /// <summary>
    /// 생성된 모든 인형 을 담는 딕셔너리
    /// </summary>
    public Dictionary<int, Doll> dollDict = new Dictionary<int, Doll>();

    /// <summary>
    /// 인형 프로필 액자사진 아틀라스 경로
    /// </summary>
    public string[] AtlasPath;

    /// <summary>
    /// 데이터 로드 이후 활성화 할 오브젝트들
    /// </summary>
    public GameObject[] afterLoadingObjects;
    /// <summary>
    /// 데이터 로드 이후 비활성화 할 오브젝트들
    /// </summary>
    public GameObject beforeLoadingObject;
    /// <summary>
    /// 로드 진행 슬라이더 바
    /// </summary>
    public Slider loadingSlider;
    /// <summary>
    /// cutCount 만큼 로드 진행후 슬라이더 갱신합니다
    /// </summary>
    public int cutCount = 20;


    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        StartCoroutine(MakeDollData(JsonMapper.ToObject(dollJson.text)));
    }

    /// <summary>
    /// 해당 코루틴에서 모든 데이터 로드 및 로딩진행 표기 합니다.
    /// 비동기씬로드로 로드진행상황 표시 안됩니다. (슬라이더 90%에서 진행이됨)
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private IEnumerator MakeDollData(JsonData data)
    {
        var count = data.Count;
        var list = new List<DollData>();

        var keys = new List<string>();

        for (int i = 0; i < count; i++)
        {
            var dolldata = new DollData();
            try
            {
                dolldata.id = (int)data[i]["id"];
                dolldata.name = data[i]["name"].ToString();
                dolldata.krName = data[i]["krName"].ToString();
                dolldata.rank = (int)data[i]["rank"];
                dolldata.type = SingleTon.GetDollType(
                    data[i]["type"].ToString());

                /////Grid Effect/////
                var effect = new Effect();

                effect.effectType = SingleTon.GetDollType(
                    data[i]["effect"]["effectType"].ToString());
                effect.effectCenter = (int)data[i]["effect"]["effectCenter"];

                var arrayCount = data[i]["effect"]["effectPos"].Count;
                effect.effectPos = new int[arrayCount];

                for (int j = 0; j < arrayCount; j++)
                {
                    effect.effectPos[j] = (int)data[i]["effect"]["effectPos"][j];
                }

                arrayCount = data[i]["effect"]["gridEffect"].Count;
                effect.gridEffects = new Effect.GridEffect[arrayCount];

                keys.Clear();
                keys.AddRange(data[i]["effect"]["gridEffect"].Keys);

                for (int j = 0; j < arrayCount; j++)
                {
                    effect.gridEffects[j] = new Effect.GridEffect(keys[j]
                        , (int)data[i]["effect"]["gridEffect"][keys[j]]);
                }

                dolldata.effect = effect;
                /////Grid Effect/////

                /////Doll Stat/////
                dolldata.stat = new DollStat();

                keys.Clear();
                keys.AddRange(data[i]["stats"].Keys);
                arrayCount = data[i]["stats"].Count;

                for (int j = 0; j < arrayCount; j++)
                {
                    dolldata.stat.SetStat(keys[j], (int)data[i]["stats"][keys[j]]);
                }
                /////Doll Stat/////

                dolldata.grow = (int)data[i]["grow"];
            }
            catch
            {
                Debug.Log("Wrong json data " + i);
                continue;
            }

            list.Add(dolldata);
        }

        var sb = new StringBuilder();

        int myCutCount = 0;
        int totalCount = list.Count * 2;
        int currCount = 0;

        for (int i = 0; i < list.Count; i++)
        {
            sb.Length = 0;
            sb.Append("character/");
            sb.Append(list[i].name);
            sb.Append("/");
            sb.Append(list[i].name);
            sb.Append("_SkeletonData");

            var skel = Resources.Load<SkeletonDataAsset>(sb.ToString());
            if (skel == null)
            {
                Debug.Log("Null " + list[i].name);
                continue;
            }

            var go = Instantiate(dollPrefab, dollPool.position, Quaternion.identity);
            var doll = go.GetComponent<Doll>();
            doll.tr.SetParent(dollPool, false);

            doll.id = list[i].id;
            doll.dollData = list[i];
            doll.skelAnim.skeletonDataAsset = skel;
            doll.skelAnim.Reset();

            go.SetActive(false);

            dollDict.Add(list[i].id, doll);

            //LoadingBar
            myCutCount++;
            currCount++;
            if (myCutCount > cutCount)
            {
                myCutCount = 0;
                loadingSlider.value = (float)currCount / totalCount;
                yield return null;
            }
            //LoadingBar
        }

        //LoadFromAtlas
        for (int i = 0; i < AtlasPath.Length; i++)
        {
            var pics = Resources.LoadAll<Sprite>(AtlasPath[i]);

            for (int j = 0; j < pics.Length; j++)
            {
                int key;
                Doll doll;
                //tryparse : false일때 key는 0반환
                if (Int32.TryParse(pics[j].name, out key))
                {
                    if (dollDict.TryGetValue(key, out doll))
                    {
                        doll.profilePic = pics[j];
                        SingleTon.instance.dollList.AddContent(doll);
                    }
                    else
                    {
                        Debug.Log("There is no Key " + key);
                    }
                }
                else
                {
                    Debug.Log("Convert Failed " + pics[j].name);
                }

                //LoadingBar
                myCutCount++;
                currCount++;
                if (myCutCount > cutCount)
                {
                    myCutCount = 0;
                    loadingSlider.value = (float)currCount / totalCount;
                    yield return null;
                }
                //LoadingBar
            }
        }

        //로드데이터처리 및 프리셋 초기화
        SingleTon.instance.dollPresetList.InitAllPresets();

        beforeLoadingObject.SetActive(false);
        for (int i = 0; i < afterLoadingObjects.Length; i++)
        {
            afterLoadingObjects[i].SetActive(true);
        }
        //Debug.Log(Time.time);

    }

    //private void LoadFromAtlas(string path)
    //{

    //}

    public void AppExit()
    {
        Application.Quit();
    }
}
