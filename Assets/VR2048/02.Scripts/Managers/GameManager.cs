﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public event Action OnExpBarrel;

	//public UIManager getUIManager { get; set; }//이미 전역인 객체로 부터 받기 p117

	private void Awake() {
		if (Instance != null) { Destroy(this); return; }
		Instance = this;
		//   getUIManager = GameObject.FindObjectOfType<UIManager>();
	}

	public void ExpBarrel() {
		OnExpBarrel?.Invoke();
	}

	public void StartGame() {
	}
}