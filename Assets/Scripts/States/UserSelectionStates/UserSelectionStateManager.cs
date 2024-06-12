using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UserSelectionStateManager : MonoBehaviour
{
    private IUserSelectionStates currentState;
    [Header("Selected Object")]
    [Space(5)]
    public GameObject selectedObject;
    [Space(20)]
    [Header("Movement Gizmos")]
    [Space(5)]
    public GameObject movementGizmo;
    public GameObject xMovementGizmo;
    public GameObject yMovementGizmo;
    public GameObject zMovementGizmo;
    [Space(20)]
    [Header("Rotation Gizmos")]
    [Space(5)]
    public GameObject rotationGizmo;
    public GameObject xRotationGizmo;
    public GameObject yRotationGizmo;
    public GameObject zRotationGizmo;
    [Space(20)]
    [Header("Scale Gizmos")]
    [Space(5)]
    public GameObject scaleGizmo;
    public GameObject centerScaleGizmo;
    public GameObject xScaleGizmo;
    public GameObject yScaleGizmo;
    public GameObject zScaleGizmo;

	void Start()
    {
        SetState(new EmptyUserSelectionState());
        selectedObject = null;
    }

    void Update()
    {
        currentState.OnUpdateState(this);
    }

    public void SetState(IUserSelectionStates state)
    {
        if (currentState != null)
        {
            currentState.OnExitState(this);
        }

        currentState = state;

        if (state != null)
        {
            currentState.OnEnterState(this);
        }
    }
}
