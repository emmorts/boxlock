using UnityEngine;
using System.Collections;

public class LavaDamage : MonoBehaviour {
	public float damage = 5;
	// Use this for initialization

	public float damage_time = 1;
	float time_until_damage = 0;

	void DoDamageOverTime(Collision col){
		if(time_until_damage < Time.time){
			col.gameObject.GetComponent<Player>().DoDamage(damage);
			time_until_damage = Time.time + damage_time;
		}
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionStay (Collision col){
		if (col.collider.tag == "Fragile" || col.collider.tag == "Player"){
			DoDamageOverTime(col);
		}
	}
}
