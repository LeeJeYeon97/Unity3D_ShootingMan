using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;

    public static Managers Instance
    {
        get { return _instance; }
    }

    //PhotonManager _photon = new PhotonManager();
    PoolManager _pool = new PoolManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();
    

    //public static PhotonManager Photon { get { return _instance._photon; } }
    public static PoolManager Pool { get { return _instance._pool; }}
    public static SceneManagerEx Scene { get { return _instance._scene; } }
    public static UIManager UI { get { return _instance._ui; }}

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        Init();
    }
    private void Init()
    {
        
    }
}
