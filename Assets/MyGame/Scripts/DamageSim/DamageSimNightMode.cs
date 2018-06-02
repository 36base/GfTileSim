using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class DamageSimNightMode : MonoBehaviour
{
    public Sprite sun;
    public Sprite moon;

    public Image daily;
    public Image buttonDaily;
    public Image dim;
    public UIGridRenderer grid;
    public Image whiteBoard;

    private float currTime = 0f;
    private bool night = false;
    private bool anim = false;
    private void Update()
    {
        if (anim)
        {
            currTime += Time.deltaTime;
            if (currTime > 2f)
            {
                anim = false;
                return;
            }

            if (night)
            {
                var value = Mathf.Lerp(1, 0, currTime / 2f);
                dim.color = new Color(value, value, value, 0.5f);
            }
            else
            {
                var value = Mathf.Lerp(0, 1, currTime / 2f);
                dim.color = new Color(value, value, value, 0.5f);
            }
        }
    }

    public void ChangeNightMode()
    {
        daily.sprite = moon;
        buttonDaily.sprite = sun;
        night = true;
        anim = true;
        currTime = 0f;

        whiteBoard.color = new Color32(73, 73, 73, 255);
        grid.color = new Color32(151, 161, 104, 255);
    }
    public void ChangeDayMode()
    {
        daily.sprite = sun;
        buttonDaily.sprite = moon;
        night = false;
        anim = true;
        currTime = 0f;

        whiteBoard.color = new Color32(251, 255, 233, 255);
        grid.color = Color.black;
    }
}
