using JetBrains.Annotations;
using System.Runtime.Serialization;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

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
	}
	public void OnExitState(ObjectManager obj)
	{

	}
}

public class SelectedObjectState : IObjectStates
{
	private ObjectManager objectManager;
	public void OnEnterState(ObjectManager obj)
	{
		objectManager = obj;
		#region EVENTS
		SelectedUserSelectionState.E_TriggerMovementGizmo += HandleMovementTrigger;
		#endregion
	}

	public void OnUpdateState(ObjectManager obj)
    {

    }

    public void OnExitState(ObjectManager obj)
    {
		#region EVENTS
		SelectedUserSelectionState.E_TriggerMovementGizmo -= HandleMovementTrigger;
		#endregion
	}

	private void HandleMovementTrigger()
	{
		objectManager.SetState(new MovementGizmoObjectState());
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
	private bool canLeave = false;
	public void OnEnterState(ObjectManager obj)
	{
		Debug.Log("i like batman");
		obj.gameObject.layer = 2;
		//obj.Child_MovementGizmo.SetActive(true);
		//obj.Child_MovementGizmo.transform.rotation = Quaternion.identity;
		SelectedUserSelectionState.E_TriggerMovementGizmo += HandleEvent;
		//obj.GetComponent<BoxCollider>().enabled = false;
	}

	public void OnUpdateState(ObjectManager obj)
	{
		if (canLeave == true)
		{
			obj.SetState(new SelectedObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{
		Debug.Log("bye bye");
		SelectedUserSelectionState.E_TriggerMovementGizmo -= HandleEvent;
		obj.gameObject.layer = 0;
	}

	private void HandleEvent()
	{
		canLeave = true;
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
		Debug.Log("getting tired of debugs");
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
		Debug.Log("i also like spiderman");
		screenPoint = Camera.main.WorldToScreenPoint(obj.transform.position);
		offset = obj.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	public void OnUpdateState(ObjectManager obj)
	{
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
	private bool canLeave = false;
	public void OnEnterState(ObjectManager obj)
	{
		obj.gameObject.layer = 2;
		SelectedUserSelectionState.E_TriggerRotationGizmo += HandleEvent;
	}

	public void OnUpdateState(ObjectManager obj)
	{
		if (canLeave)
		{
			obj.SetState(new SelectedObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{
		SelectedUserSelectionState.E_TriggerRotationGizmo -= HandleEvent;
		obj.gameObject.layer = 0;
	}

	private void HandleEvent()
	{
		canLeave = true;
	}
}

public class GRotateXObjectState : IObjectStates
{
	public float rotationSpeed = 4f;

    public void OnEnterState(ObjectManager obj)
	{

    }

	public void OnUpdateState(ObjectManager obj)
	{
		RotateMouse_X(obj);
        if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new RotationGizmoObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}

    public void RotateMouse_X(ObjectManager obj)
    {
		float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
		//float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

		obj.transform.rotation = Quaternion.AngleAxis(rotX, obj.transform.right) * obj.transform.rotation;
    }
}

public class GRotateYObjectState : IObjectStates
{
    public float rotationSpeed = 4f;

    public void OnEnterState(ObjectManager obj)
	{

	}

	public void OnUpdateState(ObjectManager obj)
	{
		RotateMouse_Y(obj);
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new RotationGizmoObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}

    public void RotateMouse_Y(ObjectManager obj)
    {
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
        //float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

        obj.transform.rotation = Quaternion.AngleAxis(rotX, -obj.transform.up) * obj.transform.rotation;
    }
}

public class GRotateZObjectState : IObjectStates
{
    public float rotationSpeed = 4f;

    public void OnEnterState(ObjectManager obj)
	{

	}

	public void OnUpdateState(ObjectManager obj)
	{
		RotateMouse_Z(obj);

		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new RotationGizmoObjectState());
		}
	}

	public void OnExitState(ObjectManager obj)
	{

	}

    public void RotateMouse_Z(ObjectManager obj)
    {
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
        //float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

        obj.transform.rotation = Quaternion.AngleAxis(rotX, -obj.transform.forward) * obj.transform.rotation;
    }
}
#endregion

#region Scale Gizmo Classes
public class ScaleGizmoObjectState : IObjectStates
{
	private bool canLeave = false;
	public void OnEnterState(ObjectManager obj)
	{
		SelectedUserSelectionState.E_TriggerScaleGizmo += HandleEvent;
		obj.gameObject.layer = 2;
    }

    public void OnUpdateState(ObjectManager obj)
    {
		if (canLeave == true)
		{
			obj.SetState(new SelectedObjectState());
		}
    }

    public void OnExitState(ObjectManager obj)
    {
		SelectedUserSelectionState.E_TriggerScaleGizmo -= HandleEvent;
		obj.gameObject.layer = 0;
	}

	private void HandleEvent()
	{
		canLeave = true;
	}
}

public class GScaleCenterObjectState : IObjectStates
{
	private Vector3 initialScale;
	private Vector3 initialMousePosition;
    public void OnEnterState(ObjectManager obj)
    {
		initialScale = obj.transform.localScale;
		initialMousePosition = Input.mousePosition;
    }

    public void OnUpdateState(ObjectManager obj)
    {

    }

    public void OnExitState(ObjectManager obj)
    {

    }
}

public class GScaleXObjectState : IObjectStates
{
	private Vector3 initialScale;
	private Vector3 initialMousePosition;
	private Vector3 newScale;
	private float minScale = 0.1f;
	private float maxScale = 50f;
	public void OnEnterState(ObjectManager obj)
	{
		initialScale = obj.transform.localScale;
		newScale = initialScale;
		initialMousePosition = Input.mousePosition;
	}

	public void OnUpdateState(ObjectManager obj)
	{
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new ScaleGizmoObjectState());
		}
		Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
		float scaleAmount = mouseDelta.x * 0.01f;
		newScale.x = Mathf.Clamp(initialScale.x + scaleAmount, minScale, maxScale);
		obj.transform.localScale = newScale;
	}

	public void OnExitState(ObjectManager obj)
	{

	}
}

public class GScaleYObjectState : IObjectStates
{
	private Vector3 initialScale;
	private Vector3 initialMousePosition;
	private Vector3 newScale;
	private float minScale = 0.1f;
	private float maxScale = 50f;
	public void OnEnterState(ObjectManager obj)
    {
		initialScale = obj.transform.localScale;
		newScale = initialScale;
		initialMousePosition = Input.mousePosition;
	}

    public void OnUpdateState(ObjectManager obj)
    {
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new ScaleGizmoObjectState());
		}
		Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
		float scaleAmount = mouseDelta.y * 0.01f;
		newScale.y = Mathf.Clamp(initialScale.y + scaleAmount, minScale, maxScale);
		obj.transform.localScale = newScale;
	}

    public void OnExitState(ObjectManager obj)
    {

    }
}

public class GScaleZObjectState : IObjectStates
{
	private Vector3 initialScale;
	private Vector3 initialMousePosition;
	private Vector3 newScale;
	private float minScale = 0.1f;
	private float maxScale = 50f;
	public void OnEnterState(ObjectManager obj)
	{
		initialScale = obj.transform.localScale;
		newScale = initialScale;
		initialMousePosition = Input.mousePosition;
	}

	public void OnUpdateState(ObjectManager obj)
	{
		if (Input.GetMouseButtonUp(0))
		{
			obj.SetState(new ScaleGizmoObjectState());
		}
		Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
		float scaleAmount = mouseDelta.x * 0.01f;
		newScale.z = Mathf.Clamp(initialScale.z + scaleAmount, minScale, maxScale);
		obj.transform.localScale = newScale;
	}

	public void OnExitState(ObjectManager obj)
	{

	}
}
#endregion