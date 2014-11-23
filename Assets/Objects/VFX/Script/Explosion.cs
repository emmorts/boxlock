using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		audio.Play();
		Destroy(gameObject, 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
