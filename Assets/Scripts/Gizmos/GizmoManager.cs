using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum GizmoType
{
	None,
	Movement,
	Scale,
	Rotation
}


public class GizmoManager : MonoBehaviour
{
	public UserSelectionStateManager statesManager;
	public GameObject movementGizmo;
	public GameObject scaleGizmo;
	public GameObject rotationGizmo;
	public GizmoType currentActiveGizmo = GizmoType.None;
	public GameObject activeGizmo = null;

	private void OnEnable()
	{
		SelectedUserSelectionState.E_TriggerMovementGizmo += HandleMovementTrigger;
		SelectedUserSelectionState.E_TriggerScaleGizmo += HandleScaleTrigger;
		SelectedUserSelectionState.E_TriggerRotationGizmo += HandleRotationTrigger;
		SelectedUserSelectionState.E_TriggerAll += DeactivateAll;
	}

	private void OnDisable()
	{
		SelectedUserSelectionState.E_TriggerMovementGizmo -= HandleMovementTrigger;
		SelectedUserSelectionState.E_TriggerScaleGizmo -= HandleScaleTrigger;
		SelectedUserSelectionState.E_TriggerRotationGizmo -= HandleRotationTrigger;
		SelectedUserSelectionState.E_TriggerAll -= DeactivateAll;
	}

	private void Start()
	{
		DeactivateAll();
	}

	private void Update()
	{
		if (activeGizmo != null)
		{
			/*if (currentActiveGizmo == GizmoType.Rotation || currentActiveGizmo == GizmoType.Scale)
			{
				activeGizmo.transform.rotation = statesManager.selectedObject.transform.rotation;
			}*/
			activeGizmo.transform.position = statesManager.selectedObject.transform.position;
		}
	}

	#region EVENT HANDLERS
	public void HandleMovementTrigger()
	{
		if (currentActiveGizmo != GizmoType.Movement)
		{
			DeactivateAll();
			movementGizmo.SetActive(true);
			movementGizmo.transform.position = statesManager.selectedObject.transform.position;
			activeGizmo = movementGizmo;
			currentActiveGizmo = GizmoType.Movement;
		}
		else
		{
			movementGizmo.SetActive(false);
			activeGizmo = null;
			currentActiveGizmo = GizmoType.None;
		}
	}

	public void HandleScaleTrigger()
	{
		if (currentActiveGizmo != GizmoType.Scale)
		{
			DeactivateAll();
			scaleGizmo.SetActive(true);
			scaleGizmo.transform.position = statesManager.selectedObject.transform.position;
			activeGizmo = scaleGizmo;
			currentActiveGizmo = GizmoType.Scale;
		}
		else
		{
			scaleGizmo.SetActive(false);
			activeGizmo = null;
			currentActiveGizmo = GizmoType.None;
		}
	}

	public void HandleRotationTrigger()
	{
		if (currentActiveGizmo != GizmoType.Rotation)
		{
			DeactivateAll();
			
			rotationGizmo.SetActive(true);
            Debug.Log("reached");
            rotationGizmo.transform.position = statesManager.selectedObject.transform.position;
			activeGizmo = rotationGizmo;
			currentActiveGizmo = GizmoType.Rotation;
		}
		else
		{
			rotationGizmo.SetActive(false);
			activeGizmo = null;
			currentActiveGizmo = GizmoType.None;
		}
	}

	public void DeactivateAll()
	{
		movementGizmo.SetActive(false);
		rotationGizmo.SetActive(false);
		scaleGizmo.SetActive(false);
		currentActiveGizmo = GizmoType.None;
		activeGizmo = null;
	}
	#endregion
}
