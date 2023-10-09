using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public Define.SceneType SceneType { get; protected set; } = Define.SceneType.Unknown;

    void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {

    }

    public virtual void Clear()
    {

    }
}
