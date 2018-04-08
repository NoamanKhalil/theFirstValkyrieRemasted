using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkCs : MonoBehaviour {

    //[SerializeField] private GameObject thisPlayerCamera;
    [SerializeField] private PlayerBehaviourCs playerControlScript;

    private PhotonView photonView;
    // Use this for initialization
    void Start ()
    {
        // faster than using photon.monobehaviour
        photonView = GetComponent<PhotonView>();
        playerControlScript = GetComponent<PlayerBehaviourCs>();
        Initilaze();
	}
	void Initilaze ()
    {
        if (photonView.isMine)
        {
        
        }
        // handles fucntionality for non local character
        else
        {
            // disbale its camera
            //thisPlayerCamera.SetActive(false);
            //disable its control scripts 
                playerControlScript.enabled = false;
          
        }
	}

   
}
