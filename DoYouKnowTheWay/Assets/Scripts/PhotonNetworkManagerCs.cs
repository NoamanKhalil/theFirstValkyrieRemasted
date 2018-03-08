using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class PhotonNetworkManagerCs :Photon.PunBehaviour
{
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private GameObject[] Player;
    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private Text networkUpdateText;
    
    private PhotonView photonView;

    private int spawnCount;
	// Use this for initialization
	void Start ()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        photonView = GetComponent<PhotonView>();
        spawnCount = 0;
	}
    public virtual void OnJoinedLobby()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        //PhotonNetwork.Instantiate(Player[spawnCount].name, spawnPoints[spawnCount].transform.position, spawnPoints[spawnCount].transform.rotation, 0);
        PhotonNetwork.Instantiate(Player[PhotonNetwork.room.PlayerCount-1].name, spawnPoints[PhotonNetwork.room.PlayerCount-1].transform.position, spawnPoints[PhotonNetwork.player.ID - 1].transform.rotation, 0);
       
        if (Camera !=null)
        Camera.SetActive(false);
    }

    
	// Update is called once per frame
	void Update ()
    {
        networkUpdateText.text ="Stat:" + PhotonNetwork.connectionStateDetailed.ToString () +
            " Ping: " + PhotonNetwork.GetPing()+"ms"; 
	}

    
}
