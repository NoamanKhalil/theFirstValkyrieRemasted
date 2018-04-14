using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPlayerInfoCs : MonoBehaviour
{
    [Header("Links To send and recive data")]
    [SerializeField]
    private string SendLink;
    private string ReciveLink;
    private int GamesCountToIncrease;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SendMyInfo ()
    {
        StartCoroutine("SendInfo");
    }

    void reciveInfo ()
    {

    }

    IEnumerator SendInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", PlayerPrefs.GetString("pName"));
        form.AddField("gamesPost", GamesCountToIncrease.ToString());

        WWW www = new WWW(SendLink, form);
        // Debug.Log("Data");
        yield return www;

        string SendInfoAndReturn = www.text;
        Debug.Log(SendInfoAndReturn);
       
    }
}
