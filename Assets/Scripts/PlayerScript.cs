using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript: MonoBehaviour
{
    public CharacterController controller;
	public Vector3 playerVelocity;
	float xRot;
	public bool groundedPlayer;
	public float playerSpeed = 2.0f;
	public float runMultiplier = 1.4f;
	public float jumpHeight = 1.0f;
	public float gravityValue = -9.81f;
	float turnSmoothVelocity;
	public float turnSmoothTime;
	private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
		Camera.main.transform.parent = transform;
		Camera.main.transform.position = transform.position + transform.up * .5f;
		Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
		if (Input.GetKey(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("TestScene");
		}
		float mouseX=Input.GetAxis("Mouse X")* 1000 * Time.deltaTime;
		float mouseY=Input.GetAxis("Mouse Y")* 1000 * Time.deltaTime;

		xRot -= mouseY;
		xRot=Mathf.Clamp(xRot, -90f, 90f);
		
		Camera.main.transform.localRotation=Quaternion.Euler(xRot,0,0);
		transform.Rotate(mouseX * Vector3.up);

		groundedPlayer = controller.isGrounded;
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		Vector3 moveDir = transform.right * moveInput.x + transform.forward * moveInput.z;
		moveDir *= playerSpeed;
		
		playerVelocity = new Vector3(moveDir.x, playerVelocity.y, moveDir.z);

        if (Input.GetButtonDown("Jump")&& groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            groundedPlayer = false;

		}

        if (!groundedPlayer)
		{
			controller.stepOffset = 0f;
			playerVelocity.y += gravityValue * Time.deltaTime;
		}
		else 
		{
			playerVelocity.y = -1f;
			controller.stepOffset = 1f;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }
}