using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
	
	public enum State {None, Walking}
	public State state = State.None;
	public Vector2 pos;
	public Tile currentTile;
	public Vector3 walkTarget;
	public float walkSpeed = 2f;
	public Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		
		currentTile = transform.parent.GetComponent<Tile> ();
	}
	
	// Update is called once per frame
	void Update () {
		// if (Game.state == GameState.AIMoving) {
			if(state == State.Walking){
				transform.localPosition = Vector3.MoveTowards (transform.localPosition, walkTarget, walkSpeed * Time.deltaTime);
				
				if(Vector3.Distance(transform.localPosition, walkTarget) == 0){
					// Arrived
					
					AttachToTile(currentTile);
					animator.SetBool("idle", true);
					state = State.None;
					Game.state = GameState.HeroTurn; // FIXME
				}
			}
		// }

		if (Game.state == GameState.AITurn && state == State.None){
			Tile tile = Game.tiles.GetRandomWalkableTileFromTile(currentTile);
			MoveTo (tile);
		}
	}
	
	public void AttachToTile(Tile tile){
		transform.parent = tile.transform;
		transform.localPosition = new Vector2 (0, 0);
	}
	
	public void MoveTo (Tile tile){
		if (state == State.None && tile != null){{
				int direction = 0;
				
				if(tile.pos.y < currentTile.pos.y){
					direction = 2;
					walkTarget = new Vector3(0, -1, 0);
				}
				
				if(tile.pos.y > currentTile.pos.y){
					direction = 0;
					walkTarget = new Vector3(0, 1, 0);
				}
				
				if(tile.pos.x < currentTile.pos.x){
					direction = 3;
					walkTarget = new Vector3(-1, 0, 0);
				}
				
				if(tile.pos.x > currentTile.pos.x){
					direction = 1;
					walkTarget = new Vector3(1, 0, 0);
				}
				
				animator.SetInteger ("direction", direction);
				animator.SetBool ("idle", false);
				
				state = State.Walking;
				currentTile = tile;
			}
		}
	}
}
