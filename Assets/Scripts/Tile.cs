using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Sprite[] sprites;

	private enum DragDir {None, Horizontal, Vertical};

	private Sprite sprite;
	private bool dragging = false;
	private DragDir draggingDir = DragDir.None;
	private Vector3 draggingPos;

	// Use this for initialization
	void Start () {
		sprite = sprites [Random.Range (0, sprites.Length)];
		this.GetComponent<SpriteRenderer> ().sprite = sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if(dragging){
			Vector2 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
			float diff = Vector3.Distance(mousePos, draggingPos);

			if(draggingDir == DragDir.None && diff > 0){
				draggingDir = Mathf.Abs(mousePos.x - draggingPos.x) > Mathf.Abs (mousePos.y - draggingPos.y) ? DragDir.Horizontal : DragDir.Vertical;
			}
		}
	}

	void OnMouseDown () {
		dragging = true;
		draggingPos = transform.position;
		Debug.Log ("Dragging");
	}

	void OnMouseUp () {
		dragging = false;
		draggingDir = DragDir.None;
		Debug.Log ("Not Dragging");
	}

	void OnMouseDrag () {
		if (dragging) {
			Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
			Debug.Log (mousePos - draggingPos);
		}
	}
}
