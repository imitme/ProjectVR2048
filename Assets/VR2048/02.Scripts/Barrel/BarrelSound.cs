using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ExpSfx { public AudioClip[] expBarrel; }

public class BarrelSound : MonoBehaviour
{
	public EXPTYPE currExp = EXPTYPE.EXP1;
	public ExpSfx expSfx;
	private AudioSource _audio;

	private void Start() {
		_audio = GetComponent<AudioSource>();
	}

	public void ExpSfx() {
		Debug.Log("ExpSfx / PlayOneShot");
		var _sfx = expSfx.expBarrel[(int)currExp];
		_audio.PlayOneShot(_sfx);
	}
}