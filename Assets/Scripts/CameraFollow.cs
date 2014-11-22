using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public float DeltaX = 1;
    public float DeltaY = 1;
    public float DeltaZ = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Target.position.x + DeltaX, Target.position.y+DeltaY, Target.position.z + DeltaZ);
    }
}
