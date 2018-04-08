using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Bullet sctipt controls bullets behaviour 

public class BulletCs : MonoBehaviour
{

    ///explosion sprite 
    public GameObject CollisionEffect;

    public bool isOnline;
    public bool isLocal;

    // Use this for initialization
    void Start()
    {
        // destroys bullet after 0,45 seconds 
        Invoke("DestroyMe", 1.8f);
    }

    /// TO be invoked when collison is achived 
    private void DestroyMe()
    {
        // Debug.Log("destroy works");
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
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
        else if (col.gameObject.tag.Equals("Player"))
        {
            col.gameObject.GetComponent<HealthCs>().TakeDamage();
        }
        else if (col.gameObject.tag.Equals("Shield"))
        {
            col.gameObject.GetComponent<ShieldCs>().TakeDamage();
        }


        ///Disables bullet for pooling 
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        /// Destroys bullet 0.08 seconds after collision 
        Invoke("DestroyMe", 0.1f);
    }
    [PunRPC]
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
        }
        else
        {
        }
    }
}