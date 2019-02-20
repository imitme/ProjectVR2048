using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
	public GameObject InGameCanvas;
	public GameObject MenuCanvas;
	public GameObject LobbyCanvas;

	private void Start() {
		OnLobby_Canvas();
	}

	public void OnInGame_Canvas() {
		InGameCanvas.SetActive(true);
		MenuCanvas.SetActive(false);
		LobbyCanvas.SetActive(false);
	}

	public void OnOffMenu_Canvas() {
		bool currState = MenuCanvas.activeSelf;
		if (currState != !currState) {
			MenuCanvas.SetActive(!currState);
		}
	}

	public void OnLobby_Canvas() {
		InGameCanvas.SetActive(false);
		MenuCanvas.SetActive(false);
		LobbyCanvas.SetActive(true);
	}
}