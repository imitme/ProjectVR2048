using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	//GridBarrel;
	public event Action OnRemoveCellNumWhenExpBarrelEvent, ResetGridBarrelEvent;

	//AsteroidManager;
	public event Action StartAsteroidEvent;

	//public UIManager getUIManager { get; set; }//이미 전역인 객체로 부터 받기 p117
	public bool IsCellMovedCheckforControl { get; set; }

	public TextMesh scoreText;
	public Text controlPointText;
	public Text gameStateText;

	public GAMESTATE GameState { get; set; }

	private int score = 0;

	public int Score {
		get { return score; }
		set { score = value; scoreText.text = string.Format("Score : {0}", score); }
	}

	private void Start() {
		GameState = GAMESTATE.LOAD;
	}

	private void Awake() {
		if (Instance != null) { Destroy(this); return; }
		Instance = this;
		//   getUIManager = GameObject.FindObjectOfType<UIManager>();
	}

	public void GotoLobby() {
		GameState = GAMESTATE.LOAD;
		ResetUI();
	}

	public void AddChargeGauge(float chargeGauge) {
		Debug.Log(chargeGauge);
	}

	public void RemoveCellNumWhenExpBarrel() {
		OnRemoveCellNumWhenExpBarrelEvent?.Invoke();
	}

	public void GotoInGame() {
		GameState = GAMESTATE.START;
		StartAsteroidEvent?.Invoke();
		ResetUI();
		ResetGridBarrel();
	}

	private void ResetUI() {
		Score = 0;
		controlPointText.text = " ";
		gameStateText.text = " ";
	}

	private void ResetGridBarrel() {
		ResetGridBarrelEvent?.Invoke();
	}

	public bool CheckIsCellMoved() {
		bool isCellMove = IsCellMovedCheckforControl;
		IsCellMovedCheckforControl = false;
		return isCellMove;
	}

	public void GameOver() {
		gameStateText.text = "Game Over!\nNo Barrel";
		GameState = GAMESTATE.GAMEOVER;
		Debug.Log(GameState);
	}

	public void GameRestart() {
		gameStateText.text = "Press 'Grip_Button' for Restart Immediately!!";
		GameState = GAMESTATE.RESTART;
	}
}