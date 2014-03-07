using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private Sprite sprite;
	public Transform left;
	public Transform right;
	public Transform up;
	public Transform down;
	public Transform floor;

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
		left = Random.value >= 0.5 ? Add (left, 1) : null;
		right = Random.value >= 0.5 ? Add (right, 1, 180) : null;
		up = Random.value >= 0.5 ? Add (up, 1, 90) : null;
		down = Random.value >= 0.5 ? Add (down, 1, 270) : null;
	}
}
