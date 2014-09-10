using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Motor))]
public class BaseController : MonoBehaviour 
{
	protected virtual void StartController() {}
	protected virtual void UpdateController(){}

	protected Motor motor = null;
	public Dialog Dialog = null;
	public BaseSprite BaseSprite = null;
	
	public void Start()
	{
		motor = this.GetComponent<Motor> ();
		StartController ();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		motor.VerticalVelocity = motor.MoveVector.y;
		motor.MoveVector = Vector3.zero;

		//Apply any changes we need done to the Motor, then update them.
		UpdateController ();

		motor.UpdateMotor();
	}
}
