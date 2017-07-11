using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileInputControl:MonoBehaviour
	{
	private GameObject reticleObject;
	private Vector2 targetVector;
	// private float targetVector_angle;
	// private Vector2 targetVector_angleVector;
	private Vector2 targetVector_almost=new Vector2(0f,0f);
	private float targetVector_almost_angle;
	public float targetingSwiftness=0.7f;
	public float thrustForce=15.3f;

	// Use this for initialization
	void Start()
		{
		reticleObject=GameObject.Find("MouseControlReticle");
		if (reticleObject==null)
			{
			// oopsie, can't find reticle object
			}
		}


	// Update is called once per frame
	void Update()
		{
		// find out where missile should be aiming
		targetVector.x=reticleObject.transform.position.x-transform.position.x;
		targetVector.y=reticleObject.transform.position.y-transform.position.y;
		// tilt the missile in the direction of the target
		targetVector_almost.x+=targetingSwiftness*(targetVector.x-targetVector_almost.x);
		targetVector_almost.y+=targetingSwiftness*(targetVector.y-targetVector_almost.y);

		// get angle and make vector from it for target
		// targetVector_angle=Mathf.Atan2(targetVector.y,targetVector.x);
		// targetVector_angleVector.x=1.0f*Mathf.Cos(targetVector_angle);
		// targetVector_angleVector.y=1.0f*Mathf.Sin(targetVector_angle);

		// get angle for almost
		targetVector_almost_angle=Mathf.Atan2(targetVector_almost.y,targetVector_almost.x)*Mathf.Rad2Deg;
		// actually rotate sprite
		transform.rotation=Quaternion.Euler(0f,0f,targetVector_almost_angle);
		}

	void FixedUpdate()
		{
		if (Input.GetMouseButton(0))
			{
			Rigidbody2D missileRB2D=GetComponent<Rigidbody2D>();

			missileRB2D.AddForce(thrustForce*targetVector.normalized,(ForceMode2D)1);
			}
		}

	}
