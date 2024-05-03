using System.Runtime.Serialization;
using UnityEngine;

public class IdleObjectState : IObjectStates
{
	private float cycleSpeed = 0.2f;
	private float hueValue = 0f;
	public void OnEnterState(ObjectManager obj)
	{
		obj.Child_MovementGizmo.SetActive(false);
		obj.Child_RotationGizmo.SetActive(false);
		obj.GetComponent<BoxCollider>().enabled = true;
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
		obj.Child_MovementGizmo.SetActive(false);
		obj.Child_RotationGizmo.SetActive(false);
		obj.GetComponent<BoxCollider>().enabled = true;
	}

    public void OnUpdateState(ObjectManager obj)
    {
		if (Input.GetKeyDown(KeyCode.G))
		{
			obj.SetState(new MovementGizmoObjectState());
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			obj.SetState(new RotationGizmoObjectState());
		}
    }

    public void OnExitState(ObjectManager obj)
    {
        
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

#region Movement Gizmo Classes
public class MovementGizmoObjectState : IObjectStates
{
	public void OnEnterState(ObjectManager obj)
	{
		obj.Child_MovementGizmo.SetActive(true);
		obj.GetComponent<BoxCollider>().enabled = false;
	}

	public void OnUpdateState(ObjectManager obj)
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			obj.SetState(new SelectedObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}
}

public class GMoveXObjectState : IObjectStates
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
		Debug.Log("MOVING ON THE X AXIS");
		FollowMouse_X(obj);
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new MovementGizmoObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}

	public void FollowMouse_X(ObjectManager obj)
	{
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		obj.currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
		obj.transform.position = new Vector3(obj.currentPosition.x,obj.transform.position.y,obj.transform.position.z);
			
	}
}

public class GMoveYObjectState : IObjectStates
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
		Debug.Log("MOVING ON THE Y AXIS");
		FollowMouse_Y(obj);
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new MovementGizmoObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}
	public void FollowMouse_Y(ObjectManager obj)
	{
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		obj.currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
		obj.transform.position = new Vector3(obj.transform.position.x, obj.currentPosition.y, obj.transform.position.z);

	}
}

public class GMoveZObjectState : IObjectStates
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
		Debug.Log("MOVING ON THE Z AXIS");
		FollowMouse_Z(obj);
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new MovementGizmoObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}
	public void FollowMouse_Z(ObjectManager obj)
	{
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		obj.currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
		obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.currentPosition.z);

	}
}
#endregion

#region Rotation Gizmo Classes
public class RotationGizmoObjectState : IObjectStates
{
	public void OnEnterState(ObjectManager obj)
	{
		obj.Child_RotationGizmo.SetActive(true);
		obj.GetComponent<BoxCollider>().enabled = false;
	}

	public void OnUpdateState(ObjectManager obj)
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			obj.SetState(new SelectedObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}
}

public class GRotateXObjectState : IObjectStates
{
	public void OnEnterState(ObjectManager obj)
	{

	}

	public void OnUpdateState(ObjectManager obj)
	{
		Debug.Log("ROTATING ON THE X AXIS");
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new RotationGizmoObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}
}

public class GRotateYObjectState : IObjectStates
{
	public void OnEnterState(ObjectManager obj)
	{

	}

	public void OnUpdateState(ObjectManager obj)
	{
		Debug.Log("ROTATING ON THE Y AXIS");
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new RotationGizmoObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}
}

public class GRotateZObjectState : IObjectStates
{
	public void OnEnterState(ObjectManager obj)
	{

	}

	public void OnUpdateState(ObjectManager obj)
	{
		Debug.Log("ROTATING ON THE Z AXIS");
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new RotationGizmoObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}
}
#endregion