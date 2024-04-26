using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectManager : MonoBehaviour
{
	private IObjectStates currentState;

	public new Renderer renderer;
	public Vector3 screenPoint;
	public Vector3 offset;
	public Vector3 currentPosition;
	public GameObject Child_MovementGizmo;

	private void Start()
	{
		SetState(new IdleObjectState());
	}

	public void Update()
	{
		currentState.OnUpdateState(this);
	}

	public void SetState(IObjectStates state)
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

	private void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}


	public void SelfDestruct()
	{
		Destroy(gameObject);
	}
}