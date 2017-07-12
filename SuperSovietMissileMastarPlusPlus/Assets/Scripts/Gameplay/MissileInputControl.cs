using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileInputControl:MonoBehaviour
	{
	public GameObject reticleObject;
	public GameObject gameplayManagerObject;

	private ParticleSystem missilePartSysComp;
	private ParticleSystem.EmissionModule missileEmissionModule;
	private ParticleSystem.MainModule missileMainModule;

	// movement variables
	private Vector2 targetVector;
	// private float targetVector_angle;
	// private Vector2 targetVector_angleVector;
	private Vector2 targetVector_almost=new Vector2(0f,0f);
	private float targetVector_almost_angle;
	public float targetingSwiftness=0.7f;
	public float thrustForce=15.3f;
	

	// effect variables
	[SerializeField] private float thrust_volume_thrusting=0.25f;
	[SerializeField] private float thrust_volume_empty=0.01f;
	[SerializeField] private float thrust_volume_normal=0.12f;
	[SerializeField] private Vector2 thrust_particle_emission_thrusting=new Vector2(10,40);
	[SerializeField] private Vector2 thrust_particle_emission_empty=new Vector2(1,5);
	[SerializeField] private Vector2 thrust_particle_emission_normal=new Vector2(5,14);
	[SerializeField] private Vector2 thrust_particle_size_thrusting=new Vector2(2,4);
	[SerializeField] private Vector2 thrust_particle_size_empty=new Vector2(0.1f,1);
	[SerializeField] private Vector2 thrust_particle_size_normal=new Vector2(1,2);

	void Start()
		{
		missilePartSysComp=GetComponentInChildren<ParticleSystem>();
		missileEmissionModule=missilePartSysComp.emission;
		missileMainModule=missilePartSysComp.main;



		// if (reticleObject==null) reticleObject=GameObject.Find("MouseControlReticle");
		if (reticleObject==null)
			{
			// oopsie, can't find reticle object
			}
		}

	
	void Update()
		{

		if (gameplayManagerObject.GetComponent<GameplayManagement>().getMissileThrustFuelPercentage()>0.0f) // fuel is not depleted
			{
			if (Input.GetButton("Fire1"))
				{
				GetComponent<AudioSource>().volume=thrust_volume_thrusting;
				missileEmissionModule.rateOverTime=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_emission_thrusting));
				missileMainModule.startSize=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_size_thrusting));
				}
			else
				{
				GetComponent<AudioSource>().volume=thrust_volume_normal;
				missileEmissionModule.rateOverTime=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_emission_normal));
				missileMainModule.startSize=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_size_normal));
				}
			}
		else
			{
			GetComponent<AudioSource>().volume=thrust_volume_empty;
			missileEmissionModule.rateOverTime=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_emission_empty));
			missileMainModule.startSize=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_size_empty));
			}


		// find out where missile should be aiming
		targetVector.x=reticleObject.transform.position.x-transform.position.x;
		targetVector.y=reticleObject.transform.position.y-transform.position.y;
		// tilt the missile in the direction of the target
		targetVector_almost.x+=targetingSwiftness*(targetVector.x-targetVector_almost.x);
		targetVector_almost.y+=targetingSwiftness*(targetVector.y-targetVector_almost.y);

		// get angle and make vector from it for target - don't need to thanks to Vector2.normalized property
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
		if (Input.GetButton("Fire1") && gameplayManagerObject.GetComponent<GameplayManagement>().getMissileThrustFuelPercentage()>0.0f)
			{
			Rigidbody2D missileRB2D=GetComponent<Rigidbody2D>();

			missileRB2D.AddForce(thrustForce*targetVector.normalized,ForceMode2D.Impulse);
			}
		}



	public float randomRangeVectorBounds(Vector2 bounds) // code shortener
		{
		return Random.Range(bounds.x,bounds.y);
		}
	}
