using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float health = 100;
	public float armorValue = 1;
	public float healthRegen = 1;
	public int timeToRespawn = 5;

	private Animator animator;
	private float timeToWaitUntilRegen = 5;
	private float timeUntilRegen = 0;

	float DamageFromArmor (float damage){
		float armor = 1 - (armorValue / 100);
		return damage * armor;
	}

	public void DoDamage (float damage){
		health -= DamageFromArmor(damage);
	}

	void DoRegen (){
		if(health < 100 && (timeUntilRegen < Time.time)){
			timeUntilRegen = timeToWaitUntilRegen + Time.time;
			health += healthRegen;
		}
	}

	void Start () {
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0){
			gameObject.GetComponent<MainCharacterController>().enabled = false;
			gameObject.GetComponent<LookAtMouse>().enabled = false;
			gameObject.GetComponent<Cast>().enabled = false;
			animator.SetBool("IsFallen", true);
			StartCoroutine(Respawn(timeToRespawn));
		}
		DoRegen();
	}

	IEnumerator Respawn(int timeToRespawn = 0)
	{
		yield return new WaitForSeconds (timeToRespawn);
		animator.SetBool ("IsFallen", false);
		gameObject.GetComponent<MainCharacterController>().enabled = true;
		gameObject.GetComponent<LookAtMouse>().enabled = true;
		gameObject.GetComponent<Cast>().enabled = true;
		health = 100;
	}

}
