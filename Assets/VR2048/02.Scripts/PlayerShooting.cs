using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class PlayerShooting : MonoBehaviour {
	public HandRole hand = HandRole.RightHand;
	public ControllerButton button = ControllerButton.Trigger;
	public float timeBetweenBullets = 0.15f;

	private float timer;
	private Ray shootRay = new Ray();

	// Start is called before the first frame update
	private void Start() {
	}

	// Update is called once per frame
	private void Update() {
		timer += Time.deltaTime;
		if (ViveInput.GetPress(hand, button) && timer >= timeBetweenBullets && Time.timeScale != 0) {
			Shoot();
		}
	}

	private void Shoot() {
		timer = 0;
		Debug.Log("Shoot");
	}
}