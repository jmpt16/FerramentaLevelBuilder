public interface IUIStates
{
	void OnEnterState(UIManager uiManager);
	void OnUpdateState(UIManager uiManager);
	void OnExitState(UIManager uiManager);
}