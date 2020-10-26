using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	//References
	[SerializeField] private Joystick joystick;
	[SerializeField] private Animator anim;
	[SerializeField] private SpriteRenderer spriteRenderer;
	//Values
	[SerializeField] private float moveSpeed = 10f;
	[SerializeField] private float jumpForce = 10f;
	[SerializeField] private Transform groundCheck;

	public const string RIGHT = "right";
	public const string LEFT = "left";

	private Rigidbody2D rigid;
	private string buttonPressed;
	private bool isJumping = false;
	private bool lastFlip;
	private float distToGround;


	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	private bool IsGrounded()
	{
		return Physics2D.OverlapCircle(groundCheck.position, 0.5f, LayerMaskUtility.Ground);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(groundCheck.position, 0.5f);
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			spriteRenderer.flipX = true;
			anim.SetBool("isRunning", true);
			buttonPressed = LEFT;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			spriteRenderer.flipX = false;
			anim.SetBool("isRunning", true);
			buttonPressed = RIGHT;
		}
		else
		{
			spriteRenderer.flipX = lastFlip;

			anim.SetBool("isRunning", false);
			buttonPressed = null;
		}

		lastFlip = spriteRenderer.flipX;

		if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
		{
			Debug.Log("Hallo Jump");
			isJumping = true;
		}
	}

	private void FixedUpdate()
	{
		if (buttonPressed == RIGHT)
		{
			rigid.velocity = new Vector2(moveSpeed, 0);
		}
		else if (buttonPressed == LEFT)
		{
			rigid.velocity = new Vector2(-moveSpeed, 0);
		}
		else
		{
			rigid.velocity = new Vector2(0, 0);
		}

		if (isJumping)
		{
			rigid.velocity = new Vector2(0, jumpForce);
		}
		else if(!IsGrounded())
		{
			isJumping = false;
			//rigid.velocity = new Vector2(0, 0);
		}
	}

	/*
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
	*/
}

/*

#if UNITY_EDITOR
	PCMovement();
#endif

#if UNITY_ANDROID
		MobileMovement();
#endif

*/
