using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    public int Boundary = 0;
    public float MaxSpeed = 20;
    public float MinSpeed = 5;
    public float VelocityChange = 2;
    private float Speed = 5;

    private int theScreenWidth;
    private int theScreenHeight;

    void Start()
    {
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
    }

    void Update()
    {
        var changed = false;
        if (Input.mousePosition.x > theScreenWidth - Boundary)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.World);
            changed = true;
        }

        if (Input.mousePosition.x < 0 + Boundary)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime, Space.World);
            changed = true;
        }

        if (Input.mousePosition.y > theScreenHeight - Boundary)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.World);
            changed = true;
        }

        if (Input.mousePosition.y < 0 + Boundary)
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime, Space.World);
            changed = true;
        }

        if (!changed)
            Speed = MinSpeed;
        else if (Speed < MaxSpeed)
        {
            Speed += Time.deltaTime*VelocityChange;
            if (Speed > MaxSpeed) Speed = MaxSpeed;
        }
    }
}
