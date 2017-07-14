﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerScript:MonoBehaviour
	{



	// Use this for initialization
	void Start()
		{
		StartCoroutine("spawnChanceEveryHalfSecond");
		}
	
	// Update is called once per frame
	void Update()
		{
		}
	
	private IEnumerator spawnChanceEveryHalfSecond()
		{
		while (5==5)
			{
			if (0==Mathf.RoundToInt((120f/Time.time)*Random.value)) // literal constant is the time in seconds elapsed when the chance for spawning hits fifty percent
				{
				int itemType=Mathf.RoundToInt(15*Random.value);
				Vector3 randomPosition=new Vector3(transform.position.x,(Random.value*20)-10,0f);
				switch (itemType)
					{
					case 0: // fuel
					case 1:
					case 2:
						Instantiate(Resources.Load("Prefabs/FuelPickup"),randomPosition,Quaternion.identity);
						break;
					case 3: // health
					case 4:
						Instantiate(Resources.Load("Prefabs/HealthPickup"),randomPosition,Quaternion.identity);
						break;
					case 5: // airplane
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 14:
						Instantiate(Resources.Load("Prefabs/Airplane"),randomPosition,Quaternion.identity);
						break;
					case 15: // helicopter
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 21:
					case 22:
					case 23:
					case 24:
					case 25:
					case 26:
						Instantiate(Resources.Load("Prefabs/Helicopter"),randomPosition,Quaternion.identity);
						break;
					case 27: // spaceship
						Instantiate(Resources.Load("Prefabs/HominidSpaceShip"),randomPosition,Quaternion.identity);
						break;
					}
				}

			yield return new WaitForSeconds(0.5f);
			}

		}

	}
