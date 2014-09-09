using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDialog: Dialog
{
	private void Update()
	{
		if(Input.GetKeyUp(KeyCode.O))
			StartCoroutine (base.YieldAddDialog( "testing self created dialog"));
	}

	/// <summary>
	/// Initializes the dialog to appear.
	/// </summary>
	/// <param name="aText">A text.</param>
	public void InitDialog(string aText)
	{
		StartCoroutine (base.YieldAddDialog (aText));
	}
}
