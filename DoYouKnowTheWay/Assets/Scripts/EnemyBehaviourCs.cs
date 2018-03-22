using UnityEngine;
using System.Collections;

public class EnemyBehaviourCs : MonoBehaviour
{
    public int shotsToDie;
    public GameObject[] PowerUps;
	public Rigidbody2D bullet ;
    public Transform enemyShootPos;
    public float shotReset = 1.0f;
    private PhotonView myPhotonView;

    // Use this for initialization
    private void Awake()
    {
        myPhotonView = GetComponent<PhotonView>();
    }
    void Update ()
	{
        shotReset -= Time.deltaTime;
        if (shotReset <0)
        {
            if (PhotonNetwork.offlineMode == true)
            {
                FireProjectile();
            }
            else
            {
                myPhotonView.RPC("FireProjectile", PhotonTargets.All);
            }
            
        }
        if (shotsToDie <= 0)
        {
            if (PhotonNetwork.offlineMode == true)
            {
                makePowerUp();
            }
            else
            {
                myPhotonView.RPC("makePowerUp", PhotonTargets.All);
            }
        }
    }

    [PunRPC]
    void makePowerUp()
    {
        Instantiate(PowerUps[Random.Range(0, 14)], transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
    [PunRPC]
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
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(shotsToDie);
        }
        else
        {
            shotsToDie = (int)stream.ReceiveNext();
        }
    }
}
