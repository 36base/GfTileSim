using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 인형 선택기, 소환할 grid 위치 판단
/// </summary>
public class DollSelecter : MonoBehaviour
{
    [System.Serializable]
    public class Select
    {
        public int gridPos;
        public Button btn;
        public Image image;
    }

    public Select[] selects;

    private void Awake()
    {
        for (int i = 0; i < selects.Length; i++)
        {
            SingleTon.instance.grid.tiles[i].selecter
                = selects[i];
        }
    }

    public void OpenDollList(int num)
    {
        if (SingleTon.instance.dollList.isWindow)
            return;

        SingleTon.instance.dollList.Open();
        SingleTon.instance.dollList.currentSelection = num;
    }

    public void ResetAll()
    {

        for (int i = 0; i < SingleTon.instance.grid.tiles.Length; i++)
        {
            SingleTon.instance.grid.tiles[i].selecter = null;
        }

        for (int i = 0; i < selects.Length; i++)
        {
            SingleTon.instance.grid.tiles[i].selecter = selects[i];
            selects[i].gridPos = i + 1;
            selects[i].image.sprite = SingleTon.instance.nullButtonSprite;
        }
    }
}
