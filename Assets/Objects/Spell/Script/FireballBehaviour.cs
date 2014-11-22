using UnityEngine;
using System.Collections;

public class FireballBehaviour : MonoBehaviour {
	public int knockback = 0;
	public int destroy_time = 5;
	public int damage_from = 0;
	public int damage_to = 0;

	// Use this for initialization
	void Start () {
		audio.Play();
		Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){
		if(col.collider.tag == "Fireball" || col.collider.tag == "Destruction"){
			Destroy(gameObject, 0.1f);
		}
		else{
			col.rigidbody.AddForce(this.rigidbody.velocity.normalized*knockback);
		}
		if(col.collider.tag == "Fragile"){
			col.gameObject.GetComponent<HealthMeter>().DoDamage(Random.Range(damage_from, damage_to));
			Destroy(gameObject);
		}
	}

}
