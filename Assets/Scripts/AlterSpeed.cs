using UnityEngine;
using System.Collections;

public class AlterSpeed : MonoBehaviour {

	public float speedCoefficient = 1.0f;

	private float originalSpeed = 1.0f;

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Player") { 
			originalSpeed = col.gameObject.GetComponent<MainCharacterController> ().speed;
		}
	}
	
	void OnCollisionStay (Collision col)
	{
		if (col.collider.tag == "Player") {
			col.gameObject.GetComponent<MainCharacterController>().speed = originalSpeed * speedCoefficient;
		}
	}

	void OnCollisionExit (Collision col)
	{
		if (col.collider.tag == "Player") {
			col.gameObject.GetComponent<MainCharacterController> ().speed = originalSpeed;
		}
	}
}
