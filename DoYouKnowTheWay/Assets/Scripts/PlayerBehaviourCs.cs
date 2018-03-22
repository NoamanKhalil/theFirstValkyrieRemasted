using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBehaviourCs : MonoBehaviour
{
    public Rigidbody2D[] projectile;
    public GameObject health;
    public GameObject Shield;
    public Transform[] firePos;
    public bool OnController;
    public Rigidbody2D rb;
    public float speed;
    public float maxSpeed;
    public float shotReset = 1.0f;
    public bool isPlayerOne;
    public int shotsToDie;
    public GameManager gm;
    private Vector2 movement;

    // to check current game mode
    private bool isSinglePlayerActive;
    private bool isMultiplayerActive;
    private bool isLocalMultiplayerActive;


    private PhotonView myPhotonView;
    private int attackLevel;
    private int ProjectilePrefab;


    // for powerups 
    private bool onSpeed;
    private bool onAttack;
    private bool onShield;
    private float tempShotReset = 0.5f;

    //for smooth movement 
    Vector3 targetPos;

    // Use this for initialization
    void Awake()
    {
        PhotonNetwork.sendRate = 20;
        PhotonNetwork.sendRateOnSerialize = 20;
        ProjectilePrefab = 0;
        attackLevel = 0;

       myPhotonView = GetComponent<PhotonView>();
        
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        isSinglePlayerActive = gm.isSinglePlayerCheck();
        isMultiplayerActive = gm.isMultiplayerCheck();
        isLocalMultiplayerActive = gm.isLocalMultiplayerCheck();

        if (isPlayerOne )
        {
            health = GameObject.Find("Player_txt") ;
        } 
        else
        {
            health = GameObject.Find("Player0_txt");
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        health.GetComponent<Text>().text = ": " + shotsToDie; 
        shotReset -= Time.deltaTime;
        Movement();
        // fire works differently in photon 
        if (isMultiplayerActive )
        {
            myPhotonView.RPC("Fire", PhotonTargets.All);
        }
        else if (isSinglePlayerActive || isLocalMultiplayerActive)
        {
            Fire();
        }

        if (onSpeed == true)
        {
            maxSpeed = 1.5f;
        }
        else
        {
            speed = 1.0f;
        }

    }

    void Movement()
    {
        //smoothMovemnt();
        float h, v;
        if (isPlayerOne == true)
        {
            h = Input.GetAxis("Horizontal") * speed ;
            v = Input.GetAxis("Vertical") * speed;
            //Debug.Log("H " + h + "V= " + v);
        }
        else 
        {
            h = Input.GetAxis("Horizontal0") * speed;
            v = Input.GetAxis("Vertical0") * speed;
           // Debug.Log("H " + h  +"V= "+ v);
        }

        movement = new Vector2(h, v);
        if (rb.velocity.magnitude > maxSpeed)
        {
            Debug.Log("we be moving");
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        rb.AddForce(movement);
    }
    void smoothMovemnt ()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.25f);
    }
   [PunRPC ]
    void Fire()
    {
        if (shotReset <0)
        {

            if (Input.GetAxis("Fire4") != 0 && !isPlayerOne )
            {
                Debug.Log("PEw PEW");
                Rigidbody2D[] clone = new Rigidbody2D [3];
                //clone= Instantiate(projectile, firePos[0].position, transform.rotation) as Rigidbody;
                if (attackLevel ==0)
                {
                    clone[0] = Instantiate(projectile[ProjectilePrefab], firePos[0].position, Quaternion.Euler(0, 0, 90)) as Rigidbody2D;
                    clone[0].GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500, ForceMode2D.Force);
                }
                else if (attackLevel ==1 )
                {
                    for (int i = 1; i < 3; i++)
                    {
                    clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, 90)) as Rigidbody2D;
                    clone[i].GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500, ForceMode2D.Force);
                    }
                }
                else if (attackLevel == 2)
                {
                    for (int i =0; i <3 ; i++)
                    {
                        clone [i]= Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, 90)) as Rigidbody2D;
                        clone[i].GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500, ForceMode2D.Force);
                    }
                 
                    
                }
                shotReset = 1.0f;
            }
            else if (Input.GetAxis("Fire5")!=0 &&isPlayerOne == true)
            {
                Debug.Log("BEw BEW");
                Rigidbody2D[] clone = new Rigidbody2D[3];
                //clone= Instantiate(projectile, firePos[0].position, transform.rotation) as Rigidbody;
                if (attackLevel == 0)
                {
                    clone[0] = Instantiate(projectile[ProjectilePrefab], firePos[0].position, Quaternion.Euler(0, 0, -90)) as Rigidbody2D;
                    clone[0].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500, ForceMode2D.Force);
                }
                else if (attackLevel == 1)
                {
                    for (int i = 1; i < 3; i++)
                    {
                        clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, 90)) as Rigidbody2D;
                        clone[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500, ForceMode2D.Force);
                    }
                }
                else if (attackLevel == 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, 90)) as Rigidbody2D;
                        clone[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500, ForceMode2D.Force);
                    }


                }
                shotReset = 1.0f;
            }
  
        }
    }

    void TakeDamage()
    {
        shotsToDie--; 
    }

    public void powerUp(string power , string color)
    {
        if (color == "Green")
        {
            attackLevel = 0;
            ProjectilePrefab = 1;
            
        }
        else if (color =="Blue")
        {
            attackLevel = 0;
            ProjectilePrefab = 0;
        }
        else if (color == "Red")
        {
            attackLevel = 0;
            ProjectilePrefab = 2;
        }
        
        if (power == "Speed")
        {
            onSpeed = true;
        }
        else if (power == "Shield")
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            Shield.SetActive(true);
            //invoke to break shield 
        }
        else if (power =="Attack")
        {
            attackLevel++;
        }
        else if (power == "Health")
        {
            shotsToDie++;
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            TakeDamage();
        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(shotsToDie);
            stream.SendNext(transform.position);
           // stream.SendNext ()
        }
        else
        {
            shotsToDie=(int)stream.ReceiveNext();
            targetPos = (Vector3)stream.ReceiveNext();
        }
    }
}
