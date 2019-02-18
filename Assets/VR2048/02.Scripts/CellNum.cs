using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNum : MonoBehaviour
{
	public int c = 0;
	public int r = 0;

	private int startNum = 2;
	private int num;
	private TextMesh txt;

	public int Num
	{
		get { return num; }
		set { num = value; txt.text = num.ToString(); }
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
}