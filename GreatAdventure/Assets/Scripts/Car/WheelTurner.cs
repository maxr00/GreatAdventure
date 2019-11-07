using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTurner : MonoBehaviour {

	public Transform[] wheels = new Transform[4];

	public Vector3[] offs;

	Vector3 forward = Vector3.forward;

	private void Start()
	{
		offs = new Vector3[wheels.Length];

		for (int i = 0; i < wheels.Length; i++)
		{
			offs[i] = new Vector3(
				Vector3.Dot(wheels[i].position - transform.position, transform.forward),
				Vector3.Dot(wheels[i].position - transform.position, transform.up),
				Vector3.Dot(wheels[i].position - transform.position, transform.right)
				);
		}
	}

	public void Turn(Vector3 f)
	{
		forward = f;
	}
	
	void Update()
	{
		for(int i = 0; i < wheels.Length; i++)
		{
			wheels[i].up = forward;
			wheels[i].Rotate(forward, 90);

			wheels[i].position = transform.position + 
				offs[i].x * transform.forward +
				offs[i].y * transform.up +
				offs[i].z * transform.right;
		}
	}

}
