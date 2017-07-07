using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragToRotate : MonoBehaviour {
	
	private Vector2 dragStartPos;
	private Vector2 dragEndPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))
			{
			dragStartPos=new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			}
	}
}
