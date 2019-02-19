using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	[SerializeField] private string collisionTagNameforExplosion = "BULLET";

	private int limitHitCount = 3;
	private float barrelAddForce = 5f;
	private float expTimeMin = 3.0f;
	public GameObject expEffect;
	public GameObject normalEffect;
	public GameObject limitEffect;
	public ParticleSystem lightPS;
	public Mesh[] meshes;

	private int hitCount = 0;
	private float expTimeMax = 0.0f;
	private float expTime = 0.0f;
	private bool isFire = false;
	private GameObject barrel = null;
	private Rigidbody rb = null;
	private MeshFilter meshFilter = null;
	private CellNum myCellNum = null;

	private void Start()
	{
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

	private void OnDamage(object[] _params)
	{
		Vector3 hitPos = (Vector3)_params[0];
		Vector3 firePos = (Vector3)_params[2];
		Vector3 incomeVector = (hitPos - firePos).normalized;
		rb.AddForceAtPosition(incomeVector * barrelAddForce, hitPos);

		hitCount += 1;
		CollBarrelProcess();
		if (hitCount == limitHitCount - 1) { ChangeStateCanMoveBarrel(); }
		if (hitCount == limitHitCount) { StartCoroutine(ExpBarrelProcess()); }
	}

	public void CollBarrelProcess()
	{
		ChangeBarrelMesh();

		if (hitCount == limitHitCount - 1) { isFire = true; }
		PlayBarrelEffect(isFire);
	}

	private void ChangeBarrelMesh()
	{
		int idx = UnityEngine.Random.Range(0, meshes.Length);
		meshFilter.sharedMesh = meshes[idx];
	}

	private void PlayBarrelEffect(bool isFire)
	{
		float effectScale = (float)hitCount / (float)(limitHitCount - 1);
		normalEffect.SetActive(true);
		normalEffect.transform.localScale = new Vector3(effectScale, effectScale, effectScale);
		if (isFire == true)
		{
			limitEffect.SetActive(true);
			if (hitCount < limitHitCount) { StartCoroutine(DrawLightPSProcess(0, 3, 2.2f)); }
		}
	}

	private IEnumerator DrawLightPSProcess(float startScalar, float goalScalar, float endTime)
	{
		var module = lightPS.lights;

		for (float t = 0.0f; t <= endTime; t += Time.deltaTime)
		{
			float currScalar = Mathf.Lerp(startScalar, goalScalar, t / endTime);
			module.intensityMultiplier = currScalar;
			yield return null;
		}
	}

	private void ChangeStateCanMoveBarrel()
	{
		rb.isKinematic = false;
	}

	public IEnumerator ExpBarrelProcess()
	{
		float endExpEffectTime = 2.0f;
		RemoveAtCellNumList();
		yield return new WaitForSeconds(expTime);
		HideBarrelandImpactMeshandEffects();
		ShowExpEffect();
		Destroy(barrel, endExpEffectTime);
		GetComponent<BarrelSound>().ExpSfx();
	}

	private void RemoveAtCellNumList()
	{
		myCellNum.OnExplosion();
		GameManager.Instance.ExpBarrel();
	}

	private void HideBarrelandImpactMeshandEffects()
	{
		DecalDestroyer[] bulletImpacts = GetComponentsInChildren<DecalDestroyer>();
		foreach (var bulletImpact in bulletImpacts)
		{
			bulletImpact.gameObject.SetActive(false);
		}
		barrel.GetComponent<MeshRenderer>().enabled = false;
		normalEffect.SetActive(false);
		limitEffect.SetActive(false);
	}

	private void ShowExpEffect()
	{
		expEffect.SetActive(true);
	}
}