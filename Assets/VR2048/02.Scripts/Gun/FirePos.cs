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
	public float fireRate = 0.2f;
	public ParticleSystem cartridge;
	public PlayerSfx playerSfx;

	public HandRole hand = HandRole.RightHand;
	public ControllerButton button = ControllerButton.Trigger;

	private float nextFire = 0.0f;
	private Transform firePos = null;
	private ParticleSystem muzzleFlash;
	private AudioSource _audio;

	private string BarrelTag = "BARREL";
	private string QuadsTag = "MOVEQUAD";
	private string barrelOnDamageMethod = "OnDamage";
	private string barrelRemoveCellNumMethod = "RemoveCellNum";

	private void Awake() {
		firePos = transform;
	}

	private void Start() {
		muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
		_audio = GetComponent<AudioSource>();
	}

	private void Update() {
		if (GameManager.Instance.GameState == GAMESTATE.GAMEOVER)
			return;

		if (ViveInput.GetPressDown(hand, button)) {
			if (Time.time >= nextFire) {
				FireRay();
				RaycastHit hit;
				if (Physics.Raycast(firePos.position, firePos.forward, out hit, 100f)) {
					if (hit.collider.CompareTag(BarrelTag)) {
						object[] _parms = new object[3];
						_parms[0] = hit.point;
						_parms[1] = hit.normal;
						_parms[2] = firePos.position;

						hit.collider.gameObject.SendMessage(barrelOnDamageMethod, _parms, SendMessageOptions.DontRequireReceiver);
					}
					if (hit.collider.CompareTag(QuadsTag)) {
						object[] _parms = new object[4];
						_parms[0] = hit.point;
						_parms[1] = hit.normal;
						_parms[2] = firePos.position;
						if (hand == HandRole.RightHand) {
							_parms[3] = HANDTYPE.RIGHTHAND;
						} else {
							_parms[3] = HANDTYPE.LEFTHAND;
						}

						Quad hitQuad = hit.collider.gameObject.GetComponent<Quad>();
						hitQuad.SendControlPoint(_parms);
					}
				}

				nextFire = Time.time + fireRate;
			}
		}
	}

	private void FireRay() {
		cartridge.Play();
		muzzleFlash.Play();
		FireSfx();
	}

	private void FireSfx() {
		var _sfx = playerSfx.fire[(int)currWeapon];
		_audio.PlayOneShot(_sfx);
	}
}