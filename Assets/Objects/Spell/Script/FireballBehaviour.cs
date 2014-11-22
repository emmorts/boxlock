using UnityEngine;
using System.Collections;

public class FireballBehaviour : MonoBehaviour {
	public int knockback = 0;
	public int destroy_time = 5;
	public int damage_from = 0;
	public int damage_to = 0;

	public AudioClip sound_splosion;

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	// Use this for initialization
	void Start () {
		audio.Play();
		Destroy(gameObject, destroy_time);
	}
	
	// Update is called once per frame
	void Update () {
		if (!networkView.isMine) {
			SyncedMovement();
		}
	}

	void SyncedMovement() {
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void OnCollisionEnter (Collision col){
		AudioSource.PlayClipAtPoint(sound_splosion, this.transform.position);
		if(col.collider.tag == "Fireball" || col.collider.tag == "Destruction"){
			Destroy(gameObject, 0.1f);
		}
		else{
			col.rigidbody.AddForce(this.rigidbody.velocity.normalized * knockback);
		}
		if(col.collider.tag == "Fragile"){
			col.gameObject.GetComponent<HealthMeter>().DoDamage(Random.Range(damage_from, damage_to));
			Destroy(gameObject);
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
		}
	}

}
