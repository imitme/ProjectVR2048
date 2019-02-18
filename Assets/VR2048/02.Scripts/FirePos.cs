using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

[System.Serializable]
public struct PlayerSfx { public AudioClip[] fire; public AudioClip[] reload; }

public class FirePos : MonoBehaviour
{
	public WEAPONTYPE currWeapon = WEAPONTYPE.SHOTGUN;
	public GameObject bullet;
	public float fireRate = 0.1f;
	public ParticleSystem cartridge;
	public PlayerSfx playerSfx;

	public HandRole hand = HandRole.RightHand;
	public ControllerButton button = ControllerButton.Trigger;

	private float nextFire = 0.0f;
	private Transform firePos = null;
	private ParticleSystem muzzleFlash;
	private AudioSource _audio;

	private void Awake()
	{
		firePos = transform;
	}

	private void Start()
	{
		muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
		_audio = GetComponent<AudioSource>();
	}

	private void Update()
	{
		//Debug.DrawRay(firePos.position, firePos.forward * 20.0f, Color.green);

		if (ViveInput.GetPressDown(hand, button))
		{
			if (Time.time >= nextFire)
			{
				Fire();
				nextFire = Time.time + fireRate;
			}
			else
				Debug.Log("wait");
		}
	}

	private void Fire()
	{
		Instantiate(bullet, firePos.position, firePos.rotation);

		cartridge.Play();
		muzzleFlash.Play();
		FireSfx();
	}

	private void FireSfx()
	{
		var _sfx = playerSfx.fire[(int)currWeapon];
		_audio.PlayOneShot(_sfx);
	}
}