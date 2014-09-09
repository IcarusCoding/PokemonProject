using UnityEngine;
using System.Collections;

public class PlayerController : BaseController 
{
	public LayerMask CollisionMask = 0;
	public LayerMask mouseClickMask = 0;

	private short numLockCount = 0;

	protected override void StartController()
	{
		Physics.IgnoreLayerCollision (this.gameObject.layer, this.gameObject.layer);
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	protected override void UpdateController() 
	{
		//If there is no Camera, then quit out early.
		if(Camera.main == null)
			return;

		GetLocomotionInput();
		HandleActionInput();
	}

	/// <summary>
	/// Gets the locomotion input.
	/// </summary>
	private void GetLocomotionInput()
	{
		// Toggable Numlock
		if(Input.GetKeyDown(KeyCode.Numlock))
			numLockCount++;

		if(numLockCount % 2 == 1)
			base.motor.MoveVector += new Vector3(0, 0, 1);

		// If both left and right buttons are clicked, then move forward
		if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
		{
			if(numLockCount % 2 == 1)
				numLockCount++;
			
			base.motor.MoveVector += new Vector3(0, 0, 1);
		}

		if(Input.GetMouseButton(1))
			base.motor.SnapAllignCharacterWithCamera();

		//if(ChatSystem.Instance != null)
			//if(ChatSystem.Instance.InGameChatActive)
				//return;

		if(Input.GetKey(KeyBindings.Jump))
			base.motor.Jump();

		if(Input.GetKey(KeyBindings.Forward) || Input.GetKey(KeyCode.UpArrow))
		{
			if(numLockCount % 2 == 1)
				numLockCount++;
			base.motor.MoveVector += new Vector3(0, 0, 1);
		}

		if(Input.GetKey(KeyBindings.Backward) || Input.GetKey(KeyCode.DownArrow))
		{
			if(numLockCount % 2 == 1)
				numLockCount++;
			base.motor.MoveVector += new Vector3(0, 0, -1);
		}
				
		if(Input.GetKey(KeyBindings.StrafLeft))
			base.motor.MoveVector += new Vector3(-1, 0, 0);

		if(Input.GetKey(KeyBindings.StrafRight))
			base.motor.MoveVector += new Vector3( 1, 0, 0);

		if(Input.GetKey(KeyBindings.Left) || Input.GetKey(KeyCode.LeftArrow))
			base.motor.RotationDirection = -1;
		else if(Input.GetKey(KeyBindings.Right) || Input.GetKey(KeyCode.RightArrow))
			base.motor.RotationDirection = 1;
		else
			base.motor.RotationDirection = 0;

		ApplyRotation();
	}

	/// <summary>
	/// Handles the action input.
	/// </summary>
	private void HandleActionInput()
	{				
		if(Input.GetKey(KeyCode.Escape))
			HandleWindows();
		
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
    	{
      		RaycastHit _hitInfo = new RaycastHit();
			bool _hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hitInfo, Mathf.Infinity, mouseClickMask);

			if(_hit)
			{

			}
		}

		//Allows the user to create a single image 
		if(Input.GetKeyUp(KeyBindings.PrintScreen))
			StartCoroutine(GameMaster.Instance.CreateScreenshot());
	}
	
	/// <summary>
	/// Applies the rotation to the player.
	/// </summary>
	private void ApplyRotation()
	{
		this.transform.eulerAngles = new Vector3( this.transform.eulerAngles.x,
		                                          this.transform.eulerAngles.y + base.motor.RotationSpeed * base.motor.RotationDirection,
		                                          this.transform.eulerAngles.z);		
	}

	/// <summary>
	/// Handles the windows.
	/// </summary>
	private void HandleWindows()
	{

	}
}
