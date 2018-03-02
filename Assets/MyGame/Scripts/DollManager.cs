using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class DollManager : MonoBehaviour
{
    public TextAsset dollJson;

    public GameObject dollPrefab;
    public Transform dollPool;

    public Dictionary<int, Doll> dollDict = new Dictionary<int, Doll>();

    public string[] AtlasPath;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        MakeDollData(JsonMapper.ToObject(dollJson.text));
    }

    private void MakeDollData(JsonData data)
    {
        var count = data.Count;
        var list = new List<DollData>();
        for (int i = 0; i < count; i++)
        {
            var dolldata = new DollData();
            try
            {
                dolldata.id = (int)data[i]["id"];
                dolldata.name = data[i]["name"].ToString();
                dolldata.rank = (int)data[i]["rank"];
                dolldata.type = (DollType)(int)data[i]["type"];


                var effect = new Effect();

                effect.effectType = (DollType)(int)data[i]["effect"]["effectType"];
                effect.effectCenter = (int)data[i]["effect"]["effectCenter"];

                var arrayCount = data[i]["effect"]["effectPos"].Count;
                effect.effectPos = new int[arrayCount];

                for (int j = 0; j < arrayCount; j++)
                {
                    effect.effectPos[j] = (int)data[i]["effect"]["effectPos"][j];
                }

                arrayCount = data[i]["effect"]["gridEffect"].Count;
                effect.gridEffects = new Effect.GridEffect[arrayCount];

                for (int j = 0; j < arrayCount; j++)
                {
                    effect.gridEffects[j] = new Effect.GridEffect(
                        data[i]["effect"]["gridEffect"][j]["type"].ToString()
                        , (int)data[i]["effect"]["gridEffect"][j]["value"]);
                }

                dolldata.effect = effect;
            }
            catch
            {
                Debug.Log("Wrong json data " + i);
                continue;
            }

            list.Add(dolldata);
        }

        var sb = new StringBuilder();
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
        }

        for (int i = 0; i < AtlasPath.Length; i++)
        {
            LoadFromAtlas(AtlasPath[i]);
        }

    }

    private void LoadFromAtlas(string path)
    {
        var pics = Resources.LoadAll<Sprite>(path);
        if (pics == null)
            return;

        for (int i = 0; i < pics.Length; i++)
        {
            int key;
            Doll doll;
            //tryparse : false일때 key는 0반환
            if (Int32.TryParse(pics[i].name, out key))
            {
                if (dollDict.TryGetValue(key, out doll))
                {
                    doll.profilePic = pics[i];
                    SingleTon.instance.dollList.AddContent(doll);
                }
                else
                {
                    Debug.Log("There is no Key " + key);
                }
            }
            else
            {
                Debug.Log("Convert Failed " + pics[i].name);
            }
        }
    }

    public void AppExit()
    {
        Application.Quit();
    }
}
