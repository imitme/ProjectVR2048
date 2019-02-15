using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	[SerializeField] private string collisionTagNameforExplosion = "BULLET";

	private int limitHitCount = 4;
	[SerializeField] private float expTimeMin = 0.0f;
	[SerializeField] private float expTimeMax = 0.0f;
	public GameObject expEffect;
	public GameObject normalEffect;
	public GameObject limitEffect;
	public ParticleSystem lightPS;
	public Mesh[] meshes;

	private int hitCount = 0;
	private Rigidbody rb;
	private float expTime = 0;
	private GameObject barrel = null;
	private MeshFilter meshFilter;
	private bool isFire = false;

	private void Start()
	{
		barrel = this.gameObject;
		rb = GetComponent<Rigidbody>();

		expTimeMin = 3.0f;
		expTimeMax = expTimeMin * 2.0f;
		expTime = Random.Range(expTimeMin, expTimeMax);

		meshFilter = GetComponent<MeshFilter>();

		expEffect.SetActive(false);
		normalEffect.SetActive(false);
		limitEffect.SetActive(false);
	}

	private void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.CompareTag(collisionTagNameforExplosion))
		{
			hitCount += 1;
			CollBarrelProcess();
			if (hitCount == limitHitCount - 1) { ChangeStateCanMoveBarrel(); }
			if (hitCount == limitHitCount) { StartCoroutine(ExpBarrelProcess()); }
		}
	}

	public void CollBarrelProcess()
	{
		ChangeBarrelMesh();

		if (hitCount == limitHitCount - 1) { isFire = true; }
		PlayBarrelEffect(isFire);
	}

	private void ChangeBarrelMesh()
	{
		int idx = Random.Range(0, meshes.Length);
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
		yield return new WaitForSeconds(expTime);
		HideBarrelandImpactMeshandEffects();
		ShowExpEffect();
		Destroy(barrel, endExpEffectTime);
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