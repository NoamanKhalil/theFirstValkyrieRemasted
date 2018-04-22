using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;


public class PlayerBehaviourCs : PunBehaviour
{
    #region Public variables implementation


    //public GameObject health;
    public bool onAndroid;
    public VirtualJoystickCs vs;
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
    public AudioSource aud;

    #endregion

    #region private serilaized variables implementation

    [Header("projectile prefab")]
    [SerializeField]
    private GameObject[] projectile;

    [Header("Positons to Clamp ")]
    [SerializeField]
    private GameObject[] clampPoints;

    #endregion

    #region private  variables implementation
    private Vector2 movement;

    // to check current game mode
    private bool isSinglePlayerActive;
    private bool isMultiplayerActive;
    private bool isLocalMultiplayerActive;


    public Button fireBtn;

    private PhotonView myPhotonView;
    [SerializeField]
    private int attackLevel;
    private int ProjectilePrefab;
    //private AudioSource aud; 

    // for powerups 
    private bool onSpeed;
    private bool onAttack;
    private bool onShield;
    private float tempShotReset = 0.5f;

    public bool isControllable;
    //for smooth movement 
    Vector3 targetPos;
    float h, v;

    #endregion


    #region  MonoBehaviour CallBacks
    // Use this for initialization
    void Awake()
    {
        clampPoints[0] = GameObject.Find("ClampPosY0");
        clampPoints[1] = GameObject.Find("ClampPosY");
        aud = GetComponent<AudioSource>();
        /* if (isPlayerOne)
         {

         }
         else if (!isPlayerOne)
         {
             clampPoints[0] = GameObject.Find("ClampPosY1");
             clampPoints[1] = GameObject.Find("ClampPosY2");
         }*/

        PhotonNetwork.sendRate = 20;
        PhotonNetwork.sendRateOnSerialize = 10;
        ProjectilePrefab = 0;
        attackLevel = 0;

        myPhotonView = GetComponent<PhotonView>();

        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        gm.spawnSystem.PlayerCountIncrease();
        isSinglePlayerActive = gm.isSinglePlayerCheck();
        isMultiplayerActive = gm.isMultiplayerCheck();
        isLocalMultiplayerActive = gm.isLocalMultiplayerCheck();
        if (onAndroid)
        {
            vs = gm.VirtualJoystick.GetComponentInChildren<VirtualJoystickCs>(); //GameObject.FindGameObjectWithTag("AndroidControl").GetComponentInChildren<VirtualJoystickCs>();


        }
        /*if (isPlayerOne )
        {

            health = GameObject.Find("Player0_txt");
        } 
        else if (isMultiplayerActive&& isPlayerOne)
        {
            //health = GameObject.Find("Player_txt");
            health = GameObject.Find("Player0_txt");
        }
        else
        {
            health = GameObject.Find("Player_txt");
        }*/

    }
    void Start()
    {
        if (myPhotonView.isMine)
        {
            canControl();
        }
        // handles fucntionality for non local character
        else
        {
            //disable its control 
            cannotControl();
        }
        if (onAndroid)
        {
            // type of button  , finds object with tag 
            fireBtn = GameObject.FindGameObjectWithTag("FireBtn").GetComponent<Button>();
            //fireBtn.onClick.AddListener(FireProjectile);
            fireBtn.onClick.AddListener(() => FirePro());
            Debug.Log("Listerner working");
        }

    }



    void Update()
    {
        OnCheat();
        //health.GetComponent<Text>().text = ": " + shotsToDie;
        shotReset -= Time.deltaTime;

        Movement();

        if (shotReset < 0)
        {
            // fire works differently in photon 
            if (isMultiplayerActive && myPhotonView.isMine && onAndroid == false)
            {
                Debug.Log("Update Fp called netowrked  ");
                FireProjectile0();
            }
            else if (isSinglePlayerActive || isLocalMultiplayerActive)
            {
                if (  onAndroid == false)
                {
                    Debug.Log("Update Fp");
                    FirePro();
                }
               
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

    #endregion


    #region Public  Methods

    [PunRPC]
    public void cannotControl()
    {
        isControllable = false;
    }
    [PunRPC]
    public void canControl()
    {
        isControllable = true;
    }

    [PunRPC]
    public void TakeDamage()
    {
        shotsToDie--;
    }
    [PunRPC]
    public void EnableCollider()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = true;
    }
    [PunRPC]
    public void powerUp(string power, string color)
    {
        if (color == "Green")
        {
            attackLevel = 0;
            ProjectilePrefab = 1;

        }
        else if (color == "Blue")
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
        else if (power == "Attack")
        {
            attackLevel++;
        }
        else if (power == "Health")
        {
            shotsToDie++;
        }

    }

    #endregion

    #region Private  Methods

    void FireProjectile0()
    {
        myPhotonView.RPC("FireProjectile", PhotonTargets.All);
    }
    void OnCheat()
    {
        if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.K))
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            attackLevel = 3;
            Shield.SetActive(true);
            shotsToDie += 10;
        }
    }
    [PunRPC]
    public void FireProjectile()
    {
        if (Input.GetAxis("Fire4") != 0 && !isPlayerOne)
        {
            GameObject bPrefab = PhotonNetwork.Instantiate(projectile[ProjectilePrefab].name, firePos[0].position, Quaternion.Euler(0, 0, -90), 0) as GameObject;
            bPrefab.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 500);
            shotReset = 0.5f;
            aud.Play();

        }
        else if (Input.GetAxis("Fire5") != 0 && isPlayerOne)
        {
            GameObject bPrefab = PhotonNetwork.Instantiate(projectile[ProjectilePrefab].name, firePos[0].position, Quaternion.Euler(0, 0, 90), 0) as GameObject;
            bPrefab.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 500);
            shotReset = 0.5f;
            aud.Play();
        }
    }
    [PunRPC]
    void FirePro()
    {
        // Debug.Log("Fire be working ");

        if (Input.GetAxis("Fire4") != 0|| onAndroid && !isPlayerOne && shotReset < 0)
        {
            //Debug.Log("PEw PEW");
            GameObject[] clone = new GameObject[3];
            //clone= Instantiate(projectile, firePos[0].position, transform.rotation) as Rigidbody;
            if (attackLevel == 0)
            {
                clone[0] = Instantiate(projectile[ProjectilePrefab], firePos[0].position, Quaternion.Euler(0, 0, -90)) as GameObject;
                clone[0].GetComponent<Rigidbody2D>().AddForce(Vector3.right * 500, ForceMode2D.Force);
                aud.Play();
                shotReset = 0.5f;
            }
            else if (attackLevel == 1)
            {
                for (int i = 1; i < 3; i++)
                {
                    clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, -90)) as GameObject;
                    clone[i].GetComponent<Rigidbody2D>().AddForce(Vector3.right * 500, ForceMode2D.Force);
                    aud.Play();
                    shotReset = 0.5f;
                }
            }
            else if (attackLevel == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, -90)) as GameObject;
                    clone[i].GetComponent<Rigidbody2D>().AddForce(Vector3.right * 500, ForceMode2D.Force);
                    aud.Play();
                    shotReset = 0.5f;
                }
            }

        }
        else if (Input.GetAxis("Fire5") != 0 || onAndroid&& isPlayerOne == true  &&shotReset < 0)
        {
                //Debug.Log("BEw BEW");
                GameObject[] clone = new GameObject[3];
                //clone= Instantiate(projectile, firePos[0].position, transform.rotation) as Rigidbody;
                if (attackLevel == 0)
                {
                clone[0] = Instantiate(projectile[ProjectilePrefab], firePos[0].position, Quaternion.Euler(0, 0, 90)) as GameObject ;
                clone[0].GetComponent<Rigidbody2D>().AddForce(Vector3.left * 500, ForceMode2D.Force);
                aud.Play();
                shotReset = 0.5f;
            }
                else if (attackLevel == 1)
                {
                    for (int i = 1; i < 3; i++)
                    {
                    clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, 90)) as GameObject;
                    clone[i].GetComponent<Rigidbody2D>().AddForce(Vector3.left * 500, ForceMode2D.Force);
                    aud.Play();
                    shotReset = 0.5f;
                }
                }
                else if (attackLevel == 2)
                {
                for (int i = 0; i < 3; i++)
                {
                    clone[i] = Instantiate(projectile[ProjectilePrefab], firePos[i].position, Quaternion.Euler(0, 0, 90)) as GameObject;
                    clone[i].GetComponent<Rigidbody2D>().AddForce(Vector3.left * 500, ForceMode2D.Force);
                    aud.Play();
                    shotReset = 0.5f;
                }
       
            }
        }
    }
    [PunRPC]
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

        // conditions to check if its player one or not and use different input axises in the same script 
        if (isPlayerOne && !onAndroid)
        {
            h = Input.GetAxis("Horizontal") * speed;
            v = Input.GetAxis("Vertical") * speed;
            //Debug.Log("H " + h + "V= " + v);
        }
        else if (!isPlayerOne && !onAndroid)
        {
            h = Input.GetAxis("Horizontal0") * speed;
            v = Input.GetAxis("Vertical0") * speed;
            // Debug.Log("H " + h  +"V= "+ v);
        }
        else if (onAndroid)
        {
            h = vs.Horizontal();
            v = vs.Vertical();
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
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            TakeDamage();
        }

    }
    [PunRPC]
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
           // stream.SendNext(shotsToDie);
            stream.SendNext(transform.position);
           // stream.SendNext(health.GetComponent<Text>().text);
           // stream.SendNext ()
        }
        else
        {
           // shotsToDie=(int)stream.ReceiveNext();
            targetPos = (Vector3)stream.ReceiveNext();
          //  health.GetComponent<Text>().text = (string)stream.ReceiveNext();
        }
    }


    #endregion
}
