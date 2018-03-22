using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Used to test sending data to local host 

public class DatabaseManagerCs : MonoBehaviour {

    private string urlRegister;

    // Use this for initialization
    void Start () {
        urlRegister = "http://localhost/Valkyrie/connect.php";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Register(string def )
    {
        string username = "Noaman";
        string email  = "Caedriel@Live.com";
        string password = "BAE";
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("emailPost", email);
        form.AddField("passwordPost", password);

        WWW www = new WWW(urlRegister, form);
    }
}
