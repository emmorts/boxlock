using System;
using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
    public Collider Surface;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // create a ray from the mousePosition
		var mousePos = Input.mousePosition;
		Debug.Log(mousePos);
		var pointTo = new Vector3 (0f, mousePos.y, 0f);
		var ray = Camera.main.ScreenPointToRay(mousePos);
        // plane.Raycast returns the distance from the ray start to the hit point
	    RaycastHit floorHit;

	    if (Physics.Raycast(ray, out floorHit))
	    {
            transform.LookAt(floorHit.point);
	    }
	}
}
