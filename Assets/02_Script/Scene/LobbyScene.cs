using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    GameObject ui_Lobby;
    GameObject player;
    GameObject renderCam;
    RenderTexture texture;

    protected override void Init()
    {
        base.Init();

        // UI ����
        ui_Lobby = Instantiate(Resources.Load<GameObject>("UI/Scene/UI_Lobby"));

        // Player ����
        player = Instantiate(Resources.Load<GameObject>("Objects/LobbyPlayer"));
        player.transform.position = new Vector3(-0.1f, -1.05f, 2.2f);
        player.transform.rotation = Quaternion.Euler(new Vector3(0, -210, 0));
        // ī�޶� ����
        renderCam = Instantiate(Resources.Load<GameObject>("Objects/RenderCamera"));
        // RenderTarget ����
        texture = Resources.Load<RenderTexture>("UI/LobbyPlayerTexture");
        renderCam.GetComponent<Camera>().targetTexture = texture;
    }

    public override void Clear()
    {
        Destroy(ui_Lobby);
        Destroy(player);
        Destroy(renderCam);
    }

}
