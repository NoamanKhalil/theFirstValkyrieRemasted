using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SendPlayerInfoCs : MonoBehaviour
{
    [Header("Links To send and recive data")]
    [SerializeField]
    private string SendLink;
    [SerializeField]
    private string ReciveLink;

    public Text GlobalLeaderText;


    public string [] myData;
    public void SendMyInfo ()
    {
        StartCoroutine("SendInfo");
    }

    public void reciveInfo ()
    {
        StartCoroutine("ReciveInfo");
    }
    IEnumerator ReciveInfo()
    {
        myData = null;
        GlobalLeaderText.text = null;
        WWW Data = new WWW(ReciveLink);


        yield return Data;

        string DataString = Data.text;
        Debug.Log(DataString);
        myData = DataString.Split(';');

        for (int i = 0; i < myData.Length; i++)
        {
            //for every score we add it to the highScore UI text
            GlobalLeaderText.text += myData[i] + "\n"+ "\t";
        }
    }
    IEnumerator SendInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", PlayerPrefs.GetString("pName"));

        WWW www = new WWW(SendLink, form);
        // Debug.Log("Data");
        yield return www;

        string SendInfoAndReturn = www.text;
        Debug.Log(SendInfoAndReturn);
       
    }
}
