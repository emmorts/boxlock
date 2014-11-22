using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
    public Transform leader;
    public float mouseSensitivity = 0.01f;
    private Vector3 lastPosition;

    // Use this for initialization
    void Start()
    {

    }

    public float speed = 10.0f;
    public float chaseRange = 10.0f;
    private float range;


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            var delta = Input.mousePosition - lastPosition;
            transform.Translate(delta.x * mouseSensitivity, -1*delta.y * mouseSensitivity, 0);
            lastPosition = Input.mousePosition;
        }

        // Calculate the distance between the follower and the leader.
        range = Vector3.Distance(transform.position, leader.position);

        if (range >= chaseRange)
        {

            Vector3 dir = leader.transform.position - transform.position;
            dir = dir.normalized;
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
            transform.LookAt(leader);

        }

    }
}
