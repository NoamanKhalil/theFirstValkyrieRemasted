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
    private string ReciveLinkName;
    [SerializeField]
    private string ReciveLinkGame;

    public Text GlobalLeaderNameText;
    public Text GlobalLeaderGameText;


    public string[] myData;
    public string[] alsoMyData;
    public void SendMyInfo ()
    {
        StartCoroutine("SendInfo");
    }

    public void reciveInfo ()
    {
        StartCoroutine("ReciveNameInfo");
        StartCoroutine("ReciveGameInfo");
    }
    IEnumerator ReciveGameInfo()
    {
        alsoMyData = null;
        GlobalLeaderGameText.text = null;
        WWW Data = new WWW(ReciveLinkGame);


        yield return Data;

        string DataString = Data.text;
        Debug.Log(DataString);
        alsoMyData = DataString.Split(';');

        for (int i = 0; i < alsoMyData.Length; i++)
        {
            //for every score we add it to the highScore UI text
            GlobalLeaderGameText.text += alsoMyData[i] + "\n" + "\t";
        }
    }
    IEnumerator ReciveNameInfo()
    {
        myData = null;
        GlobalLeaderGameText.text = null;
        WWW Data = new WWW(ReciveLinkName);


        yield return Data;

        string DataString = Data.text;
        Debug.Log(DataString);
        myData = DataString.Split(';');

        for (int i = 0; i < myData.Length; i++)
        {
            //for every score we add it to the highScore UI text
            GlobalLeaderNameText.text += myData[i] + "\n"+ "\t";
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
