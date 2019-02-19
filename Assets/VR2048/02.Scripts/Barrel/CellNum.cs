using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNum : MonoBehaviour
{
	public int c = 0;
	public int r = 0;
	private bool isExplosion = false;

	private int startNum = 2;
	private int num;
	private TextMesh txt;

	public int Num
	{
		get { return num; }
		set { num = value; txt.text = num.ToString(); }
	}

	public bool IsExplosion
	{
		get { return isExplosion; }
		set { isExplosion = value; }
	}

	private void Awake()
	{
		txt = GetComponentInChildren<TextMesh>();
		Num = startNum;
	}

	public void HideTxtwhenBarrelExp()
	{
		txt.text = " ";
	}

	public void OnExplosion()
	{
		IsExplosion = true;
	}
}