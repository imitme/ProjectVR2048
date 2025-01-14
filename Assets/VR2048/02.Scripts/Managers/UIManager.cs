﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HTC.UnityPlugin.Vive;

public class UIManager : MonoBehaviour
{
	[Serializable] public class MoveEvent : UnityEvent<DIRECTION> { }

	public MoveEvent moveEvent;
	public UnityEvent OnInGameEvent;
	public UnityEvent OnOffMenuEvent;
	public UnityEvent OnNewGameEvent;
	public UnityEvent OnLobbyEvent;

	public HandRole handR = HandRole.RightHand;
	public HandRole handL = HandRole.LeftHand;
	public ControllerButton menuButton = ControllerButton.Menu;
	public ControllerButton gripButton = ControllerButton.Grip;
	public ControllerButton padButton = ControllerButton.Pad;

	private void Update() {
		if (ViveInput.GetPressDown(handR, menuButton) || ViveInput.GetPressDown(handL, menuButton)
			|| ViveInput.GetPressDown(handR, gripButton) || ViveInput.GetPressDown(handL, gripButton)) {
			OnOffMenuEvent?.Invoke();
		}
		if (GameManager.Instance.GameState == GAMESTATE.RESTART) {
			//if (ViveInput.GetPressDown(handR, gripButton) || ViveInput.GetPressDown(handL, gripButton)) {
			if (ViveInput.GetPressDown(handR, padButton) || ViveInput.GetPressDown(handL, padButton)) {
				OnInGameEvent?.Invoke();
			}
		}
	}

	//------------------- InGame
	public void OnR_Button() {
		moveEvent?.Invoke(DIRECTION.RIGHT);
	}

	public void OnL_Button() {
		moveEvent?.Invoke(DIRECTION.LEFT);
	}

	public void OnU_Button() {
		moveEvent?.Invoke(DIRECTION.UP);
	}

	public void OnD_Button() {
		moveEvent?.Invoke(DIRECTION.DOWN);
	}

	public void OnNot_Button() {
		moveEvent?.Invoke(DIRECTION.count);
	}

	//-------------------Lobby
	public void OnStart_Button() {
		OnInGameEvent?.Invoke();
	}

	//-------------------Menu
	public void OnContinue_Button() {
		OnOffMenuEvent?.Invoke();
	}

	public void OnNewGame_Button() {
		OnNewGameEvent?.Invoke();

		Debug.Log("OnNewGame_Button");
	}

	public void OnQuit_Button() {
		OnLobbyEvent?.Invoke();
	}
}