using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth2DMove:MonoBehaviour
	{
	private float xVelocity=0f;
	private float yVelocity=0f;
	public float velocityCap=10f;
	public float deltaVelocityAcl=1f;
	public float deltaVelocityDcl=1f;
	


	// Use this for initialization
	void Start()
		{
		
		}
	
	// Update is called once per frame
	void Update()
        {
		Rigidbody2D platypusRB=GetComponent<Rigidbody2D>();
		xVelocity=Incdec(xVelocity,deltaVelocityAcl,deltaVelocityDcl,velocityCap,Input.GetKey(KeyCode.RightArrow),Input.GetKey(KeyCode.LeftArrow));
		yVelocity=Incdec(yVelocity,deltaVelocityAcl,deltaVelocityDcl,velocityCap,Input.GetKey(KeyCode.UpArrow),Input.GetKey(KeyCode.DownArrow));
		platypusRB.velocity=new Vector2(xVelocity,yVelocity);

		/* // old movement
		if (Input.GetKey(KeyCode.LeftArrow))
			{
			transform.Translate(-speed,0f,0f);
			}
		if (Input.GetKey(KeyCode.RightArrow))
			{
			transform.Translate(speed,0f,0f);
			}
		if (Input.GetKey(KeyCode.UpArrow))
			{
			transform.Translate(0f,speed,0f);
			}
		if (Input.GetKey(KeyCode.DownArrow))
			{
			transform.Translate(0f,-speed,0f);
			}
		*/
	    }





	/// <summary>
	/// Called every update, it gradually increases/decreases and gradually zeroes a value based on input
	/// Written by Rhys Dailey 2017-07-04
	/// </summary>
	/// <param name="var">The variable that gets adjusted</param>
	/// <param name="inc_rate">The rate by which it is pressed away from zero</param>
	/// <param name="dec_rate">The rate by which it resets back towards zero by itself</param>
	/// <param name="cap">The maximum value that the variable may be</param>
	/// <param name="pos_bool">The bool (usually whether a key is currently being pressed) that pushes var towards positive cap</param>
	/// <param name="neg_bool">The bool (usually whether a key is currently being pressed) that pushes var towards negative cap</param>
	/// <returns>The new value of the variable</returns>
	public float Incdec(float var,float inc_rate,float dec_rate,float cap,bool pos_bool,bool neg_bool)
		{
		float var_new=var;
		inc_rate=Mathf.Abs(inc_rate);
		dec_rate=Mathf.Abs(dec_rate);
		cap=Mathf.Abs(cap);
		if (pos_bool)
			{
			var_new+=inc_rate;
			if (var_new>cap) var_new=cap;
			}
		if (neg_bool)
			{
			var_new-=inc_rate;
			if (var_new<-cap) var_new=-cap;
			}
		if (!pos_bool && !neg_bool)
			{
			if (var_new>0)
				{
				var_new-=dec_rate;
				if (var_new<0) var_new=0;
				}
			if (var_new<0)
				{
				var_new+=dec_rate;
				if (var_new>0) var_new=0;
				}
			}
		return var_new;
		}
    }
