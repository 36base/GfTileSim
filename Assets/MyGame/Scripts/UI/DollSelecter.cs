using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DollSelecter : MonoBehaviour
{
    [System.Serializable]
    public class Select
    {
        public int gridPos;
        public Button btn;
    }

    public Select[] selects;

    private void Awake()
    {
        for (int i = 0; i < selects.Length; i++)
        {
            SingleTon.instance.grid.tiles[selects[i].gridPos - 1].selecter
                = selects[i];
        }
    }

    public void OpenDollList(int num)
    {
        SingleTon.instance.dollList.Open();
        SingleTon.instance.dollList.currentSelection = num;
    }
}
