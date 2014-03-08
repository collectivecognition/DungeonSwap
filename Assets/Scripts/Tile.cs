using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private Sprite sprite;

	//[System.NonSerialized]
	public Transform[] walls;

	public Transform floor;
	public Transform wall;
	public Transform door;

	Transform Add (Transform what, int layer = 0, float rotation = 0f){
		Transform thing = (Transform)Instantiate (what);
		thing.parent = transform;
		thing.transform.localPosition = new Vector3(0, 0, -layer);
		thing.transform.Rotate(new Vector3(0, 0, 1), rotation);
		return thing;
	}

	// Use this for initialization

	void Start () {
		floor = Add (floor);

		walls = new Transform[4];

		walls[Directions.Up] = Random.value >= 0.5 ? Add (wall, 1, 270) : Add (door, 1, 270);
		walls[Directions.Right] = Random.value >= 0.5 ? Add (wall, 1, 180) : Add (door, 1, 180);
		walls[Directions.Down] = Random.value >= 0.5 ? Add (wall, 1, 90) : Add (door, 1, 90);
		walls[Directions.Left] = Random.value >= 0.5 ? Add (wall, 1) : Add (door, 1);
	}
}
