using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongTransform : MonoBehaviour
{
    public Transform tr;

    public Vector3 startPos;
    public Vector3 endPos;

    private void Start()
    {
        tr.localPosition = startPos;
    }

    void Update()
    {
        tr.localPosition = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time * 0.1f, 1f));
    }
}
