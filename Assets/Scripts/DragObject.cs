using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord, scrollVal = 0;

    void OnMouseDown()
    {
		mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		mOffset = gameObject.transform.position - GetMouseWorldPos();        
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
	}

	void OnMouseOver()
	{
        if (Input.GetMouseButtonDown(1)) {
			Destroy(gameObject);
		}
	}

	private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord+ scrollVal;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
	private void Update()
	{
        scrollVal += Input.mouseScrollDelta.y;
        if (Vector3.Distance(transform.position, Camera.main.transform.position)<2 && Input.mouseScrollDelta.y<0)
        {
			scrollVal -= Input.mouseScrollDelta.y;
		}
    }
}

