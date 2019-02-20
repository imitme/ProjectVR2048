using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
	[Serializable] public class MoveEvent : UnityEvent<DIRECTION> { }

	public MoveEvent moveEvent;

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
		moveEvent?.Invoke(DIRECTION.COUNT);
	}

	public void OnContinue_Button() {
		Debug.Log("OnContinue_Button");
	}

	public void OnNewGame_Button() {
		Debug.Log("OnNewGame_Button");
	}

	public void OnQuit_Button() {
		Debug.Log("OnQuit_Button");
	}
}