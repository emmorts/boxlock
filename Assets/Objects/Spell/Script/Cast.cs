using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour {

    public GameObject fireball;
	
	public float speed = 20;
	public float cooldown = 0.1f;
	public float offset = 2;

	private Animator anim;
    private float cooldown_time = 0;

	void Start() {
		anim = GetComponent<Animator>();
	}

	void Update () {
		if (Input.GetButtonDown("Fire1") && networkView.isMine)
		{
			if (cooldown_time < Time.time){
				anim.SetTrigger("Cast");
                GameObject inst_fireball = Network.Instantiate(fireball, transform.position, transform.rotation, 0) as GameObject;

				inst_fireball.transform.Translate(Vector3.forward * offset);

			    Vector3 knockVec = GetComponentInParent<Transform>().forward.normalized;

			    inst_fireball.GetComponent<FireballBehaviour>().direction = Vector3.forward*offset;
                cooldown_time = Time.time + cooldown;
			}
		}
	}
}
