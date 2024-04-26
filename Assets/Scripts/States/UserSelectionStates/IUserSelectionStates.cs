public interface IUserSelectionStates
{
	void OnEnterState(UserSelectionStateManager manager);
	void OnUpdateState(UserSelectionStateManager manager);
	void OnExitState(UserSelectionStateManager manager);
}