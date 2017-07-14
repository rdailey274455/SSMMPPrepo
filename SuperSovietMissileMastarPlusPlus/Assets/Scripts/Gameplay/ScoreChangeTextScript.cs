using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChangeTextScript:MonoBehaviour
	{

	public float alphaDecreasePerSec=1f/1.2f;
	private float fontSize_float;
	public float fontSizeIncreasePerSec=120f;

	// Use this for initialization
	void Start()
		{
		Color startColor=GetComponent<Text>().color;
		startColor.a=1f;
		GetComponent<Text>().color=startColor;


		fontSize_float=GetComponent<Text>().fontSize;
		}
	
	// Update is called once per frame
	void Update()
		{
		Color currentColor=GetComponent<Text>().color;
		currentColor.a-=alphaDecreasePerSec*Time.deltaTime;
		GetComponent<Text>().color=currentColor;

		fontSize_float+=fontSizeIncreasePerSec*Time.deltaTime;

		GetComponent<Text>().fontSize=Mathf.RoundToInt(fontSize_float);

		if (currentColor.a<=0f)
			{
			Destroy(gameObject);
			}
		}
	}
