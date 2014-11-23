using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    public Vector3 direction;
    public float force = 5;
    public float friction = 0.5f;

	public void Add(Vector3 direction, float force) {
		this.direction = (this.direction.normalized * this.force) + (direction.normalized * force);
		this.force += force;
	}

	void Update () {
	    if (force > 0)
	    {
			var vector = direction.normalized * force * Time.deltaTime;
            transform.Translate(vector, Space.World);
			GetComponent<NetworkView>().RPC("UpdateKnockback", RPCMode.Others, vector);
	        force -= friction * Time.deltaTime;
	        if (force < 0) force = 0;
	    }
	}

	[RPC]
	void UpdateKnockback (Vector3 newPosition)
	{
		transform.Translate (newPosition, Space.World);
	}
}
