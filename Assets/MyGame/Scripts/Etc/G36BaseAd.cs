using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G36BaseAd : MonoBehaviour
{
    public Image g36Image;
    public Image title;
    public Transform subTitle;
    private float currTime;

    private void Update()
    {
        currTime += Time.deltaTime;
        if (currTime > 12f)
            return;
        if(currTime < 5f)
        {
            var value = (byte)Mathf.Lerp(15, 255, currTime / 5f);
            title.color = new Color32(value, value, value, 255);
        }

        if (currTime > 2f && currTime < 7f)
        {
            g36Image.transform.localPosition
                = Vector3.Lerp(g36Image.transform.localPosition, Vector3.zero, (currTime - 2f) / 5f);
            var value = (byte)Mathf.Lerp(15, 255, (currTime - 2f) / 5f);
            g36Image.color = new Color32(value, value, value, 255);
        }


        if (currTime > 4f)
        {
            subTitle.localPosition = Vector3.Lerp(subTitle.localPosition, Vector3.zero, (currTime - 4f) / 8f);
        }

    }
}
