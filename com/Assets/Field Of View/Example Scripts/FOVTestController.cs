using UnityEngine;
using System.Collections;

public class FOVTestController : MonoBehaviour 
{
	public Motor Motor = null;
	
	public void Start()
	{
		Physics.IgnoreLayerCollision (this.gameObject.layer, this.gameObject.layer);
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	private void Update() 
	{
		//If there is no Camera, then quit out early.
		if(Camera.main == null)
			return;
		
		Motor.VerticalVelocity = Motor.MoveVector.y;
		Motor.MoveVector = Vector3.zero;
		
		GetLocomotionInput();
		Motor.UpdateMotor();
	}
	
	/// <summary>
	/// Gets the locomotion input.
	/// </summary>
	private void GetLocomotionInput()
	{
		
		if(Input.GetMouseButton(1))
			Motor.SnapAllignCharacterWithCamera();

		if(Input.GetKey(KeyBindings.Jump))
			this.Motor.Jump();
		
		
		if(Input.GetKey(KeyBindings.Left) || Input.GetKey(KeyCode.LeftArrow))
			Motor.RotationDirection = -1;
		else if(Input.GetKey(KeyBindings.Right) || Input.GetKey(KeyCode.RightArrow))
			Motor.RotationDirection = 1;
		else
			Motor.RotationDirection = 0;
		
		ApplyRotation();
	}
	
	/// <summary>
	/// Applies the rotation to the player.
	/// </summary>
	private void ApplyRotation()
	{
		this.transform.eulerAngles = new Vector3( this.transform.eulerAngles.x,
		                                         this.transform.eulerAngles.y + this.Motor.RotationSpeed * this.Motor.RotationDirection,
		                                         this.transform.eulerAngles.z);		
	}
}
