using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingPongText : MonoBehaviour
{
    public Text loadText;

    private void Update()
    {
        loadText.color
            = new Color(loadText.color.r, loadText.color.g, loadText.color.b, Mathf.PingPong(Time.time, 1));
    }

}
