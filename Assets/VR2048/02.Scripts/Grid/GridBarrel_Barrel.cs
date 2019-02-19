using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GridBarrel : MonoBehaviour
{
	public void RemoveCellNum()
	{
		int removeIndex = 0;
		for (int index = 0; index < cellNums.Count; index++)
		{
			if (cellNums[index].IsExplosion == true)
			{
				removeIndex = index;
			}
		}
		cellNums.RemoveAt(removeIndex);
	}
}