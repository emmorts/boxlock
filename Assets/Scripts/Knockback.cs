using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    public Vector3 direction;
    public float force = 5;
    public float friction = 0.5f;

	// Use this for initialization
	void Start () {
	
	}

	public void Add(Vector3 direction, float force) {
		this.direction = (this.direction.normalized * this.force) + (direction.normalized * force);
		this.force += force;
	}
	
	// Update is called once per frame
	void Update () {
	    if (force > 0)
	    {
	        
            transform.Translate(direction.normalized * force * Time.deltaTime, Space.World);
	        force -= friction*Time.deltaTime;
	        if (force < 0) force = 0;

	    }
	}
}
