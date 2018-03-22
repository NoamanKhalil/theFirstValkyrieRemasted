using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///LoadScene , used to control scenes andor quit d

public class LoadScenes : MonoBehaviour
{
	// Use this for initialization

	public void Score (string str)
	{
		SceneManager.LoadSceneAsync (str);
	}

	public void Exit ()
	{
		Application.Quit();
	}

}
