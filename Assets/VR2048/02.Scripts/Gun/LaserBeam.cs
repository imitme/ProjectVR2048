using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class LaserBeam : MonoBehaviour
{
	public HandRole hand = HandRole.RightHand;
	public ControllerButton button = ControllerButton.Trigger;

	private Transform tr;
	private LineRenderer line;
	private RaycastHit hit;
	private float fireRate;
	private float nextFire = 0.0f;

	private void Start() {
		tr = GetComponent<Transform>();
		line = GetComponent<LineRenderer>();
		line.useWorldSpace = false;
		line.enabled = false;
		line.startWidth = 0.01f;
		line.endWidth = 0.1f;
		fireRate = GetComponentInParent<FirePos>().fireRate;
	}

	private void Update() {
		if (GameManager.Instance.GameState == GAMESTATE.GAMEOVER)
			return;

		Ray ray = new Ray(tr.position, tr.forward);
		if (ViveInput.GetPressDown(hand, button)) {
			if (Time.time >= nextFire) {
				line.SetPosition(0, tr.InverseTransformPoint(ray.origin));
				if (Physics.Raycast(ray, out hit, 100.0f)) {
					line.SetPosition(1, tr.InverseTransformPoint(hit.point));
				} else {
					line.SetPosition(1, tr.InverseTransformPoint(ray.GetPoint(100.0f)));
				}
				StartCoroutine(ShowLaserBeam());
				nextFire = Time.time + fireRate;
			}
		}

		IEnumerator ShowLaserBeam() {
			line.enabled = true;
			yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
			line.enabled = false;
		}
	}
}