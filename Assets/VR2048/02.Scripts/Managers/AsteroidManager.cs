using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
	public Vector3 spawnValues;
	public GameObject[] hazards;
	private int hazardCount;
	private float spawnWait;
	private float startWait;
	private float waveWait;
	private float eyePos = 1.75f;

	private void Start() {
		hazardCount = 10;
		spawnWait = 0.75f;
		startWait = 1.0f;
		waveWait = 4.0f;
		StartCoroutine(SpawnWaves());
	}

	private void OnEnable() {
		GameManager.Instance.StartAsteroidEvent += StartAsteroid;
	}

	private void OnDisable() {
		GameManager.Instance.StartAsteroidEvent -= StartAsteroid;
	}

	private void StartAsteroid() {
		StartCoroutine(SpawnWaves());
	}

	private IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while (true) {
			if (GameManager.Instance.GameState == GAMESTATE.START) {
				for (int i = 0; i < hazardCount; i++) {
					GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
					Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x),
														UnityEngine.Random.Range(-spawnValues.y + eyePos / 2, spawnValues.y + eyePos / 2),
														spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate(hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds(spawnWait);
				}
			}
			yield return new WaitForSeconds(waveWait);

			if (GameManager.Instance.GameState == GAMESTATE.GAMEOVER) {
				GameManager.Instance.GameRestart();
				break;
			}
		}
	}
}