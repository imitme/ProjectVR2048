using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
	public GameObject InGameCanvas;
	public GameObject MenuCanvas;
	public GameObject LobbyCanvas;

	private void Start() {
		OnlyLobbyOn();
	}

	public void OnInGame_Canvas() {
		StartCoroutine(GotoInGame());
	}

	public void OnLobby_Canvas() {
		StartCoroutine(GotoLobby());
	}

	public void OnNewGame_Canvas() {
		StartCoroutine(GotoInGame());
	}

	public void OnOffMenu_Canvas() {
		bool currState = MenuCanvas.activeSelf;
		if (currState != !currState) {
			MenuCanvas.SetActive(!currState);
		}
	}

	private IEnumerator GotoInGame() {
		yield return new WaitForSeconds(0.3f);
		OnlyInGameOn();
		GameManager.Instance.GotoInGame();
	}

	private IEnumerator GotoLobby() {
		yield return new WaitForSeconds(0.5f);
		OnlyLobbyOn();
		GameManager.Instance.GotoLobby();
	}

	private void OnlyLobbyOn() {
		InGameCanvas.SetActive(false);
		MenuCanvas.SetActive(false);
		LobbyCanvas.SetActive(true);
	}

	private void OnlyInGameOn() {
		InGameCanvas.SetActive(true);
		MenuCanvas.SetActive(false);
		LobbyCanvas.SetActive(false);
	}
}