using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public GameObject BattleButton;
    public GameObject CancelButton;
    public static PhotonLobby lobby;

    void Awake()
    {
        if (lobby == null)
            lobby = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("player has successfully onnected to photon master server");

        BattleButton.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Random joining failed");

        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        print("'" + PhotonNetwork.CurrentRoom.Name + "' room joined");
    }



    void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room " + Random.Range(0, 10000), roomOptions);
    }

    public void BattleButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();

        BattleButton.SetActive(false);
        CancelButton.SetActive(true);
    }

    public void CancelButtonClicked()
    {
        CancelButton.SetActive(false);
        BattleButton.SetActive(true);

        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {

    }
}