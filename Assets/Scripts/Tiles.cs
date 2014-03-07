using UnityEngine;
using System.Collections;

public class Tiles : MonoBehaviour {

	public Transform tilePrefab;

	private int tileW = 7;
	private int tileH = 7;
	private Transform[,] tiles;

	private enum DragDir {None, Horizontal, Vertical};
	private bool dragging = false;
	private DragDir draggingDir = DragDir.None;
	private int draggingRow;
	private int draggingCol;
	private Vector3 draggingPos;

	// Use this for initialization

	void Start () {
		tiles = new Transform[tileW, tileH];

		for(int ii = 0; ii < tileW; ii++){
			for(int jj = 0; jj < tileH; jj++){
				Transform tile = (Transform)Instantiate(tilePrefab);
				tile.parent = this.transform;
				tile.transform.localPosition = new Vector3(ii - tileW / 2, jj - tileH / 2, 0f);
				tiles[ii, jj] = tile;
			}
		}
	}
	
	// Update is called once per frame

	void Update () {
		if(dragging){
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			float diff = Vector3.Distance(mouseWorldPos, draggingPos);
			float diffX = mouseWorldPos.x - draggingPos.x;
			float diffY = mouseWorldPos.y - draggingPos.y;

			// Determine which direction we're dragging

			if(draggingDir == DragDir.None && diff > 0.1f){
				draggingDir = Mathf.Abs(diffX) > Mathf.Abs(diffY) ? DragDir.Horizontal : DragDir.Vertical;

				// Determine which row or column is being dragged
				
				draggingCol = -(int)(transform.position.x - mouseWorldPos.x - tileW / 2);
				draggingRow = -(int)(transform.position.y - mouseWorldPos.y - tileH / 2);
			}

			// Apply offset to row / column
			
			switch(draggingDir){
				case DragDir.Horizontal:
					// Set offset on each tile in row
					
					for(int ii = 0; ii < tileW; ii++){
						Transform tile = tiles[ii, draggingRow];
						tile.localPosition = new Vector3(ii - tileW / 2 + diffX, tile.localPosition.y, tile.localPosition.z);
					}
					break;
				
				case DragDir.Vertical:
					// Set offset on each tile in column
					
					for(int ii = 0; ii < tileH; ii++){
						Transform tile = tiles[draggingCol, ii];
						tile.localPosition = new Vector3(tile.localPosition.x, ii - tileH / 2 + diffY, tile.localPosition.z);
					}	
					break;
			}
		}
	}
	
	void OnMouseDown () {
		dragging = true;
		draggingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
	
	void OnMouseUp () {
		dragging = false;
		draggingDir = DragDir.None;
	}
	
	void OnMouseDrag () {
		if (dragging) {

		}
	}
}
