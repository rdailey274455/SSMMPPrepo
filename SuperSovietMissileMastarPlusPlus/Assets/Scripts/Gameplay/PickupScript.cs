using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript:MonoBehaviour
	{
	public bool isHealthPickup;
	public bool isFuelPickup;
	
	public float alphaDecreasePerSec=1f/1.0f;
	public float scaleIncreasePerSec=0.4f;

	public float speed=-12f;
	private bool unhit=true;

	public float healthRestorePercent=0.05f;
	public float fuelRestorePercent=0.15f;

	public GameObject missileGameObject;
	public GameObject gameplayManagerObject;
	private GameplayManagement gameplayManagerScript;
	public GameObject liveZoneObject;
	private Collider2D liveZoneCollider;
	private Collider2D myCollider;
	private SpriteRenderer mySprRen;
	private Rigidbody2D myRB;
	private AudioSource myAduioSource;


	// Use this for initialization
	void Start()
		{
		if (liveZoneObject==null) liveZoneObject=GameObject.FindGameObjectWithTag("LiveZone");
		if (missileGameObject==null) missileGameObject=GameObject.FindGameObjectWithTag("Missile");
		if (gameplayManagerObject==null) gameplayManagerObject=GameObject.FindGameObjectWithTag("GameplayManager");

		gameplayManagerScript=gameplayManagerObject.GetComponent<GameplayManagement>();

		myCollider=GetComponent<Collider2D>();
		mySprRen=GetComponent<SpriteRenderer>();
		myRB=GetComponent<Rigidbody2D>();
		myAduioSource=GetComponent<AudioSource>();

		liveZoneCollider=liveZoneObject.GetComponent<Collider2D>();

		myRB.velocity=new Vector2(speed*gameplayManagerScript.getSpeedMult(),0f);
		}
	
	// Update is called once per frame
	void Update()
		{
		if (gameplayManagerScript.getMissionStatus() && unhit && myCollider.IsTouching(missileGameObject.GetComponent<Collider2D>())) // hit missile
			{
			unhit=false;
			// myCollider.enabled=false;
			mySprRen.color=Color.clear;
			myAduioSource.Play();
			if (isHealthPickup || mySprRen.sprite.name.Equals("healthpickup1"))
				{
				gameplayManagerScript.missile_HP_change(Mathf.RoundToInt(gameplayManagerScript.getMissileMaxHP()*healthRestorePercent));
				}
			if (isFuelPickup || mySprRen.sprite.name.Equals("fuelpickup1"))
				{
				gameplayManagerScript.missile_fuel_change(gameplayManagerScript.getMissileMaxFuel()*fuelRestorePercent);
				}
			}
		if (!unhit)
			{
			Color currentColor=mySprRen.color;
			currentColor.a-=alphaDecreasePerSec*Time.deltaTime;
			mySprRen.color=currentColor;
			Vector3 currentScale=transform.localScale;
			currentScale+=scaleIncreasePerSec*Vector3.one*Time.deltaTime;
			transform.localScale=currentScale;
			if (!myAduioSource.isPlaying) Destroy(gameObject);
			}
		if (!liveZoneCollider.OverlapPoint(transform.position)) // time to stop existing
			{
			Destroy(gameObject);
			}
		}
	}
