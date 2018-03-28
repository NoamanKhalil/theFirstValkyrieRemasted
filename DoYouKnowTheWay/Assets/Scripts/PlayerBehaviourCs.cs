using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBehaviourCs : MonoBehaviour
{
    [Header("projectile prefab")]
    [SerializeField]
    public GameObject[] projectile;

    [Header("Positons to Clamp ")]
    [SerializeField]
    public GameObject[] clampPoints;

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
        clampPoints[0] = GameObject.Find("ClampPosY0");
        clampPoints[1] = GameObject.Find("ClampPosY");
       /* if (isPlayerOne)
        {

        }
        else if (!isPlayerOne)
        {
            clampPoints[0] = GameObject.Find("ClampPosY1");
            clampPoints[1] = GameObject.Find("ClampPosY2");
        }*/

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
        else if (isMultiplayerActive)
        {
            health = GameObject.Find("Player_txt");
        }
        else
        {
            health = GameObject.Find("Player0_txt");
        }

    }
    void Start()
    {

    }
    [PunRPC]
    void Update()
    {
        health.GetComponent<Text>().text = ": " + shotsToDie;
        bool idk = true;
        shotReset -= Time.deltaTime;
        Movement();
        if (shotReset < 0)
        { 
            // fire works differently in photon 
            if (isMultiplayerActive )
            {
                GetComponent<PhotonView>().RPC("FirePro", PhotonTargets.All);
            }
            else if (isSinglePlayerActive || isLocalMultiplayerActive)
            {
                FirePro();
            }
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

    // Metod to control the movement of the player 
    void Movement()
    {
        // used to get the players position for clamping 
        Vector3 pos = rb.position;
        //clamps the position on the y axis to be between the two objects in clampPoints[] 
        pos.y = Mathf.Clamp(pos.y, clampPoints[0].gameObject.transform.position.y, clampPoints[1].gameObject.transform.position.y);
        // sets the y clamp to the players position using rigidbody as it uses physics to move (rigidbody.addforce)
        rb.position = pos;
        // temporary varibles used for hoizontal and vertical movement 
        float h, v;
        // conditions to check if its player one or not and use different input axises in the same script 
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
        // calculates the amount to move on x and y however x is clamped 
        movement = new Vector2(h, v);
        // clamps the maximum movement 
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        //adds the movemnt force 
        rb.AddForce(movement);
    }

    [PunRPC]
    void FireProjectile()
    {
        GameObject bPrefab = Instantiate(projectile[ProjectilePrefab], firePos[0].position, Quaternion.Euler(0, 0, 90)) as GameObject;
        bPrefab.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 500, ForceMode2D.Force);
        shotReset = 0.5f;
    }
    [PunRPC ]
    void FirePro()
    {
            if (Input.GetAxis("Fire4") != 0 && !isPlayerOne )
            {
                Debug.Log("PEw PEW");
                GameObject[] clone = new GameObject [3];
                //clone= Instantiate(projectile, firePos[0].position, transform.rotation) as Rigidbody;
                if (attackLevel ==0)
                {
                    clone[0] = Instantiate(projectile[ProjectilePrefab], firePos[0].position, Quaternion.Euler(0, 0, -90)) as GameObject;
                    clone[0].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500);
                    shotReset = 0.5f;
            }
                else if (attackLevel ==1 )
                {
                    for (int i = 1; i < 3; i++)
                    {
                    clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, -90)) as GameObject;
                    clone[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500);
                    shotReset = 0.5f;
                }
                }
                else if (attackLevel == 2)
                {
                    for (int i =0; i <3 ; i++)
                    {
                        clone [i]= Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, -90)) as GameObject;
                        clone[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500);
                    shotReset = 0.5f;
                }
                }
 
        }
            else if (Input.GetAxis("Fire5")!=0 &&isPlayerOne == true)
            {
                Debug.Log("BEw BEW");
                GameObject[] clone = new GameObject[3];
                //clone= Instantiate(projectile, firePos[0].position, transform.rotation) as Rigidbody;
                if (attackLevel == 0)
                {
                    clone[0] = Instantiate(projectile[ProjectilePrefab], firePos[0].position, Quaternion.Euler(0, 0, 90)) as GameObject ;
                    clone[0].GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500, ForceMode2D.Force);
                    shotReset = 0.5f;
            }
                else if (attackLevel == 1)
                {
                    for (int i = 1; i < 3; i++)
                    {
                        clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, 90)) as GameObject;
                        clone[i].GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500, ForceMode2D.Force);
                    shotReset = 0.5f;
                }
                }
                else if (attackLevel == 2)
                {
                for (int i = 0; i < 3; i++)
                {
                    clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, 90)) as GameObject;
                    clone[i].GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500, ForceMode2D.Force);
                    shotReset = 0.5f;
                }
       
            }
        }
    }

    public void TakeDamage()
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
