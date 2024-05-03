using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrimitives : MonoBehaviour
{
    public void spawnCube()
    {
		GameObject newObj= GameObject.CreatePrimitive(PrimitiveType.Cube);
		newObj.AddComponent<ObjectController>();
    }
	public void spawnSphere()
	{
		GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		newObj.AddComponent<ObjectController>();
	}
}
