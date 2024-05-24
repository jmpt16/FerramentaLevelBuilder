using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private IUIStates currentState;

    //menu screens
    public GameObject mainMenuObj;
    public GameObject selectMenuObj;

    void Start()
    {
        SetState(new MainMenuUIState());
    }

    void Update()
    {
        currentState.OnUpdateState(this);
    }

    public void SetState(IUIStates state)
    {
        if (currentState != null)
        {
            currentState.OnExitState(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnterState(this);
        }
    }

	#region Main Menu Functions
    //goes to select menu
	public void GoToSelectScreen()
	{
		SetState(new SelectScreenUIState());
	}
	#endregion

	#region Select Menu Functions
    //returns to main menu
	public void ReturnToMainScreen()
    {
        SetState(new MainMenuUIState());
    }
	#endregion
}
