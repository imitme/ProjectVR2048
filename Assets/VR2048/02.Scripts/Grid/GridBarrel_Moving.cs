using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GridBarrel : MonoBehaviour
{
	private bool UpMove(List<CellNum> celLine, bool checkMove, int movePoint) {
		foreach (var cel in celLine) {
			if (cel == null)
				continue;

			checkMove = SetMovingPointofCell(checkMove, cel, cel.c, movePoint);
			movePoint--;
		}

		return checkMove;
	}

	private bool DownMove(List<CellNum> celLine, bool checkMove, int movePoint) {
		foreach (var cel in celLine) {
			if (cel == null)
				continue;

			checkMove = SetMovingPointofCell(checkMove, cel, cel.c, movePoint);
			movePoint++;
		}
		return checkMove;
	}

	private bool RightMove(List<CellNum> celLine, bool checkMove, int movePoint) {
		foreach (var cel in celLine) {
			if (cel == null)
				continue;

			checkMove = SetMovingPointofCell(checkMove, cel, movePoint, cel.r);
			movePoint--;
		}
		return checkMove;
	}

	private bool LeftMove(List<CellNum> celLine, bool checkMove, int movePoint) {
		foreach (var cel in celLine) {
			if (cel == null)
				continue;

			checkMove = SetMovingPointofCell(checkMove, cel, movePoint, cel.r);
			movePoint++;
		}
		return checkMove;
	}

	private void NotMove() {
		Debug.Log("좀 더 정확한 방향을 쏴!");
	}

	private bool SetMovingPointofCell(bool checkMove, CellNum cell, int col, int row) {
		int currCol = cell.c;
		int currRow = cell.r;
		Vector3 currPos = cell.GetComponent<Transform>().position;
		float movingTime = cellMovingTime;

		if (currCol == col && currRow == row) {
			checkMove = false;
			return checkMove;
		}

		cell.c = col;
		cell.r = row;
		StartCoroutine(OnMoving(cell, currPos, col, row, movingTime));

		checkMove = true;
		return checkMove;
	}

	private IEnumerator OnMoving(CellNum movingCell, Vector3 startPos, int targetCol, int targetRow, float movingTime) {
		Vector3 currPos = startPos;
		Vector3 goalPos = PointToVector3(targetCol, targetRow);
		for (float t = 0.0f; t <= movingTime; t += Time.deltaTime) {
			currPos = Vector3.Lerp(startPos, goalPos, t / movingTime);
			movingCell.transform.position = currPos;
			yield return null;  //왜 여기에?????  >>렍더/ 그려지길 기다림.
		}

		movingCell.GetComponent<Transform>().position = goalPos;
		movingCell.name = string.Format("({0}, {1})", targetCol, targetRow);

		if (movingCell.IsMerged == true) {
			movingCell.PlayMergeEffect();
		}
	}
}