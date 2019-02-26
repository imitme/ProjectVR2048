using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public event Action OnRemoveCellNumWhenExpBarrelEvent, ResetGridBarrelEvent;

	//public UIManager getUIManager { get; set; }//이미 전역인 객체로 부터 받기 p117
	public bool isCellMoved;

	public TextMesh scoreText;
	public Text controlPointText;
	private int score = 0;

	public int Score {
		get { return score; }
		set { score = value; scoreText.text = string.Format("Score : {0}", score); }
	}

	private void Awake() {
		if (Instance != null) { Destroy(this); return; }
		Instance = this;
		//   getUIManager = GameObject.FindObjectOfType<UIManager>();
	}

	public void GotoLobby() {
		ResetPlayerInfo();
	}

	private void ResetPlayerInfo() {
		Score = 0;
	}

	public void AddChargeGauge(float chargeGauge) {
		Debug.Log(chargeGauge);
	}

	public void RemoveCellNumWhenExpBarrel() {
		OnRemoveCellNumWhenExpBarrelEvent?.Invoke();
	}

	public void GotoInGame() {
		ResetPlayerInfo();
		ResetGridBarrel();
	}

	private void ResetGridBarrel() {
		ResetGridBarrelEvent?.Invoke();
	}

	public bool CheckIsCellMoved() {
		bool isCellMove = isCellMoved;
		isCellMoved = false;
		return isCellMove;
	}
}