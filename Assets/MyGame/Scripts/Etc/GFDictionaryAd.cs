using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GFDictionaryAd : MonoBehaviour
{
    public Transform[] phase1s;
    public Transform phase2;
    public Image phase3;
    private float currTime;

    private void Update()
    {
        currTime += Time.deltaTime;

        if (currTime > 12f)
        {
            return;
        }
        if (currTime > 8f)
        {
            phase3.color = new Color(1f, 1f, 1f, (currTime - 8f) / 3f);
            return;
        }
        if (currTime > 7f)
        {
            Slide(phase2, 7f);
            return;
        }
        if (currTime > 6f)
        {
            Slide(phase1s[4], 6f);
            return;
        }
        if (currTime > 5f)
        {
            Slide(phase1s[3], 5f);
            return;
        }
        if (currTime > 4f)
        {
            Slide(phase1s[2], 4f);
            return;
        }
        if (currTime > 3f)
        {
            Slide(phase1s[1], 3f);
            return;
        }
        if (currTime > 2f)
        {
            Slide(phase1s[0], 2f);
        }
    }
    private void Slide(Transform tr, float time)
    {
        tr.localPosition = Vector3.Lerp(tr.localPosition, Vector3.zero, Mathf.Clamp(currTime - time, 0f, 1f));
    }
}
