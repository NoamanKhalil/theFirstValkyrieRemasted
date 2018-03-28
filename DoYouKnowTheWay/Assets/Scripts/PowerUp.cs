using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Power
{
    none,
    Speed,
    Shield,
    Attack,
    Health,
}
public enum Colors
{
   none,
   Red,
   Blue ,
   Green,
}

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private  Power pow;
    [SerializeField]
    private Colors col;
    bool isActive;

    private void Update()
    {
         if (isActive != true)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player") )
        {
            col.gameObject.GetComponent<PlayerBehaviourCs>().powerUp(pow.ToString(), col.ToString());
            col.gameObject.GetComponent<PlayerBehaviourCs>().gm.GetComponent<GameManager>().addScore(50, "0");
            isActive = false;
        }
        else if (col.gameObject.tag.Equals("PlayerProjectile"))
        {
            col.gameObject.GetComponent<PlayerBehaviourCs>().gm.GetComponent<GameManager>().addScore(50, "0");
            isActive = false;
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isActive);
        }
        else
        {
            isActive = (bool)stream.ReceiveNext();
        }
    }

}
