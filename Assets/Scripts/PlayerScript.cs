using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript: MonoBehaviour
{
    public CharacterController controller;
	public Vector3 playerVelocity;
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
		Camera.main.transform.parent=transform;
		Camera.main.transform.position=transform.position+ transform.up*.5f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Debug.Log(controller.velocity.y);

        groundedPlayer = controller.isGrounded;
        /*if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }*/
        Vector3 moveDir = Vector3.zero;

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if (move.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(move.x, move.z)	* Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);

			moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;

		}
		controller.Move(moveDir * Time.deltaTime * playerSpeed* (Input.GetKey(KeyCode.LeftShift) ?runMultiplier:1) );

        /*if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }*/

        // Changes the height position of the player..
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
	private void Backup()
	{
		
	}
}