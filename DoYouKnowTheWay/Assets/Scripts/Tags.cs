using UnityEngine;
using System.Collections;

public class Tags : MonoBehaviour {

	// Use this for initialization
	void Start () {
	

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider obj)
	{
		if (obj.gameObject.tag == "Enemy") 
		{
			Destroy(obj.gameObject);

		}





	}
	
    }
