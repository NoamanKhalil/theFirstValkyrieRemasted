using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourCs : MonoBehaviour
{
    public Rigidbody2D projectile;
    public Transform[] firePos;
    public bool OnController;
    public Rigidbody2D rb;
    public float speed;
    public float maxSpeed;
    public float shotReset = 1.0f;

    private Vector2 movement;
    // Use this for initialization
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        shotReset -= Time.deltaTime; 
        Movement();
        Fire();
	}

    void Movement ()
    {
        float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        movement = new Vector2(h, v);
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        rb.AddForce(movement);
    }
    void Fire()
    {
        if (shotReset <0)
        {

            if (Input.GetAxis("Fire1") != 0)
            {
                Debug.Log("PEw PEW");
                Rigidbody2D clone;
                //clone= Instantiate(projectile, firePos[0].position, transform.rotation) as Rigidbody;
                clone = Instantiate(projectile, firePos[0].position, Quaternion.identity) as Rigidbody2D;

                clone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 1000);
                shotReset = 0.5f;
            }
        }
    }
}
