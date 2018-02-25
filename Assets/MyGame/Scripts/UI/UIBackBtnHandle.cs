using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackBtnHandle : MonoBehaviour
{
    public bool isWindow = false;
    public Animator anim;
    public bool backBtnHandle;

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && backBtnHandle && isWindow)
        {
            Close();
        }
    }

    public virtual void Open()
    {
        if (isWindow)
            return;
        Window(true);
    }
    public virtual void Close()
    {
        if (!isWindow)
            return;
        Window(false);
    }

    private void Window(bool value)
    {
        isWindow = value;
        if (anim != null)
            anim.SetBool("isWindow", value);
        else if (anim == null)
        {
            gameObject.SetActive(value);
        }
    }
}
