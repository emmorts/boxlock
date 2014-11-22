using UnityEngine;
using System.Collections;

public class MainCharacterMovement : MonoBehaviour
{
    public float Speed = 20f;
    public float RotateSpeed = 80f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * Time.deltaTime * Speed);

        if (Input.GetKey(KeyCode.A)) transform.Rotate(new Vector3(0, -1*RotateSpeed * Time.deltaTime, 0));
        if (Input.GetKey(KeyCode.D)) transform.Rotate(new Vector3(0, RotateSpeed * Time.deltaTime, 0));

        if (Input.GetKey(KeyCode.Q)) transform.Translate(Vector3.left * Time.deltaTime * Speed);
        if (Input.GetKey(KeyCode.E)) transform.Translate(Vector3.right * Time.deltaTime * Speed);
    }
}
