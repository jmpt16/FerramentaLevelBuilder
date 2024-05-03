using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    public int axisNum = 0;
	private Vector3 mOffset;
	private float mZCoord, dist = 0;
	private Vector3 GetMouseWorldPos()
	{
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = mZCoord+dist;
		return Camera.main.ScreenToWorldPoint(mousePoint);
	}
	// Update is called once per frame
	void Update()
    {
        if (Input.GetMouseButtonDown(0))//basicamente o onMouseClick
        {
			RaycastHit[] hits;
			Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
			hits = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.GetMask("Arrows"));
			
			if (hits.Length>0)
			{
				Debug.Log(hits[0].transform.name);
                if (hits[0].transform.root.gameObject.GetComponent<GizmoInteract>().axis.Contains(hits[0].transform.gameObject.GetComponent<Collider>()))
                {
					axisNum = System.Array.IndexOf(hits[0].transform.root.gameObject.GetComponent<GizmoInteract>().axis, hits[0].transform.gameObject.GetComponent<Collider>());
					Debug.Log(axisNum);
				}

				Singleton.selected = hits[0].transform.root.gameObject;
				dist = Vector3.Distance(Singleton.selected.transform.position, Camera.main.transform.position);
				mZCoord = Camera.main.WorldToScreenPoint(hits[0].transform.transform.position).z;
				mOffset = hits[0].transform.position - GetMouseWorldPos();
			}
			else
			{
				hits = Physics.RaycastAll(ray);
				if (hits.Length > 0)
				{
					Debug.Log(hits[0].transform.name);
					Singleton.selected = hits[0].transform.root.gameObject;
					dist = Vector3.Distance(Singleton.selected.transform.position, Camera.main.transform.position);
					mZCoord = Camera.main.WorldToScreenPoint(Singleton.selected.transform.position).z;
					mOffset = Singleton.selected.transform.position - GetMouseWorldPos();
					axisNum = -1;
				}
				else
				{
					Singleton.selected = null;
				}
			}
		}
		if (Input.GetMouseButton(0) && Singleton.selected!=null)//basicamente o onMouseDrag
		{
			/*mOffset = GetMouseWorldPos() + mOffset;
			Debug.Log(mOffset);*/
			switch (axisNum)
			{
				case -1://movimento drag/drop
					Singleton.selected.transform.position = GetMouseWorldPos() + mOffset;
					break;
				case 0://
					Singleton.selected.transform.position = new Vector3(
						(GetMouseWorldPos() + mOffset).x,
						Singleton.selected.transform.position.y,
						Singleton.selected.transform.position.z);
					break;
				case 1:
					Singleton.selected.transform.position = new Vector3(
						Singleton.selected.transform.position.x,
						(GetMouseWorldPos() + mOffset).y,
						Singleton.selected.transform.position.z);
					break;
				case 2:
					Singleton.selected.transform.position = new Vector3(
						Singleton.selected.transform.position.x,
						Singleton.selected.transform.position.y,
						(GetMouseWorldPos() + mOffset).z);
					break;
			}
		}
	}
	void moveOnScreen() {
		Plane plane = new Plane(-Camera.main.transform.forward, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			Vector3 pointalongplane = ray.origin + (ray.direction * distance);
			//GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			//Singleton.selected.transform.position = mOffset+pointalongplane;
			Singleton.selected.transform.position = pointalongplane;
		}
	}
	void moveOnAxis_X()
	{
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			Vector3 pointalongplane = ray.origin + (ray.direction * distance);
			//GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			//Singleton.selected.transform.position = mOffset+pointalongplane;
			Singleton.selected.transform.position = new Vector3(
				(pointalongplane).x,
				Singleton.selected.transform.position.y,
				Singleton.selected.transform.position.z);
		}
	}
	void moveOnAxis_Y()
	{
		Plane plane = new Plane(Vector3.right, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			Vector3 pointalongplane = ray.origin + (ray.direction * distance);
			//GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			//Singleton.selected.transform.position = mOffset+pointalongplane;
			Singleton.selected.transform.position = new Vector3(
				Singleton.selected.transform.position.x,
				(pointalongplane).y,
				Singleton.selected.transform.position.z);
		}
	}
	void moveOnAxis_Z()
	{
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			Vector3 pointalongplane = ray.origin + (ray.direction * distance);
			//GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			//Singleton.selected.transform.position = mOffset+pointalongplane;
			Singleton.selected.transform.position = new Vector3(
				Singleton.selected.transform.position.x,
				Singleton.selected.transform.position.y,
				(pointalongplane).z);
		}
	}
}
