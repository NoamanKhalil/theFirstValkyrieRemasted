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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            col.gameObject.GetComponent<PlayerBehaviourCs>().powerUp(pow.ToString(), col.ToString());
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
