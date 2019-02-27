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

		if (other.tag == "WEAPON") {
			//StartCoroutine(MoveAsteroid(other));
		}

		if (other.tag == "WEAPON") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

			Weapon currWeapon = other.gameObject.GetComponent<Weapon>();
			float currWeaponRangeGauge = currWeapon.rangeGauge;
			Debug.Log(myAsteroid.asteroid.asteroidName + " ---");
			GameManager.Instance.AddChargeGauge(myAsteroidChargeGauge);
			Destroy(gameObject);
		}
	}

	private IEnumerator MoveAsteroid(Collider other) {
		float speed = 3.0f;
		Transform weaponPos = other.GetComponent<Transform>();
		Transform asteriodPos = this.transform;

		other.GetComponent<Rigidbody>().velocity = transform.forward * speed;

		//other.GetComponent<Rigidbody>().AddForce()

		yield return new WaitForSeconds(1.0f);
		Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
	}
}