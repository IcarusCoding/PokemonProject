using UnityEngine;
using System.Collections;

public class RotationTask : BaseTask 
{
	// Holds a value to a targets transform values
	private Transform target;

	/// <summary>
	/// Initializes a new instance of the <see cref="RotationTask"/> class.
	/// </summary>
	/// <param name="aTaskOwner">A task owner.</param>
	/// <param name="aTarget">A target.</param>
	public RotationTask (GameObject aTaskOwner, Transform aTarget) 
	{
		base.TaskOwner = aTaskOwner;
		target = aTarget;
	}
	
	/// <summary>
	/// Updates the task.
	/// </summary>
	public override void UpdateTask ()
	{
		if(target != null)
			RotateToTarget (target.position);

		base.TaskComplete = true;
	}

	/// <summary>
	/// Rotates to target.
	/// </summary>
	/// <param name="aTarget">A target.</param>
	public void RotateToTarget(Vector3 aTarget)
	{
		//Declare the Direction
		Vector3 _direction = aTarget - base.TaskOwner.transform.position;
		
		//Lock the Y axis rotation
		_direction.y = 0;

		//Rotate according to the direction this character needs to go
		base.TaskOwner.transform.rotation = Quaternion.Slerp(base.TaskOwner.transform.rotation, Quaternion.LookRotation(_direction), 1);
	}
}
