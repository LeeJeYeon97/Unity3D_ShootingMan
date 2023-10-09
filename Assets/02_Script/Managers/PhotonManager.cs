using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // ���� ����
    string gameVersion = "1.0";

    private string userId = "JeYeon";

    [SerializeField]
    private int maxPlayers = 2;

    public void Awake()
    {
        // ������ Ŭ���̾�Ʈ�� �� �ڵ� ����ȭ �ɼ�
        PhotonNetwork.AutomaticallySyncScene = true;

        // ���� ���� ����
        PhotonNetwork.GameVersion = gameVersion;

        // ���� ������ �г��� ����
        PhotonNetwork.NickName = userId;

        // ���� �������� �������� �ʴ� ���� Ƚ��
        Debug.Log(PhotonNetwork.SendRate);

        // ���� ���� ����
        Connect();
    }

    public void Connect()
    {
        if(PhotonNetwork.IsConnected) // �̹� ����Ǿ� ������ ���� �� ����
        {
            Debug.Log("IsConnected!");
        }
        else // ���� �Ǿ� ���������� ���� ���� ����
        {
            Debug.Log("Connecting....");
            // ���� ������ ����
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
    }

    public void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // �� ���� ���н� ȣ��
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom(); // �� ���� ȣ��
    }

    // ���� ������ ����
    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;

        int roomNum = Random.Range(0, 100);
        PhotonNetwork.CreateRoom($"{roomNum}", roomOptions,null);
    }

    // �� ���� ���� �� ȣ��
    public override void OnJoinedRoom()
    {
        Debug.Log($"OnJoinedRoom{PhotonNetwork.CurrentRoom.Name}");

        // UI �˾� ����
        Managers.UI.ShowPopupUI<UI_Popup_Matching>();

        if(PhotonNetwork.IsMasterClient)
        {
            Managers.Scene.LoadLevel(Define.SceneType.Game);
        }
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Debug.Log("���� �������ϴ�.");
    }

}
