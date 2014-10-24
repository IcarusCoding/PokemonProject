using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCController : BaseController 
{
	public string Name { get; set; }
	public int ID { get; set; }

	public TaskController taskController = null;
	public GameObject[] destinations;

	public Dictionary<int, Event> EventIDTaskDictionary = new Dictionary<int, Event>();

	/// <summary>
	/// Starts the controller.
	/// </summary>
	protected override void StartController()
	{
		destinations = GameObject.FindGameObjectsWithTag ("Destination");
	}

	public void CreateNPC()
	{
		taskController = new TaskController (this.gameObject);
	}

	/// <summary>
	/// Updates the controller.
	/// </summary>
	protected override void UpdateController ()
	{
		taskController.UpdateTasks ();
	}

	public void TriggerEvent ( int aEventID )
	{

	}
}
