using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public float moveSpeed, rotSpeed;

    void Update()
    {
        Vector3 forwardAxis = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 sidewaysAxis = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 upwardsAxis = transform.up * Input.GetAxisRaw("Z-Axis");
        transform.position += (forwardAxis + sidewaysAxis+ upwardsAxis).normalized*Time.deltaTime* moveSpeed;
        if (Input.GetMouseButton(1))
        {
            transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0.0f) * Time.deltaTime* rotSpeed;
        }
    }
}