using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMousePosition:MonoBehaviour
	{

	/* // old way, didn't work
    private Vector3 mousePos3D=new Vector3();
	private Vector2 mousePos2D=new Vector2();
	void OnGUI()
		{
		Camera mainCamera=Camera.main;
		Event currentEvent=Event.current;
		// Get the mouse position from Event.
		mousePos2D.x=currentEvent.mousePosition.x;
		mousePos2D.y=mainCamera.pixelHeight-currentEvent.mousePosition.y; // Note that the y position from Event is inverted.
		// remember: mousePos2D.x is a scalar used to get the mouse position, not the width of the visible plane that the reticle is moving on
		mousePos3D.x=mousePos2D.x*(mainCamera.transform.position.z*Mathf.Tan(Mathf.Deg2Rad*(0.5f*mainCamera.fieldOfView)));
		// remember: mousePos2D.x is a scalar used to get the mouse position, not the width of the visible plane that the reticle is moving on
		mousePos3D.y=mousePos2D.y*((1/mainCamera.aspect)*mainCamera.transform.position.z*Mathf.Tan(Mathf.Deg2Rad*(0.5f*mainCamera.fieldOfView))); // multiply the full by one over aspect
		transform.position=new Vector3(mousePos3D.x,mousePos3D.y,0f);
		}
	*/


	/* // Ray way works but is probably expensive
	void Update()
		{
		Ray mouseRay=Camera.main.ScreenPointToRay(Input.mousePosition);
		float rayDistance;
		Plane xyPlane=new Plane(
			new Vector3(0,0,-1),
			new Vector3(0,0,0));
		if (xyPlane.Raycast(mouseRay,out rayDistance))
			{
			transform.position=mouseRay.GetPoint(rayDistance);
			}
		}
	*/

	private void Update()
		{
		transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-Camera.main.transform.position.z));
		}







	// Use this for initialization
	void Start()
		{
		
		}
	}
