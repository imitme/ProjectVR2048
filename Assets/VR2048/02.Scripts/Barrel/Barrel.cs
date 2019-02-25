//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	private int limitHitCount = 3;
	private float barrelAddForce = 2.0f;
	private float expTimeMin = 3.0f;
	public GameObject expEffect;
	public GameObject normalEffect;
	public GameObject limitEffect;
	public ParticleSystem lightPS;
	public GameObject sparkEffect;
	public GameObject bulletMetalEffect;
	public Mesh[] meshes;

	private int hitCount = 0;
	private float expTimeMax = 0.0f;
	private float expTime = 0.0f;
	private bool isFire = false;
	private GameObject barrel = null;
	private Rigidbody rb = null;
	private MeshFilter meshFilter = null;
	private CellNum myCellNum = null;

	private void Start() {
		expEffect.SetActive(false);
		normalEffect.SetActive(false);
		limitEffect.SetActive(false);

		expTimeMax = expTimeMin * 2.0f;
		expTime = UnityEngine.Random.Range(expTimeMin, expTimeMax);

		barrel = this.gameObject;
		rb = GetComponent<Rigidbody>();
		meshFilter = GetComponent<MeshFilter>();
		myCellNum = GetComponent<CellNum>();
	}

	public void OnDamage(object[] _params) {
		Vector3 hitPos = (Vector3)_params[0];
		Vector3 hitNormal = (Vector3)_params[1];
		Vector3 firePos = (Vector3)_params[2];
		Vector3 incomeVector = (hitPos - firePos).normalized;
		float barrelAddForceR = barrelAddForce * UnityEngine.Random.Range(1.0f, 5.0f);
		rb.AddForceAtPosition(incomeVector * barrelAddForce, hitPos);

		ShotEffects(hitNormal, hitPos);

		hitCount += 1;
		CollBarrelProcess();
		if (hitCount == limitHitCount - 1) { ChangeStateCanMoveBarrel(); }
		if (hitCount == limitHitCount) { StartCoroutine(ExpBarrelProcess()); }
	}

	private void ShotEffects(Vector3 hitNormal, Vector3 hitPos) {
		ShowSparkEffect(hitNormal, hitPos);
		ShowBarrelMetalEffect(hitNormal, hitPos);
	}

	private void ShowSparkEffect(Vector3 sparkNormal, Vector3 hitPos) {
		Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, sparkNormal);
		GameObject spark = Instantiate(sparkEffect, hitPos, rot);
		spark.SetActive(true);
		spark.transform.SetParent(this.transform);
	}

	private void ShowBarrelMetalEffect(Vector3 sparkNormal, Vector3 hitPos) {
		Quaternion rot = Quaternion.FromToRotation(Vector3.forward, sparkNormal);
		GameObject Metal = Instantiate(bulletMetalEffect, hitPos, rot);
		Metal.SetActive(true);
		Metal.transform.SetParent(this.transform);
	}

	public void CollBarrelProcess() {
		ChangeBarrelMesh();
		if (hitCount == limitHitCount - 1) { isFire = true; }
		PlayBarrelEffect(isFire);
	}

	private void ChangeBarrelMesh() {
		int idx = UnityEngine.Random.Range(0, meshes.Length);
		meshFilter.sharedMesh = meshes[idx];
	}

	private void PlayBarrelEffect(bool isFire) {
		float effectScale = (float)hitCount / (float)(limitHitCount - 1);
		normalEffect.SetActive(true);
		normalEffect.transform.localScale = new Vector3(effectScale, effectScale, effectScale);
		if (isFire == true) {
			limitEffect.SetActive(true);
			if (hitCount < limitHitCount) { StartCoroutine(DrawLightPSProcess(0, 3, 2.2f)); }
		}
	}

	private IEnumerator DrawLightPSProcess(float startScalar, float goalScalar, float endTime) {
		var module = lightPS.lights;

		for (float t = 0.0f; t <= endTime; t += Time.deltaTime) {
			float currScalar = Mathf.Lerp(startScalar, goalScalar, t / endTime);
			module.intensityMultiplier = currScalar;
			yield return null;
		}
	}

	private void ChangeStateCanMoveBarrel() {
		rb.isKinematic = false;
	}

	public IEnumerator ExpBarrelProcess() {
		Debug.Log(1);
		RemoveAtCellNumList();
		Debug.Log(2 + " / " + expTime);
		yield return new WaitForSeconds(expTime);
		Debug.Log(3);
		HideBarrelandImpactMeshandEffects();
		Debug.Log(4);
		Debug.Log("ExpBarrelProcess");
		ShowExpEffect();
		Debug.Log(5);
		float endExpEffectTime = 2.0f;
		Destroy(barrel, endExpEffectTime);
		Debug.Log(6);
		Debug.Log("ExpSfx");
		GetComponent<BarrelSound>().ExpSfx();
		Debug.Log(7);
	}

	private void RemoveAtCellNumList() {
		myCellNum.ChangeStatetoExplosion();
		GameManager.Instance.RemoveCellNumWhenExpBarrel();
	}

	private void HideBarrelandImpactMeshandEffects() {
		DecalDestroyer[] bulletImpacts = GetComponentsInChildren<DecalDestroyer>();
		foreach (var bulletImpact in bulletImpacts) {
			bulletImpact.gameObject.SetActive(false);
		}
		barrel.GetComponent<MeshRenderer>().enabled = false;
		barrel.GetComponent<CellNum>().HideTxtwhenExpBarrel();
		normalEffect.SetActive(false);
		limitEffect.SetActive(false);
	}

	private void ShowExpEffect() {
		expEffect.SetActive(true);
	}
}