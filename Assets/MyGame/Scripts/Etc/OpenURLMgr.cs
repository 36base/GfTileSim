using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLMgr : MonoBehaviour
{
    public GameObject[] ads;

    private GameObject currAds;

    private void Start()
    {
        //if(PlayerPrefs.HasKey("showAd"))
        //{
        //    if (PlayerPrefs.GetInt("showAd") != 1)
        //        return;
        //}

        currAds = ads[Random.Range(0, ads.Length)];
        currAds.SetActive(true);
    }

    public void OpenGFURL()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=kr.txwy.and.snqx&hl=ko");
    }
    public void Open36BaseURL()
    {
        Application.OpenURL("https://girlsfrontline.kr/");
    }
    public void OpenGFDictionaryURL()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gfl.dic");
    }
    public void OpenGoogleSheetURL()
    {
        Application.OpenURL("https://docs.google.com/spreadsheets/d/1IxJxfpBHboVRJe92_GPC6iUZCq1M2NJkbtKo6SP-3SM");
    }

    public void CloseAd()
    {
        gameObject.SetActive(false);
    }
}
