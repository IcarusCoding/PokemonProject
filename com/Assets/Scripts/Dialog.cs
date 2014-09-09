using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dialog : MonoBehaviour 
{
	[SerializeField]
	protected GUISkin guiSkin;

	[SerializeField]
	protected string username;

	[SerializeField]
	protected GameObject Sprite;

	protected string text = "";	
	protected Vector2 size = new Vector2();

	public bool ShowGUI = false;
	public float CountDownTimer;

	void OnGUI()
	{
		if((Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position) > 15) 
		   || (!ShowGUI || Sprite.renderer.isVisible == false))
			return;

		if (!text.Equals(""))
		{
			size = guiSkin.box.CalcSize(new GUIContent(text));
			Vector3 transformPos = this.transform.position;
			transformPos.y += 0.8f;
			Vector3 pos = Camera.main.camera.WorldToScreenPoint(transformPos);
			Rect labelPos = new Rect(pos.x - size.x/2, Screen.height - pos.y, size.x, 
			                         size.y);
			GUI.Label(labelPos, text, guiSkin.box);
		}
	}

	/// <summary>
	/// Adds the dialog to the ChatSystem.
	/// </summary>
	/// <param name="aUser">A userName.</param>
	/// <param name="aText">text to be added to the ChatSystem.</param>
	public void AddDialogToChatSystem(string aUser, string aText)
	{
		if((Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position) > 15))
			return;

		//If we have no user name due to no network. then give the name Unknown.
		if(aUser.Length == 0)
			aUser = "Unknown";

		//Combine the text
		aText = aUser + ":  " + aText;
		//ChatSystem.Instance.AddChatEntry (aText);
	}

	/// <summary>
	/// Adds the dialog to screen for X seconds.
	/// </summary>
	/// <returns>The dialog to screen for X seconds.</returns>
	/// <param name="aText">A text.</param>
	public IEnumerator YieldAddDialog(string aText)
	{
		text = "";

		yield return new WaitForFixedUpdate();

		ShowGUI = true;
		text = aText;

		AddDialogToChatSystem(username, aText);
	}
}
