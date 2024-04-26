using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmptyUserSelectionState : IUserSelectionStates
{
	//private GameObject validGameObject;
	public void OnEnterState(UserSelectionStateManager manager)
	{
		//manager.selectedObject = null;
		//validGameObject = null;
	}

	public void OnUpdateState(UserSelectionStateManager manager)
	{
		if (Input.GetMouseButtonDown(0))
		{
			manager.SetState(new DraggingUserSelectionSate());
			/*CheckObject();
			if (validGameObject == null)
			{
				
			}
			else
			{
				manager.selectedObject = validGameObject;
				validGameObject.GetComponent<ObjectManager>().SetState(new GrabbedObjectState());
				manager.SetState(new SelectedUserSelectionState());
			}*/
		}
	}

	public void OnExitState(UserSelectionStateManager manager)
	{

	}
}

public class SelectedUserSelectionState : IUserSelectionStates
{
	public GameObject currentObject;
	public GameObject selectedObject;
	public void OnEnterState(UserSelectionStateManager manager)
	{
		currentObject = manager.selectedObject;
		selectedObject = null;
	}

	public void OnUpdateState(UserSelectionStateManager manager)
	{
		if (Input.GetMouseButtonDown(0))
		{
			CheckObject();
			if (selectedObject == null || selectedObject == currentObject)
			{
				currentObject.GetComponent<ObjectManager>().SetState(new IdleObjectState());
				manager.SetState(new EmptyUserSelectionState());
			}
			else if (selectedObject != null && selectedObject != currentObject)
			{
				currentObject.GetComponent<ObjectManager>().SetState(new IdleObjectState());
				manager.selectedObject = selectedObject;
				currentObject = manager.selectedObject;
				currentObject.GetComponent<ObjectManager>().SetState(new GrabbedObjectState());
			}
		}
	}

	public void OnExitState(UserSelectionStateManager manager)
	{

	}

	public void CheckObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider != null && hit.collider.gameObject.tag == "Object")
			{
				selectedObject = hit.collider.gameObject;
			}
		}
	}
}

public class DraggingUserSelectionSate : IUserSelectionStates
{
	private GameObject validGameObject;
	public void OnEnterState(UserSelectionStateManager manager)
	{
		CheckObject();
	}

	public void OnUpdateState(UserSelectionStateManager manager)
	{
		Debug.Log("TOU A ARRASTAR");
		if (Input.GetMouseButtonUp(0))
		{
			manager.SetState(new EmptyUserSelectionState());
		}
	}

	public void OnExitState(UserSelectionStateManager manager)
	{

	}

	public void CheckObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider != null && hit.collider.gameObject.tag == "Object")
			{
				validGameObject = hit.collider.gameObject;
			}
		}
	}
}
