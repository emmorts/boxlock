using UnityEngine;
using System.Collections;

public class BananaSlip : MonoBehaviour
{
    public float ReachGroundTime = 1;
    public GameObject banana;
    private Vector3 target;
    private float cooldown_time = 0f;
    public float offset = 10;
    public float cooldown = 0.1f;

    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown("b") && networkView.isMine)
        {
            Plane playerPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                if (Physics.Raycast(ray))
                {
                    if (cooldown_time < Time.time)
                    {
                        GetComponent<Animator>().SetTrigger("Cast");

                        GameObject inst_fireball =
                            Network.Instantiate(banana, transform.position, transform.rotation, 0) as GameObject;

                        inst_fireball.transform.Translate(Vector3.forward*offset);
                        inst_fireball.transform.RotateToMouse(10000);

                        inst_fireball.rigidbody.AddRelativeForce(((Vector3.up * (ReachGroundTime / 2f * 9.8f)) + (Vector3.forward * (Vector3.Distance(inst_fireball.transform.position, ray.GetPoint(hitdist)) / ReachGroundTime))), ForceMode.VelocityChange);
                        cooldown_time = Time.time + cooldown;
                    }
                }
            }

        }
    }
}
