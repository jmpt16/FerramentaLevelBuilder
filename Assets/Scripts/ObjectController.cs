using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectController : MonoBehaviour
{
    public Object myObject=new Object();
	private Vector3 mOffset;
	private float mZCoord, scrollVal = 0;
	// Start is called before the first frame update
	void Start()
    {
		myObject.SetState(new Object_IdleState());
		myObject.obj = gameObject;
    }

	// Update is called once per frame
	/*void Update()
    {
        Object.Update();
        if (Input.GetMouseButtonDown(0))//é batota usar o GetMouseButtonDOWN em vez do GetMouseButton ?
		{
            Object.SetState(new Object_ShootState());
		}
        else
        {
			Object.SetState(new Object_IdleState());
		}
    }*/

	void OnMouseDown()
	{
		if (Singleton.mode == 0)
		{
			mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			mOffset = gameObject.transform.position - GetMouseWorldPos();
		}
	}

	void OnMouseDrag()
	{
		if (Singleton.mode == 0)
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
	}

	private Vector3 GetMouseWorldPos()
	{
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = mZCoord + scrollVal;
		return Camera.main.ScreenToWorldPoint(mousePoint);
	}
	private void Update()
	{
		myObject.Update();
		scrollVal += Input.mouseScrollDelta.y;
		if (Vector3.Distance(transform.position, Camera.main.transform.position) < 2 && Input.mouseScrollDelta.y < 0)
		{
			scrollVal -= Input.mouseScrollDelta.y;
		}
	}
}
