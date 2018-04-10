using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///LoadScene , used to control scenes andor quit d

public class LoadScenes : MonoBehaviour
{
    // Use this for initialization
    bool mytimeScale = false;
	public void LoadScene (string str)
	{
		SceneManager.LoadSceneAsync (str);
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
        }
        if (mytimeScale ==false)
        {
            Time.timeScale = 0;
        }
        
    }

}
