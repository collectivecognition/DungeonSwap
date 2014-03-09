using UnityEngine;
using System.Collections;

static class GameState {
	public static int None = 0;
	public static int Paused = 1;
	public static int HeroTurn = 2;
	public static int HeroMoving = 3;
	public static int TilesScrolling = 4;
	public static int AITurn = 5;
	public static int AIMoving = 6;
}

class Game : MonoBehaviour {

	public static Tiles tiles;
	public static Hero hero;

	public static int state;

	// Use this for initialization
	void Start () {
		tiles = GameObject.Find ("Tiles").GetComponent<Tiles>();
		hero = GameObject.Find ("Hero").GetComponent<Hero>();

		state = GameState.HeroTurn;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
