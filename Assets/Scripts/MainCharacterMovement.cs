using UnityEngine;
using System.Collections;

public class MainCharacterMovement : MonoBehaviour
{
    public float Speed = 20f;
    public float RotateSpeed = 80f;

    public Camera Cam;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);
        if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * Time.deltaTime * Speed, Space.World);

        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * Time.deltaTime * Speed, Space.World);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * Time.deltaTime * Speed, Space.World);
    }
}
