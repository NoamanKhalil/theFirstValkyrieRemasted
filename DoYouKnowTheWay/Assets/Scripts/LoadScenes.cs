using UnityEngine;
using System.Collections;

public class LoadScenes : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play (string StartGame) {

		Application.LoadLevel (1);
	}

	public void Score (string ScoreScene)
	{
		Application.LoadLevel (2);
	}

	public void Exit ()
	{
		Application.Quit();
	}

	public void Back (string Back)
	{
		Application.LoadLevel (0);
	}
}
