using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 게임 버전
    string gameVersion = "1.0";

    private string userId = "JeYeon";

    [SerializeField]
    private int maxPlayers = 2;

    public void Awake()
    {
        // 마스터 클라이언트의 씬 자동 동기화 옵션
        PhotonNetwork.AutomaticallySyncScene = true;

        // 게임 버전 설정
        PhotonNetwork.GameVersion = gameVersion;

        // 접속 유저의 닉네임 설정
        PhotonNetwork.NickName = userId;

        // 포톤 서버와의 데이터의 초당 전송 횟수
        Debug.Log(PhotonNetwork.SendRate);

        // 포톤 서버 접속
        Connect();
    }

    public void Connect()
    {
        if(PhotonNetwork.IsConnected) // 이미 연결되어 있으면 랜덤 룸 접속
        {
            Debug.Log("IsConnected!");
        }
        else // 연결 되어 있지않으면 포톤 서버 연결
        {
            Debug.Log("Connecting....");
            // 포톤 서버에 접속
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

    // 룸 참여 실패시 호출
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom(); // 룸 생성 호출
    }

    // 룸이 없으면 생성
    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;

        int roomNum = Random.Range(0, 100);
        PhotonNetwork.CreateRoom($"{roomNum}", roomOptions,null);
    }

    // 룸 참여 성공 시 호출
    public override void OnJoinedRoom()
    {
        Debug.Log($"OnJoinedRoom{PhotonNetwork.CurrentRoom.Name}");

        // UI 팝업 띄우기
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
        Debug.Log("방을 나갔습니다.");
    }

}
