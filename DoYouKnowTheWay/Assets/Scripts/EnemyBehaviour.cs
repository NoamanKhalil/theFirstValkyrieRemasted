using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameObject g_prefab;

	// Use this for initialization
	void Start () // On start 
	{
		for(int i=0; i<5; ++i) // A loop that creates 40 instances of the gameobject only in the beggining of the game 
		{
			Instantiate(g_prefab, new Vector3(i, 5+ Mathf.Cos (i),0), Quaternion.identity); // instantiate the prefab with the Cos pattern
			Instantiate(g_prefab, new Vector3(i+50, 7,0), Quaternion.identity); 
			Instantiate(g_prefab, new Vector3(i+50, 5,0), Quaternion.identity);



		}
	
	}

	}
	
