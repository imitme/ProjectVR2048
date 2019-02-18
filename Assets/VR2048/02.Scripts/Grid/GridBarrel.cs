using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBarrel : MonoBehaviour
{
	public GameObject gridCellsPanel;
	public GameObject gridCellPrefab;

	private float myCellSize;
	private float cellSpan = 1.2f;
	private Vector3 firstPos = Vector3.zero;

	private int totalCount = 4;
	private int testNumCellCountLimit = 1;

	// Start is called before the first frame update
	private void Start()
	{
		ResetPanel();
	}

	public void ResetPanel()
	{
		SetGridMap(totalCount);
		SetCells(totalCount);
	}

	private void SetGridMap(int count)
	{
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

	private void SetCells(int count)
	{
		for (int c = 0; c < count; c++)
		{
			for (int r = 0; r < count; r++)
			{
				DrawCells(gridCellPrefab, gridCellsPanel, c, r, myCellSize, string.Format("Cell ({0}, {1})", c + 1, r + 1));
			}
		}
	}

	private void DrawCells(GameObject cellPrefab, GameObject CellsPanel, int c, int r, float cellSize, string cellname)
	{
		GameObject cel = Instantiate(cellPrefab, CellsPanel.transform);
		cel.GetComponent<Transform>().position = PointToVector3(c, r);
		cel.name = cellname;
	}

	private Vector3 PointToVector3(int col, int row)
	{
		return new Vector3(firstPos.x + col * myCellSize, firstPos.y + row * myCellSize, firstPos.z);
	}
}