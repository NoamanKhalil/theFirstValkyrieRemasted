using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerCs : MonoBehaviour {

    public GameObject bulletPattern;

    public GameObject []bulletInstances =new GameObject[5];
     int bulletLevel;
    int bulletCount;
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < bulletInstances.Length; i++)
        {
            GameObject Bullet = (GameObject)Instantiate(bulletPattern, transform.position, Quaternion.identity);
            bulletInstances[i] = Bullet;
            //Bullets [i].GetComponent<Bullet> ().bulletID;
        }
        bulletCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
