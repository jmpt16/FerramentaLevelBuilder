using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UserSelectionStateManager : MonoBehaviour
{
    private IUserSelectionStates currentState;
    public GameObject selectedObject;

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
