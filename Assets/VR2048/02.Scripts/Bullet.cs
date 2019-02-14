using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 1000.0f;
	public float damage = 20.0f;

	// Start is called before the first frame update
	private void Start()
	{
		GetComponent<Rigidbody>().AddForce(transform.forward*speed);
	}
}