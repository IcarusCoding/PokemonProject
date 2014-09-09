using UnityEngine;
using System.Collections;

public class YieldTask : BaseTask
{
	private float targetTime;
	private string textToScreen;
	
	private float preYieldTime, postYieldTime = 0.0f;
	private bool preYieldComplete, postYieldComplete;
	
	private bool triggeredMain;

	/// <summary>
	/// Initializes a new instance of the <see cref="YieldTask"/> class.
	/// </summary>
	/// <param name="aPreYieldTime">A pre yield time.</param>
	/// <param name="aPostYieldTime">A post yield time.</param>
	public YieldTask(float aPreYieldTime, float aPostYieldTime)
	{
		base.TaskComplete = false;
		preYieldTime = aPreYieldTime;
		postYieldTime = aPostYieldTime;
	}

	/// <summary>
	/// Updates the task.
	/// </summary>
	public override void UpdateTask ()
	{
		//If we have no completed the preYield count down, then do so
		if(!preYieldComplete)
			CountDown(ref preYieldTime, ref preYieldComplete);
		else if(preYieldComplete)
		{
			//Only trigger this event once
			if( !triggeredMain)
			{
				triggeredMain = true;
			}
			CountDown(ref postYieldTime, ref postYieldComplete);
		}
		
		if(postYieldComplete)
			base.TaskComplete = true;
	}
	
	/// <summary>
	/// Counts down a timer.
	/// </summary>
	/// <param name="aTime">A time refrenced to count down.</param>
	/// <param name="aYieldComplete">A value to determine weither the count down is complete.</param>
	protected void CountDown(ref float aTime, ref bool aYieldComplete)
	{
		aTime -= Time.deltaTime;
		
		if(aTime <= 0)
			aYieldComplete = true;
	}
}
