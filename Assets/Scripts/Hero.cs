using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

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

		Tile tile = Game.tiles.GetTile (3, 3);
		if(tile != null){
			currentTile = tile;
			AttachToTile(tile);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.state == GameState.HeroMoving) {
			if(state == State.Walking){
				transform.localPosition = Vector3.MoveTowards (transform.localPosition, walkTarget, walkSpeed * Time.deltaTime);

				if(Vector3.Distance(transform.localPosition, walkTarget) == 0){
					// Arrived
					
					AttachToTile(currentTile);
					animator.SetBool("idle", true);
					state = State.None;
					Game.state = GameState.AITurn;

					// Bumped into a chest?
					// FIXME
					
					if(currentTile.content != null){
						currentTile.content.SendMessage("Open");
					}
				}
			}
		}
	}

	public void AttachToTile(Tile tile){
		transform.parent = tile.transform;
		transform.localPosition = new Vector2 (0, 0);
	}

	public void MoveTo (Tile tile){
		// Tile tile = Game.tiles.GetRandomWalkableTileFromTile(currentTile);
		if(state != State.None && (tile == null || tile.pos == currentTile.pos)){
			Game.state = GameState.HeroTurn;
		}else
			if (state == State.None){{
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
