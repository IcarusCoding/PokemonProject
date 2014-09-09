using UnityEngine;
using System.Collections;

public class DestinationTask : BaseTask
{
	private Motor motor;

	private Ray ray;
	private RaycastHit hit;

	private Vector3 destination;
	private float threshold = 0.05f;

	/// <summary>
	/// Initializes a new instance of the <see cref="DestinationTask"/> class.
	/// </summary>
	/// <param name="aTaskOwner">A task owner.</param>
	/// <param name="aDestination">A destination.</param>
	public DestinationTask(GameObject aTaskOwner, Vector3 aDestination)
	{
		base.TaskOwner = aTaskOwner;
		base.TaskComplete = false;

		motor = base.TaskOwner.GetComponent<Motor> ();
		destination = aDestination;
	}

	/// <summary>
	/// Updates the task.
	/// </summary>
	public override void UpdateTask()
	{
		//Determine the Direction this character needs to go
		Vector3 _direction = destination - base.TaskOwner.transform.position;

		//Ignor Rotation on the Y axis
		_direction.y = 0;

		//Check Jump availbe direction
		Vector3 origin = (base.TaskOwner.transform.position - (Vector3.up / 2));

		bool check = false;

		//CheckForward ();
		//CheckLeft ();
		//CheckRight ();

		if(Physics.Raycast(origin + (Vector3.up/2), _direction, out hit, 1))
		{
			if(hit.collider.tag == "Obstacle")
				check = true;
		}

		if(check)
		{
			if(!Physics.Raycast(origin + Vector3.up, _direction, out hit, 0.65f))
				motor.Jump();
		}

		
		Debug.DrawRay (origin + (Vector3.up/2), _direction, Color.green);
		Debug.DrawRay (origin + Vector3.up, _direction, Color.red); //( base.TaskOwner.transform.position - (Vector3.up/2), _direction, Color.red);

		//Rotate according to the direction this character needs to go
		if(_direction != Vector3.zero)
			base.TaskOwner.transform.rotation = Quaternion.Slerp(base.TaskOwner.transform.rotation, Quaternion.LookRotation(_direction), 1f);

		motor.MoveVector = Vector3.forward;

		if(_direction.magnitude < threshold)
		{
			base.TaskOwner.transform.position = destination;
			motor.MoveVector = Vector3.zero;
			base.TaskComplete = true;
		}
	}

	private void CheckLeft()
	{
		Debug.DrawLine (base.TaskOwner.transform.position, 
		                base.TaskOwner.transform.position + ((base.TaskOwner.transform.forward + -base.TaskOwner.transform.right).normalized), Color.yellow);
		
		if (Physics.Linecast (base.TaskOwner.transform.position, 
		                    base.TaskOwner.transform.position + ((base.TaskOwner.transform.forward + -base.TaskOwner.transform.right).normalized), out hit))
		{
			if(hit.collider.tag == "Obstacle")
			{
				CheckRight();
				return;
			}
		}
	}
		
	private void CheckRight()
	{
		Debug.DrawLine (base.TaskOwner.transform.position, 
		                base.TaskOwner.transform.position + ((base.TaskOwner.transform.forward + base.TaskOwner.transform.right).normalized), Color.yellow);
		
		if (Physics.Linecast (base.TaskOwner.transform.position, 
		                      base.TaskOwner.transform.position + ((base.TaskOwner.transform.forward + base.TaskOwner.transform.right).normalized), out hit))
		{
			return;
		}
	}

	private void CheckForward()
	{
		Debug.DrawLine (base.TaskOwner.transform.position, 
		                base.TaskOwner.transform.position + (base.TaskOwner.transform.forward).normalized, Color.yellow);

		if(Physics.Linecast(base.TaskOwner.transform.position, 
		                    base.TaskOwner.transform.position + (base.TaskOwner.transform.forward).normalized, out hit))
		{
			if(hit.collider.tag == "Obstacle")
			{
				CheckLeft();
				return;
			}
		}
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
