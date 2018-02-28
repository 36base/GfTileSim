using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalBuffSetter : MonoBehaviour
{
    public GameObject buffPrefab;
    public Sprite[] allStatsSprites = new Sprite[15];

    public Buff[] buffs;

    public Transform[] tiles;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < 6; j++)
                Instantiate(buffPrefab).transform.SetParent(tiles[i]);
        }


    }
}
