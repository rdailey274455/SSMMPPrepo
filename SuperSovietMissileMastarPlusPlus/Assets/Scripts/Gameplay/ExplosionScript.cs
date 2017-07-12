using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript:MonoBehaviour
	{
	public float explosionTime=1.2f;

	// Use this for initialization
	void Start()
		{
		Destroy(gameObject,explosionTime);
		}
	
	// Update is called once per frame
	void Update()
		{
		}
	}
