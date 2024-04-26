using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMode : MonoBehaviour
{
    public void setMode(int mode) {
        Singleton.mode = mode;
    }
}
