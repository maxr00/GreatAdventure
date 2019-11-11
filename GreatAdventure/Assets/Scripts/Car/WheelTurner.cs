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
				Vector3.Dot(wheels[i].position - wheels[i].parent.position, wheels[i].parent.forward),
				Vector3.Dot(wheels[i].position - wheels[i].parent.position, wheels[i].parent.up),
				Vector3.Dot(wheels[i].position - wheels[i].parent.position, wheels[i].parent.right)
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

			wheels[i].position = wheels[i].parent.position + 
				offs[i].x * wheels[i].parent.forward +
				offs[i].y * wheels[i].parent.up +
				offs[i].z * wheels[i].parent.right;
		}
	}

}
