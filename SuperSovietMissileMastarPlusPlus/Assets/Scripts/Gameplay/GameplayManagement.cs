using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManagement:MonoBehaviour
	{

	public Vector2 windGravity=new Vector2(3f,0f);

	// Use this for initialization
	void Start ()
		{
		}
	
	// Update is called once per frame
	void Update()
		{
		Physics2D.gravity=windGravity;
		}
	}
