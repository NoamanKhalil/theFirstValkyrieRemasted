using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour
{
	public Transform bulletStartPos ; 
	private Rigidbody rb ;
	public Rigidbody bulletPrefab;
	public float bulletspeed = 0.5f;
	float shotReset;
	public float playerSpeed = 10f;
	public float health = 3;
	public GameObject Heart;


	// Use this for initialization
	void Start ()
	{
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{

	

		//Move Right 
		if (Input.GetKey(KeyCode.D ))
			
		{
			Debug.Log ("D pressed going right");
			rb.AddForce (new Vector3 (8,0,0));
			
		}


		if (Input.GetKey(KeyCode.RightArrow))
			
		{
			Debug.Log ("Right pressed  ");
			rb.AddForce (new Vector3 (8,0,0));
			
		}

		// move Left
		if (Input.GetKey(KeyCode.A))
			
		{
			Debug.Log ("A pressed going left ");
			rb.AddForce (new Vector3 (-8,0,0));
			
		}

		if (Input.GetKey(KeyCode.LeftArrow))
			
		{
			Debug.Log ("Left pressed ");
			rb.AddForce (new Vector3 (-8,0,0));
			
		}

		if (Time.time >= shotReset) {
		
			// to fire objects
			if (Input.GetKey (KeyCode.Space)) {
				Debug.Log ("Space pressed ");

				Fire ();
			
			}
		}



		if (health <= 0) 
		{
			//Application.LoadLevel();
		}

	}

	void Fire ()
	{
		Rigidbody bPrefab = Instantiate (bulletPrefab,bulletStartPos.position, Quaternion.identity) as Rigidbody;

		bPrefab.GetComponent<Rigidbody>().AddForce (Vector3.up * 500);

		shotReset = Time.time + bulletspeed; 
	

		

	}

	// Update is called once per frame
	void FixedUpdate ()
		
	{

	}



}
