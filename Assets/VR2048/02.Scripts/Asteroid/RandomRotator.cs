﻿using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
	public float tumble;

	private void Start() {
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
	}
}