using System.IO;
using UnityEngine;
using System.Collections;

public class HoomingFireball : MonoBehaviour
{
    public float RotationSpeed = 5;

	// Use this for initialization
	void Start () {
	
	}

    private Transform FindClosestEnemy () {
		// Find all game objects with tag Enemy
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject self = GetComponent<FireballBehaviour>().GetCaster();

        GameObject closest = null;
		var distance = Mathf.Infinity; 
		var position = transform.position; 
		// Iterate through them and find the closest one
		foreach (GameObject go in gos)  { 
			var diff = (go.transform.position - position);
			var curDistance = diff.sqrMagnitude; 
			if (curDistance < distance && go != self) { 
				closest = go; 
				distance = curDistance; 
			} 
		} 

		return closest != null ? closest.transform : null;	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 _direction = Vector3.zero;
		Quaternion _lookRotation = Quaternion.identity;
		var closestTarget = FindClosestEnemy ();

		if (closestTarget != null) {
			_direction = (closestTarget.position - transform.position).normalized;
			_lookRotation = Quaternion.LookRotation(_direction);
		}
		
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        GetComponent<FireballBehaviour>().direction = Vector3.forward;
	}
}
