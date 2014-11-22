using UnityEngine;
using System.Collections;

public class MainCharacterMovement : MonoBehaviour
{
    public float speed = 20f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidBody;
	int floorMask;
	float camRayLength = 100f;

	void Start()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidBody = GetComponent<Rigidbody> ();
	}

    void Update()
    {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		if (networkView.isMine) {
            Animating(h, v);
            if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * 20, Space.World);
            if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * Time.deltaTime * 20, Space.World);

            if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * Time.deltaTime * 20, Space.World);
            if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * Time.deltaTime * 20, Space.World);
        }
    }

	void Animating (float h, float v) {
		bool running = h != 0f || v != 0;
		anim.SetBool ("IsRunning", running);

	}
}
