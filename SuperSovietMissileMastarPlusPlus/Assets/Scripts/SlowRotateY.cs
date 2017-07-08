using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotateY : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0f,1f,0f),1f);
		// transform.RotateAround(transform.position,Vector3.up,1f);
	}
}
