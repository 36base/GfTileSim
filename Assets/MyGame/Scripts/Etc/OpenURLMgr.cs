using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLMgr : MonoBehaviour
{
    public GameObject[] ads;

    private GameObject currAds;

    private void Start()
    {
        currAds = ads[Random.Range(0, ads.Length)];
        currAds.SetActive(true);
    }

    public void OpenTestURL()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=kr.txwy.and.snqx&hl=ko");
    }

    public void CloseAd()
    {
        gameObject.SetActive(false);
    }
}
