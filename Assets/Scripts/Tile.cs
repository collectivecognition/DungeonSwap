using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private Sprite sprite;
	

	public Transform floor;
	public Transform wall;
	public Transform door;
	public Transform chest;

	public Transform up;
	public Transform down;
	public Transform left;
	public Transform right;
	public Transform content;

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

		switch(Random.Range (0, 4)){
			case 0:
				up = Add (door, 1, 90);
			break;

			case 1:
				down = Add (door, 1, 270);
			break;

			case 2:
				left = Add (door, 1, 0);
			break;

			case 3:
				right = Add (door, 1, 180);
			break;
		};

		switch(Random.Range (0, 4)){
			case 0:
				if(up == null)
					up = Add (door, 1, 90);
			break;

			case 1:
				if(down == null)
					down = Add (door, 1, 270);
			break;

			case 2:
				if(left == null)
					left = Add (door, 1, 0);
			break;

			case 3:
				if(right == null)
					right = Add (door, 1, 180);
			break;
		};

		if(up == null) up = Add (wall, 1, 90);
		if(down == null) down = Add (wall, 1, 270);
		if(left == null) left = Add (wall, 1, 0);
		if(right == null) right = Add (wall, 1, 180);

		if(Random.value < 0.1f){
			content = Add (chest, 2);
		}
	}
}
