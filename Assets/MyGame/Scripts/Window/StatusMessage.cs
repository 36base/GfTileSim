using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusMessage : MonoBehaviour
{
    public Text message;
    public Animator anim;


    public void SetMsg(string msg)
    {
        message.text = msg;
        anim.SetTrigger(0);
    }
}
