using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSimWarning : MonoBehaviour
{
    float currTime = 0f;
    public bool anim = false;
    void Update()
    {
        if(anim)
        {
            currTime += Time.deltaTime;
            if(currTime < 1f)
                transform.position = Vector3.Lerp(transform.position, Vector3.zero, currTime / 1f);

            if (currTime > 2.5f)
                gameObject.SetActive(false);
        }

    }
}
