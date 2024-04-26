using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateModeManager : MonoBehaviour
{
    public static bool InstMode_IsDrag = true;
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
}
