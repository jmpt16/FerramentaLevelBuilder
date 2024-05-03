using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera mainCamera;

	private void Awake()
	{
		mainCamera = Camera.main;
	}

	public void OnClick(InputAction.CallbackContext context)
	{
		if (!context.started)
		{
			return;
		}
	}
}
