using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//this is the default state
//the user hasnt selected or grabbed an object yet
public class EmptyUserSelectionState : IUserSelectionStates
{
	SelectionUIObserver UIObserver = new SelectionUIObserver();
	private GameObject currentGameObject;
	private GameObject selectedObject;

	//vars for the hold/click check
	private float mouseHoldDelay = 0.2f;
	private float clickTime = 0f;
	public void OnEnterState(UserSelectionStateManager manager)
	{
		UIObserver.SubscribeObjectSelectedEvent();
		clickTime = 0f;
		selectedObject = null;
		manager.selectedObject = null;
		currentGameObject = manager.selectedObject;
	}

	public void OnUpdateState(UserSelectionStateManager manager)
	{
		//start the hold/click timer
		if (Input.GetMouseButtonDown(0))
		{
			clickTime = Time.time;
		}

		//check if the user releases mb1
		if (Input.GetMouseButtonUp(0))
		{
			//check if mb1 was released in time to count as a click
			if (Time.time - clickTime <= mouseHoldDelay)
			{
				CheckObject();
				//only change states if the user clicked a valid object
				if (selectedObject != null)
				{
					UIObserver.HandleObjectSelectedEvent();
					currentGameObject = selectedObject;
					manager.selectedObject = currentGameObject;
					currentGameObject.GetComponent<ObjectManager>().SetState(new SelectedObjectState());
					manager.SetState(new SelectedUserSelectionState());
				}
			}
		}

		//check if the user is still holding mb1
		if (Input.GetMouseButton(0))
		{
			//check if the user held the key down long enough to count as a hold
			if (Time.time - clickTime > mouseHoldDelay)
			{
				CheckObject();
				//only change states if the user held the mouse over a valid object
				if (selectedObject != null)
				{
					currentGameObject = selectedObject;
					manager.selectedObject = currentGameObject;
					currentGameObject.GetComponent<ObjectManager>().SetState(new GrabbedObjectState());
					manager.SetState(new DraggingUserSelectionSate());
				}
			}
		}
	}

	public void OnExitState(UserSelectionStateManager manager)
	{
		UIObserver.UnsubscribeObjectSelectedEvent();
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

//in this state the user has a valid selected object and the object is in the selected state
public class SelectedUserSelectionState : IUserSelectionStates
{
	SelectionUIObserver UIObserver = new SelectionUIObserver();
	public GameObject currentObject;
	public GameObject selectedObject;
	public GameObject selectedGizmo;

	//movement gizmos
	public GameObject moveGizmoX;
	public GameObject moveGizmoY;
	public GameObject moveGizmoZ;

	//rotation gizmos
	public GameObject rotateGizmoX;
	public GameObject rotateGizmoY;
	public GameObject rotateGizmoZ;

	//vars for the hold/click check
	private float mouseHoldDelay = 0.2f;
	private float clickTime = 0f;
	public void OnEnterState(UserSelectionStateManager manager)
	{
		UIObserver.SubscribeObjectDeselectedEvent();
		clickTime = 0f;
		currentObject = manager.selectedObject;
		selectedObject = null;
		selectedGizmo = null;

		//init move gizmos
		moveGizmoX = currentObject.GetComponent<ObjectManager>().GMoveX;
		moveGizmoY = currentObject.GetComponent<ObjectManager>().GMoveY;
		moveGizmoZ = currentObject.GetComponent<ObjectManager>().GMoveZ;

		//init rotate gizmos
		rotateGizmoX = currentObject.GetComponent<ObjectManager>().GRotateX;
		rotateGizmoY = currentObject.GetComponent <ObjectManager>().GRotateY;
		rotateGizmoZ = currentObject.GetComponent<ObjectManager>().GRotateZ;
	}

	public void OnUpdateState(UserSelectionStateManager manager)
	{
		if (Input.GetMouseButtonDown(0))
		{
			clickTime = Time.time;
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (Time.time - clickTime <= mouseHoldDelay)
			{
				CheckObject();
				//if the user didnt click a valid object or if the object clicked was the current object
				//both return to the default state
				if (selectedObject == null || selectedObject == currentObject)
				{
					UIObserver.HandleObjectDeselectedEvent();
					currentObject.GetComponent<ObjectManager>().SetState(new IdleObjectState());
					manager.SetState(new EmptyUserSelectionState());
				}
				//if the user did click on a valid object that wasnt the current object
				//the selected objects switch
				else if (selectedObject != null && selectedObject != currentObject)
				{
					currentObject.GetComponent<ObjectManager>().SetState(new IdleObjectState());
					manager.selectedObject = selectedObject;
					currentObject = manager.selectedObject;
					currentObject.GetComponent<ObjectManager>().SetState(new SelectedObjectState());
				}
			}
		}

		if (Input.GetMouseButton(0))
		{
			if(Time.time - clickTime > mouseHoldDelay)
			{
				CheckGizmo();
				if (selectedGizmo != null)
				{
					switch (selectedGizmo)
					{
						//movement gizmos
						case var _ when selectedGizmo == moveGizmoX:
							currentObject.GetComponent<ObjectManager>().SetState(new GMoveXObjectState());
							manager.SetState(new GrabGizmoUserSelectionState());
							break;
						case var _ when selectedGizmo == moveGizmoY:
							currentObject.GetComponent<ObjectManager>().SetState(new GMoveYObjectState());
							manager.SetState(new GrabGizmoUserSelectionState());
							break;
						case var _ when selectedGizmo == moveGizmoZ:
							currentObject.GetComponent<ObjectManager>().SetState(new GMoveZObjectState());
							manager.SetState(new GrabGizmoUserSelectionState());
							break;
						//rotation gizmos
						case var _ when selectedGizmo == rotateGizmoX:
							currentObject.GetComponent<ObjectManager>().SetState(new GRotateXObjectState());
							manager.SetState(new GrabGizmoUserSelectionState());
							break;
						case var _ when selectedGizmo == rotateGizmoY:
							currentObject.GetComponent<ObjectManager>().SetState(new GRotateYObjectState());
							manager.SetState(new GrabGizmoUserSelectionState());
							break;
						case var _ when selectedGizmo == rotateGizmoZ:
							currentObject.GetComponent<ObjectManager>().SetState(new GRotateZObjectState());
							manager.SetState(new GrabGizmoUserSelectionState());
							break;
					}
				}
				else if (selectedGizmo == null)
				{
					Debug.Log("OPA TOU NULL");
				}
			}
		}
	}

	public void OnExitState(UserSelectionStateManager manager)
	{
		UIObserver.UnsubscribeObjectSelectedEvent();
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

	public void CheckGizmo()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider != null && hit.collider.gameObject.layer == 3)
			{
				selectedGizmo = hit.collider.gameObject;
			}
		}
	}
}

public class DraggingUserSelectionSate : IUserSelectionStates
{
	public void OnEnterState(UserSelectionStateManager manager)
	{

	}

	public void OnUpdateState(UserSelectionStateManager manager)
	{
		
		if (Input.GetMouseButtonUp(0))
		{
			manager.selectedObject.GetComponent<ObjectManager>().SetState(new IdleObjectState());
			manager.SetState(new EmptyUserSelectionState());
		}
	}

	public void OnExitState(UserSelectionStateManager manager)
	{

	}
}

public class GrabGizmoUserSelectionState : IUserSelectionStates
{
	public void OnEnterState(UserSelectionStateManager manager)
	{

	}

	public void OnUpdateState(UserSelectionStateManager manager)
	{
		if (Input.GetMouseButtonUp(0))
		{
			manager.SetState(new SelectedUserSelectionState());
		}
	}

	public void OnExitState(UserSelectionStateManager manager)
	{

	}
}
