using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayObserver 
{

    public void SubscribeSpaceBarEvent()
    {
		Singleton.OnShoot += CheckShot;
	}

    public void UnsubscribeSpaceBarEvent() 
    {
		Singleton.OnShoot -= CheckShot;
	}
	private void CheckShot()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.transform.GetComponent<ObjectController>())
		{
			hitInfo.transform.GetComponent<ObjectController>().myObject.SetState(new Object_DeletedState());
		}
	}
}
