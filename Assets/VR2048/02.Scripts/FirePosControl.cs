using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class FirePosControl : MonoBehaviour
{
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
	public GameObject bullet;

	public HandRole hand = HandRole.RightHand;
	public ControllerButton button = ControllerButton.Trigger;

	private GameManager gameManager = null;
	private Transform firePos = null;

	private void Awake()
	{
		gameManager=GameObject.FindObjectOfType<GameManager>();
		firePos=transform;
	}

	private void Update()
	{
		if(ViveInput.GetPressDown(hand, button))
		{
			Debug.Log("------------------------------------"+hand);

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
	}
}