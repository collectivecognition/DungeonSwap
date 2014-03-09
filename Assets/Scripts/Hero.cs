using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	public enum State {None, Walking}
	public Vector2 pos;
	public Tile currentTile;

	// Use this for initialization
	void Start () {
		Tile tile = Game.tiles.GetTile (3, 3);
		if(tile != null){
			MoveTo (tile);
		}else{
			// TODO
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.state == GameState.HeroMoving) {
			Tile tile = Game.tiles.GetRandomWalkableTileFromTile(currentTile);
			MoveTo (tile);
			Game.state = GameState.HeroTurn;
		}
	}

	public void MoveTo (Tile tile){
		if (tile == null) return;

		transform.parent = tile.transform;
		currentTile = tile;
		transform.localPosition = new Vector2 (0, 0);
	}
}
