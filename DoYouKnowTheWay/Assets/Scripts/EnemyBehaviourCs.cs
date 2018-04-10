using UnityEngine;
using System.Collections;
using Photon;
public class EnemyBehaviourCs : PunBehaviour
{
    public int shotsToDie;
    public float shootAngle;
    public string wayPointParent;
    public Vector3 fireVector;
    public GameObject[] PowerUps;
    public Transform[] pathToFollow;
	public GameObject bullet ;
    public Transform enemyShootPos;
    public Transform powerupPos;
    public float shotReset = 1.0f;
    private PhotonView myPhotonView;
    private bool isEnabled = true;
    public float frequency = 20.0f;
    public float magnitude = 0.5f;
    public float speed = 0.5f;
    public Vector3 axis;
    GameManager gm;

    private bool isOffline;
    private bool isOnline;
    public int waveCount;
    public int posPoint;
    public bool canShoot;
    // Use this for initialization
    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
          // set 0 for multiplayer 
       
        posPoint =0;
         isOffline = gm.isSinglePlayerCheck();
         isOnline = gm.isMultiplayerCheck();
        myPhotonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (waveCount == 0)
        {
         pathToFollow = GameObject.Find(wayPointParent).GetComponentsInChildren<Transform>();
        }
        else if (waveCount == 1)
        {
            pathToFollow = GameObject.Find("Pattern0").GetComponentsInChildren<Transform>();
        }
        else if (waveCount == 2)
        {
            pathToFollow = GameObject.Find("Pattern1").GetComponentsInChildren<Transform>();
        }
        else if (waveCount == 3)
        {
            pathToFollow = GameObject.Find("Pattern3").GetComponentsInChildren<Transform>();
        }
    }
    public void SetWaveCount (int wave)
    {
        waveCount = wave;
    }

   [PunRPC]
    void Update ()
	{
        //transform.position = Vector3.MoveTowards(transform.position + axis * Mathf.Sin(Time.time * frequency) * magnitude, pathToFollow[0].position, Time.deltaTime * speed);
        if (posPoint < pathToFollow.Length)
            {
             transform.position = Vector3.MoveTowards(transform.position, pathToFollow[posPoint].position, Time.deltaTime * speed);
            }
           
            if (transform.position == pathToFollow[posPoint].position)
            { 
             if (posPoint ==pathToFollow.Length-1)
             {
                posPoint = 0;
                return;
             }
                posPoint++;          
             }
         
        shotReset -= Time.deltaTime;
        if (shotReset <=0 && canShoot)
        {
            if (isOffline)
            {
                FireProjectile();
            }
            else if (PhotonNetwork.isMasterClient == true)
            {
               GameObject go = PhotonNetwork.Instantiate(bullet.name, enemyShootPos.position, Quaternion.Euler(0, 0, shootAngle), 0);
                go.GetComponent<Rigidbody2D>().AddForce(fireVector * 500);
                shotReset = 2f;
                //FireProjectile0();
            }
            
        }
        Debug.Log(isOffline);
        if (shotsToDie <= 0)
        {
            if (isOffline)
            {
                makePowerUp();
                gm.addScore(100, "0");
            }
            else
            {
                if (PhotonNetwork.isMasterClient == true)
                {

                    //myPhotonView.RPC("makePowerUp", PhotonTargets.All);
                    //PhotonNetwork.Instantiate(PowerUps[Random.Range(0, 14)].name, this.transform.position,this.transform.rotation,0 );
                    //myPhotonView.RPC("killMe", PhotonTargets.All);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }


    void FireProjectile0()
    {
        myPhotonView.RPC("FireProjectile", PhotonTargets.All);
    }


    [PunRPC]
    void killMe ()
    {
        PhotonNetwork.Instantiate(PowerUps[Random.Range(0, 14)].name, powerupPos.position, this.transform.rotation, 0);
        isEnabled = false;
        // makePowerUp();

    }


    [PunRPC]
    void makePowerUp()
    {
        Instantiate(PowerUps[Random.Range(0, 14)], powerupPos.position, Quaternion.identity) ;
        this.gameObject.SetActive(false);
    }
    [PunRPC]
	void FireProjectile ()
	{
		GameObject bPrefab = Instantiate (bullet,enemyShootPos.position, Quaternion.Euler(0,0,shootAngle)) as GameObject;
		bPrefab.GetComponent<Rigidbody2D>().AddForce (fireVector * 500);
        shotReset = 2f;
    }
    public void TakeDamage()
    {
        shotsToDie--;
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(shotsToDie);
            stream.SendNext(isEnabled);
        }
        else
        {
            shotsToDie = (int)stream.ReceiveNext();
            isEnabled = (bool)stream.ReceiveNext();
        }
    }
}
