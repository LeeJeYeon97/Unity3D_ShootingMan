using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;

    public static Managers Instance
    {
        get { return _instance; }
    }

    PoolManager _pool = new PoolManager();

    public static PoolManager Pool { get { return _instance._pool; }}


    private void Awake()
    {
        _instance = this;
        Init();
    }
    private void Init()
    {
        _pool.Init();
    }
}
