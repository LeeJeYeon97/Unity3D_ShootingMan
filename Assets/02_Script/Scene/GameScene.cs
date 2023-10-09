using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject playerPrefabs = Resources.Load<GameObject>("UI/Object/Player");
        GameObject player = Instantiate(playerPrefabs, Vector3.zero, Quaternion.identity);
        
        // Manager 추가
        Managers.Pool.Init();

    }
    public override void Clear()
    {
        
    }
}
