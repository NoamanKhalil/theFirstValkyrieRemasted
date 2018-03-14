using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourCs : MonoBehaviour
{
    public Rigidbody2D[] projectile;
    public Transform[] firePos;
    public bool OnController;
    public Rigidbody2D rb;
    public float speed;
    public float maxSpeed;
    public float shotReset = 1.0f;
    public bool isPlayerOne;
    public int shotsToDie;

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
        float h, v;
        if (isPlayerOne == true )
        {
             h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
             v = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        }
        else
        {
             h = Input.GetAxis("Horizontal0") * speed * Time.deltaTime;
             v = Input.GetAxis("Vertical0") * speed * Time.deltaTime;
        }

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

            if (Input.GetAxis("Fire4") != 0 && !isPlayerOne)
            {
                Debug.Log("PEw PEW");
                Rigidbody2D clone;
                //clone= Instantiate(projectile, firePos[0].position, transform.rotation) as Rigidbody;
                clone = Instantiate(projectile[0], firePos[0].position, Quaternion.Euler (0,0,90)) as Rigidbody2D;
                clone.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500,ForceMode2D.Force);
                shotReset = 0.5f;
            }
            else if (Input.GetAxis("Fire5")!=0 &&isPlayerOne)
            {
                Debug.Log("PEw PEW");
                Rigidbody2D clone;
                //clone= Instantiate(projectile, firePos[0].position, transform.rotation) as Rigidbody;
                clone = Instantiate(projectile[0], firePos[0].position, Quaternion.Euler (0,0,-90)) as Rigidbody2D;
                clone.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500,ForceMode2D.Force);
                shotReset = 0.5f;
            }
        }
    }

    void TakeDamage()
    {
        shotsToDie--; 
    }

    void powerUp()
    {

    }
}
