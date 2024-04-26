public interface IObjectStates
{
	void OnEnterState(ObjectManager obj);
	void OnUpdateState(ObjectManager obj);
	void OnExitState(ObjectManager obj);
}