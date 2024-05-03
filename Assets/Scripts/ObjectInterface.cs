using System;
using UnityEngine;

public class Object
{
	public IObjectState currState;
	public GameObject obj;
	public Vector3 mOffset;
	public void SetState(IObjectState state) {
		if (currState != null) {
			currState.OnExitObjectState();
		}

		currState = state;

		if (currState != null)
		{
			currState.OnEnterObjectState();
		}
	}

	public void Update() {
		if (currState != null) {
			currState.OnUpdateObjectState(obj, mOffset);
		}
	}
}

public interface IObjectState
{
	void OnEnterObjectState();
	void OnUpdateObjectState(GameObject g,Vector3 offset);
	void OnExitObjectState();
}

public class Object_IdleState : IObjectState {

	public void OnEnterObjectState() {
		//Debug.Log("Object entering Idle state");
	}
	public void OnUpdateObjectState(GameObject g, Vector3 offset) {
		//Debug.Log("Object in Idle state");
	}
	public void OnExitObjectState() {
		//Debug.Log("Object leaving Idle state");
	}
}

public class Object_GrabbedState : IObjectState
{

	public void OnEnterObjectState()
	{
		//Debug.Log("Object entering Shoot state");
	}
	public void OnUpdateObjectState(GameObject g, Vector3 offset)
	{
		//g.transform.position = offset;
	}
	public void OnExitObjectState()
	{
		//Debug.Log("Object leaving Shoot state");
	}
}


public class Object_DeletedState : IObjectState
{

	public void OnEnterObjectState()
	{
		//Debug.Log("Object entering PostShot state");
	}
	public void OnUpdateObjectState(GameObject g, Vector3 offset)
	{
		GameObject.Destroy(g);
	}
	public void OnExitObjectState()
	{
		//Debug.Log("Object leaving PostShot state");
	}
}