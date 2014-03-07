using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Sprite[] sprites;
	
	private Sprite sprite;

	// Use this for initialization
	void Start () {
		sprite = sprites [Random.Range (0, sprites.Length)];
		this.GetComponent<SpriteRenderer> ().sprite = sprite;
	}
}
