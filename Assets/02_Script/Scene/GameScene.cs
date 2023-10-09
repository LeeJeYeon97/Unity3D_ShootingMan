using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.Game;

        // UI 추가
        Instantiate(Resources.Load<GameObject>("UI/Scene/UI_JoyStick"));
        // 맵 추가

        // Player 생성        
        GameObject player = PhotonNetwork.Instantiate("Objects/Player", Vector3.zero, Quaternion.identity);
        
        // Manager 추가
        Managers.Pool.Init();

    }
    public override void Clear()
    {
        
    }
}
