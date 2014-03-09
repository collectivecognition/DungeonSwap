using UnityEngine;
using System.Collections;

class Game : MonoBehaviour {

	public static Tiles tiles;
	public static Hero hero;

	// Use this for initialization
	void Start () {
		tiles = GameObject.Find ("Tiles").GetComponent<Tiles>();
		hero = GameObject.Find ("Hero").GetComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
