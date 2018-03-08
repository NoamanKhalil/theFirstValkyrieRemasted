using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCs : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Invoke("DestroyMe",0.5f);
	}
    private void DestroyMe()
    {
        this.gameObject.SetActive(false);
    }
}
