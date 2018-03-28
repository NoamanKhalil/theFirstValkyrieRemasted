using UnityEngine;
using System.Collections;

public class EnemyBehaviourCs : MonoBehaviour
{
    public int shotsToDie;
    public GameObject[] PowerUps;
    public Transform[] pathToFollow;
	public Rigidbody2D bullet ;
    public Transform enemyShootPos;
    public Transform powerupPos;
    public float shotReset = 1.0f;
    private PhotonView myPhotonView;
    private bool isEnabled = true;
    public float frequency = 20.0f;
    public float magnitude = 0.5f;
    public float speed = 0.1f;
    public Vector3 axis;
    GameManager gm;

    public bool isOffline;
    private bool isOnline;
    // Use this for initialization
    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
       // isOffline = gm.isSinglePlayerCheck();
       // isOnline = gm.isMultiplayerCheck();
        myPhotonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    void Update ()
	{
        transform.position = Vector3.MoveTowards(transform.position + axis * Mathf.Sin(Time.time * frequency) * magnitude, pathToFollow[5].position, Time.deltaTime * speed);

        if (isEnabled!= true)
        {
            this.gameObject.SetActive(false);
        }
        shotReset -= Time.deltaTime;
        if (shotReset <0)
        {
            if (isOffline)
            {
                FireProjectile();
            }
            else if (PhotonNetwork.isMasterClient == true)
            {
                myPhotonView.RPC("FireProjectile", PhotonTargets.All);
            }
            
        }
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
                    // myPhotonView.RPC("makePowerUp", PhotonTargets.All);
                   // PhotonNetwork.Instantiate(PowerUps[Random.Range(0, 14)].name, this.transform.position,this.transform.rotation,0 );
                    myPhotonView.RPC("killMe", PhotonTargets.All);
                }
            }
        }
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
        Instantiate(PowerUps[Random.Range(0, 11)], powerupPos.position, Quaternion.identity) ;
        isEnabled = false;
    }
    [PunRPC]
	void FireProjectile ()
	{

		Rigidbody2D bPrefab = Instantiate (bullet,enemyShootPos.position, Quaternion.Euler(0,0,-90)) as Rigidbody2D;
		bPrefab.GetComponent<Rigidbody2D>().AddForce (Vector3.right * 500);
        shotReset = 0.5f;
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
