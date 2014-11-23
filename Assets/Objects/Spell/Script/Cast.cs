using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour {
	
	public Rigidbody fireball;
	
	public float speed = 20;
	public float cooldown = 1;
	public float offset = 2;

	private Animator anim;
	private float cooldown_time = 0;

	void Start() {
		anim = GetComponent<Animator>();
	}

	void Update () {
		if (Input.GetButtonDown("Fire1") &&  networkView.isMine)
		{
			if (cooldown_time < Time.time){
				anim.SetTrigger("Cast");
				Rigidbody inst_fireball = Network.Instantiate(fireball, transform.position, transform.rotation , 0) as Rigidbody;

				inst_fireball.transform.Translate(Vector3.forward * offset);

				inst_fireball.AddForce(GetComponentInParent<Transform>().forward.normalized * speed);
				cooldown_time = Time.time + cooldown;
			}
		}
	}
}
