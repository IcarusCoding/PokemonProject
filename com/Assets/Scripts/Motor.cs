using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour 
{
	public BaseSprite sprite;

	[HideInInspector]
	public float ForwardSpeed = 10.0f;

	[HideInInspector]
	public float BackwardSpeed = 2.0f;

	[HideInInspector]
	public float StrafingSpeed = 5.0f;

	[HideInInspector]
	public float SlideSpeed = 10.0f;

	[HideInInspector]
	public float JumpSpeed = 6.0f;

	[HideInInspector]
	public float Gravity = 21.0f;

	[HideInInspector]
	public float TerminalVelocity = 20.0f;

	[HideInInspector]
	public float SlideThreshold = 0.8f;

	[HideInInspector]
	public float MaxControlableSlideMagnitude = 0.4f;

	[HideInInspector]
	public int RotationSpeed = 5;

	[HideInInspector]
	public int RotationDirection = 0;
	
	private Vector3 slideDirection;
	
	public Vector3 MoveVector = Vector3.zero;//{ get; set; }
	public float VerticalVelocity { get; set; }

	/// <summary>
	/// Updates the motor.
	/// </summary>
	public void UpdateMotor()
	{
		ProcessMotion();
	}

	private void ProcessMotion()
	{
		// Transform MoveVector to World Space
		MoveVector = transform.TransformDirection(MoveVector);
		
		// Normalize our MoveVector if Magnitude is > 1
		if (MoveVector.magnitude > 1)
			MoveVector = Vector3.Normalize(MoveVector);
		
		// Apply slideing if applicable - APPLY before altering MoveVector
		//ApplySlide();
		
		// Multiply MoveVector by MoveSpeed
		MoveVector *= MoveSpeed();
		
		// Reapply Verticle Velocity to MoveVector.y
		MoveVector = new Vector3(MoveVector.x, VerticalVelocity, MoveVector.z);
		
		// Apply Gravity
		ApplyGravity();
		
		// Move the Character in World Space
		GetComponent<CharacterController>().Move (MoveVector * Time.deltaTime);
	}

	/// <summary>
	/// Applies the gravity.
	/// </summary>
	private void ApplyGravity()
	{
		//Make sure we are not exceeding out -Terminal Velocity
		if(MoveVector.y > -TerminalVelocity)
			MoveVector = new Vector3(MoveVector.x, MoveVector.y - Gravity * Time.deltaTime, MoveVector.z);
		
		if(GetComponent<CharacterController>().isGrounded && MoveVector.y <  -1.0f)
			MoveVector = new Vector3(MoveVector.x, -1.0f, MoveVector.z);
	}

	/// <summary>
	/// Applies the slide.
	/// </summary>
	private void ApplySlide()
	{
		if(!GetComponent<CharacterController>().isGrounded)
			return;
		
		slideDirection = Vector3.zero;
		
		RaycastHit hitInfo;

		if (Physics.Raycast(this.transform.position + Vector3.up, Vector3.down, out hitInfo)) 
		{
			if (hitInfo.normal.y < SlideThreshold)
				slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
		}

		if(slideDirection.magnitude < MaxControlableSlideMagnitude)
			MoveVector += slideDirection;
		else //All we can do is slide, so SLIDE!
		{
			MoveVector = slideDirection;
		}
	}

	/// <summary>
	/// Jump the character attached to this instance
	/// </summary>
	public bool Jump()
	{
		if(this.GetComponent<CharacterController>().isGrounded)
		{
			VerticalVelocity = JumpSpeed;
			return true;
		}
		return false;
	}

	/// <summary>
	/// Moves the speed.
	/// </summary>
	/// <returns>The speed.</returns>
	private float MoveSpeed()
	{
		//Depending on action, handle different speeds here
		var _moveSpeed = 3.0f;

		return _moveSpeed;
	}

	/// <summary>
	/// Snaps the character and alligns with camera.
	/// </summary>
	public void SnapAllignCharacterWithCamera()
	{
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);	
	}
}
