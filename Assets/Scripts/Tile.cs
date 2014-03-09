using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private Sprite sprite;
	

	public Transform floor;
	public Transform wall;
	public Transform door;

	public Transform up;
	public Transform down;
	public Transform left;
	public Transform right;

	public Vector2 pos;

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

		up = Random.value >= 0.5 ? Add (wall, 1, 90) : Add (door, 1, 90);
		right = Random.value >= 0.5 ? Add (wall, 1, 180) : Add (door, 1, 180);
		down = Random.value >= 0.5 ? Add (wall, 1, 270) : Add (door, 1, 270);
		left = Random.value >= 0.5 ? Add (wall, 1) : Add (door, 1);
	}
}
