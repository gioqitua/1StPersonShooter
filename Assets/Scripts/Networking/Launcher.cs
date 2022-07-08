using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Launcher : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }
    public void Connect()
    {
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Join();
        base.OnConnectedToMaster();
    }
    public override void OnJoinedRoom()
    {
        StartGame();
        base.OnJoinedRoom();
    }
    public void Join()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Create();
        base.OnJoinRandomFailed(returnCode, message);
    }

    private void Create()
    {
        PhotonNetwork.CreateRoom("");
    }
}
