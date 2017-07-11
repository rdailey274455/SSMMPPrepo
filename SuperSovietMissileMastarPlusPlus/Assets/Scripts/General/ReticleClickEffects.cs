using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleClickEffects:MonoBehaviour
	{
	/* // tried being cool
	public float[] click_alpha={0.65f,0.98f}; // index zero is unclicked, index one is clicked
	public float[] click_scale={1.60f,1.00f}; // index zero is unclicked, index one is clicked
	
	void Update()
		{
		SpriteRenderer sprRen=GetComponent<SpriteRenderer>();
		
		setAlpha(sprRen.color,click_alpha[boolToInt(Input.GetMouseButton(0))]);
		setEachVec3Component(transform.localScale,click_scale[boolToInt(Input.GetMouseButton(0))]);
		}
	*/
	
	public float click_alpha=0.98f;
	public float noclick_alpha=0.57f;
	public float click_scale=0.60f;
	public float noclick_scale=1.00f;

	void Update()
		{
		SpriteRenderer sprRen=GetComponent<SpriteRenderer>();
		
		if (Input.GetMouseButton(0))
			{
			sprRen.color=new Color (sprRen.color.r,sprRen.color.g,sprRen.color.b,click_alpha);
			transform.localScale=click_scale*Vector3.one;
			}
		else
			{
			sprRen.color=new Color (sprRen.color.r,sprRen.color.g,sprRen.color.b,noclick_alpha);
			transform.localScale=noclick_scale*Vector3.one;
			}
		}

	public void setAlpha(Color col,float val)
			{
			col.a=val;
			}

	public void setEachVec3Component(Vector3 vector,float val)
		{
		vector.x=val;
		vector.y=val;
		vector.z=val;
		}

	public int boolToInt(bool booleanValue)
		{
		if (booleanValue) return 1;
		else return 0;
		}
	}
