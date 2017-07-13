using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManagement:MonoBehaviour
	{
	public Vector2 windGravity=new Vector2(3f,0f);

	private float gameScore=0f;
	private float missile_kmps=0.3f;
	private int missile_HP_max;
	private int missile_HP;
	private float missile_ThrustFuel_max;
	private float missile_ThrustFuel;
	private float missile_ThrustFuel_burn;
	private float missile_ThrustFuel_regen;
	
	// two variables needed for bar
	public GameObject HPBarLevelImage;
	private BarLevelScaling HPBarLevelImageScript;
	// two variables needed for bar
	public GameObject FuelBarLevelImage;
	private BarLevelScaling FuelBarLevelImageScript;

	public GameObject scoreText;
	public GameObject hudCanvas;
	
	void Start ()
		{
		setMissileInitial_HP(100);
		setMissileInitial_ThrustFuel(20f,1.6f,1.5f);
		HPBarLevelImageScript=HPBarLevelImage.GetComponent<BarLevelScaling>(); // initialize bar
		FuelBarLevelImageScript=FuelBarLevelImage.GetComponent<BarLevelScaling>(); // initialize bar
		}

	void Update()
		{
		missileUpdateThrustFuel(Input.GetButton("Fire1"));
		HPBarLevelImageScript.setPercentage((float)missile_HP/missile_HP_max); // update bar
		FuelBarLevelImageScript.setPercentage(getMissileThrustFuelPercentage()); // update bar with method instead of direct calculation
		scoreText.GetComponent<Text>().text="Score: "+Mathf.RoundToInt(gameScore);
		}

	void FixedUpdate()
		{
		Physics2D.gravity=windGravity;
		}
	



	// fuel functions
	private void missileUpdateThrustFuel(bool thrusting)
		{
		if (thrusting) missile_ThrustFuel-=missile_ThrustFuel_burn*Time.deltaTime;
		else missile_ThrustFuel+=missile_ThrustFuel_regen*Time.deltaTime;
		if (missile_ThrustFuel>missile_ThrustFuel_max) missile_ThrustFuel=missile_ThrustFuel_max;
		if (missile_ThrustFuel<0f) missile_ThrustFuel=0f;
		}
	public void setMissileInitial_ThrustFuel(float thrustFuelTankSize,float engineBurnRate,float engineRegenRate)
		{
		missile_ThrustFuel_max=thrustFuelTankSize;
		missile_ThrustFuel=missile_ThrustFuel_max;
		missile_ThrustFuel_burn=Mathf.Abs(engineBurnRate);
		missile_ThrustFuel_regen=engineRegenRate;
		}
	public float getMissileThrustFuelPercentage()
		{
		return missile_ThrustFuel/missile_ThrustFuel_max;
		}





	// health functions
	public void setMissileInitial_HP(int HP)
		{
		missile_HP_max=HP;
		missile_HP=missile_HP_max;
		}

	public void dealDamageToMissile(int incomingDamage)
		{
		missile_HP-=Mathf.Abs(incomingDamage);
		}




	// score functions
	public float gameScore_dodgedObstacle(float damage_dodged,BoxCollider2D collider_dodged,float closeness_dodged)
		{
		float totalIncreaseFromObstacle=1f*damage_dodged+0.1f*vecArea(collider_dodged.size)+(closeness_dodged);
		gameScore+=totalIncreaseFromObstacle;
		return totalIncreaseFromObstacle;
		}
	public float gameScore_hitObstacle(float damage_received,BoxCollider2D collider_hit)
		{
		float totalLossFromObstacle=1f*damage_received+0.1f*vecArea(collider_hit.size);
		gameScore-=totalLossFromObstacle;
		return totalLossFromObstacle;
		}
	public void showScoreChange(float amount)
		{
		GameObject newScoreChangeTextObject=(GameObject)Instantiate(Resources.Load("Prefabs/ScoreChangeText"),new Vector3(scoreText.transform.position.x,scoreText.transform.position.y-32,scoreText.transform.position.z),Quaternion.identity);
		newScoreChangeTextObject.transform.SetParent(hudCanvas.transform);
		if (amount>=0) newScoreChangeTextObject.GetComponent<Text>().text="+"+Mathf.RoundToInt(amount);
		else newScoreChangeTextObject.GetComponent<Text>().text="-"+Mathf.RoundToInt(Mathf.Abs(amount)); // hopefully this puts a minus
		}



	// misc functions
	public float vecArea(Vector2 size)
		{
		return size.x*size.y;
		}

	}
