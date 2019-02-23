using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public event Action OnRemoveCellNumWhenExpBarrel;

	public Vector3 spawnValues;
	public GameObject[] hazards;
	private int hazardCount;
	private float spawnWait;
	private float startWait;
	private float waveWait;

	//public UIManager getUIManager { get; set; }//이미 전역인 객체로 부터 받기 p117

	private void Awake() {
		if (Instance != null) { Destroy(this); return; }
		Instance = this;
		//   getUIManager = GameObject.FindObjectOfType<UIManager>();
	}

	private void Start() {
		hazardCount = 10;
		spawnWait = 0.75f;
		startWait = 1.0f;
		waveWait = 4.0f;
		StartCoroutine(SpawnWaves());
	}

	private IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}

	public void AddChargeGauge(float chargeGauge) {
		Debug.Log(chargeGauge);
	}

	public void RemoveCellNumWhenExpBarrel() {
		OnRemoveCellNumWhenExpBarrel?.Invoke();
	}

	public void StartGame() {
	}
}