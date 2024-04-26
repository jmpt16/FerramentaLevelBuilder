using UnityEngine;
using TMPro;

public class MainMenuUIState : IUIStates
{
	public void OnEnterState(UIManager uiManager)
	{
		uiManager.mainMenuObj.SetActive(true);
	}
	public void OnUpdateState(UIManager uiManager)
	{

	}

	public void OnExitState(UIManager uiManager)
	{
		uiManager.mainMenuObj.SetActive(false);
	}
}

public class SelectScreenUIState : IUIStates
{
	public void OnEnterState(UIManager uiManager)
	{
		uiManager.selectMenuObj.SetActive(true);
	}
	public void OnUpdateState(UIManager uiManager)
	{

	}

	public void OnExitState(UIManager uiManager)
	{
		uiManager.selectMenuObj.SetActive(false);
	}
}