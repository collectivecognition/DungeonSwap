using UnityEngine;
using System.Collections;

public class Container : MonoBehaviour {

	public bool open = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Open(){
		open = true;
		GetComponent<Animator>().SetBool ("open", true);
	}
}
