using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoInteract : MonoBehaviour
{
    public GameObject gizmo;
	public Collider[] axis;
	private void Awake()
	{
		axis=new Collider[3];
		gizmo = Resources.Load<GameObject>("MovementGizmo");
		gizmo = Instantiate(gizmo, transform.position, Quaternion.Euler(0, 0, 0), transform);
		axis[0] = gizmo.transform.Find("X_Collider").GetComponent<Collider>();
		axis[1] = gizmo.transform.Find("Y_Collider").GetComponent<Collider>();
		axis[2] = gizmo.transform.Find("Z_Collider").GetComponent<Collider>();
		
		gizmo.SetActive(false);
	}
	
}
