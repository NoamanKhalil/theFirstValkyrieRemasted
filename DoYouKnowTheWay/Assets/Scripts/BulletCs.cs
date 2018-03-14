using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCs : MonoBehaviour {

    public GameObject CollisionEffect;
	// Use this for initialization
	void Start ()
    {
       Invoke("DestroyMe",0.45f);
	}
    private void DestroyMe()
    {
        Debug.Log("destroy works");
        this.gameObject.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collsion works");
        CollisionEffect.SetActive(true);
        if (col.gameObject.tag.Equals("Enemy"))
        { 
        col.gameObject.GetComponent<EnemyBehaviourCs>().TakeDamage();
        }
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false ;
       Invoke("DestroyMe", 0.08f);
    }
}
