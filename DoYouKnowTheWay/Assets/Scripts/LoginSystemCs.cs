using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public class LoginSystemCs : MonoBehaviour
{
    [SerializeField]
    private string urlRegister , urlLogin;

    [Header("UserInfo")]
    public string UserName, UserPassword, UserEmail, InputName, InputPassword;
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
    private WWW loginData;
    [SerializeField]
    private string[] loginStuff;

    private bool isEmailValid;

    private string key = "CARLOSISBAE";
    private string stufftoEncrypt = "12345";
    public string encryptedString;
    void  Start()
    {
        urlRegister = "http://localhost/Valkyrie/connect.php";
        isEmailValid = false;
        /* loginData = new WWW("http://localhost/Valkyrie/connect.php");
         yield return loginData;
         string loginDataString = loginData.text;
         Debug.Log(loginDataString);
         loginStuff = loginDataString.Split(';');*/
       // encryptedString = MD5.Create(stufftoEncrypt);
        
    }
    bool emailCheck (string mystring)
    {
        string str = mystring;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(str);
        if (match.Success)
        {
            Debug.Log(str + " is correct");
            return true;
        }
        else
        {
            Debug.Log(str + " is incorrect");
            return false;
        }
            

        
    }
    
	void Update ()
    {
        if (UserPasswordText!= null && PasswordVerifictationText != null)
        {
            if (UserPasswordText.text== PasswordVerifictationText.text)
            {
                SamePass = true;
                return;
            }
            else
            {
                //Debug.Log("Password !=Same");
                return;
            }
        }
        else
         {
            SamePass = false; 
        }
	}
 
   public void Register ( )
    {
        if (SamePass == true && UserText.text != null && EmailText.text != null )
        {
            string username = UserText.text.ToString();
            string email = EmailText.text.ToString();
            string password = PasswordText.text.ToString();
            WWWForm form = new WWWForm();
            form.AddField("usernamePost", username);
            form.AddField("emailPost", email);
            form.AddField("passwordPost", password);

            WWW www = new WWW(urlRegister, form);
            Debug.Log("Registerd ");
        }
        else
        {
            Debug.Log("Please enter the text properly " );
        }
       
    }
    public void doLogin ()
    {
        StartCoroutine("Login");
    }
    IEnumerator Login()
    {
        Debug.Log("Coroutines started ");
        string userName = UserNameText.text.ToString() ;
        string password= UserPasswordText.text.ToString() ;
        WWWForm form = new WWWForm();

        form.AddField("usernamePost", userName);
        form.AddField("passwordPost", password);
        WWW www = new WWW(urlLogin, form);
         yield return  www;

        Debug.Log(www.text);

        if (www.text == " Login Success")
        {

        }
    }
   

 ///regex key for email validation

    ///(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])

}