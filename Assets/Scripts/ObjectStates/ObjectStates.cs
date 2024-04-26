using System.Runtime.Serialization;
using UnityEditor.Build.Content;
using UnityEngine;

public class IdleObjectState : IObjectStates
{
	private float cycleSpeed = 0.2f;
	private float hueValue = 0f;
	public void OnEnterState(ObjectManager obj)
	{

	}
	public void OnUpdateState(ObjectManager obj)
	{
		hueValue += cycleSpeed * Time.deltaTime;
		hueValue %= 1f;
		Color currentColor = Color.HSVToRGB(hueValue, 1, 1);
		obj.renderer.material.color = currentColor;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			obj.SetState(new SelectedObjectState());
		}
	}
	public void OnExitState(ObjectManager obj)
	{

	}
}

public class SelectedObjectState : IObjectStates
{
	public void OnEnterState(ObjectManager obj)
	{
		obj.Child_MovementGizmo.SetActive(true);
	}

    public void OnUpdateState(ObjectManager obj)
    {

    }

    public void OnExitState(ObjectManager obj)
    {
        obj.Child_MovementGizmo.SetActive(false);
    }
}

public class GrabbedObjectState : IObjectStates
{
	public Vector3 screenPoint;
	public Vector3 offset;
	public void OnEnterState(ObjectManager obj)
	{
		screenPoint = Camera.main.WorldToScreenPoint(obj.transform.position);
		offset = obj.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	public void OnUpdateState(ObjectManager obj)
	{
		FollowMouse(obj);
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new IdleObjectState());
		}
	}
	public void OnExitState(ObjectManager obj)
	{

	}

	public void FollowMouse(ObjectManager obj)
	{
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		obj.currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
		obj.transform.position = obj.currentPosition;
	}
}

public class DeletingObjectState : IObjectStates
{
	public void OnEnterState(ObjectManager obj)
	{
		
	}
	public void OnUpdateState(ObjectManager obj)
	{
		obj.SelfDestruct();
	}
	public void OnExitState(ObjectManager obj)
	{

	}
}