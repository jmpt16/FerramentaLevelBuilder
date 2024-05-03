using UnityEngine;
using UnityEngine.Rendering;

public class ObjectManager : MonoBehaviour
{
	public IObjectStates currentState;

	public new Renderer renderer;
	public Vector3 screenPoint;
	public Vector3 offset;
	public Vector3 currentPosition;

	//movement gizmos
	public GameObject Child_MovementGizmo;
	public GameObject GMoveX;
	public GameObject GMoveY;
	public GameObject GMoveZ;

	//rotation gizmos
	public GameObject Child_RotationGizmo;
	public GameObject GRotateX;
	public GameObject GRotateY;
	public GameObject GRotateZ;

	private void Start()
	{
		if (InstantiateModeManager.InstMode_IsDrag == true)
		{
			SetState(new GrabbedObjectState());
		}
		else
		{
			SetState(new IdleObjectState());
		}
	}

	public void Update()
	{
		Debug.Log(currentState);
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