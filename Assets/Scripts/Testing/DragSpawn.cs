using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSpawn : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject obj;
    public Vector3 screenPoint;
    public Vector3 offset;
    public Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (InstantiateModeManager.InstMode_IsDrag == true)
        {
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 10;
			Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			Instantiate(obj, worldPosition, Quaternion.identity);
		}
        else
        {
            Instantiate(obj);
        }
		
	}
}
