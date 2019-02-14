using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class FirePosControl : MonoBehaviour
{
	public float fireRate = 0.5f;
	public GameObject bullet;

	public HandRole hand = HandRole.RightHand;
	public ControllerButton button = ControllerButton.Trigger;

	private float nextFire = 0.0f;
	private Transform firePos = null;
	private ParticleSystem _cartridge;

	private void Awake()
	{
		firePos=transform;
		_cartridge=GetComponentInChildren<ParticleSystem>();
	}

	private void Update()
	{
		if(ViveInput.GetPressDown(hand, button))
		{
			if(Time.time>=nextFire)
			{
				Fire();
				nextFire=Time.time+fireRate;
			}
			else
				Debug.Log("wait");
		}
	}

	private void Fire()
	{
		Debug.Log("------------------------------------Fire");
		Instantiate(bullet, firePos.position, firePos.rotation);
		_cartridge.Play();
	}
}