using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeryColors:MonoBehaviour
	{
	private System.Random RanNumGen=new System.Random();
	public Color BGRed=new Color(0.7f,0f,0f);
	public Color FGWhite=new Color(1f,1f,1f,0.97f);
	public float flickerRadius=0.05f;

	// Use this for initialization
	void Start()
		{
		
		}
	
	// Update is called once per frame
	void Update()
		{
		Camera CameraComponent=GetComponent<Camera>();
		if (CameraComponent!=null)
			{
			float flickerAmount=(flickerRadius*System.Convert.ToSingle(RanNumGen.NextDouble()));
			CameraComponent.backgroundColor=AddFloatToRGB(BGRed,flickerAmount);
			}
		}

	public Color AddFloatToRGB(Color col,float val)
		{
		return new Color(col.r+val,col.g+val,col.b+val,col.a);
		}
	}
