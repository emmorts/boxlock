using UnityEngine;
using System.Collections;

public class RelativeInitialCoordinates : MonoBehaviour
{

    public Transform Target;

    public float DeltaX = 0;
    public float DeltaY = 0;
    public float DeltaZ = 0;

	// Use this for initialization
    void Start()
    {
        transform.position = new Vector3(Target.position.x + DeltaX, Target.position.y + DeltaY, Target.position.z + DeltaZ);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
