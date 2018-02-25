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

    public void OpenDollList(int num)
    {
        SingleTon.instance.dollList.Open();
        SingleTon.instance.dollList.currentSelection = num;
    }
}
