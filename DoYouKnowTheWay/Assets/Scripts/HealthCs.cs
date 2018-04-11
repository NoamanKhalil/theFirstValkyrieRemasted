using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon; 
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
        if (isMultiplayerActive|| isLocalMultiplayerActive&& isPlayerOne)
        {
            //health = GameObject.Find("Player_txt");
            health = GameObject.Find("Player0_txt");
            PlayerName = GameObject.Find("PlayerName0");
        }
         if (isLocalMultiplayerActive|| !isPlayerOne)
        {
            health = GameObject.Find("Player_txt");
            PlayerName = GameObject.Find("PlayerName");
        }
    }

    // Update is called once per frame
    void Update()
    {
        health.GetComponent<Text>().text = ": " + shotsToDie;
        PlayerName.GetComponent<Text>().text = "Name :" +PlayerPrefs.GetString("pName");
        if (shotsToDie <= 0)
        {
            gm.OnPlayerDie();
            Destroy(this.gameObject);
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
            stream.SendNext(this.health.GetComponent<Text>().text);
            stream.SendNext(this.PlayerName.GetComponent<Text>().text);
            // stream.SendNext ()
        }
        else
        {
            this.shotsToDie = (int)stream.ReceiveNext();
            this.health.GetComponent<Text>().text = (string)stream.ReceiveNext();
            this.PlayerName.GetComponent<Text>().text = (string)stream.ReceiveNext();
        }
    }
}
