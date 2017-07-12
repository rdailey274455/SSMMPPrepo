using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLevelScaling:MonoBehaviour
	{
	private float percentage;
	private RectTransform rectTransformComponent;
	private float startWidth;

	// Use this for initialization
	void Start()
		{
		rectTransformComponent=GetComponent<RectTransform>();
		startWidth=rectTransformComponent.sizeDelta.x;
		}
	
	// Update is called once per frame
	void Update()
		{
		rectTransformComponent.sizeDelta=new Vector2(startWidth*percentage,rectTransformComponent.sizeDelta.y);
		}
	
	public void setPercentage(float toldPercentage)
		{
		percentage=toldPercentage;
		if (percentage>1.0f) percentage=1.0f;
		if (percentage<0.0f) percentage=0.0f;
		}
	}
