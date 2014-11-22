using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour {
	
	public Rigidbody fireball;
	
	public float speed = 20;
	public float cooldown = 1;

	public float offset = 2;	
	private float cooldown_time = 0;

	void Update () {
		if (Input.GetButtonDown("Fire1") && networkView.isMine)
		{
			if (cooldown_time < Time.time){
				Rigidbody inst_fireball = Instantiate(fireball, transform.position, transform.rotation) as Rigidbody;

				inst_fireball.transform.Translate(Vector3.forward * offset);

				inst_fireball.AddForce(GetComponentInParent<Transform>().forward.normalized*speed);
				cooldown_time = Time.time + cooldown;
			}
		}
	}
}
