using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Object myObject=new Object();
	private Vector3 mOffset;
	private float mZCoord, scrollVal = 0;
	int axisNum = 0;
	GizmoInteract gizmo;
	// Start is called before the first frame update
	void Start()
    {
		gizmo=transform.AddComponent<GizmoInteract>();
		myObject.SetState(new Object_IdleState());
		myObject.obj = gameObject;
    }

	/*void OnMouseDown()
	{

		mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		mOffset = gameObject.transform.position - GetMouseWorldPos();
	}

	void OnMouseDrag()
	{
		if (myObject.currState.ToString() == "Object_GrabbedState")
		{
			myObject.mOffset = GetMouseWorldPos() + mOffset;
			myObject.SetState(new Object_GrabbedState());
		}
	}

	void OnMouseUp()
	{
		myObject.SetState(new Object_IdleState());
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(1))
		{
			myObject.SetState(new Object_DeletedState());
		}
		
	}*/

	private Vector3 GetMouseWorldPos()
	{
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = mZCoord + scrollVal;
		return Camera.main.ScreenToWorldPoint(mousePoint);
	}
	private void Update()
	{
		if (Singleton.selected==gameObject)
		{
			myObject.SetState(new Object_GrabbedState());
		}
		else 
		{ 
			myObject.SetState(new Object_IdleState()); 
		}
		gizmo.gizmo.SetActive(myObject.currState.ToString() == "Object_GrabbedState");
		myObject.Update();
		/*scrollVal += Input.mouseScrollDelta.y;
		if (Vector3.Distance(transform.position, Camera.main.transform.position) < 2 && Input.mouseScrollDelta.y < 0)
		{
			scrollVal -= Input.mouseScrollDelta.y;
		}
		*/
	}
}
