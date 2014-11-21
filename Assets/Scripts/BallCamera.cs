using UnityEngine;
using System.Collections;

public class BallCamera : MonoBehaviour
{
    public Transform Ball;

    public float DeltaX = 3;
    public float DeltaY = 5;
    public float DeltaZ = 3;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Ball.transform.position.x + DeltaX, Ball.transform.position.y + DeltaY, Ball.transform.position.z + DeltaZ);
	}
}
