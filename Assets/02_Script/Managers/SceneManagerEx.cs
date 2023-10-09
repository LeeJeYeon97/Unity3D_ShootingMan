using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx 
{

    public BaseScene CurrentScene
    {
        get { return GameObject.FindObjectOfType<BaseScene>(); }
    }
    public void LoadScene(Define.SceneType type)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    private string GetSceneName(Define.SceneType type)
    {
        return System.Enum.GetName(typeof(Define.SceneType), type);
    }
    public void LoadLevel(Define.SceneType type)
    {
        PhotonNetwork.LoadLevel(GetSceneName(type));
    }
    
}
