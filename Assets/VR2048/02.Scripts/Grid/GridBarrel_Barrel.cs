using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GridBarrel : MonoBehaviour
{
	public void RemoveCellNumforExpBarrel() {
		CheckandRemoveCellNumExped();
		CheckEmptyCellNumsandChangeStatetoGameOver();
	}

	private void CheckandRemoveCellNumExped() {
		int removeIndex = 0;
		for (int index = 0; index < cellNums.Count; index++) {
			if (cellNums[index].IsExploded == true) {
				removeIndex = index;
			}
		}
		cellNums.RemoveAt(removeIndex);
	}

	private void CheckEmptyCellNumsandChangeStatetoGameOver() {
		if (cellNums.Count == 0) {
			GameManager.Instance.GameOver();
		}
	}
}