using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLMgr : MonoBehaviour
{
    public GameObject ad;

    public void OpenTestURL()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=kr.txwy.and.snqx&hl=ko");
    }

    public void CloseAd()
    {
        ad.SetActive(false);
    }
}
