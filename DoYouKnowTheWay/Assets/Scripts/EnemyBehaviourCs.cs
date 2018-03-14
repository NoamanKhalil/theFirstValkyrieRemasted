using UnityEngine;
using System.Collections;

public class EnemyBehaviourCs : MonoBehaviour
{
    public int shotsToDie;

	public Rigidbody2D bullet ;
    public Transform enemyShootPos;
    public float shotReset = 1.0f;
    // Use this for initialization
    void Update ()
	{
        shotReset -= Time.deltaTime;
        if (shotReset <0)
        {
            FireProjectile();
        }
        if (shotsToDie <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
	void FireProjectile ()
	{
		Rigidbody2D bPrefab = Instantiate (bullet,enemyShootPos.position, Quaternion.Euler(0,0,180)) as Rigidbody2D;
		bPrefab.GetComponent<Rigidbody2D>().AddForce (Vector3.down * 500);
        shotReset = 0.5f;
    }
    public void TakeDamage()
    {
        shotsToDie--;
    }

}
