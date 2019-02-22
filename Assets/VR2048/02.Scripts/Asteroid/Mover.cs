using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float speed;
	//public Vector3 goalPos ;

	private void Start() {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		//GetComponent<Rigidbody>().velocity = goalPos * speed;
	}
}