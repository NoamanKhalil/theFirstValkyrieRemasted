using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

/// LoginSystemCs , used to manage the login and register for the game 

public class LoginSystemCs : MonoBehaviour
{
    [SerializeField]
    private string urlRegister , urlLogin;

   /* [Header("UserInfo")]
    public string UserName, UserPassword, UserEmail, InputName, InputPassword;*/
    public bool UserLoggedIn,SamePass, loginValidated;
    [Header("Login")]
    [SerializeField]
    private  Text LoginNameText, LoginPasswordText;
    [Header("Register")]
    [SerializeField]
    private Text EmailText , UserText;
    [Header("Register Password ")]
    [SerializeField]
    private GameObject PasswordTextPlaceHolder, PasswordVerifictationTextPlaceHolder;
    [SerializeField]
    private Text PasswordText, PasswordVerifictationText;

    [SerializeField]
    private GameObject ConnectingPanel;

    [Header("OnScreen error")]
    [SerializeField]
    private Text[] updatetxt;
    private bool isEmailValid;

    public bool onNetworkCheck = false;
    public bool isLoggedIn;
    private string key = "CARLOSISBAE";
    private string stufftoEncrypt = "12345";
    public string encryptedString;
    GameManager gm;
    void  Start()
    {
        gm = GetComponent<GameManager>();
        isLoggedIn = false;
       // urlRegister = "http://localhost/Valkyrie/connect.php";
        isEmailValid = false;
        /* loginData = new WWW("http://localhost/Valkyrie/connect.php");
         yield return loginData;
         string loginDataString = loginData.text;
         Debug.Log(loginDataString);
         loginStuff = loginDataString.Split(';');*/
       // encryptedString = MD5.Create(stufftoEncrypt);
        
    }

    public void canNetworkCheck ()
    {
        onNetworkCheck = true;
    }

    public void cannotNetworkCheck()
    {
        onNetworkCheck = false;
    }
    
    bool myMailCheck (string mystring)
    {
        string str = mystring;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(str);
        if (!match.Success || mystring == null || mystring == "")
        {

            updatetxt[1].text = "Status :  " + str + "  Is not a Email , please enter a valid email ";
            return true;
        }
        else if (match.Success)
        {
           
            updatetxt[1].text = "Status :  " + str + "  Is a valid Email ";
            return false;
        }

        return false;
        
    }
    
	void Update ()
    {
        if (isLoggedIn)
        {
            gm.isMultiplayerActive();
        }


        if (onNetworkCheck == true)
        {
            PasswordText.text = PasswordTextPlaceHolder.GetComponent<InputField>().text;
            PasswordVerifictationText.text = PasswordVerifictationTextPlaceHolder.GetComponent<InputField>().text;
            ///Checks if email charcters are legal/ valid 
            myMailCheck(EmailText.text);
          //  updatetxt[1].text = EmailText.text + "Is a  " + myMailCheck(EmailText.text +": email ID");
            //Debug.Log(myMailCheck(EmailText.text));
            /// Condition to check if email is Valid 
            if (PasswordText.text != null && PasswordVerifictationText.text != null)
            {
                if (PasswordText.text == PasswordVerifictationText.text)
                {
                    if (PasswordText.text != " " && PasswordVerifictationText.text != " ")
                    {
                        SamePass = true;
                        updatetxt[0].text = "Hey m8 , passwords match =) ";
                        return;
                    }
                }
                else if (PasswordText.text != PasswordVerifictationText.text)
                {

                    //Debug.Log("Hey m8, password no match  " + PasswordText.text +"  "+ PasswordVerifictationText.text);
                    updatetxt[0].text = "Hey m8 , passwords no match  " + PasswordText.text +"  "+ PasswordVerifictationText.text;
                    return;
                }
            }
        }
        
	}

    public void doRegister  ()
    {
        if (SamePass == true && UserText.text != null && EmailText.text != null)
        {
            StartCoroutine("Register");
            //Debug.Log("Hey m8, password no match  " + PasswordText.text.ToString() + "  " + PasswordVerifictationText.text.ToString());
        }
        else
        {
           // Debug.Log("Please enter the text properly " );
            updatetxt[0].text = "Please enter all fields correctly ";
        }

    }
    public void doLogin ()
    {
        if (LoginNameText.text != null && LoginPasswordText.text != null)
        {
            StartCoroutine("Login");
        }
        else
        {
            //Debug.Log("Login was a failure as they are both null");
            updatetxt[1].text = "Login Failed =/";

        }

    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", UserText.text.ToString());
        form.AddField("emailPost", EmailText.text.ToString());
        form.AddField("passwordPost", PasswordText.text.ToString());

        WWW www = new WWW(urlRegister, form);
        updatetxt[0].text = "You are registering...........";
       // Debug.Log("Data");
        yield return www;

        string CreateAccountReturn = www.text;
        Debug.Log(CreateAccountReturn);
        updatetxt[1].text = CreateAccountReturn.ToString();
    }

    IEnumerator Login()
    {
            WWWForm form = new WWWForm();

            form.AddField("usernamePost", LoginNameText.text.ToString());
            form.AddField("passwordPost", LoginPasswordText.text.ToString());
            WWW www = new WWW(urlLogin, form);
             updatetxt[0].text = "You are Loging in ...........";
             //Debug.Log("Data");
             yield return www;

            string LoginAccountReturn = www.text;
            Debug.Log(LoginAccountReturn);
            updatetxt[0].text =LoginAccountReturn;
            
            if (www.text == " Login Success")
            {
               // Debug.Log("Login was a success");
                updatetxt[1].text = "You are logged in !";
                PlayerPrefs.SetString("pName", LoginNameText.text.ToString());
                Debug.Log(PlayerPrefs.GetString("pName"));
                this.gameObject.GetComponent<GameManager>().isMultiplayerActive();
            }
            if (www.error == null)
            {
                isLoggedIn = true;
            // Debug.Log("Login was a success");
            updatetxt[1].text = "You are logged in !";
            PlayerPrefs.SetString("pName", LoginNameText.text.ToString());
            Debug.Log(PlayerPrefs.GetString("pName"));
            this.gameObject.GetComponent<GameManager>().isMultiplayerActive();
        }
            
      
    }
   

 ///regex key for email validation

    ///(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])

}