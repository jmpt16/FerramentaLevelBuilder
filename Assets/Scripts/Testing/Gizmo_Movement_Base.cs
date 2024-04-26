using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo_Movement_Base : MonoBehaviour
{
    private Vector3 initialClickPos;
    private Vector3 currentMousePos;
    private float? lastMousePoint = null;
	private float cameraDist;
	public Transform cameraTransform;
    void Start()
    {
        
    }

    void Update()
    {
		cameraDist = Vector3.Distance(transform.position, cameraTransform.position);
        if (Input.GetMouseButtonDown(0))
        {
			lastMousePoint = Input.mousePosition.x;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			lastMousePoint = null;
		}
		if (lastMousePoint != null)
		{
			float difference = Input.mousePosition.x - lastMousePoint.Value;
			Debug.Log("Difference: " + difference);
			Debug.Log("TransPosX: " + transform.position.x);
			transform.position = new Vector3(transform.position.x + (difference / cameraDist), transform.position.y, transform.position.z);
			lastMousePoint = Input.mousePosition.x;
		}
	}

	public void OnMouseDown()
	{

	}

	/*public void OnMouseDrag()
	{
        //currentMousePos = currentMousePos.normalized;

        if (currentMousePos.x < initialClickPos.x)
        {
            gameObject.transform.position -= currentMousePos;
            //gameObject.transform.Translate(Vector3.left * Time.deltaTime, Space.World);
        }
        else if (currentMousePos.x > initialClickPos.x)
        {
			gameObject.transform.position += currentMousePos;
			//gameObject.transform.Translate(Vector3.right * Time.deltaTime, Space.World);
		}
	}*/
}
