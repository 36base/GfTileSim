using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DollList : UIBackBtnHandle
{
    public GameObject dollButtonPrefab;
    public Grid grid;

    public Transform content;

    public int currentSelection;

    public void AddContent(Doll doll)
    {
        var go = Instantiate(dollButtonPrefab, content.position, Quaternion.identity)
            as GameObject;
        go.transform.SetParent(content, false);
        go.GetComponent<Button>().onClick.AddListener(() => SelectDoll(doll.id));
        go.GetComponent<Image>().sprite = doll.profilePic;
    }

    public void SelectDoll(int num)
    {
        if (!isWindow)
            return;
        if (currentSelection < 1)
            return;

        Doll doll;
        if (SingleTon.instance.mgr.dollDict.TryGetValue(num, out doll))
        {
            grid.Spawn(doll
                , SingleTon.instance.dollSelecter.selects[currentSelection - 1]);
        }
        Close();
    }
}
