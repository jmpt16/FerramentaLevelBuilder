public class SelectionUIObserver
{
	#region Object Selected
	public void SubscribeObjectSelectedEvent()
	{
		SelectionUIStatesManager.E_ObjectSelected += HandleObjectSelectedEvent;
	}

	public void UnsubscribeObjectSelectedEvent()
	{
		SelectionUIStatesManager.E_ObjectSelected -= HandleObjectSelectedEvent;
	}

	public void HandleObjectSelectedEvent()
	{
		SelectionUIStatesManager.ObjectSelected();
	}
	#endregion
	#region Object Deselected
	public void SubscribeObjectDeselectedEvent()
	{
		SelectionUIStatesManager.E_ObjectSelected += HandleObjectDeselectedEvent;
	}

	public void UnsubscribeObjectDeselectedEvent()
	{
		SelectionUIStatesManager.E_ObjectSelected -= HandleObjectDeselectedEvent;
	}

	public void HandleObjectDeselectedEvent()
	{
		SelectionUIStatesManager.ObjectDeselected();
	}
	#endregion
}