using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript:MonoBehaviour
	{
	public float speed=-10f;
	public int damage=10;
	private SpriteRenderer obstacleSprRen;
	private Rigidbody2D obstacleRB;
	private Collider2D obstacleCollider;
	public GameObject missileGameObject;
	public GameObject gameplayManagerObject;
	private bool unhit=true;
	private Color hitColor=new Color(0.85f,0.85f,0.85f,0.49f);

	// Use this for initialization
	void Start()
		{
		obstacleSprRen=GetComponent<SpriteRenderer>();
		obstacleRB=GetComponent<Rigidbody2D>();
		obstacleCollider=GetComponent<Collider2D>();

		obstacleRB.velocity=new Vector2(speed,0f);
		}
	
	// Update is called once per frame
	void Update()
		{
		
		}

	private void FixedUpdate()
		{
		if (unhit && obstacleCollider.IsTouching(missileGameObject.GetComponent<Collider2D>()))
			{
			// change self
			unhit=false;
			obstacleSprRen.color=hitColor;
			obstacleCollider.isTrigger=false;
			obstacleRB.isKinematic=false;
			
			// damage player
			gameplayManagerObject.GetComponent<GameplayManagement>().dealDamageToMissile(damage);

			// move from impact
			Vector2 hitForce;
			hitForce.x=transform.position.x-missileGameObject.transform.position.x;
			hitForce.y=transform.position.y-missileGameObject.transform.position.y;
			float hitForceAngle=Mathf.Atan2(hitForce.y,hitForce.x)*Mathf.Rad2Deg;
			obstacleRB.AddForce(25f*hitForce.normalized,ForceMode2D.Impulse);
			obstacleRB.AddTorque(-10f*hitForceAngle);

			// spawn explosion
			Vector3 midpoint;
			midpoint.x=0.5f*(missileGameObject.transform.position.x+transform.position.x);
			midpoint.y=0.5f*(missileGameObject.transform.position.y+transform.position.y);
			midpoint.z=0.5f*(missileGameObject.transform.position.z+transform.position.z);
			Instantiate(Resources.Load("Prefabs/Explosion"),midpoint,Quaternion.identity);
			}
		if (!unhit)
			{
			// add real downwards gravity
			obstacleRB.AddForce(new Vector2(1f,-10f*obstacleRB.mass));
			}
		}
	}
