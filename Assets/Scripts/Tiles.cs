using UnityEngine;
using System.Collections;

public class Tiles : MonoBehaviour {

	public Transform tilePrefab;

	private int tileW = 7;
	private int tileH = 7;
	private Transform[,] tiles;

	// Use this for initialization
	void Start () {
		tiles = new Transform[tileW, tileH];

		for(int ii = 0; ii < tileW; ii++){
			for(int jj = 0; jj < tileH; jj++){
				Transform tile = (Transform)Instantiate(tilePrefab);
				tile.transform.position = new Vector3(ii, jj, 0f);
				tiles[ii, jj] = tile;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
