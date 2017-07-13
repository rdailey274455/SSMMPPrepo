using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript:MonoBehaviour
	{
	public GameObject reticleObject;
	public GameObject gameplayManagerObject;
	public GameObject screenZoneObject;
	public GameObject offscreenArrowObject;
	private SpriteRenderer offscreenArrowSprRen;
	public AudioSource sirenAudioSource;
	public AudioSource thrustAudioSource;

	private ParticleSystem missilePartSysComp;
	private ParticleSystem.EmissionModule missileEmissionModule;
	private ParticleSystem.MainModule missileMainModule;

	// movement variables
	private Vector2 targetVector;
	// private float targetVector_angle;
	// private Vector2 targetVector_angleVector;
	private Vector2 targetVector_almost=new Vector2(0f,0f);
	private float targetVector_almost_angle;
	public float targetingSwiftness=0.019f;
	public float thrustForce=2.2f;
	

	// effect variables
	[SerializeField] private float thrust_volume_thrusting=0.75f;
	[SerializeField] private float thrust_volume_empty=0.10f;
	[SerializeField] private float thrust_volume_normal=0.40f;
	[SerializeField] private Vector2 thrust_particle_emission_thrusting=new Vector2(10,40);
	[SerializeField] private Vector2 thrust_particle_emission_normal=new Vector2(7,14);
	[SerializeField] private Vector2 thrust_particle_emission_empty=new Vector2(1,5);
	[SerializeField] private Vector2 thrust_particle_size_thrusting=new Vector2(2,4);
	[SerializeField] private Vector2 thrust_particle_size_normal=new Vector2(1,2.4f);
	[SerializeField] private Vector2 thrust_particle_size_empty=new Vector2(0.1f,1);

	void Start()
		{
		missilePartSysComp=GetComponentInChildren<ParticleSystem>();
		missileEmissionModule=missilePartSysComp.emission;
		missileMainModule=missilePartSysComp.main;

		offscreenArrowSprRen=offscreenArrowObject.GetComponent<SpriteRenderer>();



		// end of start method


		/*
		foreach (SpriteRenderer sprRen in GetComponentsInChildren<SpriteRenderer>())
			{
			if (sprRen.gameObject.name.Equals("OffscreenArrow"))
				{
				arrowSprRen=sprRen;
				break;
				}
			}


		// if (reticleObject==null) reticleObject=GameObject.Find("MouseControlReticle");
		if (reticleObject==null)
			{
			// oopsie, can't find reticle object
			}
		*/


		}

	
	void Update()
		{
		// thrusting effects
		if (gameplayManagerObject.GetComponent<GameplayManagement>().getMissileThrustFuelPercentage()>0.0f) // fuel is not depleted
			{
			if (Input.GetButton("Fire1"))
				{
				thrustAudioSource.volume=thrust_volume_thrusting;
				missileEmissionModule.rateOverTime=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_emission_thrusting));
				missileMainModule.startSize=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_size_thrusting));
				}
			else
				{
				thrustAudioSource.volume=thrust_volume_normal;
				missileEmissionModule.rateOverTime=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_emission_normal));
				missileMainModule.startSize=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_size_normal));
				}
			}
		else
			{
			thrustAudioSource.volume=thrust_volume_empty;
			missileEmissionModule.rateOverTime=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_emission_empty));
			missileMainModule.startSize=new ParticleSystem.MinMaxCurve(randomRangeVectorBounds(thrust_particle_size_empty));
			}


		if (!screenZoneObject.GetComponent<Collider2D>().OverlapPoint(transform.position))
			{
			offscreenArrowSprRen.enabled=true;
			Color arrowCol=offscreenArrowSprRen.color;
			arrowCol.a=(Mathf.RoundToInt(11*Time.time))%2;
			offscreenArrowSprRen.color=arrowCol;
			Vector3 arrowMidwayPosition=new Vector3();
			arrowMidwayPosition.z=0f;
			arrowMidwayPosition.x=-(Camera.main.transform.position.x-transform.position.x)*0.40f; // dunno why these are negative but okay
			arrowMidwayPosition.y=-(Camera.main.transform.position.y-transform.position.y)*0.40f;
			float arrowAngle=Mathf.Rad2Deg*Mathf.Atan2(arrowMidwayPosition.y,arrowMidwayPosition.x);
			offscreenArrowObject.transform.position=arrowMidwayPosition;
			offscreenArrowObject.transform.rotation=Quaternion.Euler(0f,0f,arrowAngle);

			if (!sirenAudioSource.isPlaying) sirenAudioSource.Play();
			}
		else
			{
			offscreenArrowSprRen.enabled=false;
			sirenAudioSource.Stop();
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
