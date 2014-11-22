using UnityEngine;
using System.Collections;

public class MainCharacterMovement : MonoBehaviour
{
    public float speed = 20f;

	Animator anim;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	void Start()
	{
		anim = GetComponent<Animator> ();
	}

    void Update()
    {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
	    Animating(h, v);
		if (networkView.isMine) {
			InputMovement ();
		} else {
			SyncedMovement ();
		}
    }

	void InputMovement() {
		if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * 20, Space.World);
		if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * Time.deltaTime * 20, Space.World);
		if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * Time.deltaTime * 20, Space.World);
		if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * Time.deltaTime * 20, Space.World);
	}
	
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void Animating (float h, float v) {
		bool running = h != 0f || v != 0;
		anim.SetBool ("IsRunning", running);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
		}
	}
}
