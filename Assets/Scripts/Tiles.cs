using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	private enum ScrollState {None, Scrolling};
	private ScrollState scrollState = ScrollState.None;

	public void AddTile (int x, int y){
		Transform tile = (Transform)Instantiate(tilePrefab);
		tile.parent = this.transform;
		tile.transform.localPosition = new Vector3(x - tileW / 2, y - tileH / 2, 0f);
		tiles[x, y] = tile;
	}

	public void AddTile (float x, float y){
		AddTile ((int)x, (int)y);
	}

	public void RemoveTile (int x, int y){
		Destroy(tiles[x, y].gameObject);
		tiles [x, y] = null;
	}

	public void RemoveTile (float x, float y){
		RemoveTile ((int)x, (int)y);
	}

	public Tile GetTile (int x, int y){
		if(TileIsInBounds(x, y)){
			return tiles [x, y].GetComponent<Tile> ();
		}
		return default(Tile);
	}

	public Tile GetTile (float x, float y){
		return GetTile ((int)x, (int)y);
	}

	public void MoveTile (int x, int y, int newX, int newY){
		tiles [newX, newY] = tiles [x, y];
		tiles [x, y] = null;
	}

	public void MoveTile (float x, float y, float newX, float newY){
		MoveTile ((int)x, (int)y, (int)newX, (int)newY);
	}


	public bool TileIsInBounds (int x, int y){
		return x >= 0 && y >= 0 && x <= tiles.GetUpperBound (0) && y <= tiles.GetUpperBound (1);
	}

	public bool TileIsInBounds (float x, float y){
		return TileIsInBounds ((int)x, (int)y);
	}

	public int GetRandomWalkableDirection(Vector2 start){
		List<int> walkableDirections = new List<int> ();

		// TODO: Check bounds on start.x, start.y

		Debug.Log ("Checking " + start);	

		Tile startTile = GetTile (start.x, start.y);

		Vector2[] directions = new Vector2[] {
			new Vector2(0, -1),	// Up
			new Vector2(1, 0),	// Right
			new Vector2(0, 1),	// Down
			new Vector2(-1, 0)	// Left
		};

		for(int ii = 0; ii < directions.Length; ii++){
			Vector2 direction = directions[ii];
			Vector2 pos = start + direction;

			// Check bounds

			if(TileIsInBounds (pos.x, pos.y)){
				Tile tile = GetTile (pos.x, pos.y);
				bool walkable = false;
				Debug.Log (start+":"+pos);
				Debug.Log (ii + ":" + Directions.Opposite[ii]);
				Debug.Log (startTile.walls[ii].GetComponent<Wall>().walkable + ":" + tile.walls[Directions.Opposite[ii]].GetComponent<Wall>().walkable);
				if(startTile.walls[ii].GetComponent<Wall>().walkable){
					if(tile.walls[Directions.Opposite[ii]].GetComponent<Wall>().walkable){
						walkable = true;
					}
				}

				if(walkable){
					walkableDirections.Add (ii);
				}
			}
		}

		if(walkableDirections.Count > 0){
			int randomDirection = walkableDirections[Random.Range (0, walkableDirections.Count - 1)];
			Debug.Log ("Walkable: " + randomDirection);
			return randomDirection;
		}

		Debug.Log ("Not walkable");

		return 5; // FIXME: Dear god
	}

	void Scroll (int direction) {
		scrollState = ScrollState.Scrolling;
	}

	// Use this for initialization

	void Start () {
		tiles = new Transform[tileW, tileH];

		for(int ii = 0; ii < tileW; ii++){
			for(int jj = 0; jj < tileH; jj++){
				AddTile (ii, jj);
			}
		}

		Game.tiles = this;
	}

	// Update is called once per frame

	void Update () {
		// Handle scrolling of tiles

		if(scrollState == ScrollState.Scrolling){

		}

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
							RemoveTile (tileW - 1, draggingRow);
							for(int ii = tileW - 1; ii > 0; ii--){
								MoveTile(ii - 1, draggingRow, ii, draggingRow);
							}
							AddTile (0, draggingRow);
						}

						if(target.x < 0){
							RemoveTile (0, draggingRow);
							for(int ii = 0; ii < tileW - 1; ii++){
								MoveTile (ii + 1, draggingRow, ii, draggingRow);
							}
							AddTile (tileW - 1, draggingRow);
						}
					break;
						
					case DragDir.Vertical:
						if(target.y > 0){
							RemoveTile (draggingCol, tileH - 1);
							for(int ii = tileH - 1; ii > 0; ii--){
								tiles[draggingCol, ii] = tiles[draggingCol, ii - 1];
							}
							AddTile (draggingCol, 0);
						}
						
						if(target.y < 0){
							RemoveTile (draggingCol, 0);
							for(int ii = 0; ii < tileH - 1; ii++){
								MoveTile (draggingCol, ii + 1, draggingCol, ii);
							}
							AddTile (draggingCol, tileH - 1);
						}
					break;
				}

				// Reset state
				
				dragState = DragState.None;
				draggingDir = DragDir.None;

				// Take turn

				// Debug.Log ("Sending FSM event to take turn" + Random.value);
				// PlayMakerFSM.FindFsmOnGameObject(hero.gameObject, "FSM").SendEvent("TakeTurn");

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
						Tile tile = GetTile (ii, draggingRow);
						tile.transform.localPosition = new Vector3(ii - tileW / 2 + draggingOffset.x, tile.transform.localPosition.y, tile.transform.localPosition.z);
					}
				break;
					
				case DragDir.Vertical:
					// Set offset on each tile in column
					
					for(int ii = 0; ii < tileH; ii++){
						Tile tile = GetTile (draggingCol, ii);
						tile.transform.localPosition = new Vector3(tile.transform.localPosition.x, ii - tileH / 2 + draggingOffset.y, tile.transform.localPosition.z);
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
