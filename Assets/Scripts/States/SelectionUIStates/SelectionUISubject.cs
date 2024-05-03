using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionUISubject : MonoBehaviour
{
	public static GameObject selectionUIMenu;
	//events
	public static event Action E_ObjectSelected;
	public static event Action E_ObjectDeselected;

	public static void HandleObjectSelectedEvent()
	{
		E_ObjectSelected?.Invoke();
	}

	public static void HandleObjectDeselectedEvent()
	{
		E_ObjectDeselected?.Invoke();
	}

	public static void ObjectSelected()
	{
		selectionUIMenu.SetActive(true);
	}

	public static void ObjectDeselected()
	{
		selectionUIMenu.SetActive(false);
	}
}
