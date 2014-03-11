using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private Sprite sprite;
	

	public Transform floorPrefab;
	public Transform wallPrefab;
	public Transform doorPrefab;
	public Transform chestPrefab;
	public Transform monsterPrefab;

	public Transform floor;
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
		floor = Add (floorPrefab);


		switch(Random.Range (0, 4)){
			case 0:
				up = Add (doorPrefab, 1, 90);
			break;

			case 1:
				down = Add (doorPrefab, 1, 270);
			break;

			case 2:
				left = Add (doorPrefab, 1, 0);
			break;

			case 3:
				right = Add (doorPrefab, 1, 180);
			break;
		};

		if(up == null) up = Random.value < 0.5 ? Add (wallPrefab, 1, 90) : Add(doorPrefab, 1, 90);
		if(down == null) down = Random.value < 0.5 ? Add (wallPrefab, 1, 270) : Add(doorPrefab, 1, 270);
		if(left == null) left = Random.value < 0.5 ? Add (wallPrefab, 1, 0) : Add(doorPrefab, 1, 0);
		if(right == null) right = Random.value < 0.5 ? Add (wallPrefab, 1, 180) : Add(doorPrefab, 1, 180);

		if(Random.value < 0.3f){
			content = Add (chestPrefab, 2);
		}

		if(Random.value < 0.3f && content == null){
			content = Add (monsterPrefab, 2);
		}
	}
}
