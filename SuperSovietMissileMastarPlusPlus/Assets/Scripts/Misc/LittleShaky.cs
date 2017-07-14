using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleShaky:MonoBehaviour
	{
	public float shakeRadius=10f;
	private Vector3 initialPosition;

	// Use this for initialization
	void Start()
		{
		initialPosition=transform.position;
		}
	
	// Update is called once per frame
	void Update()
		{
		Vector3 shookPosition=initialPosition;
		shookPosition.x+=((2f*Random.value)-1f)*shakeRadius;
		shookPosition.y+=((2f*Random.value)-1f)*shakeRadius;
		shookPosition.z+=((2f*Random.value)-1f)*shakeRadius;
		transform.position=shookPosition;
		}
	}
