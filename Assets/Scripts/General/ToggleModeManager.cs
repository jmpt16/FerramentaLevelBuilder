using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleModeManager : MonoBehaviour
{
    public static bool InstMode_IsDrag = true;
    public static bool deleteMode = false;
    public void ChangeInstantiateMode()
    {
        if (InstMode_IsDrag == true)
        {
            InstMode_IsDrag = false;
        }
        else
        {
            InstMode_IsDrag = true;
        }
    }

	public void ChangeDeleteMode()
	{
		if (deleteMode == false)
		{
			deleteMode = true;
		}
		else
		{
			deleteMode = false;
		}
	}
}
