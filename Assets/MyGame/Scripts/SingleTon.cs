using UnityEngine;
using System.Collections;

public class SingleTon : MonoBehaviour
{
    public static SingleTon instance;

    public DollManager mgr;

    public DollList dollList;
    public DollSelecter dollSelecter;
    public Grid grid;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
}
