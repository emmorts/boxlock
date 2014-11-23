using UnityEngine;
using System.Collections;

public class HealthMeter : MonoBehaviour {
	public float health = 100;
	public float armor_value= 1;
	public float h_regen = 1;

	float time_to_wait_for_regen = 5;
	float time_until_regen = 0;

	float damage_from_armor(float damage){
		float armor = 1-(armor_value/100);
		return damage*armor;
	}

	public void DoDamage(float damage){
		health -= damage_from_armor(damage);
	}

	void DoRegen(){
		if(health < 100 && (time_until_regen < Time.time)){
			time_until_regen = time_to_wait_for_regen + Time.time;
			health+=h_regen;
		}
	}

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0){
			Destroy(gameObject);
		}
		DoRegen();
	}

}
