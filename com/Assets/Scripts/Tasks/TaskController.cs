﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskController 
{
	private List<BaseTask> TasksToComplete = new List<BaseTask>();

	private GameObject owner;
	private bool allTasksCompleted = false;
	private int taskCounter;

	public TaskController(GameObject aOwner)
	{
		owner = aOwner;
	}

	/// <summary>
	/// Update this class's tasks.
	/// </summary>
	public void UpdateTasks () 
	{
		//If we have no tasks, wait for some.  
		// TODO::  Subject to change to events and pull tasks through a JSON file.
		if(TasksToComplete.Count == 0)
		{
			AddTasks();
		}
		else
		{
			if(allTasksCompleted == false)
				UpdateAllTasks ();
			else
				ResetTasks();
		}
	}
	
	/// <summary>
	/// Updates all current tasks.
	/// </summary>
	private void UpdateAllTasks()
	{
		if(TasksToComplete[taskCounter].TaskComplete)
			taskCounter++;
		
		if(taskCounter > TasksToComplete.Count-1)
		{
			allTasksCompleted = true;
			return;
		}
		TasksToComplete[taskCounter].UpdateTask();
	}
	
	/// <summary>
	/// Adds the tasks.
	/// </summary>
	private void AddTasks()
	{
		TasksToComplete.Add(new DestinationTask(owner, owner.GetComponent<NPCController>().destinations
		                                        [Random.Range(0, owner.GetComponent<NPCController>().destinations.Length-1)].transform.position));
	}

	public void AddDestinationTask(Vector3 aDestination)
	{
		TasksToComplete.Add(new DestinationTask(owner, aDestination));
	}

	/// <summary>
	/// Adds a rotation task to our task Controller.
	/// </summary>
	/// <param name="aRotation">A rotation.</param>
	public void AddRotationTask (Vector3 aRotation)
	{
		//TODO:: Allow the NPC to rotate only
	}

	/// <summary>
	/// Adds a dialog task to our task Controller.
	/// </summary>
	/// <param name="aDialog">A dialog to be placed ont he screen.</param>
	public void AddDialogTask(string aDialog, bool aPostYieldTime)
	{
		TasksToComplete.Add (new DialogTask (owner, aDialog, aPostYieldTime));
	}
	
	/// <summary>
	/// Resets the tasks.
	/// </summary>
	private void ResetTasks()
	{
		TasksToComplete.Clear();
		allTasksCompleted = false;
		taskCounter = 0;
	}

	/// <summary>
	/// Raises the unit circle event returning a random point in the radius given.
	/// </summary>
	private Vector3 OnUnitCircle ()
	{
		float _angleInRadians = Random.Range(0, 2 * Mathf.PI);
		float _x = Mathf.Cos(_angleInRadians);
		float _z = Mathf.Sin(_angleInRadians);
		
		//Create a V3 based on a 2D plane
		return new Vector3(_x, 5, _z);
	}
}
