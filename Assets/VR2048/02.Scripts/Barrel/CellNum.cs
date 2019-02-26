using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNum : MonoBehaviour
{
	public int c = 0;
	public int r = 0;

	public GameObject mergeEffect;

	private int startNum = 2;
	private TextMesh txt;

	private int num;

	public int Num {
		get { return num; }
		set { num = value; txt.text = num.ToString(); }
	}

	public bool IsExploded {
		get;
		set;
	}

	public bool IsMerged { get; set; } = false;

	private void Awake() {
		txt = GetComponentInChildren<TextMesh>();
		Num = startNum;
	}

	public void HideTxtwhenExpBarrel() {
		txt.text = " ";
	}

	public void ChangeStatetoExplosion() {
		IsExploded = true;
	}

	public void PlayMergeEffect() {
		IsMerged = false;

		Vector3 mergePos = GetComponent<Transform>().position;
		Quaternion rot = Quaternion.identity;
		Instantiate(mergeEffect, mergePos, rot);
	}

	public void ChangeStatetoMerged() {
		IsMerged = true;
	}
}