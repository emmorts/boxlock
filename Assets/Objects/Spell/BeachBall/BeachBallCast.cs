using UnityEngine;
using System.Collections;

public class BeachBallCast : MonoBehaviour
{
    private GameObject ball;
    private bool isPickedUp = false;
    public float throwForce = 20;

	// Use this for initialization
	void Start ()
	{
	    ball = GameObject.Find("Fun Ball");
	}
	
	// Update is called once per frame
    void Update()
    {
        if (isPickedUp)
        {
            ball.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
            ball.rigidbody.rotation = rigidbody.rotation;
        }

        if (Input.GetKeyDown("space") && networkView.isMine)
        {
            if (!isPickedUp)
            {
                if (Vector3.Distance(transform.position, ball.transform.position) < 10)
                {
                    isPickedUp = true;
                }
                ball.rigidbody.velocity = Vector3.zero;
            }
            else
            {
                ball.rigidbody.AddRelativeForce((Vector3.up + Vector3.forward + Vector3.forward).normalized * throwForce, ForceMode.VelocityChange);
                isPickedUp = false;
            }
        }
	}
}
