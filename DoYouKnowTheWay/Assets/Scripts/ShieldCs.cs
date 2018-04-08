using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCs : MonoBehaviour
{
    float timeToDie = 6.0f;
    int shotsToDie= 6 ; 
	
	// Update is called once per frame
	void Update ()
    {
        timeToDie -= Time.deltaTime;
        if (shotsToDie <=0 || timeToDie <=0)
        {
            GetComponentInParent<PlayerBehaviourCs>().EnableCollider();
            this.gameObject.SetActive(false);
        }
	}
    public void TakeDamage()
    {
        shotsToDie--;
    }
}
