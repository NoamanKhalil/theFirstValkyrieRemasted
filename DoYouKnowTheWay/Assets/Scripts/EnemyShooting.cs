using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {


	public GameObject bullet ;



	
	// Use this for initialization
	void Start ()
	{
	
		InvokeRepeating("Fire1", 2, 0.3F);

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	void Fire1 ()
	{

		//Rigidbody bPrefab = Instantiate (bullet,transform.position, Quaternion.identity) as Rigidbody;
		
		//bPrefab.GetComponent<Rigidbody>().AddForce (Vector3.up * 500);

		
	}


}
