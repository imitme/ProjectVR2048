using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GridBarrel : MonoBehaviour
{
	public void OnMovetoDir(DIRECTION dir) {
		Debug.Log("OnMovetoDir : " + dir);
		StartCoroutine(MovetoDirProcess(dir));
	}

	private IEnumerator MovetoDirProcess(DIRECTION dir) {
		Debug.Log("MovetoDirProcess : " + dir);
		RemoveCellNumMerged();
		//CheckEmptyCellNumsforGameOver();
		bool isMove = GetCellsDirLine(dir);
		GameManager.Instance.IsCellMovedCheckforControl = isMove;
		yield return new WaitForSeconds(cellMovingTime);
		DrawOneCell(isMove);
	}

	private void RemoveCellNumMerged() {
		cellNums.RemoveAll(cn => cn == null); //합쳐졌을때, 없어진거 찾아서 리스트에서 빼는거.
	}

	private bool GetCellsDirLine(DIRECTION dir) {
		bool checkMove = false;
		int dirCol = 0;
		int dirRow = 0;
		int startPoint = 0;

		switch (dir) {
			case DIRECTION.UP:
				dirRow = 1;
				startPoint = totalCount - 1;

				break;

			case DIRECTION.DOWN:
				dirRow = -1;

				break;

			case DIRECTION.RIGHT:
				dirCol = 1;
				startPoint = totalCount - 1;
				break;

			case DIRECTION.LEFT:
				dirCol = -1;

				break;

			case DIRECTION.count:
				break;

			default:
				break;
		}

		checkMove = CheckandMoveCells(dir, dirCol, dirRow, startPoint);

		return checkMove;
	}

	private bool CheckandMoveCells(DIRECTION dir, int dirCol, int dirRow, int startPoint) {
		bool checkMove = false;
		bool checkMerge = false;
		int checkMergeCount = 0;
		int checkMoveCount = 0;

		for (int lineCount = 0; lineCount < totalCount; lineCount++) {
			int movePoint = startPoint;
			List<CellNum> celLine = new List<CellNum>();

			GetJustOneLineList(celLine, lineCount, dirRow);

			switch (dir) {
				case DIRECTION.UP:
					//정렬
					celLine.Sort((a, b) => b.r.CompareTo(a.r));
					//합치기
					checkMerge = MergeCellNum(celLine);
					if (checkMerge == true)
						checkMergeCount++;
					checkMove = UpMove(celLine, checkMove, movePoint);
					if (checkMove == true)
						checkMoveCount++;
					break;

				case DIRECTION.DOWN:
					celLine.Sort((a, b) => a.r.CompareTo(b.r));
					checkMerge = MergeCellNum(celLine);
					if (checkMerge == true)
						checkMergeCount++;
					checkMove = DownMove(celLine, checkMove, movePoint);
					if (checkMove == true)
						checkMoveCount++;
					break;

				case DIRECTION.RIGHT:
					celLine.Sort((a, b) => b.c.CompareTo(a.c));
					checkMerge = MergeCellNum(celLine);
					if (checkMerge == true)
						checkMergeCount++;
					checkMove = RightMove(celLine, checkMove, movePoint);
					if (checkMove == true)
						checkMoveCount++;
					break;

				case DIRECTION.LEFT:
					celLine.Sort((a, b) => a.c.CompareTo(b.c));
					checkMerge = MergeCellNum(celLine);
					if (checkMerge == true)
						checkMergeCount++;
					checkMove = LeftMove(celLine, checkMove, movePoint);
					if (checkMove == true)
						checkMoveCount++;
					break;

				case DIRECTION.count:
					NotMove();

					break;

				default:
					break;
			}
		}

		return CheckCellMergeandMove(checkMerge, checkMove, checkMergeCount, checkMoveCount);
	}

	private void GetJustOneLineList(List<CellNum> cellLine, int lineCount, int checkLineAsRow) {
		foreach (var cel in cellNums) {
			if (checkLineAsRow == 0)    //행 단위로 줄 묶기 //좌우 버튼을 눌렀다는 뜻
			{
				if (lineCount == cel.r)    //행이 같은 애들 찾아
				{
					var cellinline = GetCellNum(cel.c, cel.r);
					cellLine.Add(cellinline);
				}
			} else    //열 단위로 줄 묶기 //위아래 버튼을 눌렀다는 뜻
			  {
				if (lineCount == cel.c)     //열이 같은 애들 찾아.
				{
					var cellinline = GetCellNum(cel.c, cel.r);
					cellLine.Add(cellinline);
				}
			}
		}
	}

	private CellNum GetCellNum(int col, int row) {
		foreach (CellNum cellNum in cellNums) {
			if (cellNum.c == col && cellNum.r == row) {
				return cellNum;
			}
		}
		return null;
	}

	private bool CheckCellMergeandMove(bool checkMerge, bool checkMove, int checkMergeCount, int checkMoveCount) {
		if (checkMergeCount > 0) {
			checkMerge = true;
		}
		if (checkMoveCount > 0) {
			checkMove = true;
		}

		return checkMerge || checkMove;
	}

	private bool MergeCellNum(List<CellNum> celLine) {
		bool checkMove = false;
		///정렬된 celLine에 있는 것의 숫자를 비교해!
		for (int cellPoint = 0; cellPoint < celLine.Count; cellPoint++) {
			int currentCell = cellPoint;
			int nextCell = cellPoint + 1;

			if (nextCell >= celLine.Count)
				break;

			if (celLine[currentCell].Num == celLine[nextCell].Num) {
				///점수 보내주고
				GameManager.Instance.Score += celLine[currentCell].Num;

				//합쳐진 후 폭발!
				//celLine[currentCell].PlayMergeEffect();
				//합친 상태로 변경!
				celLine[currentCell].ChangeStatetoMerged();
				celLine[nextCell].ChangeStatetoMerged();

				///숫자 합쳐주고.
				int mergeNum = celLine[nextCell].Num;
				mergeNum += mergeNum;
				celLine[nextCell].Num = mergeNum;

				///i+1 없앤다 >> 현재셀을 없애는데, 움직시는 시간 지난 후에!
				DestroyImmediate(celLine[currentCell].gameObject);
				celLine.RemoveAt(currentCell);

				///움직임체크!
				checkMove = true;
			}
		}

		RemoveCellNumMerged();

		return checkMove;
	}

	private void DrawOneCell(bool isMove) {
		Debug.Log(isMove);
		if (!isMove)
			return;
		else if (isMove) {
			DrawRandomCells(totalCount, 1);
		}
	}
}