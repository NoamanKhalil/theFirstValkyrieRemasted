using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using UnityEngine.SceneManagement;
public class HealthCs : PunBehaviour
{

    [SerializeField]
    int shotsToDie;
    int shotsToHealthDrop;

    // to check current game mode
    private bool isSinglePlayerActive;
    private bool isMultiplayerActive;
    private bool isLocalMultiplayerActive;

    private GameManager gm;
    public bool isPlayerOne;

    public GameObject health;
    public GameObject PlayerName;
    // Use this for initialization
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        isSinglePlayerActive = gm.isSinglePlayerCheck();
        isMultiplayerActive = gm.isMultiplayerCheck();
        isLocalMultiplayerActive = gm.isLocalMultiplayerCheck();

        if (isPlayerOne && isSinglePlayerActive)
        {

            health = GameObject.Find("Player_txt");
        }
        if (isMultiplayerActive&& isPlayerOne)
        {
            //health = GameObject.Find("Player_txt");
            health = GameObject.Find("Player0_txt");
            PlayerName = GameObject.Find("PlayerName0");
        }
        if (isMultiplayerActive && !isPlayerOne)
        {

            health = GameObject.Find("Player_txt");
            PlayerName = GameObject.Find("PlayerName");
        }
        if (isLocalMultiplayerActive|| !isPlayerOne)
        {
            health = GameObject.Find("Player_txt");
            PlayerName = GameObject.Find("PlayerName");
        }
        //PhotonNetwork.playerName = PlayerPrefs.GetString("pName");
        if (photonView.isMine)
        {
            PhotonNetwork.playerName = PlayerPrefs.GetString("pName");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine && isMultiplayerActive)
        {
            return;
        }

          health.GetComponent<Text>().text = ": " + shotsToDie;
        if (isMultiplayerActive)
        {
            PlayerName.GetComponent<Text>().text = photonView.owner.NickName; // "Name :" + PlayerPrefs.GetString("pName");
        }


        if (shotsToDie <= 0)
        {
            if (isMultiplayerActive)
            {
                looseLife();
            }
            else if (isSinglePlayerActive)
            {
                SceneManager.LoadScene("Die");
            }


        }

    }
    void looseLife()
    {
        photonView.RPC("win", PhotonTargets.All);
    }
    [PunRPC]
    void win()
    {
        if (photonView.isMine)
        {
            SceneManager.LoadScene("Die");
        }
        else
        {
            SceneManager.LoadScene("Win");

        }
    }
    public void TakeDamage()
    {
        shotsToDie--;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.shotsToDie);
            //stream.SendNext(this.health.GetComponent<Text>().text);
            stream.SendNext(this.PlayerName.GetComponent<Text>().text);
            // stream.SendNext ()
        }
        else
        {
            this.shotsToDie = (int)stream.ReceiveNext();
           // this.health.GetComponent<Text>().text = (string)stream.ReceiveNext();
            this.PlayerName.GetComponent<Text>().text = (string)stream.ReceiveNext();
        }
    }
}
