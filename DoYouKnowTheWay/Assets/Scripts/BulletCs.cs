using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Bullet sctipt controls bullets behaviour 

public class BulletCs : MonoBehaviour {

    ///explosion sprite 
    public GameObject CollisionEffect;
	// Use this for initialization
	void Start ()
    {
        // destroys bullet after 0,45 seconds 
       Invoke("DestroyMe",0.45f);
	}

    /// TO be invoked when collison is achived 
    private void DestroyMe()
    {
        Debug.Log("destroy works");
        this.gameObject.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
       /// Debug.Log("Collsion works");
        CollisionEffect.SetActive(true);

        /// does damage to enemy based on objects tag 
        if (col.gameObject.tag.Equals("Enemy"))
        { 
        col.gameObject.GetComponent<EnemyBehaviourCs>().TakeDamage();
        }

        ///Disables bullet for pooling 
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false ;

        /// Destroys bullet 0.08 seconds after collision 
       Invoke("DestroyMe", 0.08f);
    }
}
