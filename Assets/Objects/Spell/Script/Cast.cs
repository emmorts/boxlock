using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour {
	
	public Rigidbody fireball;
	
	public float speed = 20;
	public float cooldown = 1;
	
	
	float cooldown_time = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown("Fire1"))
		{
			if(cooldown_time < Time.time){
				Rigidbody inst_fireball = Instantiate(fireball,
				                                               transform.position,
				                                               transform.rotation)
					as Rigidbody;
				
				inst_fireball.AddForce(GetComponentInParent<Transform>().forward.normalized*speed);
				cooldown_time = Time.time + cooldown;
			}
		}
	}
}
