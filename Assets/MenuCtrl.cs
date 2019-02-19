using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class MenuCtrl : MonoBehaviour
{
	public HandRole handR = HandRole.RightHand;
	public HandRole handL = HandRole.LeftHand;
	public ControllerButton menuButton = ControllerButton.Menu;

	private void Update()
	{
		if (ViveInput.GetPressDown(handR, menuButton) || ViveInput.GetPressDown(handL, menuButton))
		{
			Debug.Log("PressDown :" + " / " + menuButton);
			//OnOffMenu();
		}
	}
}