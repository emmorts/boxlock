using System;
using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
	// Use this for initialization
    void Start()
    {
        if (rigidbody)
            rigidbody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateToMouse (25);
	}
}
