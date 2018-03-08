using UnityEngine;
using System.Collections;

public class Killzone : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Bullet") 
		{
			DestroyObject(other.gameObject);
			Debug.Log("bullets destroyed");
			
		}
	}
}