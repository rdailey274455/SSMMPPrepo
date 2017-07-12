using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManagement:MonoBehaviour
	{
	public Vector2 windGravity=new Vector2(3f,0f);

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
	
	void Start ()
		{
		setMissileInitial_HP(100);
		setMissileInitial_ThrustFuel(10f,2f,1.5f);
		HPBarLevelImageScript=HPBarLevelImage.GetComponent<BarLevelScaling>(); // initialize bar
		FuelBarLevelImageScript=FuelBarLevelImage.GetComponent<BarLevelScaling>(); // initialize bar
		}

	void Update()
		{
		missileUpdateThrustFuel(Input.GetButton("Fire1"));
		HPBarLevelImageScript.setPercentage((float)missile_HP/missile_HP_max); // update bar
		FuelBarLevelImageScript.setPercentage(getMissileThrustFuelPercentage()); // update bar with method instead of direct calculation
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
	}
