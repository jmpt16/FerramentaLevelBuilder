using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSpawn : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject obj;
    [HideInInspector]
    public Vector3 screenPoint, offset, currentPosition;
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
            Vector3 cameraPosAhead = Camera.main.transform.position + Camera.main.transform.forward * 10;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10))
            {
                cameraPosAhead = hit.point;
            }
            Instantiate(obj, cameraPosAhead, Quaternion.identity);
        }
		
	}
}
