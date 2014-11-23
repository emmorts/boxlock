using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour {

    public GameObject fireball;
    public GameObject fireball2;
	
	public float speed = 20;
	public float cooldown = 0.1f;
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

                GameObject inst_fireball = Network.Instantiate(fireball, transform.position, transform.rotation, 0) as GameObject;

				inst_fireball.transform.Translate(Vector3.forward * offset);

                inst_fireball.GetComponent<FireballBehaviour>().direction = Vector3.forward * speed;
                inst_fireball.GetComponent<FireballBehaviour>().SetCaster(gameObject);
                cooldown_time = Time.time + cooldown;
			}
		}
        if (Input.GetButtonDown("Fire2") && networkView.isMine)
        {
            if (cooldown_time < Time.time)
            {
                anim.SetTrigger("Cast");

                GameObject inst_fireball = Network.Instantiate(fireball2, transform.position, transform.rotation, 0) as GameObject;

                inst_fireball.transform.Translate(Vector3.forward * offset);

                inst_fireball.GetComponent<FireballBehaviour>().direction = Vector3.forward * speed;
                inst_fireball.GetComponent<FireballBehaviour>().SetCaster(gameObject);
                cooldown_time = Time.time + cooldown;
            }
        }
	}
}
