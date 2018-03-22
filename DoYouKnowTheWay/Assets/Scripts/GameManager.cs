using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField]
    private bool isSinglePlayer;
    [SerializeField]
    private bool isLocalMultiplayer;
    [SerializeField]
    private bool isMultiPlayer;

    [Header("UI Canvas")]
    [SerializeField]
    GameObject[] myCanvas;

    [Header("Score")]
    [SerializeField]
    float playerScore, playerScore0;
    [SerializeField]
    Text[] playerScoreTxt;

    [Header("PlayerObjects")]
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private GameObject[] spawnPos;
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (isLocalMultiplayer == true)
        {
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

        #if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        // myCanvas[2].SetActive(true);
        #endif

        #if UNITY_ANDROID || UNITY_IOS

        #endif
    }

    // Use this for initialization
    void Start()
    {
        playerScore = 0;
        playerScore0 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreTxt[0].text ="Score: " + playerScore.ToString ();
        playerScoreTxt[1].text ="Score: " + playerScore0.ToString();
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
    }
    public void isMultiplayerActive()
    {
        isSinglePlayer = false;
        isLocalMultiplayer = false;
        isMultiPlayer = true;
    }
    public void isLocalMultiplayerActive()
    {
        isSinglePlayer = false;
        isLocalMultiplayer = false;
        isMultiPlayer = true;
    }
}
