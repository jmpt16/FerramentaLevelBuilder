using UnityEngine;

public class MouseManager : MonoBehaviour
{
	private IMouseStates currentState;

	private void Start()
	{
		SetState(new FreeMoveMouseState());
	}

	private void Update()
	{
		currentState.OnUpdateState(this);
	}

	public void SetState(IMouseStates state)
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