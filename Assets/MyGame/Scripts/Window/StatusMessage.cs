using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusMessage : MonoBehaviour
{
    public Text message;
    public Animator anim;

    public float coolTime = 2.5f;
    float currTime;
    bool locked;

    private void Update()
    {
        if(locked)
        {
            currTime += Time.deltaTime;
            if (currTime > coolTime)
                locked = false;
        }
    }

    public void SetMsg(string msg)
    {
        if (locked)
            return;
        message.text = msg;
        anim.SetTrigger("On");
        locked = true;
        currTime = 0f;
    }
}
