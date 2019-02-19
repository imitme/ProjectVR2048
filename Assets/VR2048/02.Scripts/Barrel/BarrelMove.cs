using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMove : MonoBehaviour
{
	[SerializeField] private string collisionTagNameforBarrelMove = "FORBARRELMOVE";

	public DIRECTION currDir = DIRECTION.RIGHT;

	public void OnBarrelMove()
	{
		Debug.Log("Move : " + currDir);
	}
}