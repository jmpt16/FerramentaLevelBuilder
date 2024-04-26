using UnityEngine;

public class FreeMoveMouseState : IMouseStates
{
	public void OnEnterState(MouseManager mouse)
	{

	}
	public void OnUpdateState(MouseManager mouse)
	{
		if (Input.GetMouseButtonDown(0))
		{
			mouse.SetState(new DraggingMouseState());
		}
		if (Input.GetMouseButtonDown(1))
		{
			mouse.SetState(new EraseMouseState());
		}
	}
	public void OnExitState(MouseManager mouse)
	{

	}
}

public class DraggingMouseState : IMouseStates
{
	private GameObject selectedObject;
	public void OnEnterState(MouseManager mouse)
	{
		CheckObject();
	}
	public void OnUpdateState(MouseManager mouse)
	{
		if (selectedObject != null)
		{
			if (selectedObject.tag == "Object")
			{
				selectedObject.GetComponent<ObjectManager>().SetState(new SelectedObjectState());
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			mouse.SetState(new FreeMoveMouseState());
		}
	}
	public void OnExitState(MouseManager mouse)
	{
		if (selectedObject != null)
		{
			if (selectedObject.tag == "Object")
			{
				selectedObject.GetComponent<ObjectManager>().SetState(new IdleObjectState());
			}
		}
	}

	public void CheckObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider != null)
			{
				GameObject objectHit = hit.collider.gameObject;
				selectedObject = objectHit;
			}
		}
	}
}

public class EraseMouseState : IMouseStates
{
	private GameObject selectedObject;
	public void OnEnterState(MouseManager mouse)
	{
		CheckObject();
	}
	public void OnUpdateState(MouseManager mouse)
	{
		if (selectedObject != null)
		{
			selectedObject.GetComponent<ObjectManager>().SetState(new DeletingObjectState());
		}

		if (Input.GetMouseButtonUp(1))
		{
			mouse.SetState(new FreeMoveMouseState());
		}
	}
	public void OnExitState(MouseManager mouse)
	{

	}

	public void CheckObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider != null)
			{
				GameObject objectHit = hit.collider.gameObject;
				selectedObject = objectHit;
			}
		}
	}
}