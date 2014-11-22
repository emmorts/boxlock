using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour {
	
	public Rigidbody fireball;
	
	public float speed = 20;
	public float cooldown = 1;

    public float offset = 5;

	float cooldown_time = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown("Fire1") && networkView.isMine)
		{
			if(cooldown_time < Time.time)
			{
				Rigidbody inst_fireball = Instantiate(fireball, transform.position.Duplicate(), transform.rotation) as Rigidbody;
				
                inst_fireball.transform.Translate(Vector3.forward * offset);

				inst_fireball.AddForce(GetComponentInParent<Transform>().forward.normalized*speed);
				cooldown_time = Time.time + cooldown;
			}
		}
	}
}
