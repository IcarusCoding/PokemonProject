using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and it 
/// ensures that every players position, rotation, and scale,
/// are kept up to date across the network.
/// 
/// This script is closely based on a script written by M2H.
/// </summary>


public class MovementUpdate : MonoBehaviour 
{
	public Motor Motor;
	private Vector3 lastPosition;	
	private Quaternion lastRotation;	
	private Transform myTransform;

	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
			myTransform = transform;
						
			//Ensure taht everyone sees the player at the correct location
			//the moment they spawn.			
			networkView.RPC("updateMovement", RPCMode.OthersBuffered,
			                myTransform.position, myTransform.rotation, Motor.MoveVector);
		}
		
		else
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		networkView.RPC("updateMovement", RPCMode.OthersBuffered,
		                myTransform.position, myTransform.rotation, Motor.MoveVector);
	}
	
	
	[RPC]
	void updateMovement (Vector3 newPosition, Quaternion newRotation, Vector3 newMoveVector)
	{
		transform.position = newPosition;
		transform.rotation = newRotation;
		Motor.MoveVector = newMoveVector;
	}
	
	
	
	
	
	
	
	
	
	
	
	
}
