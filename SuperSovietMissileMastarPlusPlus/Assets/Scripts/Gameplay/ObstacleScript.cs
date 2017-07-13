using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript:MonoBehaviour
	{
	public float speed=-10f;
	public int damage=10;
	private SpriteRenderer mySprRen;
	private Rigidbody2D myRB;
	private Collider2D myCollider;
	public GameObject missileGameObject;
	public GameObject gameplayManagerObject;
	private GameplayManagement gameplayManagementScript;
	public GameObject liveZoneObject;
	private Collider2D liveZoneCollider;
	private bool unhit=true;
	private Color hitColor=new Color(0.85f,0.85f,0.85f,0.49f);
	private float lowestDistance;

	// Use this for initialization
	void Start()
		{
		mySprRen=GetComponent<SpriteRenderer>();
		myRB=GetComponent<Rigidbody2D>();
		myCollider=GetComponent<BoxCollider2D>();
		liveZoneCollider=liveZoneObject.GetComponent<Collider2D>();
		gameplayManagementScript=gameplayManagerObject.GetComponent<GameplayManagement>();

		myRB.velocity=new Vector2(speed,0f);
		}
	
	// Update is called once per frame
	void Update()
		{
		float currentDistance=distance(missileGameObject.transform.position,transform.position);
		if (currentDistance<lowestDistance) lowestDistance=currentDistance;
		}

	private void FixedUpdate()
		{
		if (!liveZoneCollider.OverlapPoint(transform.position)) // time to stop existing
			{
			// award points
			if (unhit)
				{
				gameplayManagementScript.showScoreChange(
					gameplayManagementScript.gameScore_dodgedObstacle
						(damage,GetComponent<BoxCollider2D>(),lowestDistance)); // this will both increase score and show the increase
				}
			// die
			Destroy(gameObject);
			}
		if (unhit && myCollider.IsTouching(missileGameObject.GetComponent<Collider2D>())) // collide with missile
			{
			// change appearance and in-script state
			unhit=false;
			mySprRen.color=hitColor;
			myCollider.isTrigger=false;
			myRB.isKinematic=false;
			
			// damage player
			gameplayManagementScript.dealDamageToMissile(damage);

			// move from impact
			Vector2 hitForce;
			hitForce.x=transform.position.x-missileGameObject.transform.position.x;
			hitForce.y=transform.position.y-missileGameObject.transform.position.y;
			float hitForceAngle=Mathf.Atan2(hitForce.y,hitForce.x)*Mathf.Rad2Deg;
			myRB.AddForce(15*hitForce.normalized,ForceMode2D.Impulse);
			myRB.AddTorque(-10f*hitForceAngle);

			// spawn explosion
			Vector3 midpoint;
			midpoint.x=0.5f*(missileGameObject.transform.position.x+transform.position.x);
			midpoint.y=0.5f*(missileGameObject.transform.position.y+transform.position.y);
			midpoint.z=0.5f*(missileGameObject.transform.position.z+transform.position.z);
			Instantiate(Resources.Load("Prefabs/Explosion"),midpoint,Quaternion.identity);

			// lose points
			gameplayManagementScript.showScoreChange(
				gameplayManagementScript.gameScore_hitObstacle
					(damage,GetComponent<BoxCollider2D>())); // this will both lower score and show the loss
			}
		if (!unhit)
			{
			// add real downwards gravity
			myRB.AddForce(new Vector2(1f,-10f*myRB.mass));
			}
		}


	public float distance(Vector2 pointA,Vector2 pointB)
		{
		return Mathf.Sqrt(Mathf.Pow(pointA.x-pointB.x,2)+Mathf.Pow(pointA.y-pointB.y,2));
		}
	}
