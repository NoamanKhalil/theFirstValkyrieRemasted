using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField]
    bool isSinglePlayer;
    [SerializeField]
    bool isLocalMultiplayer;
    [SerializeField]
    bool isMultiPlayer;

    [Header("UI Canvas")]
    [SerializeField]
    GameObject [] myCanvas;

    [Header("Score")]
    [SerializeField]
    float player, player0;
    [SerializeField]
    Text playerScore, playerScore0;

    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null&& instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
  

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
       // myCanvas[2].SetActive(true);
#endif

#if UNITY_ANDROID || UNITY_IOS

#endif
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerScore.text = ":" + player;	
	}

    public void addScore(float score)
    {
        
    }

}
