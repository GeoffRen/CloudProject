﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonNetworkManager : Photon.MonoBehaviour 
{

    public GameObject lobbyCamera;
    public GameObject light;
    public GameObject player;
    [SerializeField] public Transform[] spawnPoints;

    private const string GAME_VERSION = "0.1";

    void Awake () 
    {
        Debug.Log ("Connect to network");
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.ConnectUsingSettings (GAME_VERSION);
    }

//    void Update()
//    {
//        Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
//    }
    
    public virtual void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinOrCreateRoom("Room0", null, null);
    }

    public virtual void OnCreatedRoom()
    {
        Debug.Log("Created room");
    }

    public virtual void OnJoinedRoom() 
    {
        Debug.Log ("Joined room");
        lobbyCamera.SetActive (false);
		light.SetActive (false);
        int spawnPoint = (PhotonNetwork.playerList.Length - 1) % spawnPoints.Length;
        Debug.Log("Current number of players: " + PhotonNetwork.playerList.Length);
        Debug.Log("Spawning at spawn number " + spawnPoint);
        PhotonNetwork.Instantiate (player.name, spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation, 0);
    }

    public virtual void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("Player connected");
    }

    public virtual void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log("Player disconnected");
    }

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("Couldn't connect to Photon network");
    }

    public virtual void OnConnectionFail(DisconnectCause cause)
    {
        Debug.Log("Connection failed to the Photon network");
    }

    public virtual void OnDisconnectedFromPhoton()
    {
        Debug.Log("We got disconnected from the Photon network");
    }
}

