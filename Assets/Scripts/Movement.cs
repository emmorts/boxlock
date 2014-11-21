using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float ZSpeed = 0;
    public float XSpeed = 0;

    public float Multiply = 0.05f;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
	    var vertical = Input.GetAxis("Vertical") * Multiply;
	    var horizontal = Input.GetAxis("Horizontal") * Multiply;

	    ZSpeed += vertical;
	    XSpeed += horizontal;

        rigidbody.velocity = new Vector3(XSpeed, 0, ZSpeed);
	}
}
