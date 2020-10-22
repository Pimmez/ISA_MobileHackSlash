using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	[SerializeField] private Joystick joystick;
	[SerializeField] private float moveSpeed = 40f;

	private float horizontalMovement = 0f;
	private float verticalMovement = 0f;
	private bool isJumping = false;
	private bool isCrouching = false;

	private void Update()
	{
#if UNITY_EDITOR
		PCMovement();
#endif

#if UNITY_ANDROID
		MobileMovement();
#endif
	}

	private void PCMovement()
	{
		Debug.Log("Hallo Movement"); 
		horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;

		transform.Translate(horizontalMovement, 0, 0);

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
		{
			Debug.Log("Hallo Jump");
			isJumping = true;
		}

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			Debug.Log("Hallo Crouch");
			isCrouching = true;
		}
	}

	private void MobileMovement()
	{
		if(joystick.Horizontal >= 0.2f)
		{
			horizontalMovement = moveSpeed;
		}
		else if(joystick.Horizontal <= -0.2f)
		{
			horizontalMovement = -moveSpeed;
		}
		else
		{
			horizontalMovement = 0f;
		}

		transform.Translate(horizontalMovement, 0, 0);

		verticalMovement = joystick.Vertical;

		if (verticalMovement >= 0.5f)
		{
			isJumping = true;
		}

		if(verticalMovement <= -0.5f)
		{
			isCrouching = true;
		}
		else
		{
			isCrouching = false;
		}
	}
}