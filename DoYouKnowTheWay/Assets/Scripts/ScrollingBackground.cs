using UnityEngine;
using System.Collections;
 

public class ScrollingBackground : MonoBehaviour
{


	public float q_initialSpeed ;
	
	static public float speed ;
	
	public float q_speed ;
	
	private float m_xscale ; 
	
	private float m_distanceCounter ; 

	private float m_distance ;

	//    public  m_distance = Score ;
	
	
	// Use this for initialization
	void Start ()
	{
		m_xscale = transform.lossyScale.x ;
		
		speed = q_initialSpeed; 
		
		
		
	}
	
	// Update is called once per frame
	void Update () 	
	{
		
		if (m_distanceCounter >20)
		{
			
			// multiplys base speed so it increases by a certain amount ever 20 meters 
			speed  +=  Time.deltaTime*1.2f   ; 
			
		}
		

		// displays speed increasing in unity 
		
		m_distance +=q_speed*Time.deltaTime ; 
		// calaculates distance based on delta time 
		
		m_distanceCounter+= speed *Time.deltaTime ;
		
		Vector3 position = transform.position ;
		
		position.y = Mathf.Repeat(m_distance*-1,m_xscale);
		
		transform.position = position;
		

		
	
		
	}



}