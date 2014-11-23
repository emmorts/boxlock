using UnityEngine;
using System.Collections;

public class BananaSlip : MonoBehaviour
{
	private GameObject banana;
	private Vector3 target;

	void Start ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray))
		{
			//Instantiate(bana)
		}
	}

	void Update ()
	{
		
	}
}
