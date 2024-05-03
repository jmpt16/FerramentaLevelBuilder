using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionUIStatesManager : MonoBehaviour
{
	public GameObject selectionUIMenu;
	private static bool objIsSelected = false;
	//events
	public static event Action E_ObjectSelected;
	public static event Action E_ObjectDeselected;

	private void Start()
	{
		objIsSelected = false;
	}

	private void Update()
	{
		if (objIsSelected)
		{
			selectionUIMenu.SetActive(true);
		}
		else
		{
			selectionUIMenu.SetActive(false);
		}
	}

	#region Event Handlers
	public static void HandleObjectSelectedEvent()
	{
		E_ObjectSelected?.Invoke();
	}

	public static void HandleObjectDeselectedEvent()
	{
		E_ObjectDeselected?.Invoke();
	}
	#endregion

	#region Event Functions
	public static void ObjectSelected()
	{
		objIsSelected = true;
	}

	public static void ObjectDeselected()
	{
		objIsSelected = false;
	}
	#endregion
}
