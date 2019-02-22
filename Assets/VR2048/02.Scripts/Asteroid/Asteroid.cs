using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public string asteroidName = "Asteroid1";
	public float chargeGauge = 3.0f;
	public AsteroidInfo asteroid = null;

	private void Start() {
		asteroid = gameObject.AddComponent<AsteroidInfo>();
		asteroid.asteroidName = asteroidName;
		asteroid.chargeGauge = chargeGauge;
	}
}