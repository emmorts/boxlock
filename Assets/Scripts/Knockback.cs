using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    public Vector3 direction;
    public float force = 5;
    public float friction = 8f;

	public void Add(Vector3 direction, float force) {
		this.direction = (this.direction.normalized * this.force) + (direction.normalized * force);
		this.force += force;
	}

	void Update () {
	    if (force > 0)
	    {
			var vector = direction.normalized * force * Time.deltaTime;
            transform.Translate(vector, Space.World);
			networkView.RPC("UpdateKnockback", RPCMode.Others, transform.position, transform.rotation);
	        force -= friction * Time.deltaTime;
	        if (force < 0) force = 0;
	    }
	}

	[RPC]
	void UpdateKnockback (Vector3 newPosition, Quaternion newRotation)
	{
		transform.position = newPosition;
		transform.rotation = newRotation;
	}
}
