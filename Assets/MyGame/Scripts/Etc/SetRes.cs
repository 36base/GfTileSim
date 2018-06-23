using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기준해상도 19:6 비율 이하일 경우 필러박스비활성,
/// 19:6비율 이상일 경우 필러박스보임
/// </summary>
public class SetRes : MonoBehaviour
{
#if UNITY_STANDALONE && !UNITY_EDITOR
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
#endif

    // Use this for initialization
    void Start()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight <= 1.000564f)
        {
            GameObject.Find("PillarBox").gameObject.SetActive(false);
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
