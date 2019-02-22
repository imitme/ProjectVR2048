using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;
	//public Vector3 goalPos ;

	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		//GetComponent<Rigidbody>().velocity = goalPos * speed;
	}
}
