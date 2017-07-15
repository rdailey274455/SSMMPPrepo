using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpeedScript:MonoBehaviour
	{
	[SerializeField] private GameObject gameplayManagerObject;
	private GameplayManagement gameplayManagerScript;
	[SerializeField] private ParticleSystem myPartSys;
	private ParticleSystem.MainModule myPartSys_mainModule;
	private float initialStartSpeed;

	// Use this for initialization
	void Start()
		{
		gameplayManagerScript=gameplayManagerObject.GetComponent<GameplayManagement>();
		if (myPartSys==null) myPartSys=GetComponent<ParticleSystem>();
		myPartSys_mainModule=myPartSys.main;
		initialStartSpeed=myPartSys_mainModule.startSpeed.constant;
		}
	
	// Update is called once per frame
	void Update()
		{
		myPartSys_mainModule.startSpeedMultiplier=initialStartSpeed*gameplayManagerScript.getSpeedMult();
		}
	}
