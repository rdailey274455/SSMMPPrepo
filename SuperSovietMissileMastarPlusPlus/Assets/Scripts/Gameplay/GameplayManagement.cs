using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;

public class GameplayManagement:MonoBehaviour
	{
	public Vector2 windGravity=new Vector2(3f,0f);
	
	private int gameScore=0;
	private int finalScore=0;
	private float finalDistance;
	private float missile_kmps=3.1f;
	private int missile_HP_max;
	private int missile_HP;
	private float missile_fuel_max;
	private float missile_fuel;
	private float missile_fuel_burn;
	private float missile_fuel_regen;
	private float finalTime=0;
	private bool mission_live=true;
	private float speedMult=1f;
	[SerializeField] private AudioSource musicAudioSource;
	[SerializeField] private float speedMult_deltaPerSec=0.036f;
	private string playerName="Player1";
	private bool acceptingTextInput=false;
	public float gameOverMusicDelay=2f;

	public GameObject missileObject;

	public GameObject hudCanvas;

	// two variables needed for bar
	public GameObject HPBarLevelImage;
	private BarLevelScaling HPBarLevelImageScript;
	// two variables needed for bar
	public GameObject FuelBarLevelImage;
	private BarLevelScaling FuelBarLevelImageScript;
	
	public GameObject scoreText;
	public GameObject quitButton;
	public GameObject timeText;
	public GameObject distanceText;
	public GameObject missionOverText;
	public GameObject missionReportText;
	private Text missionReportTextComponent;
	
	void Start ()
		{
		setMissileInitial_HP(100);
		setMissileInitial_fuel(20f,1.8f,1.5f);
		HPBarLevelImageScript=HPBarLevelImage.GetComponent<BarLevelScaling>(); // initialize bar
		FuelBarLevelImageScript=FuelBarLevelImage.GetComponent<BarLevelScaling>(); // initialize bar

		if (musicAudioSource==null) musicAudioSource=GetComponent<AudioSource>();
		musicAudioSource.volume=1.0f;

		if (missileObject==null) missileObject=GameObject.FindGameObjectWithTag("Missile");

		missionReportTextComponent=missionReportText.GetComponent<Text>();
		}

	void Update()
		{



		// update hud items
		Cursor.visible=false;
		HPBarLevelImageScript.setPercentage((float)missile_HP/missile_HP_max); // update bar
		FuelBarLevelImageScript.setPercentage(getMissileFuelPercentage()); // update bar with method instead of direct calculation
		if (scoreText.activeSelf) scoreText.GetComponent<Text>().text="Score: "+Mathf.RoundToInt(gameScore);
		if (timeText.activeSelf) timeText.GetComponent<Text>().text="Time: "+secondsToString(Time.time);
		if (distanceText.activeSelf) distanceText.GetComponent<Text>().text="Dist.: "+Mathf.RoundToInt(missile_kmps*Time.time*getSpeedMult())+" km";
		
		// update gameplay items
		speedMult+=speedMult_deltaPerSec*Time.deltaTime;
		updateMissileFuel(Input.GetButton("Fire1"));
		if (!getMissionStatus())
			{
			missionReportTextComponent.text="Your score: "+finalScore+"\nYour time: "+secondsToString(finalTime)+"\nYour distance: "+finalDistance+" km"+"\nYour name: "+playerName;
			
			if (Time.time>=finalTime+gameOverMusicDelay && !musicAudioSource.isPlaying) musicAudioSource.Play();

			if (acceptingTextInput)
				{
				if (Input.anyKeyDown)
					{
					if (Input.inputString=="\b")
						{
						playerName=backspaced(playerName);
						}
					else if (Input.inputString=="\n" || Input.inputString=="\r")
						{
						acceptingTextInput=false;
						}
					else
						{
						playerName+=Input.inputString;
						}
					}
				if (Mathf.RoundToInt(3f*Time.time)%2==0) missionReportTextComponent.text+="_";
				}
			else
				{
				quitButton.SetActive(true);
				}
			}

		}

	void FixedUpdate()
		{
		Physics2D.gravity=windGravity;
		}
	



	// central game functions
	public void missionOver()
		{
		GameObject finalExplosion=(GameObject)Instantiate(Resources.Load("Prefabs/Explosion"),missileObject.transform.position,Quaternion.identity);
		finalExplosion.transform.localScale=Vector3.one*6.3f;
		finalScore=gameScore;
		mission_live=false;
		finalTime=Time.time;
		finalDistance=Mathf.RoundToInt(missile_kmps*finalTime*getSpeedMult());
		missionOverText.GetComponent<Text>().enabled=true;
		missionReportTextComponent.enabled=true;
		acceptingTextInput=true;
		missileObject.SetActive(false);
		scoreText.SetActive(false);
		timeText.SetActive(false);
		distanceText.SetActive(false);
		missileObject.GetComponent<MissileScript>().offscreenArrowObject.SetActive(false);
		musicAudioSource.Stop();
		musicAudioSource.volume=0.72f;
		musicAudioSource.clip=(AudioClip)Resources.Load("Audio/gameover");
		}
	public bool getMissionStatus()
		{
		return mission_live;
		}
	public float getSpeedMult()
		{
		return speedMult;
		}
	public void newHighScore()
		{
		// load the list of high scores
		List<HighScore> highScores=HighScore.loadHighScores_lengthy();
		// add in the new one
		highScores.Add(new HighScore(playerName,finalScore,finalTime,finalDistance));
		// save the list
		HighScore.saveHighScores(highScores);
		}


	// fuel functions
	private void updateMissileFuel(bool thrusting)
		{
		if (thrusting) missile_fuel-=missile_fuel_burn*Time.deltaTime;
		else missile_fuel+=missile_fuel_regen*Time.deltaTime;
		if (missile_fuel>missile_fuel_max) missile_fuel=missile_fuel_max;
		if (missile_fuel<0f) missile_fuel=0f;
		}
	public void setMissileInitial_fuel(float thrustFuelTankSize,float engineBurnRate,float engineRegenRate)
		{
		missile_fuel_max=thrustFuelTankSize;
		missile_fuel=missile_fuel_max;
		missile_fuel_burn=Mathf.Abs(engineBurnRate);
		missile_fuel_regen=engineRegenRate;
		}
	public float getMissileFuelPercentage()
		{
		return missile_fuel/missile_fuel_max;
		}
	public float getMissileMaxFuel()
		{
		return missile_fuel_max;
		}
	public void missile_fuel_change(float fuelDelta)
		{
		missile_fuel+=fuelDelta;
		if (missile_fuel>missile_fuel_max) missile_fuel=missile_fuel_max;
		}




	// health functions
	public void setMissileInitial_HP(int HP)
		{
		missile_HP_max=HP;
		missile_HP=missile_HP_max;
		}
	
	public int getMissileMaxHP()
		{
		return missile_HP_max;
		}

	public void missile_HP_change(int HPdelta)
		{
		missile_HP+=HPdelta;
		if (missile_HP<0)
			{
			missionOver();
			}
		if (missile_HP>missile_HP_max)
			{
			missile_HP=missile_HP_max;
			}
		}




	// score functions
	public float gameScore_dodgedObstacle(float damage_dodged,BoxCollider2D collider_dodged,float closeness_dodged)
		{
		int totalIncreaseFromObstacle=Mathf.RoundToInt(1.2f*damage_dodged+0.2f*vecArea(collider_dodged.size)+2500f*(1f/closeness_dodged));
		if (getMissionStatus()) gameScore+=totalIncreaseFromObstacle;
		return totalIncreaseFromObstacle;
		}
	public float gameScore_hitObstacle(float damage_received,BoxCollider2D collider_hit)
		{
		int totalLossFromObstacle=-Mathf.RoundToInt(1.2f*damage_received+0.2f*vecArea(collider_hit.size));
		if (getMissionStatus()) gameScore+=totalLossFromObstacle;
		return totalLossFromObstacle;
		}
	public void showScoreChange(float amount)
		{
		if (getMissionStatus())
			{
			GameObject newScoreChangeTextObject=(GameObject)Instantiate(Resources.Load("Prefabs/ScoreChangeText"),
				new Vector3(scoreText.transform.position.x,scoreText.transform.position.y,scoreText.transform.position.z),
				Quaternion.identity);
			newScoreChangeTextObject.transform.SetParent(hudCanvas.transform);
			if (amount>=0) newScoreChangeTextObject.GetComponent<Text>().text="+"+amount;
			else newScoreChangeTextObject.GetComponent<Text>().text="-"+Mathf.Abs(amount);
			}
		}



	// misc functions
	public float vecArea(Vector2 size)
		{
		return size.x*size.y;
		}


	public string secondsToString(float seconds)
		{
		int secondsFloored=Mathf.FloorToInt(seconds);
		float secondsDecimal=0.01f*Mathf.RoundToInt(100*(seconds-secondsFloored));
		int minutes=secondsFloored/60;
		float secondsFinal=(int)(secondsFloored-(60*minutes))+secondsDecimal;
		if (secondsFinal<10) return minutes+"'0"+secondsFinal+"''";
		else return minutes+"'"+secondsFinal+"''";
		}


	public string backspaced(string str)
		{
		switch (str.Length)
			{
			case 0:
			case 1:
				return "";
			default:
				return str.Substring(0,str.Length-1);
			}
		}
	}
