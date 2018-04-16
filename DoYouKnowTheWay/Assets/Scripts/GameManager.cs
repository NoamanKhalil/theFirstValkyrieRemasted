using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Game Settings
    [Header("Game Settings")]
    [SerializeField]
    private bool isSinglePlayer;
    [SerializeField]
    private bool isLocalMultiplayer;
    [SerializeField]
    private bool isMultiPlayer;
    #endregion

    #region Managers 
    [Header("Managers")]
    [SerializeField]
    private PhotonNetworkManagerCs photonManager;
    public SpawnSystemCs spawnSystem;
    #endregion

    #region Ui Stuff 
    [Header("UI Canvas")]
    [SerializeField]
    GameObject[] myCanvas;
    #endregion

    #region Score Stuff 
    [Header("Score")]
    [SerializeField]
    float playerScore, playerScore0;
    [SerializeField]
    Text[] playerScoreTxt;
    #endregion

    [Header("Buttons to Disable")]
    private GameObject[] Buttons;

    #region Player objects 
    [Header("PlayerObjects")]
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private GameObject[] spawnPos;
    public static GameManager instance = null;
    [SerializeField]
    private GameObject[] stuffToDisable;
    #endregion


    #region MonoBehaviour CallBacks 
    void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        photonManager = GetComponent<PhotonNetworkManagerCs>();
        spawnSystem = GetComponent<SpawnSystemCs>();
    }
    void Start()
    {
        playerScore = 0;
        playerScore0 = 0;
    }
    void Update()
    {
        playerScoreTxt[0].text = "Score: " + playerScore.ToString();
        playerScoreTxt[1].text = "Score: " + playerScore0.ToString();
        // playerScore.text = ":" + player;	
        if (isSinglePlayer == true)
        {
            isLocalMultiplayer = false;
            isMultiPlayer = false;
        }
        if (isLocalMultiplayer == true)
        {
            isSinglePlayer = false;
            isMultiPlayer = false;
        }
        if (isMultiPlayer == true)
        {
            isSinglePlayer = false;
            isLocalMultiplayer = false;
        }
    }
    #endregion

    #region Public Mathods
    public void OnAwake()
    {
        photonManager = this.gameObject.GetComponent<PhotonNetworkManagerCs>();

        if (isLocalMultiplayer == true)
        {
            photonManager.enabled = false;
            GameObject[] go = new GameObject[2];
           go [0]=  Instantiate(playerPrefabs[0], spawnPos[0].transform.position, Quaternion.Euler (0,0,90));
            go[0].GetComponent<PhotonView>().OnDestroy();
            go[0].GetComponent<PlayerNetworkCs>().enabled = false;
            go[0].GetComponent<PhotonTransformView>().enabled = false;
            go[1] = Instantiate(playerPrefabs[1], spawnPos[1].transform.position, Quaternion.Euler(0,0,-90));
            go[1].GetComponent<PhotonView>().OnDestroy();
            go[1].GetComponent<PlayerNetworkCs>().enabled = false;
            go[1].GetComponent<PhotonTransformView>().enabled = false;


        }
        else if (isSinglePlayer == true)
        {
            photonManager.enabled = false;
            GameObject go;
            go= Instantiate(playerPrefabs[1], spawnPos[0].transform.position, Quaternion.Euler(0, 0, 90));
            go.GetComponent<PhotonView>().OnDestroy();
           go.GetComponent<PlayerNetworkCs>().enabled = false;
            go.GetComponent<PhotonTransformView>().enabled = false;
            stuffToDisable[0].gameObject.SetActive(false);
            stuffToDisable[1].gameObject.SetActive(false);
            stuffToDisable[2].gameObject.SetActive(false);
            stuffToDisable[4].gameObject.SetActive(false);
            stuffToDisable[5].gameObject.SetActive(false);
            stuffToDisable[6].gameObject.SetActive(false);
            stuffToDisable[7].gameObject.SetActive(false);
        }
        else if (isMultiPlayer)
        {
            stuffToDisable[8].gameObject.SetActive(false);
            //pause button
            stuffToDisable[9].gameObject.SetActive(false);
        }
    }

    // Use this for initialization
    public void OnPlayerDie ()
    {
        myCanvas[1].SetActive(false);
        myCanvas[2].SetActive(true);

    }
    public void OnplayerWin()
    {
        myCanvas[1].SetActive(false);
        myCanvas[3].SetActive(true);
    }
    // Update is called once per frame

    /// used to add score based on who killed the enemy 
    public void addScore(float score ,string  playerName)
    {
        if (playerName == "0")
        {
            playerScore += score;
        }
        else if (playerName == "1")
        {
            playerScore0 += score;
        }
      
    }
    public bool isSinglePlayerCheck()
    {
        return isSinglePlayer;
    }
    public bool isMultiplayerCheck()
    {
        return isMultiPlayer;
    }
    public bool isLocalMultiplayerCheck()
    {
        return isLocalMultiplayer;
    }
    public void isSinglePlayerActive()    
    {
        isSinglePlayer = true;
        isLocalMultiplayer = false;
        isMultiPlayer = false;
        myCanvas[0].SetActive(false);
        myCanvas[1].SetActive(true);
        OnAwake();
    }
    public void isMultiplayerActive()
    {
        isSinglePlayer = false;
        isLocalMultiplayer = false;
        isMultiPlayer = true;
        myCanvas[0].SetActive(false);
        myCanvas[1].SetActive(true);
        photonManager.enabled = true;
        OnAwake();
    }
    public void isLocalMultiplayerActive()
    {
        isSinglePlayer = false;
        isLocalMultiplayer = false;
        isMultiPlayer = true;
        myCanvas[0].SetActive(false);
        myCanvas[1].SetActive(true);
        OnAwake();
    }
    #endregion
}
