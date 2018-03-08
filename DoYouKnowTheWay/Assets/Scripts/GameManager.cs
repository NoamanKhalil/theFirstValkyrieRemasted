using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField]
    bool isSinglePlayer;
    [SerializeField]
    bool isMultiPlayer;

    [Header("UI Canvas")]
    [SerializeField]
    GameObject [] myCanvas;

    private void Awake()
    {
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
	void Update () {
		
	}

}
