using UnityEngine;
using System.Collections;

public class BaseTask 
{
	public GameObject TaskOwner { get; set; }
	public bool TaskComplete { get; set; }

	public virtual void UpdateTask() {}
}
