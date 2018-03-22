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
    private  Text UserNameText, UserPasswordText;
    [Header("Register")]
    [SerializeField]
    private Text EmailText , PasswordText ,PasswordVerifictationText, UserText;
    [SerializeField]
    private GameObject ConnectingPanel;

    [Header("SQL Table")]
    [SerializeField]
    private Text updatetxt0;

    private bool isEmailValid;

    
    private string key = "CARLOSISBAE";
    private string stufftoEncrypt = "12345";
    public string encryptedString;
    void  Start()
    {
       // urlRegister = "http://localhost/Valkyrie/connect.php";
        isEmailValid = false;
        /* loginData = new WWW("http://localhost/Valkyrie/connect.php");
         yield return loginData;
         string loginDataString = loginData.text;
         Debug.Log(loginDataString);
         loginStuff = loginDataString.Split(';');*/
       // encryptedString = MD5.Create(stufftoEncrypt);
        
    }
    bool myMailCheck (string mystring)
    {
        string str = mystring;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(str);
        if (!match.Success || mystring == null || mystring == "")
        {

            updatetxt0.text = "Status :  " + str + "  Is not a Email , please enter a valid email ";
            return true;
        }
        else if (match.Success)
        {
           
            updatetxt0.text = "Status :  " + str + "  Is a valid Email ";
            return false;
        }

        return false;
        
    }
    
	void Update ()
    {
        ///Checks if email charcters are legal/ valid 
        myMailCheck(EmailText.text.ToString());

        /// Condition to check if email is Valid 
        if (UserPasswordText!= null && PasswordVerifictationText != null)
        {
            if (UserPasswordText.text== PasswordVerifictationText.text)
            {
                SamePass = true;
                updatetxt0.text = "Hey Gringo , passwords match =) ";
                return;
            }
            else
            {
                Debug.Log("Password !=Same");
                updatetxt0.text = "Hey Gringo , password no match  ";
                return;
            }
        }
        else
         {
            SamePass = false; 
        }
	}

    IEnumerator Register ( )
    {
            WWWForm form = new WWWForm();
            form.AddField("usernamePost", UserText.text.ToString());
            form.AddField("emailPost", EmailText.text.ToString());
            form.AddField("passwordPost", PasswordText.text.ToString());
            WWW www = new WWW(urlRegister, form);
            updatetxt0.text = "You are registering...........";
            Debug.Log("Data");
            yield return www;

        string CreateAccountReturn = www.text;
        Debug.Log(CreateAccountReturn);
        // updatetxt0.text = "You are Registered !";
    }
    public void doRegister  ()
    {
        if (SamePass == true && UserText.text != null && EmailText.text != null)
        {
        StartCoroutine("Register");
        }
        else
        {
            Debug.Log("Please enter the text properly " );
            updatetxt0.text = "Please enter all fields correctly ";
        }

    }
    public void doLogin ()
    {
        if (UserNameText != null && UserPasswordText != null)
        {
            StartCoroutine("Login");
        }
        else
        {
            Debug.Log("Login was a failure as they are both null");
            updatetxt0.text = "Login Failed =/";

        }

    }
    IEnumerator Login()
    {
      
            Debug.Log("Coroutines started ");
            WWWForm form = new WWWForm();

            form.AddField("usernamePost", UserNameText.text.ToString());
            form.AddField("passwordPost", UserPasswordText.text.ToString());
            WWW www = new WWW(urlLogin, form);
     

            Debug.Log(www.error);

            if (www.text == " Login Success")
            {
                Debug.Log("Login was a success");
                updatetxt0.text = "You are logged in !";
            }
            yield return www;
      
    }
   

 ///regex key for email validation

    ///(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])

}