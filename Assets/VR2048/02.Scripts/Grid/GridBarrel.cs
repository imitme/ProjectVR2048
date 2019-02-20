using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GridBarrel : MonoBehaviour
{
	public GameObject gridCellsPanel;
	public GameObject gridCellPrefab;
	public GameObject cellNumsPanel;
	public GameObject cellNumsPrefab;

	private float myCellSize;
	private Vector3 firstPos = Vector3.zero;

	//변경가능 요소들

	private int totalCount = 4;
	private int testNumCellCountLimit = 1;
	private float cellMovingTime = 0.1f;
	private float cellSpan = 1.2f;
	//--

	public List<CellNum> cellNums;

	private void Start() {
		ResetPanelProcess();
	}

	private void OnEnable() {
		GameManager.Instance.OnExpBarrel += RemoveCellNum;
	}

	private void OnDisable() {
		GameManager.Instance.OnExpBarrel -= RemoveCellNum;
	}

	public void ResetPanelProcess() {
		SetGridMap(totalCount);
		SetCells(totalCount);
		DrawRandomCells(totalCount, testNumCellCountLimit);
	}

	private void SetGridMap(int count) {
		var myPanel = gridCellsPanel.GetComponent<Transform>();
		var midSize = cellSpan * ((float)totalCount - 1) * 0.5f;
		var firstPositionX = myPanel.position.x - midSize;
		var firstPositionY = myPanel.position.y - midSize + 1.75f;
		var firstPositionZ = myPanel.position.z + midSize;

		Vector3 firstPosition = new Vector3(firstPositionX, firstPositionY, firstPositionZ);
		firstPos = firstPosition;

		var cellSize = cellSpan;
		myCellSize = cellSize;
	}

	private void SetCells(int count) {
		for (int c = 0; c < count; c++) {
			for (int r = 0; r < count; r++) {
				DrawCells(gridCellPrefab, gridCellsPanel, c, r, myCellSize, string.Format("Cell ({0}, {1})", c, r));
			}
		}
	}

	private void DrawCells(GameObject cellPrefab, GameObject CellsPanel, int c, int r, float cellSize, string cellname) {
		GameObject cel = Instantiate(cellPrefab, CellsPanel.transform);
		cel.GetComponent<Transform>().position = PointToVector3(c, r);
		cel.name = cellname;
	}

	private Vector3 PointToVector3(int col, int row) {
		return new Vector3(firstPos.x + col * myCellSize, firstPos.y + row * myCellSize, firstPos.z);
	}

	private void DrawRandomCells(int count, int totalCellNum) {
		int limitCount = 0;

		while (limitCount < totalCellNum) {
			int col = Random.Range(0, count);
			int row = Random.Range(0, count);

			if (IsEmpty(col, row)) {
				DrawCellNum(cellNumsPrefab, cellNumsPanel, col, row);
				limitCount++;
			}
		}
	}

	private bool IsEmpty(int col, int row) {
		foreach (CellNum cellNum in cellNums) {
			if (cellNum.c == col && cellNum.r == row) {
				return false;
			}
		}
		return true;
	}

	private void DrawCellNum(GameObject cellNumPrefab, GameObject cellNumPanel, int col, int row) {
		GameObject cel = Instantiate(cellNumPrefab, cellNumPanel.transform);
		cel.GetComponent<Transform>().position = PointToVector3(col, row);

		var cellNum = cel.GetComponent<CellNum>();
		cellNum.c = col;
		cellNum.r = row;
		cellNum.name = string.Format("({0}, {1})", cellNum.c, cellNum.r);
		cellNums.Add(cellNum);
	}
}