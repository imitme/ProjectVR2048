using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	private void Update() {
		if (GameManager.Instance.GameState == GAMESTATE.START) {
			GameManager.Instance.PlayTime += Time.deltaTime;
		}
		if (GameManager.Instance.GameState == GAMESTATE.GAMEOVER) {
		}
	}
}