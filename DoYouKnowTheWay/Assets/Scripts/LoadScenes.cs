using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///LoadScene , used to control scenes andor quit d

public class LoadScenes : MonoBehaviour
{
    // Use this for initialization
    bool mytimeScale = true;
	public void LoadScene (string str)
	{
		SceneManager.LoadScene (str);
	}

	public void Exit ()
	{
		Application.Quit();
	}

    public void pause ()
    {
        if (mytimeScale == true)
        {
            Time.timeScale = 1;
            mytimeScale = false;
            return;

        }
        if (mytimeScale ==false)
        {
            Time.timeScale = 0;
            mytimeScale = true;
            return;
        }
        
    }

}
