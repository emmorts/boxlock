using UnityEngine;
using System.Collections;

public class FireballBehaviour : MonoBehaviour
{
    public Vector3 direction;
    private GameObject caster;

	public int knockback = 0;
	public int destroy_time = 5;
	public int damage_from = 0;
	public int damage_to = 0;

	public GameObject explosion;

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
	    if (!networkView.isMine)
	    {
			SyncedMovement();
		}
	    else
	    {
	        transform.Translate(direction);
	    }
	}

	void SyncedMovement() {
		syncTime += Time.deltaTime;
		transform.Translate(Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay));
	}

    public void SetCaster(GameObject caster)
    {
        this.caster = caster;
    }
    public GameObject GetCaster()
    {
        return this.caster;
    }

	void OnCollisionEnter (Collision col)
	{
	    if (col.gameObject == caster) return;
		if (col.collider.tag == "Fireball" || col.collider.tag == "Destruction"){
			ContactPoint contactPoint = col.contacts[0];
			Instantiate(explosion, contactPoint.point, col.transform.rotation);
			if (col.collider.rigidbody) {
            	col.collider.rigidbody.AddForce((col.gameObject.transform.position - contactPoint.point).normalized * 100, ForceMode.VelocityChange);
			}
			networkView.RPC("DestroyObject", RPCMode.AllBuffered);
		}
		if (col.collider.tag == "Player") {
			col.gameObject.GetComponent<Knockback>().Add(col.gameObject.transform.position - transform.position, knockback);
		}
		if (col.collider.tag == "Fragile" || col.collider.tag == "Player"){
			ContactPoint contactPoint = col.contacts[0];
			col.gameObject.GetComponent<Player>().DoDamage(Random.Range(damage_from, damage_to));
			Instantiate(explosion, contactPoint.point , col.transform.rotation);
			networkView.RPC("DestroyObject", RPCMode.AllBuffered);
		}
	}

	[RPC]
	void DestroyObject ()
	{
		Destroy (gameObject);
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Quaternion syncRotation = Quaternion.identity;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncRotation = rigidbody.rotation;
			stream.Serialize(ref syncRotation);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncRotation);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
			rigidbody.rotation = syncRotation;
		}
	}

}
