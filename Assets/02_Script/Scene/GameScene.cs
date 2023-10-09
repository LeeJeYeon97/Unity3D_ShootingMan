using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.Game;

        // UI �߰�
        Instantiate(Resources.Load<GameObject>("UI/Scene/UI_JoyStick"));
        // �� �߰�

        // Player ����        
        GameObject playerPrefabs = Resources.Load<GameObject>("UI/Object/Player");
        GameObject player = Instantiate(playerPrefabs, Vector3.zero, Quaternion.identity);
        
        // Manager �߰�
        Managers.Pool.Init();

    }
    public override void Clear()
    {
        
    }
}
