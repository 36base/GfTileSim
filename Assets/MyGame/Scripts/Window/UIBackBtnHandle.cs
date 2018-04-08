using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 윈도우 들 뒤로가기 처리 및 애니메이션 처리용 부모클래스
/// </summary>
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

    public virtual void OpenOrClose()
    {
        Window(!isWindow);
    }

    private void Window(bool value)
    {
        SingleTon.instance.UISound();
        isWindow = value;
        if (anim != null)
            anim.SetBool("isWindow", value);
        else if (anim == null)
        {
            gameObject.SetActive(value);
        }
    }
}
