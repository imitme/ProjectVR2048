using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;

	private Asteroid myAsteroid = null;
	private float myAsteroidChargeGauge;

	private void Start() {
		myAsteroid = gameObject.GetComponent<Asteroid>();
		myAsteroidChargeGauge = myAsteroid.chargeGauge;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary" || other.tag == "Enemy") {
			return;
		}

		//if (explosion != null) {
		//	Instantiate(explosion, transform.position, transform.rotation);
		//}

		if (other.tag == "WEAPON") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

			Weapon currWeapon = other.gameObject.GetComponent<Weapon>();
			float currWeaponRangeGauge = currWeapon.rangeGauge;
			Debug.Log(myAsteroid.asteroid.asteroidName + " ---");
			GameManager.Instance.AddChargeGauge(myAsteroidChargeGauge);
			Destroy(gameObject);
		}
	}
}