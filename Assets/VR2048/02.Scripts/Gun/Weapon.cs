using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string weaponName = "Gun";
	public float rangeGauge = 5.0f;

	private void Start() {
		WeaponInfo weaponGun = gameObject.AddComponent<WeaponInfo>();

		weaponGun.name = weaponName;
		weaponGun.rangeGauge = rangeGauge;
	}
}