using System;
using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
	// Use this for initialization
    void Start()
    {
        if (rigidbody && networkView.isMine) 
            rigidbody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (networkView.isMine) {
				transform.RotateToMouse (25);
		}
	}
}
