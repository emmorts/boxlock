using System;
using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour
{
    public float speed = 20f;
	public float timeUntilMaxSpeed = 2;

	Animator animator;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private IList colorList; 

    private MainCharacterMovementController movementController;

	void Start()
	{
		animator = GetComponent<Animator>();
		movementController = new MainCharacterMovementController(gameObject);
	}
	
	void Update()
    {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
	    Animating(h, v);
		if (networkView.isMine) {
            movementController.Update(speed, timeUntilMaxSpeed);
		} else {
			SyncedMovement ();
		}
    }
	
	

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void Animating (float h, float v)
	{
		bool running = Math.Abs (h) > 0f || Math.Abs(v) > 0f;
		animator.SetBool ("IsRunning", running);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Quaternion syncRotation = Quaternion.identity;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);

			syncRotation = rigidbody.rotation;
			stream.Serialize(ref syncRotation);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncRotation);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
			rigidbody.rotation = syncRotation;
		}
	}
}
