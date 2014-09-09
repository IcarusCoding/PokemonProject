using UnityEngine;
using System.Collections;

public class DialogTask : BaseTask 
{
	private YieldTask yieldTask;
	private Dialog ownerDialog;
	private string textToScreen;
	private bool triggeredOnce = false;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="DialogTask"/> class.
	/// </summary>
	/// <param name="aTaskOwner">A task owner.</param>
	/// <param name="aText">A text.</param>
	/// <param name="aPostYield">If set to <c>true</c> a post yield will be set.</param>
	public DialogTask(GameObject aTaskOwner, string aText, bool aPostYield)
	{
		base.TaskOwner = aTaskOwner;
		base.TaskComplete = false;
		triggeredOnce = false;
		
		float _time = 0.0f;
		_time = (aText.Replace (" ", string.Empty).Length / 10.0f);
		_time = (_time < 2.0f) ? 2.0f : _time;
		
		yieldTask = new YieldTask (0, _time);
		yieldTask.TaskComplete = (!aPostYield);

		textToScreen = aText;
		
		ownerDialog = base.TaskOwner.GetComponent<BaseController> ().Dialog;

		if(yieldTask.TaskComplete)
			TriggerTask();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DialogTask"/> class.
	/// </summary>
	/// <param name="aTaskOwner">A task owner.</param>
	/// <param name="aText">A text.</param>
	/// <param name="aPostYield">If set to <c>true</c> a post yield will be set.</param>
	public DialogTask(GameObject aTaskOwner, string aText, int aPostYieldTime)
	{
		base.TaskOwner = aTaskOwner;
		base.TaskComplete = false;
		triggeredOnce = false;
		
		yieldTask = new YieldTask (0, aPostYieldTime);
		yieldTask.TaskComplete = false;
		
		textToScreen = aText;
		
		ownerDialog = base.TaskOwner.GetComponent<NPCDialog> ();
		
		if(yieldTask.TaskComplete)
			TriggerTask();
	}

	/// <summary>
	/// Updates the task.
	/// </summary>
	public override void UpdateTask ()
	{
		if(!yieldTask.TaskComplete)
		{
			yieldTask.UpdateTask();

			if(triggeredOnce == false)
			{
				TriggerTask();
				triggeredOnce = true;
			}
		}
		else
		{
			ownerDialog.ShowGUI = false;
			base.TaskComplete = true;
		}
	}

	private void TriggerTask()
	{
		ownerDialog.StartCoroutine(ownerDialog.YieldAddDialog(textToScreen));
	}
}
