using UnityEngine;
using System.Collections;

public class Tiles : MonoBehaviour {

	public Transform tilePrefab;

	private int tileW = 7;
	private int tileH = 7;
	private Transform[,] tiles;

	private enum DragDir {None, Horizontal, Vertical};
	private enum DragState {None, Dragging, Snapping};

	private DragState dragState = DragState.None;
	private DragDir draggingDir = DragDir.None;
	private int draggingRow;
	private int draggingCol;
	private Vector3 draggingPos;
	private Vector2 draggingOffset;
	private float dragMax = 1f;

	private float snapSpeed = 4f;

	void AddTile (int x, int y){
		Transform tile = (Transform)Instantiate(tilePrefab);
		tile.parent = this.transform;
		tile.transform.localPosition = new Vector3(x - tileW / 2, y - tileH / 2, 0f);
		tiles[x, y] = tile;
	}

	// Use this for initialization

	void Start () {
		tiles = new Transform[tileW, tileH];

		for(int ii = 0; ii < tileW; ii++){
			for(int jj = 0; jj < tileH; jj++){
				AddTile (ii, jj);
			}
		}
	}
	
	// Update is called once per frame

	void Update () {
		// Handle dragging of rows / columns of tiles

		if(dragState == DragState.Dragging){
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			float diff = Vector3.Distance(mouseWorldPos, draggingPos);
			float diffX = mouseWorldPos.x - draggingPos.x;
			float diffY = mouseWorldPos.y - draggingPos.y;

			diffX = Mathf.Clamp(diffX, -dragMax, dragMax);
			diffY = Mathf.Clamp(diffY, -dragMax, dragMax);

			// Determine which direction we're dragging

			if(draggingDir == DragDir.None && diff > 0.1f){
				draggingDir = Mathf.Abs(diffX) > Mathf.Abs(diffY) ? DragDir.Horizontal : DragDir.Vertical;

				// Determine which row or column is being dragged
				
				draggingCol = -(int)(transform.position.x - mouseWorldPos.x - tileW / 2 - 0.5f);
				draggingRow = -(int)(transform.position.y - mouseWorldPos.y - tileH / 2 - 0.5f);
			}

			draggingOffset = new Vector2(diffX, diffY);
		}

		// Handle snapping of rows / columns of tiles to the grid after dragging stops

		if(dragState == DragState.Snapping){
			Vector2 target;
			
			if(draggingDir == DragDir.Horizontal){
				target = new Vector2(draggingOffset.x > 0 ? 1 : -1, 0);
			}else{
				target = new Vector2(0, draggingOffset.y > 0 ? 1 : -1);
			}

			// Are we done snapping?

			if(draggingOffset.x == target.x && draggingOffset.y == target.y){
				// Update tiles array to reflect changes

				switch(draggingDir){
					case DragDir.Horizontal:
						if(target.x > 0){
							Destroy(tiles[tileW - 1, draggingRow].gameObject);
							for(int ii = tileW - 1; ii > 0; ii--){
								tiles[ii, draggingRow] = tiles[ii - 1, draggingRow];
							}
							AddTile (0, draggingRow);
						}

						if(target.x < 0){
							Destroy(tiles[0, draggingRow].gameObject);
							for(int ii = 0; ii < tileW - 1; ii++){
								tiles[ii, draggingRow] = tiles[ii + 1, draggingRow];
							}
							AddTile (tileW - 1, draggingRow);
						}
					break;
						
					case DragDir.Vertical:
						if(target.y > 0){
							Destroy(tiles[draggingCol, tileH - 1].gameObject);
							for(int ii = tileH - 1; ii > 0; ii--){
								tiles[draggingCol, ii] = tiles[draggingCol, ii - 1];
							}
							AddTile (draggingCol, 0);
						}
						
						if(target.y < 0){
							Destroy(tiles[draggingCol, 0].gameObject);
							for(int ii = 0; ii < tileH - 1; ii++){
								tiles[draggingCol, ii] = tiles[draggingCol, ii + 1];
							}
							AddTile (draggingCol, tileH - 1);
						}
					break;
				}

				// Reset state
				
				dragState = DragState.None;
				draggingDir = DragDir.None;

			// Not done snapping, move towards target

			}else{
				draggingOffset = Vector2.MoveTowards(draggingOffset, target, Time.deltaTime * snapSpeed);
			}
		}

		// Handle actual movement of tiles

		if(dragState != DragState.None){
			switch(draggingDir){
				case DragDir.Horizontal:
					// Set offset on each tile in row
					
					for(int ii = 0; ii < tileW; ii++){
						Transform tile = tiles[ii, draggingRow];
					tile.localPosition = new Vector3(ii - tileW / 2 + draggingOffset.x, tile.localPosition.y, tile.localPosition.z);
					}
				break;
					
				case DragDir.Vertical:
					// Set offset on each tile in column
					
					for(int ii = 0; ii < tileH; ii++){
						Transform tile = tiles[draggingCol, ii];
					tile.localPosition = new Vector3(tile.localPosition.x, ii - tileH / 2 + draggingOffset.y, tile.localPosition.z);
					}	
				break;
			}
		}
	}
	
	void OnMouseDown () {
		if(dragState == DragState.None){
			dragState = DragState.Dragging;
			draggingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
	
	void OnMouseUp () {
		if(dragState == DragState.Dragging){
			dragState = DragState.Snapping;
		}
	}
	
	void OnMouseDrag () {
		// ???
	}
}
