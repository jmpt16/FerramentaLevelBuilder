using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnObject : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] GameObject obj;
	public Vector3 screenPoint;
	public Vector3 offset;
	public Vector3 currentPosition;
	private UserSelectionStateManager userSelectionManager;

	public void OnPointerDown(PointerEventData pointerEventData)
	{
		if (ToggleModeManager.InstMode_IsDrag == true)
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 10;
			Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			Instantiate(obj, worldPosition, Quaternion.identity);
		}
		else
		{
			Instantiate(obj);
		}
	}
}
