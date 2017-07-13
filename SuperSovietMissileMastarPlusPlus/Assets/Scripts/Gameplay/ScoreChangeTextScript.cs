using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChangeTextScript:MonoBehaviour
	{

	public float alphaDecreasePerSec=1f/1.5f;
	public float scaleIncreasePerSec=0.18f;

	// Use this for initialization
	void Start()
		{
		Color startColor=GetComponent<Text>().color;
		startColor.a=1f;
		GetComponent<Text>().color=startColor;
		}
	
	// Update is called once per frame
	void Update()
		{
		Color currentColor=GetComponent<Text>().color;
		currentColor.a-=alphaDecreasePerSec*Time.deltaTime;
		GetComponent<Text>().color=currentColor;
		transform.localScale+=scaleIncreasePerSec*Vector3.one*Time.deltaTime;
		if (currentColor.a<=0f)
			{
			Destroy(gameObject);
			}
		}
	}
